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
    setPreviewUrl
} from '../../actions';
import { pageBuilderLabels, pageBuilderDefaultState } from '../../libs/constants';

const ActionButtons = ({ 
    formData, 
    moduleComponents, 
    moduleList,
    validation, 
    ui,
    isEditMode,
    languageList,
    landingPageUrl,
    saveLandingPage,
    generatePreviewUrl,
    setFormData,
    setModuleComponents,
    clearValidationErrors,
    setValidationErrors,
    setHasPreviewedOrCreated,
    setPreviewUrl
}) => {
    const handlePreview = useCallback(async () => {
        // Always validate form and show errors when Preview is clicked
        // Pass current formData and moduleComponents to ensure we have latest values
        if (!validateForm(formData, moduleComponents)) {
            // Validation errors are already set by validateForm, just return
            return;
        }
        
        try {
            const result = await generatePreviewUrl(formData, moduleComponents);
            
            if (result && result.IsSuccessful) {
                // Construct preview URL: landingPageUrl + shortedLanguageCode + pageCode
                let constructedPreviewUrl = '';
                
                if (landingPageUrl && formData.pageCode) {
                    // Find the language with matching LanguageCode and TargetCountryCode
                    const selectedLanguage = languageList?.find(lang => 
                        lang.LanguageCode === formData.languageCode && 
                        lang.TargetCountryCode === formData.targetCountryCode
                    );
                    
                    const shortedLanguageCode = selectedLanguage?.ShortedLanguageCode || '';
                    
                    // Combine: landingPageUrl + shortedLanguageCode + pageCode
                    constructedPreviewUrl = `${landingPageUrl}/${shortedLanguageCode}/${formData.pageCode}?isPreview=true&pageId=${result.Message}`;
                }
                
                setPreviewUrl(constructedPreviewUrl || result.PreviewUrl);
                setHasPreviewedOrCreated(true); // Enable Copy Link button
            } else {
                alert(result ? result.Message : 'Failed to generate preview. Please check your form data and try again.');
            }
        } catch (err) {
            console.error('Preview generation error:', err);
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

            // Validate IsRequired modules - ensure at least one instance exists
            const requiredModules = moduleList.filter(m => m.IsRequired);
            
            // Check if "Logo Sponsorship & Register" (moduleId 3) is selected
            const hasLogoSponsorshipRegister = currentModuleComponents.some(comp => comp.moduleId === 3);
            
            requiredModules.forEach(requiredModule => {
                const hasRequiredModule = currentModuleComponents.some(comp => comp.moduleId === requiredModule.ModuleId);
                
                // Special case: If "Logo Sponsorship & Register" (moduleId 3) is selected,
                // then "Logo $ Sponsorship" (moduleId 2) is not required
                if (requiredModule.ModuleId === 2 && hasLogoSponsorshipRegister) {
                    // Skip validation for "Logo $ Sponsorship" when "Logo Sponsorship & Register" is present
                    return;
                }
                
                if (!hasRequiredModule) {
                    errors[`required_module_${requiredModule.ModuleId}`] = `${requiredModule.ModuleName || 'Unknown Module'} is required and must be included`;
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
        if (window.confirm('Are you sure you want to reset the form? All changes will be lost.')) {
            setFormData(pageBuilderDefaultState.formData);
            setModuleComponents(pageBuilderDefaultState.moduleComponentsSetting);
            clearValidationErrors();
            setHasPreviewedOrCreated(false); // Reset Copy Link availability
        }
    }, [setFormData, setModuleComponents, clearValidationErrors, setHasPreviewedOrCreated]);

    const handleCreate = useCallback(async () => {
        // Always validate form and show errors when Create is clicked
        // Pass current formData and moduleComponents to ensure we have latest values
        if (!validateForm(formData, moduleComponents)) {
            // Validation errors are already set by validateForm, just return
            return;
        }

        const result = await saveLandingPage(formData, moduleComponents);
        
        if (result && result.IsSuccessful) {
            alert(isEditMode ? 'Landing page updated successfully!' : 'Landing page created successfully!');
            setHasPreviewedOrCreated(true); // Enable Copy Link button after successful creation
            // Don't reset form immediately - let user copy link first if needed
        } else {
            alert(result ? result.Message : (isEditMode ? 'Failed to update landing page. Please try again.' : 'Failed to create landing page. Please try again.'));
        }
    }, [validateForm, saveLandingPage, formData, moduleComponents, setHasPreviewedOrCreated]);

    // Note: Removed isFormValid logic since buttons are now always clickable
    // and validation happens when buttons are clicked

    return (
        <Row className="py-4">
            <Col>
                <div className="action-buttons" style={{ display: 'grid', gridTemplateColumns: 'repeat(1, 1fr)', gap: '10px' }}>
                    {
                        !isEditMode && <Button 
                            color="info" 
                            onClick={handlePreview}
                            disabled={ui.isLoading}
                            title="Preview landing page"
                        >
                            <i className="fa fa-eye me-1"></i>
                            {pageBuilderLabels.preview}
                        </Button>
                    }

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
                        color="secondary" 
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

function mapStateToProps({ formData, moduleComponents, moduleList, validation, ui, isEditMode, languageList, landingPageUrl }) {
    return Object.assign({}, { formData, moduleComponents, moduleList, validation, ui, isEditMode, languageList, landingPageUrl });
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
        setPreviewUrl
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ActionButtons));
