import React from 'react';
import { Modal, Button } from 'reactstrap';

export const Notification = (props) => {

    !!props.timeout && setTimeout(() => props.onClose(), props.timeout);

    return (
        <div className="notification">
            <Modal isOpen={props.isNotification} toggle={() => props.onClose()} backdrop='static'>
                <div className="notification-content p-3" style={{ fontSize: 15 }}>
                    <h4>{window.location.host} says</h4>
                    <p>{props.message}</p>
                    <Button style={{ width: 70, height: 35 }} color="primary mt-4 float-right" onClick={() => props.onClose()}><strong>OK</strong></Button>
                </div>
            </Modal>
        </div>
    );
}

export default React.memo(Notification);