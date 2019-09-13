import React from 'react';
import { Modal, Button } from 'reactstrap';

export const NotificationPopup = (props) => {

    return (
        <Modal isOpen={!!props.message} toggle={() => props.onClose()} backdrop='static'>
            <div className="notification-popup-content p-3" style={{ fontSize: 15 }}>
                <h4>{window.location.host} says</h4>
                <p>{props.message}</p>
                {
                    props.isConfirmPopup ? (
                        <div className="float-right mt-4">
                            <Button style={{ width: 70, height: 35 }} color="primary mr-2" onClick={() => props.onClose(true)}><strong>OK</strong></Button>
                            <Button style={{ width: 70, height: 35 }} color="light border" onClick={() => props.onClose(false)}><strong>Cancel</strong></Button>
                        </div>
                    ) : (<Button style={{ width: 70, height: 35 }} color="primary mt-4 float-right" onClick={() => props.onClose(false)}><strong>OK</strong></Button>)
                }
            </div>
        </Modal>
    );
}

export default React.memo(NotificationPopup);