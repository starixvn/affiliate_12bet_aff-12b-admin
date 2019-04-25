import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm, capitalizeFirstChar } from '../../modules/service';
import { setUserAccountInfo } from '../../actions';

export class Personal extends PureComponent {
    constructor(props) {
        super(props);

        const { userAccountInfo } = this.props;
        this.arrayContactApps = JSON.parse(userAccountInfo && userAccountInfo.ContactApps);
    }

    handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'radio' ? (target.value === '1' ? true : false) : target.value;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    handleChangeContactApps = (event) => {
        this.arrayContactApps[0] = Object.assign({}, this.arrayContactApps[0], { Value: event.target.value });
        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { ContactApps: JSON.stringify(this.arrayContactApps) })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    _renderGenderForm(isMale) {
        return (
            <FormGroup row>
                <Label for={name} sm={3} xl={2} className="required">Gender</Label>
                <Col sm={9} xl={10}>
                    <FormGroup check className="d-inline-block mr-3">
                        <Label check>
                            <Input type="radio" name="gender" value="1" checked={isMale != null && isMale} onChange={this.handleChange} />{' '} Male
                            </Label>
                    </FormGroup>
                    <FormGroup check className="d-inline-block">
                        <Label check>
                            <Input type="radio" name="gender" value="0" checked={isMale != null && !isMale} onChange={this.handleChange} />{' '} Female
                            </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        )
    }

    render() {
        const { userAccountInfo } = this.props;

        return userAccountInfo && (
            <Form tag="div">
                {renderInputForm('username', 'User Name', userAccountInfo.AffName, true)}
                {renderInputForm('firstName', 'First Name', userAccountInfo.FirstName, false, true, this.handleChange)}
                {renderInputForm('lastName', 'Last Name', userAccountInfo.LastName, false, true, this.handleChange)}
                {this._renderGenderForm(userAccountInfo.Gender)}
                {renderInputForm('email', 'Email Address', userAccountInfo.Email, false, true, this.handleChange)}
                {renderInputForm('phone', 'Contact Number', userAccountInfo.Phone, false, true, this.handleChange)}
                {this.arrayContactApps && this.arrayContactApps[0] && renderInputForm('contactApp', this.arrayContactApps[0].Name, this.arrayContactApps[0].Value, false, false, this.handleChangeContactApps)}
            </Form>
        )
    }
}

function mapStateToProps({ userPanel }) {
    const { userAccountInfo } = userPanel;
    return Object.assign({}, { userAccountInfo });
}

function mapDispatchToProp(dispatch) {
    return bindActionCreators({ setUserAccountInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProp)(Personal);