import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Input, Button } from "reactstrap";
import { NotificationPopup } from "./NotificationPopup";

export const InputPopUp = (props) => {
    const widthHeight = { width: 70, height: 30 };
    const { isOpen, handleSubmit, title, value, toggle } = props;

    const [text, setText] = useState(value)
    const [message, setMessage] = useState("");

    const toggleNested = () => {
        if (!!message) {
            setMessage("")
        }
        else {
            setMessage("Are your sure?")
        }
    }

    const handleChangeText = (value) => setText(value);

    const handleConfirm = (isConfirm) => {
        toggleNested();
        if (isConfirm) {
            handleSubmit(text);
        }
    }

    return (
        <Modal isOpen={isOpen} toggle={toggle} backdrop='static'>
            <ModalHeader toggle={toggle}>
                <div className="text-truncate" style={{ width: 450 }}>
                    <span className="iconpopup"></span>{title}
                </div>
            </ModalHeader>
            <ModalBody>
                <div className="modal-body-wrapper">
                    <div className="notification-popup-container" style={{ fontSize: 14 }}>
                        <Input type="text" style={{ fontSize: 14 }} onChange={handleChangeText} value={text} />
                        <div className="mt-3">
                            <Button style={widthHeight} color="success mr-3" onClick={toggleNested}><strong>OK</strong></Button>
                            <Button style={widthHeight} color="info" onClick={toggle}><strong>Cancel</strong></Button>
                        </div>
                    </div>
                </div>
                <NotificationPopup message={message} isConfirmPopup={true} onClose={handleConfirm} />
            </ModalBody>
        </Modal>
    );
}
export default React.memo(InputPopUp);