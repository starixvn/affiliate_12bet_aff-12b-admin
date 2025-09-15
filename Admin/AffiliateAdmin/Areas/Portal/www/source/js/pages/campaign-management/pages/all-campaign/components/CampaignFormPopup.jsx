import React, { useState, useEffect, useCallback, useRef } from 'react';
import { FormGroup, Label, Col, Input, Button, Modal, ModalHeader, ModalBody, Form, Alert } from 'reactstrap';
import { CAMPAIGN_STATUS } from '../../constants/status';

const CampaignFormPopup = ({
    isAdd = true,
    campaignCode = '',
    isOpen = false,
    onAddUpdate,
    onCancel
}) => {
    const [formData, setFormData] = useState({
        campaignCode: '',
        campaignName: '',
        campaignDescription: '',
        status: 'Draft'
    });

    const [state, setState] = useState({
        message: '',
        isSuccess: false,
        isShowMessage: false,
        errors: {
            campaignName: false
        }
    });

    const modalRef = useRef();
    const messageTimeoutRef = useRef();

    useEffect(() => {
        if (!isAdd && campaignCode) {
            fetchCampaignData(campaignCode);
        } else {
            setFormData({
                campaignCode: '',
                campaignName: '',
                campaignDescription: '',
                status: 'Draft'
            });
        }
    }, [isAdd, campaignCode]);

    const fetchCampaignData = useCallback(async (code) => {
        try {
            setFormData({
                campaignCode: code,
                campaignName: '',
                campaignDescription: '',
                status: 'Draft'
            });
        } catch (error) {
            console.error('Error fetching campaign data:', error);
            showMessage('Error fetching campaign data', false);
        }
    }, []);

    const showMessage = useCallback((message, isSuccess = false, duration = 3000) => {
        if (messageTimeoutRef.current) {
            clearTimeout(messageTimeoutRef.current);
        }

        modalRef.current && modalRef.current.scrollTo({ top: 0, behavior: "smooth" });

        setState(prev => ({
            ...prev,
            message,
            isSuccess,
            isShowMessage: true
        }));

        if (duration) {
            messageTimeoutRef.current = setTimeout(() => {
                setState(prev => ({ ...prev, isShowMessage: false }));
                
                if (isSuccess) {
                    handleClosePopup();
                }
            }, duration);
        }
    }, []);

    const handleInputChange = useCallback((e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));

        setState(prev => ({
            ...prev,
            errors: {
                ...prev.errors,
                [name]: false
            }
        }));
    }, []);

    const handleClosePopup = useCallback(() => {
        if (messageTimeoutRef.current) {
            clearTimeout(messageTimeoutRef.current);
        }
        
        setFormData({
            campaignCode: '',
            campaignName: '',
            campaignDescription: '',
            status: 'Draft'
        });

        setState({
            message: '',
            isSuccess: false,
            isShowMessage: false,
            errors: {
                campaignName: false
            }
        });

        onCancel && onCancel();
    }, [onCancel]);

    const handleSubmit = useCallback((e) => {
        e.preventDefault();

        const newErrors = {
            campaignName: !formData.campaignName.trim()
        };

        setState(prev => ({ ...prev, errors: newErrors }));

        if (newErrors.campaignName) {
            showMessage('Campaign Name is required.', false);
            return;
        }

        if (onAddUpdate) {
            const result = onAddUpdate(formData);
            if (result && typeof result.then === 'function') {
                result.then((response) => {
                    if (response && response.IsSuccessful) {
                        const action = isAdd ? 'added' : 'updated';
                        showMessage(`Campaign has been ${action} successfully!`, true);
                    } else {
                        showMessage(response?.Message || 'Operation failed', false);
                    }
                }).catch(() => {
                    showMessage('An error occurred', false);
                });
            } else {
                const action = isAdd ? 'added' : 'updated';
                showMessage(`Campaign has been ${action} successfully!`, true);
            }
        }
    }, [formData, onAddUpdate, isAdd, showMessage]);

    useEffect(() => {
        return () => {
            if (messageTimeoutRef.current) {
                clearTimeout(messageTimeoutRef.current);
            }
        };
    }, []);

    const { message, isSuccess, isShowMessage, errors } = state;
    const title = isAdd ? 'Add New Campaign' : 'Edit Campaign';

    return (
        <Modal isOpen={isOpen} toggle={handleClosePopup} backdrop='static' innerRef={modalRef}>
            <ModalHeader toggle={handleClosePopup}>
                <span className="iconpopup"></span>{title}
            </ModalHeader>
            <ModalBody>
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                
                <Form onSubmit={handleSubmit} autoComplete="off">
                    {!isAdd && (
                        <FormGroup row className="mb-2">
                            <Label sm={3} htmlFor="campaignCode">Campaign Code</Label>
                            <Col sm={9}>
                                <Input 
                                    type="text"
                                    name="campaignCode"
                                    id="campaignCode"
                                    value={formData.campaignCode}
                                    disabled={true}
                                />
                            </Col>
                        </FormGroup>
                    )}

                    <FormGroup row className="mb-2">
                        <Label sm={3} htmlFor="campaignName" className="required">
                            Campaign Name
                        </Label>
                        <Col sm={9}>
                            <Input 
                                type="text"
                                name="campaignName"
                                id="campaignName"
                                placeholder="e.g. Holiday Season FTD Campaign"
                                value={formData.campaignName}
                                onChange={handleInputChange}
                                className={errors.campaignName ? 'form-validation-warning' : ''}
                                required
                            />
                        </Col>
                    </FormGroup>

                    <FormGroup row className="mb-2">
                        <Label sm={3} htmlFor="campaignDescription">Campaign Description</Label>
                        <Col sm={9}>
                            <Input 
                                type="textarea"
                                name="campaignDescription"
                                id="campaignDescription"
                                placeholder="Describe the campaign purpose, target audience, and key features"
                                value={formData.campaignDescription}
                                onChange={handleInputChange}
                                rows="4"
                            />
                        </Col>
                    </FormGroup>

                    <FormGroup row className="mb-3">
                        <Label sm={3} htmlFor="status">Status</Label>
                        <Col sm={9}>
                            <Input 
                                type="select"
                                name="status"
                                id="status"
                                value={formData.status}
                                onChange={handleInputChange}
                            >
                                {CAMPAIGN_STATUS.map(statusOption => (
                                    <option key={statusOption.id} value={statusOption.status}>
                                        {statusOption.status}
                                    </option>
                                ))}
                            </Input>
                        </Col>
                    </FormGroup>

                    <div className="campaign-form-actions">
                        <Button 
                            type="submit"
                            color="success"
                            className="mr-2"
                            disabled={!formData.campaignName.trim()}
                        >
                            {isAdd ? 'Add' : 'Update'}
                        </Button>
                        <Button 
                            type="button"
                            color="info"
                            onClick={handleClosePopup}
                        >
                            Cancel
                        </Button>
                    </div>
                </Form>
            </ModalBody>
        </Modal>
    );
};

export default CampaignFormPopup;
