import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Row, Col } from 'reactstrap';

import { 
    setPreviewModal, 
    setCopyLinkModal, 
    saveLandingPage, 
    setFormData,
    setModuleComponents,
    clearValidationErrors,
    setValidationErrors
} from '../../actions';
import { pageBuilderLabels, pageBuilderDefaultState } from '../../libs/constants';

const ActionButtons = ({ 
    formData, 
    moduleComponents, 
    validation, 
    ui,
    setPreviewModal,
    setCopyLinkModal,
    saveLandingPage,
    setFormData,
    setModuleComponents,
    clearValidationErrors,
    setValidationErrors
}) => {
    const handlePreview = useCallback(() => {
        setPreviewModal(true);
    }, [setPreviewModal]);

    const validateForm = useCallback(() => {
        const errors = {};

        // Required field validations
        if (!formData.campaignId) {
            errors.campaignId = 'Campaign Name is required';
        }

        if (!formData.pageName || formData.pageName.trim() === '') {
            errors.pageName = 'Page Name is required';
        }

        if (!formData.pageCode || formData.pageCode.trim() === '') {
            errors.pageCode = 'Page Code is required';
        }

        // Module components validation
        if (!moduleComponents || moduleComponents.length === 0) {
            errors.moduleComponents = 'At least one module component is required';
        } else {
            // Validate each module component
            moduleComponents.forEach(module => {
                if (!module.moduleId) {
                    errors[`module_${module.id}`] = 'Module is required';
                }
                if (!module.sourceId) {
                    errors[`source_${module.id}`] = 'Source is required';
                }
            });
        }

        if (Object.keys(errors).length > 0) {
            setValidationErrors(errors);
            return false;
        }

        return true;
    }, [formData, moduleComponents, setValidationErrors]);

    const handleReset = useCallback(() => {
        if (window.confirm('Are you sure you want to reset the form? All changes will be lost.')) {
            setFormData(pageBuilderDefaultState.formData);
            setModuleComponents(pageBuilderDefaultState.moduleComponents);
            clearValidationErrors();
        }
    }, [setFormData, setModuleComponents, clearValidationErrors]);

    const handleCreate = useCallback(async () => {
        // Validate form before saving
        if (!validateForm()) {
            return;
        }

        const result = await saveLandingPage(formData, moduleComponents);
        
        if (result && result.IsSuccessful) {
            alert('Landing page created successfully!');
            handleReset(); // Reset form after successful creation
        } else {
            alert(result ? result.Message : 'Failed to create landing page. Please try again.');
        }
    }, [validateForm, saveLandingPage, formData, moduleComponents, handleReset]);

    const handleCopyLink = useCallback(() => {
        setCopyLinkModal(true);
    }, [setCopyLinkModal]);

    const isFormValid = validation.isValid && 
                       formData.campaignId && 
                       formData.pageName && 
                       formData.pageCode &&
                       moduleComponents.length > 0 &&
                       moduleComponents.every(m => m.moduleId && m.sourceId);

    return (
        <Row>
            <Col>
                <div className="action-buttons d-flex flex-wrap gap-2">
                    <Button 
                        color="info" 
                        onClick={handlePreview}
                        disabled={ui.isLoading}
                    >
                        <i className="fa fa-eye me-1"></i>
                        {pageBuilderLabels.preview}
                    </Button>

                    <Button 
                        color="success" 
                        onClick={handleCreate}
                        disabled={!isFormValid || ui.isLoading}
                    >
                        {ui.isLoading ? (
                            <>
                                <i className="fa fa-spinner fa-spin me-1"></i>
                                Creating...
                            </>
                        ) : (
                            <>
                                <i className="fa fa-save me-1"></i>
                                {pageBuilderLabels.create}
                            </>
                        )}
                    </Button>

                    <Button 
                        color="secondary" 
                        onClick={handleReset}
                        disabled={ui.isLoading}
                    >
                        <i className="fa fa-refresh me-1"></i>
                        {pageBuilderLabels.reset}
                    </Button>

                    <Button 
                        color="primary" 
                        outline 
                        onClick={handleCopyLink}
                        disabled={ui.isLoading}
                    >
                        <i className="fa fa-link me-1"></i>
                        {pageBuilderLabels.copyLink}
                    </Button>
                </div>

                {!isFormValid && (
                    <div className="mt-2">
                        <small className="text-danger">
                            <i className="fa fa-exclamation-triangle me-1"></i>
                            Please fill in all required fields before creating the landing page.
                        </small>
                    </div>
                )}
            </Col>
        </Row>
    );
};

function mapStateToProps({ formData, moduleComponents, validation, ui }) {
    return Object.assign({}, { formData, moduleComponents, validation, ui });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ 
        setPreviewModal, 
        setCopyLinkModal, 
        saveLandingPage, 
        setFormData,
        setModuleComponents,
        clearValidationErrors,
        setValidationErrors
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ActionButtons));
