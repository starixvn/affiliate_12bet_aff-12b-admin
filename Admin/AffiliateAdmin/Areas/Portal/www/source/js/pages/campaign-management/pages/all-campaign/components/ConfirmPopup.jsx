import React from 'react';
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';

const ConfirmPopup = ({
    title = "Confirm this Action",
    content = [],
    isOpen = false,
    onCancel,
    onSubmit,
    cancelText = "Cancel",
    submitText = "Yes"
}) => {
    const handleCancel = () => {
        onCancel && onCancel();
    };

    const handleSubmit = () => {
        onSubmit && onSubmit();
    };

    return (
        <Modal isOpen={isOpen} toggle={handleCancel} backdrop='static'>
            <ModalHeader toggle={handleCancel}>
                <span className="iconpopup"></span>{title}
            </ModalHeader>
            <ModalBody>
                <div className="confirm-popup-content">
                    {Array.isArray(content) ? (
                        content.map((line, index) => (
                            <p key={index} className="confirm-popup-line">
                                {line}
                            </p>
                        ))
                    ) : (
                        <p className="confirm-popup-line">{content}</p>
                    )}
                </div>

                <div className="confirm-popup-actions mt-3">
                    <Button 
                        type="button"
                        color="info"
                        onClick={handleCancel}
                        className="mr-2"
                    >
                        {cancelText}
                    </Button>
                    <Button 
                        type="button"
                        color="success"
                        onClick={handleSubmit}
                    >
                        {submitText}
                    </Button>
                </div>
            </ModalBody>
        </Modal>
    );
};

export default ConfirmPopup;
