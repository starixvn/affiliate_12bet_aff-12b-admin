import React, { useState, useEffect, useRef, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedMediaId, getFormDetail, fetchData, uploadForms, updateForm, toggleLoader, setNotification } from '../actions/index';
import { defaultState, defaultFormManagementModuleId } from '../libs/constants';
import { regSpecialCharacters, globalResource, defaultInsertId } from '../../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { validateFileExtension, validateFileSize } from '../../libs/utils';
import InputFile from '../../../common/components/InputFile';

const regexSpecialCharacters = new RegExp(regSpecialCharacters);

export const RegistrationForm = (props) => {
    const { moduleList, targetCountryList, languageList, selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions } = props;
    
    const [state, setState] = useState({
        ...defaultState,
        mediaNames: [''],
        previewImages: [''],
        marketId: '',
        moduleId: defaultFormManagementModuleId,
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

    // Reusable method to extract media name from file name
    const getMediaNameFromFileName = useCallback((fileName, allowedExtensions) => {
        if (!fileName || !allowedExtensions || allowedExtensions.length === 0) {
            return fileName;
        }
        
        // Remove dots from extensions and join them for regex pattern
        const cleanExtensions = allowedExtensions.map(ext => ext.replace(/^\./, ''));
        const extensionPattern = new RegExp(`\\.(${cleanExtensions.join('|')})$`, 'i');
        return fileName.replace(extensionPattern, '');
    }, []);

    useEffect(() => {
        setState(prev => ({ ...prev, isInsert: selectedMediaId === defaultInsertId }));
        if (selectedMediaId) {
            handleGetFormDetail(selectedMediaId);
        }
    }, [selectedMediaId]);

    // Keep lists updated but don't auto-select values
    useEffect(() => {
        // Only update if we're editing an existing asset and have loaded data
        if (selectedMediaId && selectedMediaId !== defaultInsertId) {
            // Values will be set by handleGetAssetDetail
        }
    }, [selectedMediaId]);

    const handleGetFormDetail = useCallback(async (selectedMediaId) => {
        const assetInfo = await props.getFormDetail(selectedMediaId);

        if (assetInfo) {
            const mediaName = htmlDecode(assetInfo.MediaName);
            const fileName = htmlDecode(assetInfo.FileName);
            const indexOfShortFileName = fileName.lastIndexOf('/') + 1;
            const shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

            setState(prev => ({
                ...prev,
                inputFileNames: [shortFileName],
                mediaNames: [mediaName],
                previewImages: [shortFileName],
                marketId: assetInfo.MarketId || prev.marketId,
                moduleId: assetInfo.ModuleId || prev.moduleId,
                targetCountryCode: assetInfo.TargetCountryCode || prev.targetCountryCode,
                isMediaIdInUse: assetInfo.IsMediaIdInUse || prev.IsMediaIdInUse,
                initialFileName: shortFileName
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

    const handleClosePopup = useCallback(() => {
        // Clear any pending message timeout
        if (messageTimeoutRef.current) {
            clearTimeout(messageTimeoutRef.current);
        }
        
        props.setSelectedMediaId(null);
        setState({
            ...defaultState,
            mediaNames: [''],
            previewImages: [''],
            marketId: '',
            moduleId: defaultFormManagementModuleId,
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

    const handleChangeFormName = useCallback((event, index) => {
        const value = event.target.value;

        if (regexSpecialCharacters.test(value)) {
            showMessage(`A Form Name can't contain any of the following characters: \\/:*?"<>|#%&+`, false);
        } else {
            setState(prev => {
                let mediaNames = [...prev.mediaNames];
                mediaNames[index] = value;
                return { ...prev, mediaNames };
            });
        }
    }, [showMessage]);

    const handleFile = useCallback((event) => {
        let { files } = event.target;

        if (files && files.length > 0) {
            let inputFileNames = [];
            let mediaNames = [];
            let previewImages = [];
            let allResults = {
                isOK: true,
                message: ''
            };

            for (let file of files) {
                let fileName = file.name;
                let mediaName = getMediaNameFromFileName(fileName, allowedExtensions);
                inputFileNames.push(fileName);
                mediaNames.push(mediaName);
                previewImages.push(fileName);
                
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
                mediaNames,
                previewImages,
                isDisabled: !allResults.isOK,
                // Clear file error when files are selected
                errors: {
                    ...prev.errors,
                    file: false
                }
            }));
            !allResults.isOK && showMessage(allResults.message, false);
        }
    }, [allowedExtensions, maxFileSize, getMediaNameFromFileName, showMessage]);

    const handleRemoveImageAt = useCallback((index) => {
        setState(prev => {
            let inputFileNames = prev.inputFileNames;
            let mediaNames = prev.mediaNames;
            let previewImages = prev.previewImages;
            let files = prev.files;
            if (!files || files.length == 0 || files.length < index + 1) {
                return prev;
            }

            inputFileNames = [...inputFileNames.slice(0, index), ...inputFileNames.slice(index + 1, inputFileNames.length)];
            mediaNames = [...mediaNames.slice(0, index), ...mediaNames.slice(index + 1, mediaNames.length)];
            previewImages = [...previewImages.slice(0, index), ...previewImages.slice(index + 1, previewImages.length)];
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

                !allResults.isOK && showMessage(allResults.message, false);
                return { ...prev, files, inputFileNames, mediaNames, previewImages, isDisabled: !allResults.isOK };
            } else {
                return { ...prev, inputFileNames: [''], mediaNames: [''], previewImages: [''], files, isDisabled: true };
            }
        });
    }, [allowedExtensions, maxFileSize, showMessage]);

    const handleAfterSave = useCallback((isInsert, result) => {
        if (result) {
            const message = isInsert ? "Your asset has been added successfully!" : "Your asset has been updated successfully!";
            showMessage(result.IsSuccessful ? message : result.Message, result.IsSuccessful, result.IsSuccessful && 3000);
        } else {
            const message = isInsert ? "The add is failed!" : "The update is failed!";
            showMessage(message, false);
        }
    }, [showMessage]);

    const save = useCallback(async () => {
        const { inputFileNames, isInsert, initialFileName, files } = state;
        const assetInfo = getParam();

        if (isInsert) {
            const result = await props.uploadForms(assetInfo);
            handleAfterSave(isInsert, result);
        } else {
            if (inputFileNames[0] === initialFileName && !!files.length) {
                props.setNotification({
                    message: 'This form name exists already. Do you want to overwrite the current form?',
                    isConfirmPopup: true,
                    onConfirm: () => updateFormHandler(assetInfo)
                });
            } else {
                updateFormHandler(assetInfo);
            }
        }
    }, [state, props]);

    const getParam = useCallback(() => {
        const { files, inputFileNames, mediaNames, isInsert, marketId, moduleId, targetCountryCode } = state;

        return {
            MediaId: selectedMediaId,
            files,
            inputFileNames: isInsert ? inputFileNames.map(item => item.trim()).join('|') : inputFileNames[0] && inputFileNames[0].trim(),
            inputMediaNames: isInsert ? mediaNames.map(item => item.trim()).join('|') : mediaNames[0] && mediaNames[0].trim(),
            marketId,
            moduleId,
            targetCountryCode
        };
    }, [state, selectedMediaId]);

    const updateFormHandler = useCallback(async (assetInfo) => {
        const result = await props.updateForm(assetInfo);
        handleAfterSave(state.isInsert, result);
    }, [props, state.isInsert]);

    const validateMediaNames = useCallback(() => {
        const { mediaNames, isInsert, files } = state;

        let validMediaNames = {
            isOK: true,
            message: ''
        };

        if (isInsert) {
            for (let i = 0; i < mediaNames.length; i++) {
                let mediaName = mediaNames[i];

                if (!mediaName || mediaName.trim() === '') {
                    validMediaNames = {
                        isOK: false,
                        message: `Form Name ${i + 1} is required.`
                    };
                    break;
                } else if (regexSpecialCharacters.test(mediaName.trim())) {
                    validMediaNames = {
                        isOK: false,
                        message: `Form Name ${i + 1} contains invalid characters.`
                    };
                    break;
                }
            }
        } else {
            const mediaName = mediaNames[0];
            if (!mediaName || mediaName.trim() === '') {
                if (files && files.length > 0) {
                    // Use the original file name if no form name provided
                } else {
                    validMediaNames = {
                        isOK: false,
                        message: 'You must either enter a form name or browse a file.'
                    };
                }
            } else if (regexSpecialCharacters.test(mediaName.trim())) {
                validMediaNames = {
                    isOK: false,
                    message: 'Form Name contains invalid characters.'
                };
            }
        }

        return validMediaNames;
    }, [state]);

    const hasDuplicateMediaNames = useCallback((mediaNames) => {
        const length = mediaNames.length;
        for (let i = 0; i <= length - 1; i++) {
            let mediaName = mediaNames[i].trim().toLowerCase();

            for (let j = i + 1; j < length; j++) {
                if (mediaName == mediaNames[j].trim().toLowerCase()) {
                    return true;
                }
            }
        }

        return false;
    }, []);

    const handleSubmit = useCallback(() => {
        props.toggleLoader(true);

        const { mediaNames, files, isInsert, moduleId, targetCountryCode, marketId } = state;

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
            props.toggleLoader(false);
            return;
        }

        if (mediaNames.length > maximumFiles) {
            showMessage(`You can upload up to ${maximumFiles} files only.`, false);
            props.toggleLoader(false);
            return;
        }
        if (hasDuplicateMediaNames(mediaNames)) {
            showMessage("Duplicate form name(s) detected. Please rename or remove the form(s).", false);
            props.toggleLoader(false);
            return;
        }

        const validMediaNames = validateMediaNames();
        if (validMediaNames.isOK) {
            save();
        } else {
            showMessage(validMediaNames.message, false);
            props.toggleLoader(false);
        }

        props.toggleLoader(false);
    }, [state, props, maximumFiles, hasDuplicateMediaNames, validateMediaNames, save, showMessage]);

    // Cleanup timeout on unmount
    useEffect(() => {
        return () => {
            if (messageTimeoutRef.current) {
                clearTimeout(messageTimeoutRef.current);
            }
        };
    }, []);

    const { mediaNames, previewImages, isShowMessage, isSuccess, message, isInsert, isDisabled, files, errors, isMediaIdInUse } = state;
    const isShowPopup = selectedMediaId !== null;
    const title = isInsert ? "Registration Form" : "Update Registration Form";
    const buttonLabel = isInsert ? "Add" : "Update";

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
                            <Form tag="form" className="mb-3 p-1" autoComplete="off">
                                <div className='px-1 mb-3 border'>
                                    <p>[Valid Types: {allowedExtensions && allowedExtensions.join(', ')}]</p>
                                    <p className="d-block">[File Size Limit: {maxFileSize}MB]</p>
                                    <p className="d-block">[Maximum files: {maximumFiles}]</p>
                                    <span><strong>Note:</strong> Module, Target Country, and Language cannot be changed if the image is already used in an existing landing page.</span>
                                </div>
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="moduleId">Module</Label>
                                    <Col sm={7} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="moduleId" 
                                            id="moduleId"
                                            value={state.moduleId}
                                            disabled={true}
                                            onChange={handleChange}
                                            className={errors.moduleId ? 'form-validation-warning' : ''}>
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
                                    <Col sm={7} className='align-self-center'>
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
                                    <Col sm={7} className='align-self-center'>
                                        <Input 
                                            type="select" 
                                            name="marketId" 
                                            id="marketId" 
                                            disabled={isMediaIdInUse} 
                                            value={state.marketId} 
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
                                    isInsert ? mediaNames && mediaNames.map((value, index) => {
                                        let mediaNameId = `mediaName${index}`;
                                        let previewImageId = `previewImage${index}`;

                                        return (
                                            <React.Fragment key={`FormSection${index + 1}`}>
                                                <FormGroup row className="mb-2">
                                                    <Label sm={3} htmlFor={mediaNameId}>{`Form Name ${index + 1}`}</Label>
                                                    <Col sm={7} className='align-self-center'>
                                                        <Input type="text"
                                                            name={mediaNameId}
                                                            id={mediaNameId}
                                                            title={value}
                                                            value={value} 
                                                            onChange={(e) => handleChangeFormName(e, index)} />
                                                    </Col>
                                                </FormGroup>
                                                <FormGroup row className="mb-2">
                                                    <Label sm={3} className='pr-0' htmlFor={previewImageId}>{`Preview Image ${index + 1}`}</Label>
                                                    <Col sm={7} className='align-self-center'>
                                                        <Input type="text"
                                                            name={previewImageId}
                                                            id={previewImageId}
                                                            value={previewImages[index] || ''}
                                                            disabled={true}/>
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
                                                </FormGroup>
                                            </React.Fragment>
                                        )
                                    }) : (
                                        <React.Fragment>
                                            <FormGroup row className="mb-2">
                                                <Label sm={3} htmlFor="mediaName">Form Name</Label>
                                                <Col sm={7} className='align-self-center'>
                                                    <Input type="text"
                                                        name="mediaName"
                                                        id="mediaName"
                                                        value={mediaNames && mediaNames[0]}
                                                        onChange={(e) => handleChangeFormName(e, 0)} />
                                                </Col>
                                            </FormGroup>
                                            <FormGroup row className="mb-2">
                                                <Label sm={3} htmlFor="previewImage">Preview Image</Label>
                                                <Col sm={7} className='align-self-center'>
                                                    <Input type="text"
                                                        name="previewImage"
                                                        id="previewImage"
                                                        value={previewImages && previewImages[0]}
                                                        disabled={true} />
                                                </Col>
                                            </FormGroup>
                                        </React.Fragment>
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
    return bindActionCreators({ setSelectedMediaId, getFormDetail, fetchData, uploadForms, updateForm, toggleLoader, setNotification }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(RegistrationForm));