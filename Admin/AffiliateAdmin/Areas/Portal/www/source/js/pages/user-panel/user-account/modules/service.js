import React from 'react';
import { Col, FormGroup, Label, Input } from 'reactstrap';

export const renderInputForm = (name, label, inputValue, isStatic, isRequired, changeHandler) => {
    return (
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2} className={isRequired ? "required" : ""}>{label}</Label>
            <Col sm={9} xl={10}>
                { isStatic ? 
                    (<p className="form-control-plaintext">{inputValue}</p>) :
                    (<Input type="text" id={name} name={name} value={inputValue} onChange={changeHandler} />)
                }
            </Col>
        </FormGroup>
    )
}

export const renderSelectionForm = (name, label, items, selectedKey, isStatic, isRequired, changeHandler) => {
    console.log(selectedKey);
    return(
        <FormGroup row key={name}>
            <Label for={name} sm={3} xl={2} className={isRequired ? "required" : ""}>{label}</Label>
            <Col sm={9} xl={10}>
                { isStatic ? 
                    (<p className="form-control-plaintext">{items[selectedKey]}</p>) :
                    <Input type="select" id={name} name={name} value={selectedKey} key="key" onChange={changeHandler}>
                        { 
                            selectedKey === 0 ? <option key={name + '-0'} value='0' selected='true'>Select a secret question</option>
                            : <option key={name + '-0'} value='0'>Select a secret question</option>}
                        { items && items.map((item) => {       
                            return item.Key===selectedKey 
                            ? <option key={name + '-' + item.Key} value={item.Key} selected='true'>{item.Value}</option>
                            : <option key={name + '-' + item.Key} value={item.Key}>{item.Value}</option>                           
                        })}
                    </Input>
                }
            </Col>
        </FormGroup>
    )
}

export const renderClaimBaseInputForm = (claimPrincipal, claimName, name, label, inputValue, isRequired, changeHandler) => {
    const hasWriteClaim = claimPrincipal.hasWriteClaim(claimName);
    const hasReadClaim = claimPrincipal.hasReadClaim(claimName);
    if(!hasReadClaim)
        return;
    else {
        const isStatic = !hasWriteClaim;
        return renderInputForm(name, label, inputValue, isStatic, isRequired, changeHandler);
    }
}

export const renderClaimBaseSelectionForm = (claimPrincipal, claimName, name, label, items, selectedKey, isRequired, changeHandler) => {
    const hasWriteClaim = claimPrincipal.hasWriteClaim(claimName);
    const hasReadClaim = claimPrincipal.hasReadClaim(claimName);
    console.log(items);
    window.items = items;
    if(!hasReadClaim)
        return;
    else {
        const isStatic = !hasWriteClaim;
        return renderSelectionForm(name, label, items, selectedKey, isStatic, isRequired, changeHandler);
    }
}


export const capitalizeFirstChar = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}