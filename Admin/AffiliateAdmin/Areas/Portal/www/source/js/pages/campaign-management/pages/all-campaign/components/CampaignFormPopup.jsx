import React, { useState, useEffect, useCallback, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { FormGroup, Label, Col, Input, Button, Form } from 'reactstrap';
import { ActionCreators } from '../actions';
import { v4 as uuidv4 } from 'uuid';
import { toast } from 'react-toastify';
import { showErrorToast } from '@/pages/common/components/Toast';

// Campaign Form Toast Component
const CampaignFormToastContent = ({
    title,
    formData,
    errors,
    statusList,
    onInputChange,
    onBlur,
    onSubmit,
    onClose,
    isAdd,
    selectedItem
}) => (
    <div className="campaign-form-toast">
        <div className="toast-header form-header">
            <div className="header-content">
                <span className={isAdd ? "oi oi-plus" : "oi oi-pencil"}></span>{title}
            </div>
            <button
                type="button"
                className="close-button"
                onClick={onClose}
                aria-label="Close"
            >
                ×
            </button>
        </div>
        <div className="toast-content form-content">
            <Form onSubmit={onSubmit} autoComplete="off">
                {!isAdd && (
                    <FormGroup row className="mb-2">
                        <Label sm={3} htmlFor="campaignCode">Campaign Code</Label>
                        <Col sm={9}>
                            <Input
                                type="text"
                                name="campaignCode"
                                id="campaignCode"
                                value={selectedItem?.CampaignCode || ''}
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
                            onChange={onInputChange}
                            onBlur={onBlur}
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
                            onChange={onInputChange}
                            rows="4"
                        />
                    </Col>
                </FormGroup>

                <FormGroup row className="mb-3">
                    <Label sm={3} htmlFor="statusId">Status</Label>
                    <Col sm={9}>
                        <Input
                            type="select"
                            name="statusId"
                            id="statusId"
                            value={formData.statusId}
                            onChange={onInputChange}
                        >
                            {statusList.map(statusOption => (
                                <option key={statusOption.key} value={statusOption.key}>
                                    {statusOption.name}
                                </option>
                            ))}
                        </Input>
                    </Col>
                </FormGroup>

                <div className="campaign-form-actions text-right">
                    <Button
                        type="button"
                        color="info"
                        className="mr-2"
                        onClick={onClose}
                    >
                        Cancel
                    </Button>
                    <Button
                        type="submit"
                        color="success"
                        disabled={!formData.campaignName.trim()}
                    >
                        {isAdd ? 'Add' : 'Update'}
                    </Button>
                </div>
            </Form>
        </div>
    </div>
);

const CampaignFormPopup = () => {
    const dispatch = useDispatch();
    const [
        popupState,
        param,
        statusList
    ] = useSelector(state => [
        state.popupState,
        state.param,
        state.statusList
    ]);
    const { insertCampaign, updateCampaign, setParam, closeCampaignPopup, openCampaignPopup } = ActionCreators;

    const { isOpen, isAdd, selectedItem } = popupState;
    const toastIdRef = useRef(null);


    const [formData, setFormData] = useState({
        campaignCode: '',
        campaignName: '',
        campaignDescription: '',
        statusId: 0
    });

    const [state, setState] = useState({
        errors: {
            campaignName: false
        }
    });

    useEffect(() => {
        if (!isAdd && selectedItem) {
            setFormData({
                campaignCode: selectedItem.CampaignCode || '',
                campaignName: selectedItem.CampaignName || '',
                campaignDescription: selectedItem.Description || '',
                statusId: selectedItem.StatusId || 0
            });
        } else if (isAdd) {
            setFormData({
                campaignCode: '',
                campaignName: '',
                campaignDescription: '',
                statusId: 0
            });
        }

        // Reset errors when switching modes
        setState({
            errors: {
                campaignName: false
            }
        });
    }, [isAdd, selectedItem]);



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

    const handleCampaignNameBlur = useCallback((e) => {
        const value = e.target.value.trim();
        if (!value) {
            setState(prev => ({
                ...prev,
                errors: {
                    ...prev.errors,
                    campaignName: true
                }
            }));
        }
    }, []);

    // Show/Hide toast based on popup state
    useEffect(() => {
        const title = isAdd ? 'Add New Campaign' : 'Edit Campaign';

        if (isOpen) {
            // Dismiss existing toast if any
            if (toastIdRef.current) {
                toast.dismiss(toastIdRef.current);
            }

            const newToastId = `campaign-form-${uuidv4()}`;
            toastIdRef.current = newToastId;

            toast(
                <CampaignFormToastContent
                    title={title}
                    formData={formData}
                    errors={state.errors}
                    statusList={statusList}
                    onInputChange={handleInputChange}
                    onBlur={handleCampaignNameBlur}
                    onSubmit={handleSubmit}
                    onClose={handleClosePopup}
                    isAdd={isAdd}
                    selectedItem={selectedItem}
                />,
                {
                    toastId: newToastId,
                    position: 'top-center',
                    autoClose: false,
                    closeOnClick: false,
                    closeButton: false,
                    draggable: false,
                    className: 'campaign-form-toast-container',
                    bodyClassName: 'campaign-form-toast-body',
                    hideProgressBar: true,
                }
            );
        } else if (!isOpen && toastIdRef.current) {
            toast.dismiss(toastIdRef.current);
            toastIdRef.current = null;
        }
    }, [isOpen, isAdd, selectedItem]);

    // Update toast content when form data changes
    useEffect(() => {
        if (isOpen && toastIdRef.current) {
            const title = isAdd ? 'Add New Campaign' : 'Edit Campaign';

            toast.update(toastIdRef.current, {
                render: (
                    <CampaignFormToastContent
                        title={title}
                        formData={formData}
                        errors={state.errors}
                        statusList={statusList}
                        onInputChange={handleInputChange}
                        onBlur={handleCampaignNameBlur}
                        onSubmit={handleSubmit}
                        onClose={handleClosePopup}
                        isAdd={isAdd}
                        selectedItem={selectedItem}
                    />
                )
            });
        }
    }, [formData, state.errors, isOpen, isAdd, selectedItem, statusList, handleInputChange, handleCampaignNameBlur, handleSubmit, handleClosePopup]);

    const handleClosePopup = useCallback(() => {
        if (toastIdRef.current) {
            toast.dismiss(toastIdRef.current);
            toastIdRef.current = null;
        }

        setFormData({
            campaignCode: '',
            campaignName: '',
            campaignDescription: '',
            statusId: 0
        });

        setState({
            errors: {
                campaignName: false
            }
        });

        dispatch(closeCampaignPopup());
    }, [dispatch, closeCampaignPopup]);

    const handleSubmit = useCallback(async (e) => {
        e.preventDefault();

        const newErrors = {
            campaignName: !formData.campaignName.trim()
        };

        setState(prev => ({ ...prev, errors: newErrors }));

        if (newErrors.campaignName) {
            showErrorToast('Validation Error', 'Campaign Name is required.');
            return;
        }

        if (isAdd) {
            dispatch(insertCampaign(formData, () => {
                dispatch(setParam({ ...param, RequestId: uuidv4() }));
                handleClosePopup();
            }));
        } else {
            const updateData = {
                ...selectedItem,
                CampaignName: formData.campaignName,
                Description: formData.campaignDescription,
                StatusId: formData.statusId
            };

            dispatch(updateCampaign(updateData, () => {
                dispatch(setParam({ ...param, RequestId: uuidv4() }));
                handleClosePopup();
            }));
        }
    }, [formData, isAdd, selectedItem, param, dispatch, insertCampaign, updateCampaign, setParam, handleClosePopup]);

    return null;
};

export default CampaignFormPopup;
