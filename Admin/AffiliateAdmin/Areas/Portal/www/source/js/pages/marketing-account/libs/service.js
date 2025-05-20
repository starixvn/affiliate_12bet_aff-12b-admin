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

export const extractPersonalInfo = (data) => ({
    firstName: data.FirstName,
    lastName: data.LastName,
    gender: data.Gender || false,
    email: data.Email,
    phone: data.Phone,
});

export const extractAdditionalInfo = (data) => ({
    address: data.Address,
    city: data.City,
    state: data.State,
    postcode: data.Postcode,
    country: data.Country,
    question: data.Question,
    answer: data.Answer,
    contactAppList: data.ContactAppList,
});

export const extractPaymentInfo = (data) => ({
    preferredPayment: data.PreferredPayment,
    playerUsername: data.PlayerUsername,
    moneybookersEmail: data.MoneybookersEmail,
    bankName: data.BankName,
    bankAddress: data.BankAddress,
    bankCode: data.BankCode,
    bankAccountName: data.BankAccountName,
    bankAccountNumber: data.BankAccountNumber,
});

export const extractWebsiteInfo = (data) => ({
    siteUrl: data.SiteUrl,
    moreSiteUrls: !data.MoreSiteUrls ? [] : data.MoreSiteUrls.split(';'),
    noSiteDescription: data.NoSiteDescription,
    hasWebSite: data.HasWebSite,
});