import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col, Card, CardBody, Button, Alert, Spinner } from 'reactstrap';

const LandingPageReview = ({ validation, ui }) => {
    const [iframeBlocked, setIframeBlocked] = useState(false);
    const [iframeLoaded, setIframeLoaded] = useState(false);
    const [useProxy, setUseProxy] = useState(false);

    const handleIframeLoad = useCallback(() => {
        setIframeLoaded(true);
        setIframeBlocked(false);
    }, []);

    const handleIframeError = useCallback(() => {
        console.warn('Iframe failed to load, likely blocked by CSP');
        
        if (!useProxy) {
            console.log('Trying proxy approach...');
            setIframeLoaded(false);
        } else {
            console.warn('Proxy approach also failed, showing fallback UI');
            setIframeBlocked(true);
            setIframeLoaded(false);
        }
    }, [useProxy]);

    const getProxyUrl = useCallback((originalUrl) => {
        if (!originalUrl) return null;
        return `/Portal/LandingPageProxy/ProxyLandingPage?url=${encodeURIComponent(originalUrl)}`;
    }, []);

    const handleIframeLoadTimeout = useCallback(() => {
        setTimeout(() => {
            if (!iframeLoaded && ui.previewUrl && !iframeBlocked) {
                if (!useProxy) {
                    console.warn('Iframe load timeout, trying proxy approach...');
                    setIframeLoaded(false);
                } else {
                    console.warn('Proxy approach timeout, showing fallback UI');
                    setIframeBlocked(true);
                }
            }
        }, 5000);
    }, [iframeLoaded, ui.previewUrl, iframeBlocked, useProxy]);

    const handleOpenInNewTab = useCallback(() => {
        if (ui.previewUrl) {
            window.open(ui.previewUrl, '_blank');
        }
    }, [ui.previewUrl]);

    useEffect(() => {
        if (ui.previewUrl && !iframeBlocked) {
            setIframeLoaded(false);
            handleIframeLoadTimeout();
        }
    }, [ui.previewUrl, iframeBlocked, handleIframeLoadTimeout]);
    return (
        <fieldset className='landing-page-preview'>
            <legend><h6 className="font-weight-bold text-success">Landing Page Preview</h6></legend>
            
            {/* Show validation errors when they exist */}
            {validation && validation.errors && Object.keys(validation.errors).length > 0 && (
                <Alert color="danger" className="mb-3">
                    <h6 className="alert-heading">
                        <i className="fa fa-exclamation-triangle me-2"></i>
                        Validation Errors
                    </h6>
                    <p className="mb-2">Please fix the following errors before proceeding:</p>
                    <ul className="mb-0">
                        {Object.entries(validation.errors).map(([key, error]) => (
                            <li key={key} className="mb-1">
                                {typeof error === 'string' ? error : String(error || 'Validation error')}
                            </li>
                        ))}
                    </ul>
                </Alert>
            )}
            
            <div className="preview-container">
                {/* Show preview content when available */}
                {ui.previewUrl && (
                    <div>
                        {/* Show iframe if not blocked, otherwise show fallback */}
                        {!iframeBlocked ? (
                            <div>
                                <div className="d-flex justify-content-between align-items-center mb-3">
                                    <div>
                                        <strong>Preview URL:</strong>
                                        <a 
                                            href={ui.previewUrl} 
                                            target="_blank" 
                                            rel="noopener noreferrer"
                                            className="ms-2"
                                            style={{ fontSize: '12px' }}
                                        >
                                            {ui.previewUrl}
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

                                <div className="preview-iframe-container" style={{ 
                                    border: '1px solid #dee2e6', 
                                    borderRadius: '0.375rem',
                                    overflow: 'hidden',
                                    display: iframeLoaded ? 'block' : 'none'
                                }}>
                                    <iframe
                                        src={useProxy ? getProxyUrl(ui.previewUrl) : ui.previewUrl}
                                        style={{
                                            width: '100%',
                                            height: '500px',
                                            border: 'none'
                                        }}
                                        title="Landing Page Preview"
                                        onLoad={handleIframeLoad}
                                        onError={handleIframeError}
                                        key={useProxy ? 'proxy' : 'direct'}
                                    />
                                </div>
                            </div>
                        ) : (
                            <div className="text-center py-4">
                                <Alert color="warning" className="mb-4">
                                    <i className="fa fa-exclamation-triangle me-2"></i>
                                    <strong>Preview cannot be displayed in iframe</strong>
                                    <br />
                                    This is due to Content Security Policy restrictions from the landing page domain.
                                    {useProxy && <><br />We also tried using a proxy approach, but it was unsuccessful.</>}
                                    <br />Please use the button below to open the preview in a new tab.
                                </Alert>
                                
                                <div className="mb-3">
                                    <i className="fa fa-external-link fa-3x text-primary mb-3"></i>
                                    <h6>Preview Ready</h6>
                                    <p className="text-muted">
                                        Your landing page preview has been generated successfully.
                                    </p>
                                </div>
                                
                                <div className="mb-3">
                                    <strong>Preview URL:</strong>
                                    <div className="mt-2">
                                        <code className="bg-light p-2 rounded d-inline-block" style={{ fontSize: '11px' }}>
                                            {ui.previewUrl}
                                        </code>
                                    </div>
                                </div>
                                
                                <Button 
                                    color="primary" 
                                    size="sm"
                                    onClick={handleOpenInNewTab}
                                    className="me-2"
                                >
                                    <i className="fa fa-external-link me-2"></i>
                                    Open Preview in New Tab
                                </Button>
                                
                                <Button 
                                    color="secondary" 
                                    size="sm"
                                    onClick={() => {
                                        setIframeBlocked(false);
                                        setIframeLoaded(false);
                                    }}
                                >
                                    <i className="fa fa-refresh me-2"></i>
                                    Try Iframe Again
                                </Button>
                            </div>
                        )}
                    </div>
                )}

                {/* Show placeholder when no preview is available */}
                {!ui.hasPreviewedOrCreated && (
                    <div className="preview-placeholder">
                        <div className="preview-icon">
                            <i className="fa fa-file-o" style={{ fontSize: '48px', color: '#ccc' }}></i>
                        </div>
                        <div className="preview-text">
                            <p className="text-muted text-center mt-3">
                                Create a new page, then select<br />
                                components in order to start building.
                            </p>
                            <p className="text-muted text-center mt-2" style={{ fontSize: '12px' }}>
                                Reference the Best Practice tips for<br />
                                optimal layout.
                            </p>
                        </div>
                    </div>
                )}
            </div>
        </fieldset>
    );
};

function mapStateToProps({ validation, ui }) {
    return {
        validation,
        ui
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LandingPageReview));
