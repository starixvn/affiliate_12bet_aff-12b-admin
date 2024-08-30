import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm, capitalizeFirstChar, renderClaimBaseInputForm, renderStaticForm } from '../../libs/service';
import { setUserAccountInfo } from '../../actions';
import { updateObjectInArray } from '../../../../../modules/dataHandler';
import { claimResource } from '../../../../../modules/claim';

export class Personal extends PureComponent {
    constructor(props) {
        super(props);

        const { userAccountInfo } = this.props;

        this.arrayContactApps = userAccountInfo.ContactAppList;
    }

    handleChange = (event) => {
        const target = event.target;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: target.value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    handleChangeGender = (isMale) => {
        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { Gender: isMale });

        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    handleChangeContactApps = (event) => {
        const { name, value } = event.target
        const indexItem = this.arrayContactApps.findIndex(item => item.Name === name);
        const updateItem = Object.assign({}, this.arrayContactApps[indexItem], { Value: value });
        this.arrayContactApps = updateObjectInArray(this.arrayContactApps, indexItem, updateItem)

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { ContactAppList: this.arrayContactApps })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    _renderGenderForm(isMale, isDisabled) {
        return (
            <FormGroup row>
                <Label for={name} sm={3} xl={2} className="required">Gender</Label>
                <Col sm={9} xl={10}>
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input type="radio" checked={isMale != null && isMale} disabled={isDisabled} onChange={() => this.handleChangeGender(true)} />{' '} Male
                        </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input type="radio" checked={isMale != null && !isMale} disabled={isDisabled} onChange={() => this.handleChangeGender(false)} />{' '} Female
                        </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        )
    }

    render() {
        const { userAccountInfo, userClaims } = this.props;
        const { hasWriteEditAffClaim } = userClaims;

        return (
            <Form tag="div">
                {renderStaticForm('username', 'User Name', userAccountInfo.AffName, true)}
                {renderInputForm('firstName', 'First Name', userAccountInfo.FirstName, !hasWriteEditAffClaim, true, this.handleChange)}
                {renderInputForm('lastName', 'Last Name', userAccountInfo.LastName, !hasWriteEditAffClaim, true, this.handleChange)}
                {this._renderGenderForm(userAccountInfo.Gender, !hasWriteEditAffClaim)}
                {renderClaimBaseInputForm(userClaims, claimResource.AffEmail, 'email', 'Email Address', userAccountInfo.Email, true, this.handleChange)}
                {renderClaimBaseInputForm(userClaims, claimResource.AffPhone, 'phone', 'Contact Number', userAccountInfo.Phone, true, this.handleChange)}
                {
                    this.arrayContactApps && this.arrayContactApps.map(contactApp => {
                        return (
                            renderInputForm(contactApp.Name, contactApp.Name, contactApp.Value, !hasWriteEditAffClaim, this.arrayContactApps.length === 1, this.handleChangeContactApps)
                        )
                    })
                }
            </Form>
        )
    }
}

function mapStateToProps({ edit, claimsPrincipal }) {
    const { userAccountInfo, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim } = edit;
    const userClaims = { claimsPrincipal, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim }

    return Object.assign({}, { userAccountInfo, userClaims });
}

function mapDispatchToProp(dispatch) {
    return bindActionCreators({ setUserAccountInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProp)(Personal);