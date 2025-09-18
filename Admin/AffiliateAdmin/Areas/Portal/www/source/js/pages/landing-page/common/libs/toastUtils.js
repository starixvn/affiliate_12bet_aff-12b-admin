import React from 'react';
import { toast } from 'react-toastify';
import { v4 as uuidv4 } from 'uuid';

// Landing Page Toast Components
const NotificationToast = ({ title, content, type, toastId }) => {
    const getIconClass = (type) => {
        switch (type) {
            case 'success':
                return 'oi oi-check';
            case 'error':
                return 'oi oi-warning';
            case 'info':
                return 'oi oi-info';
            case 'warning':
                return 'oi oi-warning';
            default:
                return 'oi oi-info';
        }
    };

    return (
        <div className="landing-page-notification-toast">
            <div className={`toast-header ${type}-header`}>
                <div className="header-content">
                    <span className={`${getIconClass(type)} toast-icon`}></span>{title}
                </div>
                <button
                    type="button"
                    className="close-button"
                    onClick={() => toast.dismiss(toastId)}
                    aria-label="Close"
                >
                    ×
                </button>
            </div>
            <div className="toast-content">
                {Array.isArray(content) ? (
                    content.map((line, index) => (
                        <p key={index} className="toast-line" dangerouslySetInnerHTML={{ __html: line }}></p>
                    ))
                ) : (
                    <p className="toast-line" dangerouslySetInnerHTML={{ __html: content }}></p>
                )}
            </div>
        </div>
    );
};

const ConfirmToast = ({ title, content, onConfirm, onCancel, cancelText, submitText, toastId }) => (
    <div className="landing-page-confirm-toast">
        <div className="toast-header confirm-header">
            <div className="header-content">
                <span className="oi oi-question-mark toast-icon"></span>{title}
            </div>
            <button
                type="button"
                className="close-button"
                onClick={onCancel}
                aria-label="Close"
            >
                ×
            </button>
        </div>
        <div className="toast-content">
            {Array.isArray(content) ? (
                content.map((line, index) => (
                    <p key={index} className="toast-line" dangerouslySetInnerHTML={{ __html: line }}></p>
                ))
            ) : (
                <p className="toast-line" dangerouslySetInnerHTML={{ __html: content }}></p>
            )}
            <div className="toast-actions">
                <button
                    type="button"
                    className="btn btn-info mr-2"
                    onClick={onCancel}
                >
                    {cancelText}
                </button>
                <button
                    type="button"
                    className="btn btn-success"
                    onClick={onConfirm}
                >
                    {submitText}
                </button>
            </div>
        </div>
    </div>
);

// Toast utility functions with UUID
export const showSuccessToast = (title = "Success!", message = "Operation completed successfully", autoClose = 5000) => {
    const toastId = `landing-page-success-${uuidv4()}`;

    toast(
        <NotificationToast
            title={title}
            content={message}
            type="success"
            toastId={toastId}
        />,
        {
            toastId: toastId,
            position: 'top-right',
            autoClose: autoClose,
            closeOnClick: true,
            closeButton: false,
            draggable: false,
            className: 'landing-page-notification-toast-container',
            bodyClassName: 'landing-page-notification-toast-body',
            hideProgressBar: true,
        }
    );

    return toastId;
};

export const showErrorToast = (title = "Error!", message = "Something went wrong", autoClose = 5000) => {
    const toastId = `landing-page-error-${uuidv4()}`;

    toast(
        <NotificationToast
            title={title}
            content={message}
            type="error"
            toastId={toastId}
        />,
        {
            toastId: toastId,
            position: 'top-right',
            autoClose: autoClose,
            closeOnClick: true,
            closeButton: false,
            draggable: false,
            className: 'landing-page-notification-toast-container',
            bodyClassName: 'landing-page-notification-toast-body',
            hideProgressBar: true,
        }
    );

    return toastId;
};

export const showWarningToast = (title = "Warning", message = "Warning message", autoClose = 5000) => {
    const toastId = `landing-page-warning-${uuidv4()}`;

    toast(
        <NotificationToast
            title={title}
            content={message}
            type="warning"
            toastId={toastId}
        />,
        {
            toastId: toastId,
            position: 'top-right',
            autoClose: autoClose,
            closeOnClick: true,
            closeButton: false,
            draggable: false,
            className: 'landing-page-notification-toast-container',
            bodyClassName: 'landing-page-notification-toast-body',
            hideProgressBar: true,
        }
    );

    return toastId;
};

export const showInfoToast = (title = "Information", message = "Please note", autoClose = 5000) => {
    const toastId = `landing-page-info-${uuidv4()}`;

    toast(
        <NotificationToast
            title={title}
            content={message}
            type="info"
            toastId={toastId}
        />,
        {
            toastId: toastId,
            position: 'top-right',
            autoClose: autoClose,
            closeOnClick: true,
            closeButton: false,
            draggable: false,
            className: 'landing-page-notification-toast-container',
            bodyClassName: 'landing-page-notification-toast-body',
            hideProgressBar: true,
        }
    );

    return toastId;
};

export const showConfirmToast = ({
    title = "Confirm this Action",
    content = [],
    onConfirm,
    onCancel,
    cancelText = "Cancel",
    submitText = "Yes"
}) => {
    const toastId = `landing-page-confirm-${uuidv4()}`;

    toast(
        <ConfirmToast
            title={title}
            content={content}
            cancelText={cancelText}
            submitText={submitText}
            toastId={toastId}
            onConfirm={() => {
                onConfirm && onConfirm();
                toast.dismiss(toastId);
            }}
            onCancel={() => {
                onCancel && onCancel();
                toast.dismiss(toastId);
            }}
        />,
        {
            toastId: toastId,
            position: 'top-center',
            autoClose: false,
            closeOnClick: false,
            closeButton: false,
            draggable: false,
            className: 'landing-page-confirm-toast-container',
            bodyClassName: 'landing-page-confirm-toast-body',
            hideProgressBar: true,
        }
    );

    return toastId;
};

// Status change specific functions
export const showStatusChangeToast = (isSuccess, message) => {
    if (isSuccess) {
        return showSuccessToast('Status Updated', message || 'Status changed successfully');
    } else {
        return showErrorToast('Status Update Failed', message || 'Failed to change status');
    }
};

export const showStatusChangeConfirmation = (onConfirm, onCancel) => {
    const content = [
        'Only Active pages are available for link generation.',
        'Inactive pages are not available for link generation. Existing links with an Inactive page will redirect users to the main join page.',
        'Are you sure you want to change status of this landing page?'
    ];
    
    return showConfirmToast({
        title: "Confirm Status Change",
        content: content,
        onConfirm: onConfirm,
        onCancel: onCancel,
        submitText: "Yes, Change Status"
    });
};

export default {
    showSuccessToast,
    showErrorToast,
    showWarningToast,
    showInfoToast,
    showConfirmToast,
    showStatusChangeToast,
    showStatusChangeConfirmation
};
