import React, { PureComponent } from 'react';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';
import { regExp, messages, initialState, roleList, accountStatusList } from '../libs/constants';
import { post } from '../../../../modules/ajaxRequest';
import { resolveClientUrl } from '../../../../modules/utils';

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

    _handleCancel = () => {
        window.location = resolveClientUrl("Portal/AdminRole");
    }

    _validateNewPassword(newPassword) {
        const result = {
            isValid: false,
            message: messages.RequiredNewPassword
        }

        if (newPassword) {
            const regexCheckPassword = new RegExp(regExp.CheckPassword);
            const isValid = regexCheckPassword.test(newPassword);

            result.isValid = isValid;
            result.message = isValid ? "" : messages.PasswordIncorrect;
        }

        return result;
    }

    _validateConfirmPassword(newPassword, confirmPassword) {
        const result = {
            isValid: false,
            message: messages.RequiredConfirmPassword
        }

        if (confirmPassword) {
            const isValid = confirmPassword === newPassword;

            result.isValid = isValid;
            result.message = isValid ? "" : messages.ConfirmPasswordIncorrect;
        }

        return result;
    }

    render() {
        const { isClickedSubmit, userName, newPassword, confirmPassword, role, accountStatus, isShowMessage, isSuccess, message, isInsert } = this.state;

        const newPasswordValidation = this._validateNewPassword(newPassword);
        const confirmPasswordValidation = this._validateConfirmPassword(newPassword, confirmPassword);
        const isValid = newPasswordValidation.isValid && confirmPasswordValidation.isValid;
        const isValidUserName = false;

        return (
            <Container fluid={true} className="admin-info-container">
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
                <Form tag="form" className="my-4" autocomplete="off">
                    <FormGroup row className="mb-2">
                        <Label for="userName" sm={2} className="required">User Name</Label>
                        <Col sm={6}>
                            <Input type="text" className="input-required mt-1" name="userName" id="userName"
                                value={userName} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !isValidUserName} />
                            {!isValidUserName &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: messages.RequiredUserName }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label for="newPassword" sm={2} className="required">New Password</Label>
                        <Col sm={6}>
                            <Input type="password" className="input-required mt-1" name="newPassword" id="newPassword" autocomplete="false"
                                value={newPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !newPasswordValidation.isValid} />
                            {newPasswordValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: newPasswordValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>

                    <FormGroup row className="mb-2">
                        <Label for="confirmPassword" sm={2} className="required">Confirm Password</Label>
                        <Col sm={6}>
                            <Input type="password" className="input-required mt-1" name="confirmPassword" id="confirmPassword" autocomplete="false"
                                value={confirmPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !confirmPasswordValidation.isValid} />
                            {confirmPasswordValidation.message &&
                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.message }} /></FormFeedback>}
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label for="role" sm={2} className="required">Role</Label>
                        <Col sm={6}>
                            <Input type="select" name="role" className="mt-2" value={role} onChange={this._handleChange}>
                                {
                                    isInsert && <option key={'role-0'} value="0">[Select Role]</option>
                                }
                                {
                                    Object.keys(roleList).map(key => {
                                        return (<option key={'role-' + key} value={key}>{roleList[key]}</option>)
                                    })
                                }
                            </Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label for="accountStatus" sm={2} className="pt-3">Account Status</Label>
                        <Col sm={6}>
                            <Input type="select" name="accountStatus" className="mt-2" value={accountStatus} onChange={this._handleChange}>
                                {
                                    Object.keys(accountStatusList).map(key => {
                                        return (<option key={'status-' + key} value={key}>{accountStatusList[key]}</option>)
                                    })
                                }
                            </Input>
                        </Col>
                    </FormGroup>
                </Form>
                <Row>
                    <Col sm={{ size: 'auto', offset: 2 }}>
                        <Button type="button" color="success" className="mr-3" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)}>Save</Button>
                        <Button type="button" color="info" onClick={this._handleCancel}>Cancel</Button>
                    </Col>
                </Row>
            </Container>
        )
    }
}