import React, { PureComponent } from 'react';
import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';
import { messages, initialState } from '../libs/constants';
import { post } from '../../../modules/ajaxRequest';
import { validateNewPassword } from '../../../modules/utils';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = { ...initialState }
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _handleSubmit = async (isValid) => {
        this.setState({ isClickedSubmit: true });

        if (isValid) {
            const { oldPassword, newPassword, confirmPassword } = this.state;
            const param = { oldPassword, newPassword, confirmPassword };
            const response = await post('Portal/ChangeAdminPassword/ChangePassword', param);

            if (response) {
                const { data } = response;

                this._handleShowAlert(data.IsSuccessful, data.IsSuccessful ? messages.ChangePasswordSuccess : data.Message);
            } else {
                this._handleShowAlert(false, messages.ChangePasswordFailed);
            }
        }
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage(isSuccess), 3000));
    }

    _hideMessage = (isSuccess) => {
        this.setState({
            isShowMessage: false
        });

        isSuccess && this._handleReset();
    }

    _handlePressEnter = (isValid, event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit(isValid);
        }
    }

    _handleReset = () => {
        this.setState({ ...initialState });
    }

    _validatePassword(newPassword, oldPassword) {
        if (newPassword && newPassword == oldPassword) {
            return {
                IsValid: false,
                Message: "New password must be different from old!"
            }
        }

        return validateNewPassword(newPassword);
    }

    _validateConfirmPassword(newPassword, confirmPassword) {
        const result = {
            IsValid: false,
            Message: messages.RequiredConfirmPassword
        }

        if (confirmPassword) {
            const isValid = confirmPassword === newPassword;

            result.IsValid = isValid;
            result.Message = isValid ? "" : messages.ConfirmPasswordIncorrect;
        }

        return result;
    }

    render() {
        const { isClickedSubmit, oldPassword, newPassword, confirmPassword, isShowMessage, isSuccess, message } = this.state;

        const newPasswordValidation = this._validatePassword(newPassword, oldPassword);
        const confirmPasswordValidation = this._validateConfirmPassword(newPassword, confirmPassword);
        const isValidOldPassword = !!oldPassword;
        const isValid = isValidOldPassword && newPasswordValidation.IsValid && confirmPasswordValidation.IsValid;

        return (
            <div className="change-admin-password-container">
                <fieldset>
                    <legend><h6 className="font-weight-bold">Change Password</h6></legend>
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Form tag="form" className="my-2" autoComplete="off">
                        <FormGroup row className="mb-2">
                            <Label for="oldPassword" sm={4} className="required text-right">Old Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required mt-1" name="oldPassword" id="oldPassword" autoComplete="false"
                                    value={oldPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !isValidOldPassword} />
                                {!isValidOldPassword &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: messages.RequiredOldPassword }} /></FormFeedback>}
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-2">
                            <Label for="newPassword" sm={4} className="required text-right">New Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required mt-1" name="newPassword" id="newPassword" autoComplete="false"
                                    value={newPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !newPasswordValidation.IsValid} />
                                {newPasswordValidation.Message &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: newPasswordValidation.Message }} /></FormFeedback>}
                            </Col>
                        </FormGroup>

                        <FormGroup row className="mb-2">
                            <Label for="confirmPassword" sm={4} className="required text-right">Confirm Password</Label>
                            <Col sm={8}>
                                <Input type="password" className="input-required mt-1" name="confirmPassword" id="confirmPassword" autoComplete="false"
                                    value={confirmPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !confirmPasswordValidation.IsValid} />
                                {confirmPasswordValidation.Message &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.Message }} /></FormFeedback>}
                            </Col>
                        </FormGroup>
                    </Form>
                </fieldset>

                <Button type="button" color="success" className="mr-2" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)}>Submit</Button>
                <Button type="button" color="info" onClick={this._handleReset}>Reset</Button>
            </div>
        )
    }
}