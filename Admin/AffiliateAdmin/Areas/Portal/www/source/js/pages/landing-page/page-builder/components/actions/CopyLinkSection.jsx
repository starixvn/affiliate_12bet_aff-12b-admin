import React, { useState, useCallback, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Input, FormGroup, Label, Alert } from 'reactstrap';

import { generatePreviewUrl } from '../../actions';

const CopyLinkSection = ({ formData, moduleComponents, languageList, landingPageUrl, pageBuilderDetail, isEditMode, ui, generatePreviewUrl }) => {
    const [generatedUrl, setGeneratedUrl] = useState('');
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

    // Initialize URL from pageBuilderDetail if available (edit mode)
    useEffect(() => {
        if (isEditMode && pageBuilderDetail && !generatedUrl) {
            const constructedUrl = constructPreviewUrl(
                pageBuilderDetail,
                pageBuilderDetail.LanguageCode,
                pageBuilderDetail.TargetCountryCode,
                pageBuilderDetail.PageCode
            );
            if (constructedUrl) {
                setGeneratedUrl(constructedUrl);
            }
        }
    }, [isEditMode, pageBuilderDetail, constructPreviewUrl, generatedUrl]);

    // Generate URL when hasPreviewedOrCreated becomes true (after successful create/preview)
    useEffect(() => {
        if (ui.hasPreviewedOrCreated && !isEditMode && !generatedUrl && formData.pageCode) {
            const constructedUrl = constructPreviewUrl(
                formData,
                formData.languageCode,
                formData.targetCountryCode,
                formData.pageCode
            );
            if (constructedUrl) {
                setGeneratedUrl(constructedUrl);
            }
        }
    }, [ui.hasPreviewedOrCreated, isEditMode, generatedUrl, formData, constructPreviewUrl]);

    const handleCopyToClipboard = useCallback(async () => {
        // If no generated URL, try to construct it from current form data
        let urlToCopy = generatedUrl;
        if (!urlToCopy && formData.pageCode) {
            urlToCopy = constructPreviewUrl(
                formData,
                formData.languageCode,
                formData.targetCountryCode,
                formData.pageCode
            );
            if (urlToCopy) {
                setGeneratedUrl(urlToCopy);
            }
        }
        
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
    }, [generatedUrl, formData, constructPreviewUrl]);

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

    // Check if copy link should be enabled (either has generated URL, is in edit mode with valid data, or has previewed/created)
    const isCopyLinkEnabled = generatedUrl || 
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
                                {generatedUrl || (formData.pageCode && isCopyLinkEnabled ? constructPreviewUrl(formData, formData.languageCode, formData.targetCountryCode, formData.pageCode) : '')}
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

function mapStateToProps({ formData, moduleComponents, ui, languageList, landingPageUrl, pageBuilderDetail, isEditMode }) {
    return {
        formData,
        moduleComponents,
        languageList,
        landingPageUrl,
        pageBuilderDetail,
        isEditMode,
        ui
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ generatePreviewUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CopyLinkSection));
