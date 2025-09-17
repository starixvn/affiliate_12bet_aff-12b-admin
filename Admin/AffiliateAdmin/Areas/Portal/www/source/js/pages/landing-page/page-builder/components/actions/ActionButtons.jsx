import React, { useCallback, useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Row, Col } from 'reactstrap';

import CopyLinkSection from './CopyLinkSection';
import { 
    saveLandingPage, 
    generatePreviewUrl,
    setFormData,
    setModuleComponents,
    clearValidationErrors,
    setValidationErrors,
    setHasPreviewedOrCreated,
    setPreviewUrl,
    initializeFromPageBuilderDetail
} from '../../actions';
import { pageBuilderLabels, pageBuilderDefaultState, pageCodePattern, validationMessages } from '../../libs/constants';

const ActionButtons = ({ 
    formData, 
    moduleComponents, 
    moduleList,
    validation, 
    ui,
    isEditMode,
    pageBuilderDetail,
    languageList,
    landingPageUrl,
    saveLandingPage,
    generatePreviewUrl,
    setFormData,
    setModuleComponents,
    clearValidationErrors,
    setValidationErrors,
    setHasPreviewedOrCreated,
    setPreviewUrl,
    initializeFromPageBuilderDetail
}) => {
    const handlePreview = useCallback(async () => {
        if (!validateForm(formData, moduleComponents)) {
            return;
        }
        
        try {
            const result = await generatePreviewUrl(formData, moduleComponents);
            
            if (result && result.IsSuccessful) {
                let constructedPreviewUrl = '';
                
                if (landingPageUrl && formData.pageCode) {
                    const selectedLanguage = languageList?.find(lang => 
                        lang.LanguageCode === formData.languageCode && 
                        lang.TargetCountryCode === formData.targetCountryCode
                    );
                    
                    const shortedLanguageCode = selectedLanguage?.ShortedLanguageCode || '';
                    
                    constructedPreviewUrl = `${landingPageUrl}/${shortedLanguageCode}/${formData.pageCode}?isPreview=true&pageId=${result.Message}`;
                }
                
                setPreviewUrl(constructedPreviewUrl || result.PreviewUrl);
                setHasPreviewedOrCreated(true);
            } else {
                alert(result ? result.Message : 'Failed to generate preview. Please check your form data and try again.');
            }
        } catch (err) {
            alert('An error occurred while generating the preview.');
        }
    }, [validateForm, formData, moduleComponents, generatePreviewUrl, languageList, landingPageUrl, setPreviewUrl, setHasPreviewedOrCreated]);

    const validateForm = useCallback((currentFormData = formData, currentModuleComponents = moduleComponents) => {
        const errors = {};

        if (!currentFormData.campaignId) {
            errors.campaignId = 'Campaign Name is required';
        }

        if ((!currentFormData.pageName || currentFormData.pageName.trim() === '') && (!currentFormData.pagename || currentFormData.pagename.trim() === '')) {
            errors.pageName = 'Page Name is required';
        }

        if ((!currentFormData.pageCode || currentFormData.pageCode.trim() === '') && (!currentFormData.pagecode || currentFormData.pagecode.trim() === '')) {
            errors.pageCode = 'Page Code is required';
        } else if (currentFormData.pageCode && !pageCodePattern.test(currentFormData.pageCode)) {
            errors.pageCode = validationMessages.pageCodeInvalid;
        }

        if (!currentFormData.dealIds || 
            (Array.isArray(currentFormData.dealIds) && currentFormData.dealIds.length === 0) ||
            (typeof currentFormData.dealIds === 'string' && currentFormData.dealIds.trim() === '')) {
            errors.dealIds = 'At least one deal must be selected';
        }

        // Module components validation
        if (!currentModuleComponents || currentModuleComponents.length === 0) {
            errors.moduleComponents = 'At least one module component is required';
        } else {
            // Validate each module component
            currentModuleComponents.forEach(module => {                
                if (!module.moduleId) {
                    errors[`module_${module.id}`] = 'Module is required';
                    return;
                }

                // Get module info from moduleList
                const moduleInfo = moduleList.find(m => m.ModuleId === module.moduleId);
                
                if (moduleInfo) {
                    // Check if module requires a source (mediaId)
                    // For special modules like YouTube, Contact Us, Login Button, they might not need traditional sources
                    const isYoutubeModule = moduleInfo.ModuleType === 4;
                    const isContactUsModule = moduleInfo.ModuleType === 2;
                    const isLoginBtnModule = moduleInfo.ModuleType === 5;
                    
                    // For special modules, check if they have the required data
                    if (isYoutubeModule) {
                        // YouTube modules need a URL in mediaId
                        if (!module.extraInfo || module.extraInfo.trim() === '') {
                            errors[`media_${module.id}`] = 'YouTube URL is required';
                        }
                    } else if (isContactUsModule || isLoginBtnModule) {
                        // Contact Us and Login Button modules need both mediaId (source) and extraInfo
                        if (!module.mediaId && !(Array.isArray(module.mediaId) && module.mediaId.length > 0)) {
                            errors[`media_${module.id}`] = 'Source is required';
                        }
                        // extraInfo is now required based on user changes
                        if (!module.extraInfo || module.extraInfo.trim() === '') {
                            errors[`extra_${module.id}`] = `${isContactUsModule ? 'Contact link' : 'Login endpoint'} is required`;
                        }
                    } else {
                        // Regular modules need mediaId
                        if (!module.mediaId && !(Array.isArray(module.mediaId) && module.mediaId.length > 0)) {
                            errors[`media_${module.id}`] = 'Source is required';
                        }
                    }
                }
            });

            // Validate IsUnique modules - ensure only one instance exists
            const uniqueModules = moduleList.filter(m => m.IsUnique);
            uniqueModules.forEach(uniqueModule => {
                const instances = currentModuleComponents.filter(comp => comp.moduleId === uniqueModule.ModuleId);
                if (instances.length > 1) {
                    instances.slice(1).forEach(instance => {
                        errors[`unique_module_${instance.id}`] = `${uniqueModule.ModuleName} can only be used once`;
                    });
                }
            });
        }
        
        if (Object.keys(errors).length > 0) {
            setValidationErrors(errors);
            
            return false;
        }

        // Clear validation errors if form is valid
        clearValidationErrors();
        return true;
    }, [formData, moduleComponents, moduleList, setValidationErrors, clearValidationErrors]);

    const handleReset = useCallback(() => {
        const confirmMessage = isEditMode 
            ? 'Are you sure you want to reset the form? All changes will be reverted to the original values.'
            : 'Are you sure you want to reset the form? All changes will be lost.';
            
        if (window.confirm(confirmMessage)) {
            if (isEditMode && pageBuilderDetail) {
                // Reset to original edit values
                initializeFromPageBuilderDetail(pageBuilderDetail);
                // Reset preview URL and preview state in edit mode
                setPreviewUrl('');
                setHasPreviewedOrCreated(false);
                // Clear validation errors after form is initialized
                // Use requestAnimationFrame to ensure DOM updates are complete
                requestAnimationFrame(() => {
                    clearValidationErrors();
                });
            } else {
                // Clear validation errors first for create mode
                clearValidationErrors();
                // Reset to empty form for create mode
                setFormData(pageBuilderDefaultState.formData);
                setModuleComponents(pageBuilderDefaultState.moduleComponentsSetting);
                setHasPreviewedOrCreated(false);
                setPreviewUrl(''); // Clear any existing preview URL
            }
        }
    }, [isEditMode, pageBuilderDetail, setFormData, setModuleComponents, clearValidationErrors, setHasPreviewedOrCreated, setPreviewUrl, initializeFromPageBuilderDetail]);

    const handleCreate = useCallback(async () => {
        // Always validate form and show errors when Create is clicked
        // Pass current formData and moduleComponents to ensure we have latest values
        if (!validateForm(formData, moduleComponents)) {
            // Validation errors are already set by validateForm, just return
            return;
        }

        const result = await saveLandingPage(formData, moduleComponents);
        
        if (result && result.IsSuccessful) {
            // Generate new preview URL after successful save
            const selectedLanguage = languageList?.find(lang => 
                lang.LanguageCode == formData.languageCode && 
                lang.TargetCountryCode == formData.targetCountryCode
            );
            const shortedLanguageCode = selectedLanguage?.TargetLanguageCode || selectedLanguage?.ShortedLanguageCode;
            
            let constructedPreviewUrl = '';
            if (isEditMode &&landingPageUrl && formData.pageCode && shortedLanguageCode) {
                constructedPreviewUrl = `${landingPageUrl}/${shortedLanguageCode}/${formData.pageCode}`;
            }
            
            if (isEditMode) {
                // Update mode: Show success message and keep current data
                alert('Landing page has been edited successfully!');
                // Update preview URL for the updated page
                if (constructedPreviewUrl) {
                    setPreviewUrl(constructedPreviewUrl);
                    setHasPreviewedOrCreated(true);
                }
            } else {
                // Create mode: Show success message and reset form to initial state
                alert('Landing page has been created successfully!');
                
                // Reset form data to initial state
                setFormData(pageBuilderDefaultState.formData);
                setModuleComponents(pageBuilderDefaultState.moduleComponentsSetting);
                clearValidationErrors();
            }
        } else {
            alert(result ? result.Message : (isEditMode ? 'Failed to update landing page. Please try again.' : 'Failed to create landing page. Please try again.'));
        }
    }, [validateForm, saveLandingPage, formData, moduleComponents, setHasPreviewedOrCreated, isEditMode, setFormData, setModuleComponents, clearValidationErrors, setPreviewUrl, languageList, landingPageUrl]);

    // Note: Removed isFormValid logic since buttons are now always clickable
    // and validation happens when buttons are clicked

    return (
        <Row className="py-4">
            <Col>
                <div className="action-buttons" style={{ display: 'grid', gridTemplateColumns: 'repeat(1, 1fr)', gap: '10px' }}>
                    <Button 
                        color="info" 
                        onClick={handlePreview}
                        disabled={ui.isLoading}
                        title="Preview landing page"
                    >
                        <i className="fa fa-eye me-1"></i>
                        {pageBuilderLabels.preview}
                    </Button>
                    <Button 
                        color="success" 
                        onClick={handleCreate}
                        disabled={ui.isLoading}
                        title={isEditMode ? "Update landing page" : "Create landing page"}
                    >
                        {ui.isLoading ? (
                            <>
                                <i className="fa fa-spinner fa-spin me-1"></i>
                                {isEditMode ? 'Updating...' : 'Creating...'}
                            </>
                        ) : (
                            <>
                                <i className="fa fa-save me-1"></i>
                                {isEditMode ? pageBuilderLabels.update : pageBuilderLabels.create}
                            </>
                        )}
                    </Button>

                    <Button 
                        className="border border-primary text-dark bg-light" 
                        onClick={handleReset}
                        disabled={ui.isLoading}
                        title="Reset form to initial state"
                    >
                        <i className="fa fa-refresh me-1"></i>
                        {pageBuilderLabels.reset}
                    </Button>
                </div>

                {/* Copy Link Section - Shows when enabled */}
                <CopyLinkSection />

            </Col>
        </Row>
    );
};

function mapStateToProps({ formData, moduleComponents, moduleList, validation, ui, isEditMode, pageBuilderDetail, languageList, landingPageUrl }) {
    return Object.assign({}, { formData, moduleComponents, moduleList, validation, ui, isEditMode, pageBuilderDetail, languageList, landingPageUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ 
        saveLandingPage, 
        generatePreviewUrl,
        setFormData,
        setModuleComponents,
        clearValidationErrors,
        setValidationErrors,
        setHasPreviewedOrCreated,
        setPreviewUrl,
        initializeFromPageBuilderDetail
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ActionButtons));
