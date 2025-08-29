import React, { useState, useEffect, useRef, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader } from '../actions/index';
import { defaultState, regSpecialCharacters } from '../libs/constants';
import { globalResource, defaultInsertId } from '../../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import NotificationPopup from '../../../common/components/NotificationPopup';
import { validateFileExtension, validateInputFile, validateFileSize } from '../libs/utils';
import InputFile from '../../../common/components/InputFile';

const regexSpecialCharacters = new RegExp(regSpecialCharacters);

export const AssetUploaderForm = (props) => {
    const { moduleList, targetCountryList, languageList, selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions } = props;
    
    const [state, setState] = useState({
        ...defaultState,
        language: '',
        module: '',
        targetCountry: '',
        errors: {
            module: false,
            targetCountry: false,
            language: false,
            file: false
        }
    });

    const modalRef = useRef();

    useEffect(() => {
        setState(prev => ({ ...prev, isInsert: selectedMediaId === defaultInsertId }));
        if (selectedMediaId) {
            handleGetAssetDetail(selectedMediaId);
        }
    }, [selectedMediaId]);

    // Keep lists updated but don't auto-select values
    useEffect(() => {
        // Only update if we're editing an existing asset and have loaded data
        if (selectedMediaId && selectedMediaId !== defaultInsertId) {
            // Values will be set by handleGetAssetDetail
        }
    }, [moduleList, targetCountryList, languageList, selectedMediaId]);

    const handleGetAssetDetail = useCallback(async (selectedMediaId) => {
        const assetInfo = await props.getAssetDetail(selectedMediaId);

        if (assetInfo) {
            const fileName = htmlDecode(assetInfo.FileName);
            const indexOfShortFileName = fileName.lastIndexOf('/') + 1;
            const shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

            setState(prev => ({
                ...prev,
                inputFileNames: [shortFileName],
                language: assetInfo.Language || prev.language,
                module: assetInfo.Module || prev.module,
                targetCountry: assetInfo.TargetCountry || prev.targetCountry,
                iniatalFileName: shortFileName
            }));
        }
    }, [props]);

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

    const handleChangeInputFileName = useCallback((event, index) => {
        const value = event.target.value;

        if (regexSpecialCharacters.test(value)) {
            handleShowAlert(false, `A File Name can't contain any of the following characters: \\/:*?"<>|#%&+`);
        } else {
            setState(prev => {
                let inputFileNames = [...prev.inputFileNames];
                inputFileNames[index] = value;
                return { ...prev, inputFileNames };
            });
        }
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
            !allResults.isOK && handleShowAlert(false, allResults.message);
        }
    }, [allowedExtensions, maxFileSize]);

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

                !allResults.isOK && handleShowAlert(false, allResults.message);
                return { ...prev, files, inputFileNames, isDisabled: !allResults.isOK };
            } else {
                return { ...prev, inputFileNames: [''], files, isDisabled: true };
            }
        });
    }, [allowedExtensions, maxFileSize]);

    const handleAfterSave = useCallback((isInsert, result) => {
        if (result) {
            const message = isInsert ? "Your asset has been added successfully!" : "Your asset has been updated successfully!";
            handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            handleShowAlert(false, isInsert ? "The add is failed!" : "The update is failed!");
        }
    }, []);

    const save = useCallback(async () => {
        const { inputFileNames, isInsert, iniatalFileName, files } = state;
        const assetInfo = getParam();

        if (isInsert) {
            const result = await props.uploadAssets(assetInfo);
            handleAfterSave(isInsert, result);
        } else {
            if (inputFileNames[0] === iniatalFileName && !!files.length) {
                setState(prev => ({ ...prev, messageNotificationPopup: 'This file name exists already. Do you want to overwrite the current file?' }));
            } else {
                updateAssetHandler(assetInfo);
            }
        }
    }, [state, props]);

    const getParam = useCallback(() => {
        const { files, inputFileNames, language, module, targetCountry, isInsert } = state;

        return {
            MediaId: selectedMediaId,
            files,
            inputFileNames: isInsert ? inputFileNames.map(item => item.trim()).join('|') : inputFileNames[0] && inputFileNames[0].trim(),
            language,
            module,
            targetCountry
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
    }, [state]);

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
        props.toggleLoader(true);

        const { inputFileNames, module, targetCountry, language, files, isInsert } = state;

        // Track validation errors
        const newErrors = {
            module: !module,
            targetCountry: !targetCountry,
            language: !language,
            file: isInsert && (!files || files.length === 0)
        };

        // Update state with errors
        setState(prev => ({ ...prev, errors: newErrors }));

        // Check if any validation failed
        const hasErrors = Object.values(newErrors).some(error => error);

        if (hasErrors) {
            let errorMessage = "<strong>Please complete the required fields:</strong><br />";
            if (newErrors.module) errorMessage += "• Module selection is required<br />";
            if (newErrors.targetCountry) errorMessage += "• Target Country selection is required<br />";
            if (newErrors.language) errorMessage += "• Language selection is required<br />";
            if (newErrors.file) errorMessage += "• File upload is required<br />";
            
            // Use a custom warning alert for validation errors
            setState(prev => ({
                ...prev,
                message: errorMessage,
                isSuccess: false,
                isShowMessage: true
            }));
            
            modalRef.current && modalRef.current.scrollTo({ top: 0, behavior: "smooth" });
            setTimeout(() => setState(prev => ({ ...prev, isShowMessage: false })), 7000);
            props.toggleLoader(false);
            return;
        }

        if (inputFileNames.length > maximumFiles) {
            handleShowAlert(false, `You can upload up to ${maximumFiles} files only.`);
            return;
        }
        if (hasDuplicateFileNames(inputFileNames)) {
            handleShowAlert(false, "Duplicate file name(s) detected. Please rename or remove the file(s).");
            return;
        }

        const validInputFileNames = validateInputFileName();
        validInputFileNames.isOK ? save() : handleShowAlert(false, validInputFileNames.message);

        props.toggleLoader(false);
    }, [state, props, maximumFiles, hasDuplicateFileNames, validateInputFileName, save]);

    const handleShowAlert = useCallback((isSuccess, message) => {
        modalRef.current && modalRef.current.scrollTo({ top: 0, behavior: "smooth" });

        setState(prev => ({
            ...prev,
            message,
            isSuccess,
            isShowMessage: true,
        }));

        setTimeout(() => hideMessage(isSuccess), isSuccess ? 3000 : 5000);
        props.toggleLoader(false);
    }, [props]);

    const hideMessage = useCallback((isSuccess) => {
        setState(prev => ({ ...prev, isShowMessage: false }));

        if (isSuccess) {
            props.fetchData(param);
            handleClosePopup();
        }
    }, [props, param]);

    const handleClosePopup = useCallback(() => {
        props.setSelectedMediaId(null);
        setState({
            ...defaultState,
            language: '',
            module: '',
            targetCountry: '',
            errors: {
                module: false,
                targetCountry: false,
                language: false,
                file: false
            }
        });
    }, [props]);

    const handleCloseNotificationPopup = useCallback((confirmResult) => {
        setState(prev => ({ ...prev, messageNotificationPopup: '' }));

        if (confirmResult) {
            const assetInfo = getParam();
            updateAssetHandler(assetInfo);
        }
    }, [getParam, updateAssetHandler]);

    const { inputFileNames, language, isShowMessage, isSuccess, message, isInsert, isDisabled, messageNotificationPopup, files, errors } = state;
    const isShowPopup = selectedMediaId !== null;
    const title = isInsert ? "Add New Asset" : "Update Existing Asset";
    const buttonLabel = isInsert ? "Upload" : "Update";

    return (
        <React.Fragment>
            {
                !!messageNotificationPopup && <NotificationPopup
                    message={messageNotificationPopup}
                    isConfirmPopup={true}
                    onClose={handleCloseNotificationPopup}
                />
            }
            <Modal isOpen={isShowPopup} toggle={handleClosePopup} backdrop='static' innerRef={modalRef}>
                <ModalHeader toggle={handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="banner-list-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-3 p-1 border" autoComplete="off">
                                <Label>[Valid Types: {allowedExtensions && allowedExtensions.join(', ')}]</Label>
                                <Label className="d-block">[File Size Limit: {maxFileSize}MB]</Label>
                                {
                                    isInsert && <Label className="d-block">[Maximum files: {maximumFiles}]</Label>
                                }
                                <div className="alert alert-info p-2 mb-3">
                                    <small><strong>Note:</strong> Module, Target Country, and Language cannot be changed if the image is already used in an existing landing page.</small>
                                </div>
                                <br />
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="module" className="required">Module</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="module" 
                                            id="module" 
                                            value={state.module} 
                                            onChange={handleChange}
                                            className={errors.module ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                moduleList && moduleList.map(item => {
                                                    return (<option key={`module-${item.ModuleName}`} value={item.ModuleName}>{item.ModuleName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="targetCountry" className="required pr-0">Target Country</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="targetCountry" 
                                            id="targetCountry" 
                                            value={state.targetCountry} 
                                            onChange={handleChange}
                                            className={errors.targetCountry ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                targetCountryList && targetCountryList.map(item => {
                                                    return (<option key={`country-${item.TargetCountryCode}`} value={item.TargetCountryCode}>{item.TargetCountryName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="language" className="required">Language</Label>
                                    <Col sm={5} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="language" 
                                            id="language" 
                                            value={language} 
                                            onChange={handleChange}
                                            className={errors.language ? 'form-validation-warning' : ''}>
                                            <option value="">Please Select</option>
                                            {
                                                languageList && languageList.map(item => {
                                                    return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="file" className={isInsert ? "required" : ""}>Upload File</Label>
                                    <Col sm={9} className='align-self-center'>
                                        <InputFile id="file" 
                                            className={`input-file-group border ${errors.file ? "form-validation-warning" : (isInsert && isDisabled) || files.length === 0 ? "text-danger" : "text-success"}`}
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
                                disabled={((isInsert || (inputFileNames && !inputFileNames[0]))) ? isDisabled : false}
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
    return bindActionCreators({ setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AssetUploaderForm));