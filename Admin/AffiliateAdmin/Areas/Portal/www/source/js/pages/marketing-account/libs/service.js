import React from 'react';
import { Col, FormGroup, Label, Input } from 'reactstrap';

export const renderInputForm = (name, label, inputValue, isStatic, isRequired, changeHandler) => {
    return (
        <FormGroup row key={name}>
            <Label for={name} sm={2} className={isRequired ? 'required' : ''}>{label}</Label>
            <Col sm={6} className="input-item">
                {
                    isStatic ? (<p className="form-control-plaintext">{inputValue}</p>) :
                        (<Input type="text" id={name} name={name} value={inputValue || ''} onChange={changeHandler} />)
                }
            </Col>
        </FormGroup>
    )
}

export const lowerFirstChar = (obj) => {
    return Object.fromEntries(
        Object.entries(obj).map(([k, v]) => [
            k.charAt(0).toLowerCase() + k.slice(1),
            v
        ])
    );
}