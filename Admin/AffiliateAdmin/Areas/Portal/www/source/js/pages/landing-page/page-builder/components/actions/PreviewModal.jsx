import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Alert, Spinner } from 'reactstrap';

import { setPreviewModal, generatePreviewUrl } from '../../actions';

const PreviewModal = ({ isOpen, formData, moduleComponents, languageList, landingPageUrl, setPreviewModal, generatePreviewUrl }) => {
    const [previewUrl, setPreviewUrl] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [iframeBlocked, setIframeBlocked] = useState(false);
    const [iframeLoaded, setIframeLoaded] = useState(false);
    const [useProxy, setUseProxy] = useState(false);

    const generatePreview = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        
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
                    constructedPreviewUrl = `${landingPageUrl}/${shortedLanguageCode}/${formData.pageCode}`;
                }
                
                setPreviewUrl(constructedPreviewUrl || result.PreviewUrl);
            } else if (result && result.IsSuccessful === false) {
                setError(result.Message || 'Failed to generate preview. Please check your form data and try again.');
            } else {
                setError('Failed to generate preview. Please check your form data and try again.');
            }
        } catch (err) {
            setError('An error occurred while generating the preview.');
            console.error('Preview generation error:', err);
        } finally {
            setIsLoading(false);
        }
    }, [generatePreviewUrl, formData, moduleComponents, languageList, landingPageUrl]);

    useEffect(() => {
        if (isOpen) {
            generatePreview();
        }
    }, [isOpen, generatePreview]);

    useEffect(() => {
        if (previewUrl && !iframeBlocked) {
            // Reset proxy state when preview URL changes
            //setUseProxy(false);
            setIframeLoaded(false);
            handleIframeLoadTimeout();
        }
    }, [previewUrl, iframeBlocked, handleIframeLoadTimeout]);

    const toggle = useCallback(() => {
        setPreviewModal(false);
        // Reset state when modal closes
        setPreviewUrl(null);
        setError(null);
        setIframeBlocked(false);
        setIframeLoaded(false);
        //setUseProxy(false);
    }, [setPreviewModal]);

    const handleIframeLoad = useCallback(() => {
        setIframeLoaded(true);
        setIframeBlocked(false);
    }, []);

    const handleIframeError = useCallback(() => {
        console.warn('Iframe failed to load, likely blocked by CSP');
        
        if (!useProxy) {
            // First try: Switch to proxy URL
            console.log('Trying proxy approach...');
            //setUseProxy(true);
            setIframeLoaded(false);
            // Don't set iframeBlocked yet, give proxy a chance
        } else {
            // Second try failed: Show fallback UI
            console.warn('Proxy approach also failed, showing fallback UI');
            setIframeBlocked(true);
            setIframeLoaded(false);
        }
    }, [useProxy]);

    const getProxyUrl = useCallback((originalUrl) => {
        if (!originalUrl) return null;
        // Use our proxy endpoint to serve the landing page content from same origin
        return `/Portal/LandingPageProxy/ProxyLandingPage?url=${encodeURIComponent(originalUrl)}`;
    }, []);

    const handleIframeLoadTimeout = useCallback(() => {
        // If iframe hasn't loaded after 5 seconds, try different approaches
        setTimeout(() => {
            if (!iframeLoaded && previewUrl && !iframeBlocked) {
                if (!useProxy) {
                    console.warn('Iframe load timeout, trying proxy approach...');
                    //setUseProxy(true);
                    setIframeLoaded(false);
                } else {
                    console.warn('Proxy approach timeout, showing fallback UI');
                    setIframeBlocked(true);
                }
            }
        }, 5000);
    }, [iframeLoaded, previewUrl, iframeBlocked, useProxy]);

    const handleRefresh = useCallback(() => {
        generatePreview();
    }, [generatePreview]);

    const handleOpenInNewTab = useCallback(() => {
        if (previewUrl) {
            window.open(previewUrl, '_blank');
        }
    }, [previewUrl]);

    return (
        <Modal isOpen={isOpen} toggle={toggle} size="xl" backdrop="static">
            <ModalHeader toggle={toggle}>
                <i className="fa fa-eye me-2"></i>
                Landing Page Preview
            </ModalHeader>
            <ModalBody style={{ minHeight: '500px' }}>
                {isLoading && (
                    <div className="text-center py-5">
                        <Spinner color="primary" />
                        <p className="mt-2">Generating preview...</p>
                    </div>
                )}

                {error && (
                    <Alert color="danger">
                        <i className="fa fa-exclamation-triangle me-2"></i>
                        {error}
                        <Button 
                            color="danger" 
                            outline 
                            size="sm" 
                            className="ms-2"
                            onClick={handleRefresh}
                        >
                            Try Again
                        </Button>
                    </Alert>
                )}

                {previewUrl && !isLoading && (
                    <div>
                        {/* Show iframe if not blocked, otherwise show fallback */}
                        {!iframeBlocked ? (
                            <div>
                                <div className="d-flex justify-content-between align-items-center mb-3">
                                    <div>
                                        <strong>Preview URL:</strong>
                                        <a 
                                            href={previewUrl} 
                                            target="_blank" 
                                            rel="noopener noreferrer"
                                            className="ms-2"
                                        >
                                            {previewUrl}
                                        </a>
                                    </div>
                                    <Button 
                                        color="primary" 
                                        size="sm" 
                                        onClick={handleOpenInNewTab}
                                    >
                                        <i className="fa fa-external-link me-1"></i>
                                        Open in New Tab
                                    </Button>
                                </div>

                                {/* Loading indicator for iframe */}
                                {!iframeLoaded && (
                                    <div className="text-center py-3">
                                        <Spinner color="primary" size="sm" />
                                        <span className="ms-2">Loading preview...</span>
                                    </div>
                                )}

                                <div className="preview-container" style={{ 
                                    border: '1px solid #dee2e6', 
                                    borderRadius: '0.375rem',
                                    overflow: 'hidden',
                                    display: iframeLoaded ? 'block' : 'none'
                                }}>
                                    <iframe
                                        src={useProxy ? getProxyUrl(previewUrl) : previewUrl}
                                        style={{
                                            width: '100%',
                                            height: '600px',
                                            border: 'none'
                                        }}
                                        title="Landing Page Preview"
                                        onLoad={handleIframeLoad}
                                        onError={handleIframeError}
                                        key={useProxy ? 'proxy' : 'direct'} // Force re-render when switching
                                    />
                                </div>
                            </div>
                        ) : (
                            <div className="text-center py-5">
                                <Alert color="warning" className="mb-4">
                                    <i className="fa fa-exclamation-triangle me-2"></i>
                                    <strong>Preview cannot be displayed in iframe</strong>
                                    <br />
                                    This is due to Content Security Policy restrictions from the landing page domain.
                                    {useProxy && <><br />We also tried using a proxy approach, but it was unsuccessful.</>}
                                    <br />Please use the button below to open the preview in a new tab.
                                </Alert>
                                
                                <div className="mb-4">
                                    <i className="fa fa-external-link fa-4x text-primary mb-3"></i>
                                    <h5>Preview Ready</h5>
                                    <p className="text-muted">
                                        Your landing page preview has been generated successfully.
                                    </p>
                                </div>
                                
                                <div className="mb-4">
                                    <strong>Preview URL:</strong>
                                    <div className="mt-2">
                                        <code className="bg-light p-2 rounded d-inline-block">
                                            {previewUrl}
                                        </code>
                                    </div>
                                </div>
                                
                                <Button 
                                    color="primary" 
                                    size="lg"
                                    onClick={handleOpenInNewTab}
                                    className="me-2"
                                >
                                    <i className="fa fa-external-link me-2"></i>
                                    Open Preview in New Tab
                                </Button>
                                
                                <Button 
                                    color="secondary" 
                                    size="lg"
                                    onClick={() => {
                                        setIframeBlocked(false);
                                        setIframeLoaded(false);
                                        //setUseProxy(false);
                                        //handleRefresh();
                                    }}
                                >
                                    <i className="fa fa-refresh me-2"></i>
                                    Try Iframe Again
                                </Button>
                            </div>
                        )}
                    </div>
                )}

                {!isLoading && !error && !previewUrl && (
                    <div className="text-center py-5">
                        <i className="fa fa-info-circle fa-3x text-muted mb-3"></i>
                        <p className="text-muted">Preview will be generated automatically</p>
                    </div>
                )}
            </ModalBody>
            <ModalFooter>
                <Button color="secondary" onClick={toggle}>
                    Close
                </Button>
                {/* {previewUrl && !iframeBlocked && (
                    <Button color="primary" onClick={handleRefresh}>
                        <i className="fa fa-refresh me-1"></i>
                        Refresh Preview
                    </Button>
                )} */}
                {previewUrl && (
                    <Button color="info" onClick={handleOpenInNewTab}>
                        <i className="fa fa-external-link me-1"></i>
                        Open in New Tab
                    </Button>
                )}
            </ModalFooter>
        </Modal>
    );
};

function mapStateToProps({ formData, moduleComponents, ui, languageList, landingPageUrl }) {
    return {
        formData,
        moduleComponents,
        languageList,
        landingPageUrl,
        isOpen: ui.isPreviewOpen
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPreviewModal, generatePreviewUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(PreviewModal));
