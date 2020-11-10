import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm, capitalizeFirstChar, renderClaimBaseInputForm, renderStaticForm } from '../../modules/service';
import { setUserAccountInfo } from '../../actions';
import { updateObjectInArray } from '../../../../../modules/dataHandler';
import { claimResource } from '../../../../../modules/claim';

export class Personal extends PureComponent {
    constructor(props) {
        super(props);

        const { userAccountInfo } = this.props;

        this.arrayContactApps = userAccountInfo && userAccountInfo.ContactAppList;
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

    _renderGenderForm(isMale) {
        return (
            <FormGroup row>
                <Label for={name} sm={3} xl={2} className="required">Gender</Label>
                <Col sm={9} xl={10}>
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input type="radio" checked={isMale != null && isMale} onChange={() => this.handleChangeGender(true)} />{' '} Male
                        </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input type="radio" checked={isMale != null && !isMale} onChange={() => this.handleChangeGender(false)} />{' '} Female
                        </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        )
    }

    render() {
        const { userAccountInfo, claimsPrincipal } = this.props;

        return userAccountInfo && (
            <Form tag="div">
                {renderStaticForm('username', 'User Name', userAccountInfo.AffName, true)}
                {renderInputForm('firstName', 'First Name', userAccountInfo.FirstName, false, true, this.handleChange)}
                {renderInputForm('lastName', 'Last Name', userAccountInfo.LastName, false, true, this.handleChange)}
                {this._renderGenderForm(userAccountInfo.Gender)}
                {renderClaimBaseInputForm(claimsPrincipal, claimResource.Email, 'email', 'Email Address', userAccountInfo.Email, true, this.handleChange)}
                {renderClaimBaseInputForm(claimsPrincipal, claimResource.PhoneNumber, 'phone', 'Contact Number', userAccountInfo.Phone, true, this.handleChange)}
                {
                    this.arrayContactApps && this.arrayContactApps.map(contactApp => {
                        return (
                            renderInputForm(contactApp.Name, contactApp.Name, contactApp.Value, false, false, this.handleChangeContactApps)
                        )
                    })
                }
            </Form>
        )
    }
}

function mapStateToProps({ edit, claimsPrincipal }) {
    const { userAccountInfo } = edit;
    return Object.assign({}, { userAccountInfo, claimsPrincipal });
}

function mapDispatchToProp(dispatch) {
    return bindActionCreators({ setUserAccountInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProp)(Personal);