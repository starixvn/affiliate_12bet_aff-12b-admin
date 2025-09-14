import React, { useState, useCallback, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Input, FormGroup, Label, Alert } from 'reactstrap';

const CopyLinkSection = ({ formData, moduleComponents, languageList, landingPageUrl, pageBuilderDetail, isEditMode, ui, validation }) => {
    const [copySuccess, setCopySuccess] = useState(false);
    const [error, setError] = useState(null);

    // Function to construct URL from pageBuilderDetail or formData
    const constructPreviewUrl = useCallback((sourceData, sourceLanguageCode, sourceTargetCountryCode, sourcePageCode) => {
        if (!landingPageUrl || !sourcePageCode) return '';
        
        // Find the language with matching LanguageCode and TargetCountryCode
        const selectedLanguage = languageList?.find(lang => 
            lang.LanguageCode === sourceLanguageCode && 
            lang.TargetCountryCode === sourceTargetCountryCode
        );
        
        const shortedLanguageCode = selectedLanguage?.TargetLanguageCode || selectedLanguage?.ShortedLanguageCode;
        
        // Combine: landingPageUrl + shortedLanguageCode + pageCode
        return `${landingPageUrl}/${shortedLanguageCode}/${sourcePageCode}`;
    }, [landingPageUrl, languageList]);

    // Get the preview URL from Redux state or construct it as fallback
    const getPreviewUrl = useCallback(() => {
        // First try to get from Redux state (set by ActionButtons)
        if (ui.previewUrl) {
            return ui.previewUrl;
        }
        
        // Fallback: construct URL for edit mode
        if (isEditMode && pageBuilderDetail && pageBuilderDetail.PageCode && !validation.isValidating) {
            return constructPreviewUrl(
                pageBuilderDetail,
                pageBuilderDetail.LanguageCode,
                pageBuilderDetail.TargetCountryCode,
                pageBuilderDetail.PageCode
            );
        }
        
        return '';
    }, [ui.previewUrl, isEditMode, pageBuilderDetail, constructPreviewUrl]);

    const handleCopyToClipboard = useCallback(async () => {
        const urlToCopy = getPreviewUrl();
        
        if (!urlToCopy) return;

        try {
            await navigator.clipboard.writeText(urlToCopy);
            setCopySuccess(true);
            
            // Reset success message after 3 seconds
            setTimeout(() => {
                setCopySuccess(false);
            }, 3000);
        } catch (err) {
            console.error('Failed to copy to clipboard:', err);
            
            // Fallback for older browsers
            const textArea = document.createElement('textarea');
            textArea.value = urlToCopy;
            document.body.appendChild(textArea);
            textArea.select();
            
            try {
                document.execCommand('copy');
                setCopySuccess(true);
                setTimeout(() => setCopySuccess(false), 3000);
            } catch (fallbackErr) {
                console.error('Fallback copy failed:', fallbackErr);
            }
            
            document.body.removeChild(textArea);
        }
    }, [getPreviewUrl]);

    // In edit mode, we can use pageBuilderDetail data, otherwise validate formData
    const isFormValid = isEditMode && pageBuilderDetail ? 
                       (pageBuilderDetail.CampaignId && 
                        pageBuilderDetail.PageName && 
                        pageBuilderDetail.PageCode &&
                        pageBuilderDetail.ModuleComponentsSetting?.length > 0) :
                       (formData.campaignId && 
                        formData.pageName && 
                        formData.pageCode &&
                        moduleComponents.length > 0);

    // Check if copy link should be enabled
    const isCopyLinkEnabled = !!getPreviewUrl() || 
                             (isEditMode && pageBuilderDetail && isFormValid) || 
                             ui.hasPreviewedOrCreated;

    return (
        <div className="mt-3">
            <FormGroup>
                {/* <Label for="previewUrl" className="small text-muted">
                    <i className="fa fa-link me-1"></i>
                    Preview Link
                </Label> */}
                
                {error && (
                    <Alert color="danger" className="mb-2 py-2">
                        <small>
                            <i className="fa fa-exclamation-triangle me-1"></i>
                            {error}
                        </small>
                    </Alert>
                )}

                    <div>
                        <div className="input-group">
                            <div className="mt-2 p-2 bg-light border rounded w-100" style={{ fontSize: '11px', fontFamily: 'monospace', wordBreak: 'break-all' }}>
                                {getPreviewUrl()}
                            </div>
                            <Button
                                color="primary"
                                className='w-100'
                                disabled={!isCopyLinkEnabled}
                                onClick={handleCopyToClipboard}
                                title="Copy to clipboard"
                            >
                                <i className="fa fa-copy me-1"></i>
                                Copy Link
                            </Button>
                        </div>
                    </div>
                {copySuccess && (
                    <Alert color="success" className="mt-2">
                        <small>
                            <i className="fa fa-check me-1"></i>
                            Link copied to clipboard successfully!
                        </small>
                    </Alert>
                )}
            </FormGroup>
        </div>
    );
};

function mapStateToProps({ formData, moduleComponents, ui, languageList, landingPageUrl, pageBuilderDetail, isEditMode, validation }) {
    return {
        formData,
        moduleComponents,
        languageList,
        landingPageUrl,
        pageBuilderDetail,
        isEditMode,
        ui,
        validation
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CopyLinkSection));
