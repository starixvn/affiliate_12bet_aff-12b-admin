import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';
import { regExp, messages, initialState, roleList } from '../libs/constants';
import { post } from '../../../../modules/ajaxRequest';
import { resolveClientUrl } from '../../../../modules/utils';
import { fetchData, createUser, updateUser, deleteUser, toggleLoader } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = { ...initialState }
    }

    componentDidMount() {
        const { userName } = this.props;

        userName && this.props.fetchData(userName);
    }

    componentDidUpdate(prevProps) {
        const { userName } = this.props;
        if (userName && userName !== prevProps.userName) {
            this.props.fetchData(userName);
        }
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
        }, () => window.setTimeout(() => this._hideMessage(), 3000));
    }

    _hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    _handlePressEnter = (isValid, event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit(isValid);
        }
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
        const { userName } = this.props;
        const isCreateNew = !userName;
        const { isClickedSubmit, newUserName, newPassword, confirmPassword, role, isActive, isShowMessage, isSuccess, message, isEdit } = this.state;

        const newPasswordValidation = this._validateNewPassword(newPassword);
        const confirmPasswordValidation = this._validateConfirmPassword(newPassword, confirmPassword);
        const isValid = newPasswordValidation.isValid && confirmPasswordValidation.isValid;
        const isValidUserName = false;

        return (
            <Fragment>
                <div className="my-2">
                    {
                        isCreateNew || isEdit ? (
                            <Fragment>
                                <Button outline type="button" color="danger" onClick={() => this.setState({ isEdit: false })}>Cancel<span className="oi oi-x ml-2"></span></Button>
                                <Button outline type="button" className="ml-2" onClick={() => this.setState({ isEdit: false })}>Save<span className="oi oi-task ml-2"></span></Button>
                            </Fragment>
                        ) : (
                                <Fragment>
                                    <Button outline type="button" color="danger">Delete<span className="oi oi-trash ml-2"></span></Button>
                                    <Button outline type="button" className="ml-2" onClick={() => this.setState({ isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                                </Fragment>
                            )
                    }
                </div>
                <Container fluid={true} className="admin-info-container">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    {
                        isEdit || isCreateNew ? (
                            <Form tag="form" className="my-4" autocomplete="off">
                                <FormGroup row className="mb-1">
                                    <Label for="newUserName" sm="auto" className="required">User Name</Label>
                                    <Col sm={2}>
                                        <Input type="text" className="input-required mt-1" name="newUserName" id="newUserName"
                                            value={newUserName} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !isValidUserName} />
                                        {!isValidUserName &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: messages.RequiredUserName }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label for="newPassword" sm="auto" className="required">New Password</Label>
                                    <Col sm={2}>
                                        <Input type="password" className="input-required mt-1" name="newPassword" id="newPassword" autocomplete="false"
                                            value={newPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !newPasswordValidation.isValid} />
                                        {newPasswordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: newPasswordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>

                                <FormGroup row className="mb-1">
                                    <Label for="confirmPassword" sm="auto" className="required">Confirm Password</Label>
                                    <Col sm={2}>
                                        <Input type="password" className="input-required mt-1" name="confirmPassword" id="confirmPassword" autocomplete="false"
                                            value={confirmPassword} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !confirmPasswordValidation.isValid} />
                                        {confirmPasswordValidation.message &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: confirmPasswordValidation.message }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label for="role" sm="auto" className="required">Role</Label>
                                    <Col sm={2}>
                                        {
                                            <Input type="select" name="role" className="mt-1" value={role} onChange={this._handleChange}>
                                                {
                                                    isCreateNew && <option key={'role-0'} value="0">[Select Role]</option>
                                                }
                                                {
                                                    Object.keys(roleList).map(key => {
                                                        return (<option key={'role-' + key} value={key}>{roleList[key]}</option>)
                                                    })
                                                }
                                            </Input>
                                        }
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label for="isActive" sm="auto" className="pt-2">Account Status</Label>
                                    <Col sm={2}>
                                        <input type="checkbox" name="isActive" className="mt-2" checked={isActive} onChange={() => this.setState({ isActive: !isActive })} />
                                    </Col>
                                </FormGroup>
                            </Form>
                        ) : (
                                <Form tag="form" className="my-4">
                                    <FormGroup row className="mb-1">
                                        <Label sm="auto" className="required">User Name</Label>
                                        <Col sm={2}>
                                            <Input type="text" className="input-required mt-2" disabled value={newUserName} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-1">
                                        <Label sm="auto" className="required">Role</Label>
                                        <Col sm={2}>
                                            <Input type="text" className="input-required mt-2" disabled value={role} />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row className="mb-1">
                                        <Label sm="auto" className="pt-2">Account Status</Label>
                                        <Col sm={2}>
                                            <Label className="pt-2">{isActive ? 'Active' : 'Closed'}</Label>
                                        </Col>
                                    </FormGroup>
                                </Form >
                            )
                    }
                    {/* <Row>
                        <Col sm={{ size: 'auto', offset: 2 }}>
                            <Button type="button" color="success" className="mr-3" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)}>Save</Button>
                            <Button type="button" color="info" onClick={this._handleCancel}>Cancel</Button>
                        </Col>
                    </Row> */}
                </Container>
            </Fragment>
        )
    }
}

function mapStateToProps({ userName, userInfo }) {
    return Object.assign({}, { userName, userInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, createUser, updateUser, deleteUser, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);