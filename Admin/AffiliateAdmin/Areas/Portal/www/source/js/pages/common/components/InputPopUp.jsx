import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Input, ModalFooter, Button } from "reactstrap";
import { NotificationPopup } from "./NotificationPopup";

export const InputPopUp = (props) => {
    const { isOpen, handleSubmit, title, value } = props;
    
    const [text, setText] = useState(value)
    const [message, setMessage] = useState("");

    const toggle = () => setModal(!modal);

    const toggleNested = () => {
        if (!!message)
        {
            setMessage("")
        }
        else{
            setMessage("Are your sure?")
        }
    }

    const handleChangeText = (value) => setText(value);

    const handleConfirm = (isConfirm) => {
        toggleNested();
        if(isConfirm)
        {
            handleSubmit(text);
            toggle();
        }
    }

    return (
        <Modal isOpen={isOpen} toggle={toggle} backdrop='static'>
            <ModalHeader toggle={toggle}>{title}</ModalHeader>
            <ModalBody>
                <Input type="text" onChange={handleChangeText} value={text}/>
                <NotificationPopup message={message} isConfirmPopup={true} handleClose={handleConfirm}/>
            </ModalBody>
            <ModalFooter>
                <Button onClick={toggleNested}>OK</Button>
                <Button onClick={toggle}>Cancel</Button>
            </ModalFooter>
        </Modal>
    );
}
export default InputPopUp;