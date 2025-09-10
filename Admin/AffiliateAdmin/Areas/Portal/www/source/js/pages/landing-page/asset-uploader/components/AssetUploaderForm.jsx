import React, { useState, useEffect, useRef, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader, setNotification } from '../actions/index';
import { defaultState } from '../libs/constants';
import { regSpecialCharacters, globalResource, defaultInsertId } from '../../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { validateFileExtension, validateInputFile, validateFileSize } from '../../libs/utils';
import InputFile from '../../../common/components/InputFile';

const regexSpecialCharacters = new RegExp(regSpecialCharacters);

export const AssetUploaderForm = (props) => {
    const { moduleList, targetCountryList, languageList, selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions } = props;
    
    const [state, setState] = useState({
        ...defaultState,
        marketId: '',
        moduleId: '',
        targetCountryCode: '',
        isMediaIdInUse: false,
        errors: {
            moduleId: false,
            targetCountryCode: false,
            marketId: false,
            file: false
        }
    });

    const modalRef = useRef();
    const messageTimeoutRef = useRef();

    useEffect(() => {
        setState(prev => ({ ...prev, isInsert: selectedMediaId === defaultInsertId }));
        if (selectedMediaId) {
            handleGetAssetDetail(selectedMediaId);
        }
    }, [selectedMediaId]);

    const handleGetAssetDetail = useCallback(async (selectedMediaId) => {
        const queryParam = { mediaId: selectedMediaId };
        const assetInfo = await props.getAssetDetail(queryParam);

        if (assetInfo) {
            const fileName = htmlDecode(assetInfo.FileName);
            const mediaName = htmlDecode(assetInfo.MediaName);
            const indexOfShortFileName = fileName.lastIndexOf('/') + 1;
            const shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

            setState(prev => ({
                ...prev,
                inputFileNames: [mediaName],
                marketId: assetInfo.MarketId || prev.MarketId,
                moduleId: assetInfo.ModuleId || prev.ModuleId,
                targetCountryCode: assetInfo.TargetCountryCode || prev.TargetCountryCode,
                isMediaIdInUse: assetInfo.IsMediaIdInUse || prev.IsMediaIdInUse,
                initialFileName: shortFileName
            }));
        }
    }, [props, param]);

    const handleChange = useCallback((event) => {
        const value = event.target.value;
        const name = event.target.name;

        setState(prev => ({ 
            ...prev, 
            [name]: value,
            // Clear error when user selects a value
            errors: {
                ...prev.errors,
                [name]: false
            }
        }));
    }, []);

    const handleFile = useCallback((event) => {
        let { files } = event.target;

        if (files && files.length > 0) {
            let inputFileNames = [];
            let allResults = {
                isOK: true,
                message: ''
            };

            for (let file of files) {
                let fileName = file.name;
                inputFileNames.push(fileName);
                
                // Validate file extension
                let extensionResult = validateFileExtension(fileName, allowedExtensions);
                if (!extensionResult.isOK) {
                    allResults = extensionResult;
                    break;
                }
                
                // Validate file size
                let sizeResult = validateFileSize(file, maxFileSize);
                if (!sizeResult.isOK) {
                    allResults = sizeResult;
                    break;
                }
            }

            setState(prev => ({ 
                ...prev, 
                files: Array.from(files), 
                inputFileNames, 
                isDisabled: !allResults.isOK,
                // Clear file error when files are selected
                errors: {
                    ...prev.errors,
                    file: false
                }
            }));
            if (!allResults.isOK) {
                showMessage(allResults.message, false, 5000);
            }
        }
    }, [allowedExtensions, maxFileSize, props]);

    const handleRemoveImageAt = useCallback((index) => {
        setState(prev => {
            let inputFileNames = prev.inputFileNames;
            let files = prev.files;
            if (!files || files.length == 0 || files.length < index + 1) {
                return prev;
            }

            inputFileNames = [...inputFileNames.slice(0, index), ...inputFileNames.slice(index + 1, inputFileNames.length)];
            files = [...files.slice(0, index), ...files.slice(index + 1, files.length)];
            
            if (files && files.length) {
                let allResults = {
                    isOK: true,
                    message: ''
                };

                for (let file of files) {
                    let fileName = file.name;
                    
                    // Validate file extension
                    let extensionResult = validateFileExtension(fileName, allowedExtensions);
                    if (!extensionResult.isOK) {
                        allResults = extensionResult;
                        break;
                    }
                    
                    // Validate file size
                    let sizeResult = validateFileSize(file, maxFileSize);
                    if (!sizeResult.isOK) {
                        allResults = sizeResult;
                        break;
                    }
                }

                if (!allResults.isOK) {
                    showMessage(allResults.message, false);
                }
                return { ...prev, files, inputFileNames, isDisabled: !allResults.isOK };
            } else {
                return { ...prev, inputFileNames: [''], files, isDisabled: true };
            }
        });
    }, [allowedExtensions, maxFileSize, props]);

    const handleClosePopup = useCallback(() => {
        // Clear any pending message timeout
        if (messageTimeoutRef.current) {
            clearTimeout(messageTimeoutRef.current);
        }
        
        props.setSelectedMediaId(null);
        setState({
            ...defaultState,
            marketId: '',
            moduleId: '',
            targetCountryCode: '',
            isMediaIdInUse: false,
            errors: {
                moduleId: false,
                targetCountryCode: false,
                marketId: false,
                file: false
            }
        });
    }, [props]);

    const showMessage = useCallback((message, isSuccess = false, duration) => {
        // Clear any existing timeout
        if (messageTimeoutRef.current) {
            clearTimeout(messageTimeoutRef.current);
        }

        modalRef.current && modalRef.current.scrollTo({ top: 0, behavior: "smooth" });

        setState(prev => ({
            ...prev,
            message,
            isSuccess,
            isShowMessage: true
        }));

        messageTimeoutRef.current = duration && setTimeout(() => {
            setState(prev => ({ ...prev, isShowMessage: false }));
            
            if (isSuccess) {
                props.fetchData(param);
                handleClosePopup();
            }
        }, duration);
    }, [props, param, handleClosePopup]);

    const handleAfterSave = useCallback((isInsert, result) => {
        if (result) {
            const message = isInsert ? "Your image(s) has been added successfully!" : "Your image has been updated successfully!";
            if (result.IsSuccessful) {
                showMessage(message, true, 3000);
            } else {
                showMessage(result.Message, false);
            }
        } else {
            const message = isInsert ? "Oops! Failed to add your image(s)!" : "Oops! Failed to update your image!";
            showMessage(message, false);
        }
    }, [showMessage]);

    const save = useCallback(async () => {
        const { inputFileNames, isInsert, initialFileName, files } = state;
        const assetInfo = getParam();

        if (isInsert) {
            const result = await props.uploadAssets(assetInfo);
            handleAfterSave(isInsert, result);
        } else {
            if (inputFileNames[0] === initialFileName && !!files.length) {
                props.setNotification({
                    message: 'This file name exists already. Do you want to overwrite the current file?',
                    isConfirmPopup: true,
                    onConfirm: () => updateAssetHandler(assetInfo)
                });
            } else {
                updateAssetHandler(assetInfo);
            }
        }
    }, [state, props]);

    const getParam = useCallback(() => {
        const { files, inputFileNames, marketId, moduleId, targetCountryCode, isInsert } = state;

        return {
            MediaId: selectedMediaId,
            files,
            inputFileNames: isInsert ? inputFileNames.map(item => item.trim()).join('|') : inputFileNames[0] && inputFileNames[0].trim(),
            marketId,
            moduleId,
            targetCountryCode
        };
    }, [state, selectedMediaId]);

    const updateAssetHandler = useCallback(async (assetInfo) => {
        const result = await props.updateAsset(assetInfo);
        handleAfterSave(state.isInsert, result);
    }, [props, state.isInsert]);

    const validateInputFileName = useCallback(() => {
        const { inputFileNames, isInsert, files } = state;

        let validInputFileNames = {
            isOK: true,
            message: ''
        };

        if (isInsert) {
            for (let i = 0; i < inputFileNames.length; i++) {
                let inputFileName = inputFileNames[i];

                if (inputFileName) {
                    let result = validateInputFile(inputFileName.trim(), allowedExtensions);

                    if (!result.isOK) {
                        result.message = `File Name ${i + 1} incorrect.<br />` + result.message;
                        validInputFileNames = result;
                    }
                } else {
                    if (files && files.length > 0) {
                        let result = validateInputFile(files[i].name, allowedExtensions);

                        if (!result.isOK) {
                            validInputFileNames = result;
                        }
                    } else {
                        validInputFileNames = {
                            isOK: false,
                            message: 'You must browse a file.'
                        };
                    }
                }
            }
        } else {
            const inputFileName = inputFileNames[0];
            if (inputFileName) {
                let result = validateInputFile(inputFileNames[0], allowedExtensions);

                if (!result.isOK) {
                    validInputFileNames = result;
                }
            } else {
                if (files && files.length > 0) {
                    let result = validateInputFile(files[0].name, allowedExtensions);

                    if (!result.isOK) {
                        validInputFileNames = result;
                    }
                } else {
                    validInputFileNames = {
                        isOK: false,
                        message: 'You must either enter a file name for asset or browse other file.'
                    };
                }
            }
        }

        return validInputFileNames;
    }, [state, allowedExtensions]);

    const hasDuplicateFileNames = useCallback((inputFileNames) => {
        const length = inputFileNames.length;
        for (let i = 0; i <= length - 1; i++) {
            let fileName = inputFileNames[i].trim().toLowerCase();

            for (let j = i + 1; j < length; j++) {
                if (fileName == inputFileNames[j].trim().toLowerCase()) {
                    return true;
                }
            }
        }

        return false;
    }, []);

    const handleSubmit = useCallback(() => {
        const { inputFileNames, moduleId, targetCountryCode, marketId, files, isInsert } = state;

        // Track validation errors
        const newErrors = {
            moduleId: !moduleId,
            targetCountryCode: !targetCountryCode,
            marketId: !marketId,
            file: isInsert && (!files || files.length === 0)
        };

        // Update state with errors
        setState(prev => ({ ...prev, errors: newErrors }));

        // Check if any validation failed
        const hasErrors = Object.values(newErrors).some(error => error);

        if (hasErrors) {
            let errorMessage = "";
            if (newErrors.moduleId) errorMessage += "Please select a module.<br />";
            if (newErrors.targetCountryCode) errorMessage += "Please select a target country.<br />";
            if (newErrors.marketId) errorMessage += "Please select a language.<br />";
            if (newErrors.file) errorMessage += "Please browse a file.<br />";
            
            showMessage(errorMessage, false);
            return;
        }

        if (inputFileNames.length > maximumFiles) {
            showMessage(`Upload limit exceeded. You can only upload up to ${maximumFiles} files at a time.`, false);
            return;
        }
        if (hasDuplicateFileNames(inputFileNames)) {
            showMessage("Duplicate file name(s) detected. Please rename or remove the file(s) before uploading.", false);
            return;
        }

        const validInputFileNames = validateInputFileName();
        if (validInputFileNames.isOK) {
            props.toggleLoader(true);
            save();
            props.toggleLoader(false);
        } else {
            showMessage(validInputFileNames.message, false);
        }
    }, [state, props, maximumFiles, hasDuplicateFileNames, validateInputFileName, save, showMessage]);


    // Cleanup timeout on unmount
    useEffect(() => {
        return () => {
            if (messageTimeoutRef.current) {
                clearTimeout(messageTimeoutRef.current);
            }
        };
    }, []);


    const { inputFileNames, marketId, isShowMessage, isSuccess, message, isInsert, isDisabled, files, errors, isMediaIdInUse } = state;
    const isShowPopup = selectedMediaId !== null;
    const title = isInsert ? "Add New Asset" : "Edit Asset";
    const buttonLabel = isInsert ? "Upload" : "Update";

    return (
        <React.Fragment>
            <Modal isOpen={isShowPopup} toggle={handleClosePopup} backdrop='static' innerRef={modalRef}>
                <ModalHeader toggle={handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="banner-list-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-3" autoComplete="off">
                                <div className='px-1 mb-3 border'>
                                    <p>[Valid Types: {allowedExtensions && allowedExtensions.join(', ')}]</p>
                                    <p className="d-block">[File Size Limit: {maxFileSize}MB]</p>
                                    <p className="d-block">[Maximum files: {maximumFiles}]</p>
                                    <span><strong>Note:</strong> Module, Target Country, and Language cannot be changed if the image is already used in an existing landing page.</span>
                                </div>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="moduleId" className="required">Module</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="moduleId" 
                                            id="moduleId"
                                            disabled={isMediaIdInUse} 
                                            value={state.moduleId} 
                                            onChange={handleChange}
                                            className={errors.moduleId ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                moduleList && moduleList.map(item => {
                                                    return (<option key={`module-${item.key}`} value={item.key}>{item.name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="targetCountryCode" className="required pr-0">Target Country</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="targetCountryCode" 
                                            id="targetCountryCode" 
                                            disabled={isMediaIdInUse} 
                                            value={state.targetCountryCode} 
                                            onChange={handleChange}
                                            className={errors.targetCountryCode ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                targetCountryList && targetCountryList.map(item => {
                                                    return (<option key={`country-${item.key}`} value={item.key}>{item.name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="marketId" className="required">Language</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="marketId" 
                                            id="marketId" 
                                            disabled={isMediaIdInUse} 
                                            value={marketId} 
                                            onChange={handleChange}
                                            className={errors.marketId ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                languageList && languageList.map(item => {
                                                    return (<option key={`language-${item.key}`} value={item.key}>{item.name}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="file" className={isInsert ? "required" : ""}>Upload File</Label>
                                    <Col sm={9} className='align-self-center'>
                                        <InputFile id="file" 
                                            className={`input-file-group border ${errors.file ? "form-validation-warning" : isDisabled || files.length === 0 ? "text-danger" : "text-success"}`}
                                            lblText="No file chosen"
                                            btnName={globalResource.ChooseFile}
                                            isMultiple={isInsert} files={files}
                                            handleOnChangeFiles={handleFile}
                                            accept={allowedExtensions && allowedExtensions.map(ext => `image/${ext.replace('.', '')}`).join(', ')} />
                                    </Col>
                                </FormGroup>
                                {
                                    isInsert ? inputFileNames && inputFileNames.map((value, index) => {
                                        let inputFileName = `inputFileName${index}`;

                                        return (<FormGroup row className="mb-2" key={`FileName${index + 1}`}>
                                            <Label sm={3} htmlFor={inputFileName}>{`File Name ${index + 1}`}</Label>
                                            <Col sm={7} className='align-self-center'>
                                                <Input type="text"
                                                    name={inputFileName}
                                                    id={inputFileName}
                                                    title={value}
                                                    value={value}
                                                    disabled={true} />
                                            </Col>
                                            <div className='align-self-center'>
                                                <Button className="mr-3"
                                                    type="button"
                                                    color="danger"
                                                    disabled={files && !files.length}
                                                    onClick={() => handleRemoveImageAt(index)}>
                                                    {globalResource.Remove}
                                                </Button>
                                            </div>
                                        </FormGroup>)
                                    }) : (
                                        <FormGroup row className="mb-2">
                                            <Label sm={3} htmlFor="inputFileName">File Name</Label>
                                            <Col sm={7} className='align-self-center'>
                                                <Input type="text"
                                                    name="inputFileName"
                                                    id="inputFileName"
                                                    value={inputFileNames && inputFileNames[0]}
                                                    disabled={true} />
                                            </Col>
                                        </FormGroup>
                                    )
                                }
                            </Form>

                            <Button type="button"
                                color="success"
                                className="mr-3"
                                disabled={((isInsert || (inputFileNames && isDisabled))) ? isDisabled : false}
                                onClick={handleSubmit}>
                                {buttonLabel}
                            </Button>
                            <Button type="button" color="info" onClick={handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        </React.Fragment>
    );
}

function mapStateToProps({ selectedMediaId, param, moduleList, targetCountryList, languageList, maximumFiles, maxFileSize, allowedExtensions }) {
    return Object.assign({}, {
        selectedMediaId, param, moduleList, targetCountryList, languageList, maximumFiles, maxFileSize, allowedExtensions
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader, setNotification }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AssetUploaderForm));