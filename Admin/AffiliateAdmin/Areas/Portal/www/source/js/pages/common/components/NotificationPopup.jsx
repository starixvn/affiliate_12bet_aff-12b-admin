import React from 'react';
import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';

const widthHeight = { width: 70, height: 30 };

export const NotificationPopup = (props) => {

    return (
        <Modal isOpen={!!props.message} toggle={() => props.onClose(false)} backdrop='false'>
            <ModalHeader toggle={() => props.onClose(false)}>
                <div className="text-truncate" style={{ width: 450 }}>
                    <span className="iconpopup"></span>{props.isConfirmPopup ? 'Confirmation' : 'Notification'}
                </div>
            </ModalHeader>
            <ModalBody>
                <div className="modal-body-wrapper">
                    <div className="notification-popup-container" style={{ fontSize: 14 }}>
                        <h5>{window.location.host} says</h5>
                        <div dangerouslySetInnerHTML={{ __html: props.message }}></div>
                        {
                            props.isConfirmPopup ? (
                                <div className="mt-5">
                                    <Button style={widthHeight} color="success mr-3" onClick={() => props.onClose(true)}><strong>Confirm</strong></Button>
                                    <Button style={widthHeight} color="info" onClick={() => props.onClose(false)}><strong>Cancel</strong></Button>
                                </div>
                            ) : (<Button style={widthHeight} color="success mt-5" onClick={() => props.onClose(false)}><strong>OK</strong></Button>)
                        }
                    </div>
                </div>
            </ModalBody>
        </Modal>
    );
}

export default React.memo(NotificationPopup);