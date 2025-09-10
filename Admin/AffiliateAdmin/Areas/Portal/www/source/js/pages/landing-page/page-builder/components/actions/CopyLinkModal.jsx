import React, { useState, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Input, FormGroup, Label, Alert } from 'reactstrap';

import { setCopyLinkModal, generatePreviewUrl } from '../../actions';

const CopyLinkModal = ({ isOpen, formData, moduleComponents, setCopyLinkModal, generatePreviewUrl }) => {
    const [generatedUrl, setGeneratedUrl] = useState('');
    const [isGenerating, setIsGenerating] = useState(false);
    const [copySuccess, setCopySuccess] = useState(false);
    const [error, setError] = useState(null);

    const toggle = useCallback(() => {
        setCopyLinkModal(false);
        // Reset state when modal closes
        setGeneratedUrl('');
        setCopySuccess(false);
        setError(null);
    }, [setCopyLinkModal]);

    const handleGenerateLink = useCallback(async () => {
        setIsGenerating(true);
        setError(null);
        setCopySuccess(false);

        try {
            const url = await generatePreviewUrl(formData, moduleComponents);
            
            if (url) {
                setGeneratedUrl(url);
            } else {
                setError('Failed to generate preview link. Please check your form data.');
            }
        } catch (err) {
            setError('An error occurred while generating the link.');
            console.error('Link generation error:', err);
        } finally {
            setIsGenerating(false);
        }
    }, [generatePreviewUrl, formData, moduleComponents]);

    const handleCopyToClipboard = useCallback(async () => {
        if (!generatedUrl) return;

        try {
            await navigator.clipboard.writeText(generatedUrl);
            setCopySuccess(true);
            
            // Reset success message after 3 seconds
            setTimeout(() => {
                setCopySuccess(false);
            }, 3000);
        } catch (err) {
            console.error('Failed to copy to clipboard:', err);
            
            // Fallback for older browsers
            const textArea = document.createElement('textarea');
            textArea.value = generatedUrl;
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
    }, [generatedUrl]);

    const isFormValid = formData.campaignId && 
                       formData.pageName && 
                       formData.pageCode &&
                       moduleComponents.length > 0;

    return (
        <Modal isOpen={isOpen} toggle={toggle} size="lg" backdrop="static">
            <ModalHeader toggle={toggle}>
                <i className="fa fa-link me-2"></i>
                Copy Preview Link
            </ModalHeader>
            <ModalBody>
                <div className="mb-3">
                    <p className="text-muted">
                        Generate a preview link for your landing page that can be shared with others. 
                        The link will show a preview based on your current form configuration.
                    </p>
                </div>

                {!isFormValid && (
                    <Alert color="warning">
                        <i className="fa fa-exclamation-triangle me-2"></i>
                        Please fill in the required fields (Campaign Name, Page Name, Page Code, and at least one module) 
                        before generating a preview link.
                    </Alert>
                )}

                {error && (
                    <Alert color="danger">
                        <i className="fa fa-exclamation-triangle me-2"></i>
                        {error}
                    </Alert>
                )}

                {copySuccess && (
                    <Alert color="success">
                        <i className="fa fa-check me-2"></i>
                        Link copied to clipboard successfully!
                    </Alert>
                )}

                <FormGroup>
                    <Label for="previewUrl">Preview Link</Label>
                    <div className="input-group">
                        <Input
                            type="text"
                            id="previewUrl"
                            value={generatedUrl}
                            readOnly
                            placeholder="Click 'Generate Link' to create a preview URL"
                        />
                        <Button
                            color="primary"
                            outline
                            onClick={handleCopyToClipboard}
                            disabled={!generatedUrl}
                            title="Copy to clipboard"
                        >
                            <i className="fa fa-copy"></i>
                        </Button>
                    </div>
                </FormGroup>

                <div className="text-muted small">
                    <strong>Note:</strong> The preview link will reflect the current state of your form. 
                    If you make changes to the form, you'll need to generate a new link to see the updates.
                </div>
            </ModalBody>
            <ModalFooter>
                <Button 
                    color="primary" 
                    onClick={handleGenerateLink}
                    disabled={!isFormValid || isGenerating}
                >
                    {isGenerating ? (
                        <>
                            <i className="fa fa-spinner fa-spin me-1"></i>
                            Generating...
                        </>
                    ) : (
                        <>
                            <i className="fa fa-link me-1"></i>
                            Generate Link
                        </>
                    )}
                </Button>
                
                <Button color="secondary" onClick={toggle}>
                    Close
                </Button>
            </ModalFooter>
        </Modal>
    );
};

function mapStateToProps({ formData, moduleComponents }) {
    return Object.assign({}, { formData, moduleComponents });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setCopyLinkModal, generatePreviewUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CopyLinkModal));
