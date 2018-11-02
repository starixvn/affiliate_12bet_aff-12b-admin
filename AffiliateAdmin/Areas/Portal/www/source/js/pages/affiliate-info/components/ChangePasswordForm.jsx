import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { changePasswordText } from '../modules/constants';
import { setToggleModal } from '../actions';
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

    handleChangePassword = (affId, password) => {
        const param = {
            AffId: affId,
            Password: password
        }

        this.saveChangePassword(param)
    }

    async saveChangePassword(param) {
        this.props.toggleLoader(true);

        const url = `Portal/AffiliateInfo/ChangePassword`;
        const response = await post(url, param);

        if (response) {
            if (response.data.IsSuccessful) {
                alert(changePasswordText.ChangePasswordSuccess);
                this.props.setToggleModal({
                    isToggleModal: false,
                    AffId: 0
                });
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
            message: changePasswordText.RequiredPassword
        }

        if (password) {
            const regexCheckPassword = new RegExp(changePasswordText.RegexCheckPassword);
            const isValid = regexCheckPassword.test(password);

            result.isValid = isValid;
            result.message = isValid ? "" : changePasswordText.PasswordErrorMessage;
        }

        return result;
    }

    _validateConfirmPassword(password, confirmPassword) {
        const result = {
            isValid: false,
            message: changePasswordText.RequiredConfirmPassword
        }

        if (confirmPassword) {
            const isValid = confirmPassword == password;

            result.isValid = isValid;
            result.message = isValid ? "" : changePasswordText.ConfirmPasswordErrorMessage;
        }

        return result;
    }

    render() {
        const { toggleModal } = this.props;
        const { password, confirmPassword } = this.state;

        const passwordValidation = this._validatePassword(password);
        const confirmPasswordValidation = this._validateConfirmPassword(password, confirmPassword);
        const isValid = passwordValidation.isValid && confirmPasswordValidation.isValid;

        return (
            <Modal isOpen={toggleModal.isToggleModal} toggle={() => this.props.setToggleModal(0)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setToggleModal(0)}><span className="iconpopup"></span>{changePasswordText.ChangePassword}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="change-password-form-container">
                            <Form tag="div" className="mb-5">
                                <FormGroup row className="mb-2">
                                    <Label for="Password" sm={5} className="required">Password:</Label>
                                    <Col sm={7}>
                                        <Input type="password" name="password" id="Password"
                                            value={password} onChange={this.handleChange} invalid={!passwordValidation.isValid} />
                                        {passwordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: passwordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>

                                <FormGroup row>
                                    <Label for="ConfirmPassword" sm={5} className="required">Confirm Password:</Label>
                                    <Col sm={7}>
                                        <Input type="password" name="confirmPassword" id="ConfirmPassword"
                                            value={confirmPassword} onChange={this.handleChange} invalid={!confirmPasswordValidation.isValid} />
                                        {confirmPasswordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={!isValid} onClick={() => this.handleChangePassword(toggleModal.AffId, password)}>{changePasswordText.Submit}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setToggleModal({ isToggleModal: false, AffId: 0 })}>{changePasswordText.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ toggleModal }) {
    return Object.assign({}, { toggleModal });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setToggleModal, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ChangePasswordForm);