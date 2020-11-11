import React from 'react';
import { Col, FormGroup, Label, Input } from 'reactstrap';

export const renderStaticForm = (name, label, inputValue) => {
    return(
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2}>{label}</Label>
            <Col sm={9} xl={10}>
                <p className="form-control-plaintext">{inputValue}</p>
            </Col>
        </FormGroup>
    )
}

export const renderInputForm = (name, label, inputValue, isDisabled, isRequired, changeHandler) => {
    return (
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2} className={isRequired ? "required" : ""}>{label}</Label>
            <Col sm={9} xl={10}>
                <Input type="text" id={name} name={name} value={inputValue} disabled={isDisabled} onChange={changeHandler} />
            </Col>
        </FormGroup>
    )
}

export const renderSelectionForm = (name, label, items, selectedKey, isDisabled, isRequired, changeHandler) => {
    return (
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2} className={isRequired ? "required" : ""}>{label}</Label>
            <Col sm={9} xl={10}>
                <Input type="select" id={name} name={name} value={selectedKey} key="name" disabled={isDisabled} onChange={changeHandler}>
                    {
                        selectedKey === 0 ? <option key={name + '-0'} value='0' selected='true'>Select a secret question</option>
                            : <option key={name + '-0'} value='0'>Select a secret question</option>}
                    { items && items.map((item) => {
                        return item.Key === selectedKey
                            ? <option key={name + '-' + item.Key} value={item.Key} selected='true'>{item.Value}</option>
                            : <option key={name + '-' + item.Key} value={item.Key}>{item.Value}</option>
                    })}
                </Input>
            </Col>
        </FormGroup>
    )
}

export const renderClaimBaseInputForm = (claimPrincipal, claimName, name, label, inputValue, isRequired, changeHandler) => {
    const hasWriteClaim = claimPrincipal.hasWriteClaim(claimName);
    const hasReadClaim = claimPrincipal.hasReadClaim(claimName);
    if (!hasReadClaim)
        return;
    else {
        const isDisabled = !hasWriteClaim;
        return renderInputForm(name, label, inputValue, isDisabled, isRequired, changeHandler);
    }
}

export const renderClaimBaseSelectionForm = (claimPrincipal, claimName, name, label, items, selectedKey, isRequired, changeHandler) => {
    const hasWriteClaim = claimPrincipal.hasWriteClaim(claimName);
    const hasReadClaim = claimPrincipal.hasReadClaim(claimName);
    if (!hasReadClaim)
        return;
    else {
        const isDisabled = !hasWriteClaim;
        return renderSelectionForm(name, label, items, selectedKey, isDisabled, isRequired, changeHandler);
    }
}


export const capitalizeFirstChar = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}