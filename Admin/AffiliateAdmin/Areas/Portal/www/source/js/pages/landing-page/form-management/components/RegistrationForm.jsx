import React, { useState, useEffect, useRef, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader } from '../actions/index';
import { defaultState, regSpecialCharacters } from '../../asset-uploader/libs/constants';
import { globalResource, defaultInsertId } from '../../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import NotificationPopup from '../../../common/components/NotificationPopup';
import { validateFileExtension, validateInputFile, validateFileSize } from '../../asset-uploader/libs/utils';
import InputFile from '../../../common/components/InputFile';

const regexSpecialCharacters = new RegExp(regSpecialCharacters);

export const RegistrationForm = (props) => {
    const { selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions } = props;
    
    const [state, setState] = useState({
        ...defaultState,
        formNames: [''],
        previewImages: [''],
        errors: {
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
    }, [selectedMediaId]);

    const handleGetAssetDetail = useCallback(async (selectedMediaId) => {
        const assetInfo = await props.getAssetDetail(selectedMediaId);

        if (assetInfo) {
            const fileName = htmlDecode(assetInfo.FileName);
            const indexOfShortFileName = fileName.lastIndexOf('/') + 1;
            const shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

            setState(prev => ({
                ...prev,
                inputFileNames: [shortFileName],
                formNames: [shortFileName.replace(/\.(png|jpg|jpeg)$/i, '')],
                previewImages: [shortFileName],
                iniatalFileName: shortFileName
            }));
        }
    }, [props]);

    const handleChangeFormName = useCallback((event, index) => {
        const value = event.target.value;

        if (regexSpecialCharacters.test(value)) {
            handleShowAlert(false, `A Form Name can't contain any of the following characters: \\/:*?"<>|#%&+`);
        } else {
            setState(prev => {
                let formNames = [...prev.formNames];
                let inputFileNames = [...prev.inputFileNames];
                formNames[index] = value;
                // Update the corresponding file name if we have files
                if (prev.files && prev.files[index]) {
                    const fileExtension = prev.files[index].name.split('.').pop();
                    inputFileNames[index] = value ? `${value}.${fileExtension}` : prev.files[index].name;
                }
                return { ...prev, formNames, inputFileNames };
            });
        }
    }, []);

    const handleFile = useCallback((event) => {
        let { files } = event.target;

        if (files && files.length > 0) {
            let inputFileNames = [];
            let formNames = [];
            let previewImages = [];
            let allResults = {
                isOK: true,
                message: ''
            };

            for (let file of files) {
                let fileName = file.name;
                let formName = fileName.replace(/\.(png|jpg|jpeg)$/i, '');
                inputFileNames.push(fileName);
                formNames.push(formName);
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
                formNames,
                previewImages,
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
            let formNames = prev.formNames;
            let previewImages = prev.previewImages;
            let files = prev.files;
            if (!files || files.length == 0 || files.length < index + 1) {
                return prev;
            }

            inputFileNames = [...inputFileNames.slice(0, index), ...inputFileNames.slice(index + 1, inputFileNames.length)];
            formNames = [...formNames.slice(0, index), ...formNames.slice(index + 1, formNames.length)];
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

                !allResults.isOK && handleShowAlert(false, allResults.message);
                return { ...prev, files, inputFileNames, formNames, previewImages, isDisabled: !allResults.isOK };
            } else {
                return { ...prev, inputFileNames: [''], formNames: [''], previewImages: [''], files, isDisabled: true };
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
        const { files, inputFileNames, formNames, isInsert } = state;

        return {
            MediaId: selectedMediaId,
            files,
            inputFileNames: isInsert ? inputFileNames.map(item => item.trim()).join('|') : inputFileNames[0] && inputFileNames[0].trim(),
            formNames: isInsert ? formNames.map(item => item.trim()).join('|') : formNames[0] && formNames[0].trim(),
        };
    }, [state, selectedMediaId]);

    const updateAssetHandler = useCallback(async (assetInfo) => {
        const result = await props.updateAsset(assetInfo);
        handleAfterSave(state.isInsert, result);
    }, [props, state.isInsert]);

    const validateFormNames = useCallback(() => {
        const { formNames, isInsert, files } = state;

        let validFormNames = {
            isOK: true,
            message: ''
        };

        if (isInsert) {
            for (let i = 0; i < formNames.length; i++) {
                let formName = formNames[i];

                if (!formName || formName.trim() === '') {
                    validFormNames = {
                        isOK: false,
                        message: `Form Name ${i + 1} is required.`
                    };
                    break;
                } else if (regexSpecialCharacters.test(formName.trim())) {
                    validFormNames = {
                        isOK: false,
                        message: `Form Name ${i + 1} contains invalid characters.`
                    };
                    break;
                }
            }
        } else {
            const formName = formNames[0];
            if (!formName || formName.trim() === '') {
                if (files && files.length > 0) {
                    // Use the original file name if no form name provided
                } else {
                    validFormNames = {
                        isOK: false,
                        message: 'You must either enter a form name or browse a file.'
                    };
                }
            } else if (regexSpecialCharacters.test(formName.trim())) {
                validFormNames = {
                    isOK: false,
                    message: 'Form Name contains invalid characters.'
                };
            }
        }

        return validFormNames;
    }, [state]);

    const hasDuplicateFormNames = useCallback((formNames) => {
        const length = formNames.length;
        for (let i = 0; i <= length - 1; i++) {
            let formName = formNames[i].trim().toLowerCase();

            for (let j = i + 1; j < length; j++) {
                if (formName == formNames[j].trim().toLowerCase()) {
                    return true;
                }
            }
        }

        return false;
    }, []);

    const handleSubmit = useCallback(() => {
        props.toggleLoader(true);

        const { formNames, files, isInsert } = state;

        // Track validation errors
        const newErrors = {
            file: isInsert && (!files || files.length === 0)
        };

        // Update state with errors
        setState(prev => ({ ...prev, errors: newErrors }));

        // Check if any validation failed
        const hasErrors = Object.values(newErrors).some(error => error);

        if (hasErrors) {
            let errorMessage = "<strong>Please complete the required fields:</strong><br />";
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

        if (formNames.length > maximumFiles) {
            handleShowAlert(false, `You can upload up to ${maximumFiles} files only.`);
            return;
        }
        if (hasDuplicateFormNames(formNames)) {
            handleShowAlert(false, "Duplicate form name(s) detected. Please rename or remove the form(s).");
            return;
        }

        const validFormNames = validateFormNames();
        validFormNames.isOK ? save() : handleShowAlert(false, validFormNames.message);

        props.toggleLoader(false);
    }, [state, props, maximumFiles, hasDuplicateFormNames, validateFormNames, save]);

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
            formNames: [''],
            previewImages: [''],
            errors: {
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

    const { inputFileNames, formNames, previewImages, isShowMessage, isSuccess, message, isInsert, isDisabled, messageNotificationPopup, files, errors } = state;
    const isShowPopup = selectedMediaId !== null;
    const title = isInsert ? "Registration Form" : "Update Registration Form";
    const buttonLabel = isInsert ? "Add" : "Update";

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
                                <br />
                                <FormGroup row className="mb-2">
                                    <Label sm={3} htmlFor="module">Module</Label>
                                    <Col sm={7} className='align-self-center'>
                                        <Input 
                                            type="text"
                                            disabled={true}
                                            name="module" 
                                            id="module" 
                                            value="Registration Form">
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
                                    isInsert ? formNames && formNames.map((value, index) => {
                                        let formNameId = `formName${index}`;
                                        let previewImageId = `previewImage${index}`;

                                        return (
                                            <React.Fragment key={`FormSection${index + 1}`}>
                                                <FormGroup row className="mb-2">
                                                    <Label sm={3} htmlFor={formNameId}>{`Form Name ${index + 1}`}</Label>
                                                    <Col sm={7} className='align-self-center'>
                                                        <Input type="text"
                                                            name={formNameId}
                                                            id={formNameId}
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
                                                <Label sm={3} htmlFor="formName">Form Name</Label>
                                                <Col sm={7} className='align-self-center'>
                                                    <Input type="text"
                                                        name="formName"
                                                        id="formName"
                                                        value={formNames && formNames[0]}
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
                                                        disabled={true}
                                                        className="bg-light" />
                                                </Col>
                                            </FormGroup>
                                        </React.Fragment>
                                    )
                                }
                            </Form>

                            <Button type="button"
                                color="success"
                                className="mr-3"
                                disabled={isInsert ? isDisabled : false}
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

function mapStateToProps({ selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions }) {
    return Object.assign({}, {
        selectedMediaId, param, maximumFiles, maxFileSize, allowedExtensions
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMediaId, getAssetDetail, fetchData, uploadAssets, updateAsset, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(RegistrationForm));