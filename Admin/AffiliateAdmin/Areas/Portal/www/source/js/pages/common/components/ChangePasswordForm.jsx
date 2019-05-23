import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { resource, regExp, messages } from '../modules/constants/changePassword';
import { setShowPopupPassword, updateChangePasswword } from '../actions/changePassword';
import { toggleLoader } from '../../../actions/loaderAction';
import { post } from '../../../modules/ajaxRequest';

export class ChangePasswordForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            password: '',
            confirmPassword: '',
            isShowMessage: false,
            isSuccess: true,
            message: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    handleSubmit = () => {
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

        const data = this.props.updateChangePasswword(param);

        if (data) {
            if (data.IsSuccessful) {
                window.setTimeout(this.handleClosePopup, 3000);
            }

            this.setState({
                isSuccess: data.IsSuccessful,
                isShowMessage: true,
                message: data.IsSuccessful ? messages.ChangePasswordSuccess : data.Message
            }, () => window.setTimeout(this.hideMessage, 3000));
        } else {
            this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: messages.ChangePasswordFailed
            }, () => window.setTimeout(this.hideMessage, 3000));
        }

        this.props.toggleLoader(false);
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    _validatePassword(password) {
        const result = {
            isValid: false,
            message: messages.RequiredPassword
        }

        if (password) {
            const regexCheckPassword = new RegExp(regExp.CheckPassword);
            const isValid = regexCheckPassword.test(password);

            result.isValid = isValid;
            result.message = isValid ? "" : messages.PasswordErrorMessage;
        }

        return result;
    }

    _validateConfirmPassword(password, confirmPassword) {
        const result = {
            isValid: false,
            message: messages.RequiredConfirmPassword
        }

        if (confirmPassword) {
            const isValid = confirmPassword === password;

            result.isValid = isValid;
            result.message = isValid ? "" : messages.ConfirmPasswordErrorMessage;
        }

        return result;
    }

    render() {
        const { showPopupPassword, affId } = this.props;
        const { password, confirmPassword, isShowMessage, isSuccess, message } = this.state;

        const passwordValidation = this._validatePassword(password);
        const confirmPasswordValidation = this._validateConfirmPassword(password, confirmPassword);
        const isValid = passwordValidation.isValid && confirmPasswordValidation.isValid;

        return (
            <Modal isOpen={showPopupPassword} toggle={this.handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this.handleClosePopup}><span className="iconpopup"></span>{resource.ChangePassword}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="change-password-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="Password" sm={4} className="required">{resource.Password}:</Label>
                                    <Col sm={8}>
                                        <Input type="password" name="password" id="Password" autocomplete="false"
                                            value={password} onChange={this.handleChange} invalid={!passwordValidation.isValid} />
                                        {passwordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: passwordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>

                                <FormGroup row className="mb-2">
                                    <Label for="ConfirmPassword" sm={4} className="required">{resource.ConfirmPassword}:</Label>
                                    <Col sm={8}>
                                        <Input type="password" name="confirmPassword" id="ConfirmPassword" autocomplete="false"
                                            value={confirmPassword} onChange={this.handleChange} invalid={!confirmPasswordValidation.isValid} />
                                        {confirmPasswordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={!isValid} onClick={this.handleSubmit}>{resource.Submit}</Button>
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
    return bindActionCreators({ setShowPopupPassword, toggleLoader, updateChangePasswword }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);