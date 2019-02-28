import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { resource, regExp } from '../modules/constants';
import { setShowPopupPassword } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';

export class ChangePasswordForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            password: '',
            confirmPassword: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleChangePassword = () => {
        const { affId } = this.props;
        const { password, confirmPassword } = this.state;

        const param = {
            AffId: affId,
            Password: password,
            ConfirmPassword: confirmPassword
        }

        this.saveChangePassword(param)
    }

    handleClosePopup = () => {
        this.props.setShowPopupPassword(false);
        this.setState({ password: '', confirmPassword: '' });
    }

    async saveChangePassword(param) {
        this.props.toggleLoader(true);

        const url = `Portal/AffiliateInfo/ChangePassword`;
        const response = await post(url, param);

        if (response) {
            if (response.data.IsSuccessful) {
                alert(resource.ChangePasswordSuccess);

                this.props.setShowPopupPassword(false);

                this.setState({
                    password: '',
                    confirmPassword: ''
                })
            }
            else {
                alert(response.data.Message);
            }
        }

        this.props.toggleLoader(false);
    }

    _validatePassword(password) {
        const result = {
            isValid: false,
            message: resource.RequiredPassword
        }

        if (password) {
            const regexCheckPassword = new RegExp(regExp.CheckPassword);
            const isValid = regexCheckPassword.test(password);

            result.isValid = isValid;
            result.message = isValid ? "" : resource.PasswordErrorMessage;
        }

        return result;
    }

    _validateConfirmPassword(password, confirmPassword) {
        const result = {
            isValid: false,
            message: resource.RequiredConfirmPassword
        }

        if (confirmPassword) {
            const isValid = confirmPassword === password;

            result.isValid = isValid;
            result.message = isValid ? "" : resource.ConfirmPasswordErrorMessage;
        }

        return result;
    }

    render() {
        const { showPopupPassword, affId } = this.props;
        const { password, confirmPassword } = this.state;

        const passwordValidation = this._validatePassword(password);
        const confirmPasswordValidation = this._validateConfirmPassword(password, confirmPassword);
        const isValid = passwordValidation.isValid && confirmPasswordValidation.isValid;

        return (
            <Modal isOpen={showPopupPassword} toggle={this.handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this.handleClosePopup}><span className="iconpopup"></span>{resource.ChangePassword}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="change-password-form-container">
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="Password" sm={4} className="required">Password:</Label>
                                    <Col sm={8}>
                                        <Input type="password" name="password" id="Password" autocomplete="false"
                                            value={password} onChange={this.handleChange} invalid={!passwordValidation.isValid} />
                                        {passwordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: passwordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>

                                <FormGroup row className="mb-2">
                                    <Label for="ConfirmPassword" sm={4} className="required">Confirm Password:</Label>
                                    <Col sm={8}>
                                        <Input type="password" name="confirmPassword" id="ConfirmPassword" autocomplete="false"
                                            value={confirmPassword} onChange={this.handleChange} invalid={!confirmPasswordValidation.isValid} />
                                        {confirmPasswordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={!isValid} onClick={this.handleChangePassword}>{resource.Submit}</Button>
                            <Button type="button" color="info" onClick={this.handleClosePopup}>{resource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ showPopupPassword, affId }) {
    return Object.assign({}, { showPopupPassword, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupPassword, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);