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

export const renderSelectionForm = (name, label, items, selectedKey, isDisabled, isRequired, changeHandler, defaultOption) => {
    const selectOption = "Select a secret question";
    return (
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2} className={isRequired ? "required" : ""}>{label}</Label>
            <Col sm={9} xl={10}>
                <Input type="select" id={name} name={name} value={selectedKey} key="name" disabled={isDisabled} onChange={changeHandler}>
                    <option key={name + '-0'} defaultValue value=''>{selectOption}</option>

                    {/* Fix bug that old secret question is not exist in system so still show old secret question whether user haven't seletcted new one yet*/
                        !selectedKey && !!defaultOption && <option key={name + '-default'} defaultValue value=''>{defaultOption}</option>
                    }

                    { items && items.map((item) => {
                        return item.Key === selectedKey
                            ? <option key={name + '-' + item.Key} value={item.Key} defaultValue>{item.Value}</option>
                            : <option key={name + '-' + item.Key} value={item.Key}>{item.Value}</option>
                    })}
                </Input>
            </Col>
        </FormGroup>
    )
}

export const renderClaimBaseInputForm = (userClaims, claimName, name, label, inputValue, isRequired, changeHandler) => {
    const { claimsPrincipal, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim } = userClaims;
    const hasReadClaim = claimsPrincipal.hasReadClaim(claimName);
    const hasWriteClaim = claimsPrincipal.hasWriteClaim(claimName);

    if (!hasReadClaim && !hasReadManagePendingAffClaim)
        return null;
    else {
        const isDisabled = !(hasWriteClaim && hasWriteEditAffClaim) && !hasWriteManagePendingAffClaim;

        return renderInputForm(name, label, inputValue, isDisabled, isRequired, changeHandler);
    }
}

export const renderClaimBaseSelectionForm = (userClaims, claimName, name, label, items, selectedKey, isRequired, changeHandler, defaultOption) => {
    const { claimsPrincipal, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim } = userClaims;
    const hasReadClaim = claimsPrincipal.hasReadClaim(claimName);
    const hasWriteClaim = claimsPrincipal.hasWriteClaim(claimName);

    if (!hasReadClaim && !hasReadManagePendingAffClaim)
        return null;
    else {
        const isDisabled = !(hasWriteClaim && hasWriteEditAffClaim) && !hasWriteManagePendingAffClaim;

        return renderSelectionForm(name, label, items, selectedKey, isDisabled, isRequired, changeHandler, defaultOption);
    }
}


export const capitalizeFirstChar = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}