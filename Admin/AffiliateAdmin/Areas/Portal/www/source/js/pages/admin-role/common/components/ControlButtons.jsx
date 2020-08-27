import React, { Fragment } from 'react';
import { Button } from 'reactstrap';

export const ControlButtons = (props) => {
    const { isEdit } = props;

    return (
        <div className="my-2">
            {
                isEdit ? (
                    <Fragment>
                        <Button outline type="button" color="danger" onClick={() => props.handleCancel()}>Cancel<span className="oi oi-x ml-2"></span></Button>
                        <Button outline type="button" className="ml-2" onClick={() => props.handleSave()}>Save<span className="oi oi-task ml-2"></span></Button>
                    </Fragment>
                ) : (
                        <Button outline type="button" onClick={() => props.handleEdit()}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                    )
            }
        </div>
    );
}