import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Alert, Spinner } from 'reactstrap';

import { setPreviewModal, generatePreviewUrl } from '../../actions';

const PreviewModal = ({ isOpen, formData, setPreviewModal, generatePreviewUrl }) => {
    const [previewUrl, setPreviewUrl] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const generatePreview = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        
        try {
            const url = await generatePreviewUrl(formData);
            
            if (url) {
                setPreviewUrl(url);
            } else {
                setError('Failed to generate preview. Please check your form data and try again.');
            }
        } catch (err) {
            setError('An error occurred while generating the preview.');
            console.error('Preview generation error:', err);
        } finally {
            setIsLoading(false);
        }
    }, [generatePreviewUrl, formData]);

    useEffect(() => {
        if (isOpen) {
            generatePreview();
        }
    }, [isOpen, generatePreview]);

    const toggle = useCallback(() => {
        setPreviewModal(false);
        // Reset state when modal closes
        setPreviewUrl(null);
        setError(null);
    }, [setPreviewModal]);

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

                        <div className="preview-container" style={{ 
                            border: '1px solid #dee2e6', 
                            borderRadius: '0.375rem',
                            overflow: 'hidden'
                        }}>
                            <iframe
                                src={previewUrl}
                                style={{
                                    width: '100%',
                                    height: '600px',
                                    border: 'none'
                                }}
                                title="Landing Page Preview"
                            />
                        </div>
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
                {previewUrl && (
                    <Button color="primary" onClick={handleRefresh}>
                        <i className="fa fa-refresh me-1"></i>
                        Refresh Preview
                    </Button>
                )}
            </ModalFooter>
        </Modal>
    );
};

function mapStateToProps(state) {
    return {};
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPreviewModal, generatePreviewUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(PreviewModal));
