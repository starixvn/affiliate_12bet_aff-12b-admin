import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';
import { regExp, messages, initialState } from '../libs/constants';
import { post } from '../../../../modules/ajaxRequest';
import { validateUserName } from '../../../../modules/utils';
import { fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminUsers, redirectToCreateNew } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = { ...initialState }
    }

    componentDidMount() {
        const { userId, roleList } = this.props;
        userId && this._handleFetchData(userId);

        !roleList && this.props.getRoleList();
    }

    componentDidUpdate(prevProps) {
        const { userId } = this.props;
        if (userId && userId !== prevProps.userId) {
            this._handleFetchData(userId);
        }
    }

    _handleFetchData = async (userId) => {
        this.props.toggleLoader(true);

        const userInfo = await this.props.fetchData(userId);

        if (userInfo) {
            this.setState({
                newUserName: userInfo.UserName,
                role: userInfo.RoleId,
                isActive: userInfo.IsActive,
                userInfo
            });
        } else {
            this.props.redirectToCreateNew();
            this._handleShowAlert(false, "An error occurred, please try again later!");
        }

        this.props.toggleLoader(false);
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

    _handleCancel = (isCreateNew) => {
        if (isCreateNew) {
            this.props.redirectToAdminUsers();
        } else {
            const { userInfo } = this.setState
            this.setState({ isEdit: false, isClickedSubmit: false });
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

    _renderWhenView() {
        const { roleList } = this.props;
        const { newUserName, role, isActive, isShowMessage, isSuccess, message } = this.state;

        return roleList && (
            <Fragment>
                <div className="mb-2">
                    <Button outline type="button" onClick={() => this.setState({ isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                </div>
                <Container fluid={true} className="admin-info-container pt-2">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
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
                                <Input type="text" className="input-required mt-2" disabled value={roleList[role]} />
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label sm="auto" className="pt-2">Account Status</Label>
                            <Col sm={2}>
                                <Label className="pt-2">{isActive ? 'Active' : 'Closed'}</Label>
                            </Col>
                        </FormGroup>
                    </Form >
                </Container>
            </Fragment>
        )
    }

    _renderWhenCreateAndEdit() {
        const { userName, roleList } = this.props;
        const isCreateNew = !userName;
        const { isClickedSubmit, newUserName, newPassword, confirmPassword, role, isActive, isShowMessage, isSuccess, message } = this.state;

        const newPasswordValidation = isClickedSubmit && this._validateNewPassword(newPassword);
        const confirmPasswordValidation = isClickedSubmit && this._validateConfirmPassword(newPassword, confirmPassword);
        const isValid = newPasswordValidation.isValid && confirmPasswordValidation.isValid;
        const validUserNameResult = isClickedSubmit && validateUserName(newUserName);

        return (
            <Fragment>
                <div className="mb-2">
                    <Fragment>
                        <Button outline type="button" color="danger" onClick={() => this._handleCancel(isCreateNew)}>Cancel<span className="oi oi-x ml-2"></span></Button>
                        <Button outline type="button" className="ml-2" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)}>Save<span className="oi oi-task ml-2"></span></Button>
                    </Fragment>
                </div>
                <Container fluid={true} className="admin-info-container pt-2">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Form tag="form" className="my-4" autocomplete="off">
                        <FormGroup row className="mb-1">
                            <Label for="newUserName" sm="auto" className="required">User Name</Label>
                            <Col sm={2}>
                                <Input type="text" className="input-required mt-1" name="newUserName" id="newUserName"
                                    value={newUserName} onKeyPress={(e) => this._handlePressEnter(isValid, e)} onChange={this._handleChange} invalid={isClickedSubmit && !validUserNameResult.IsValid} />
                                {!validUserNameResult.IsValid &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: validUserNameResult.Message }} /></FormFeedback>}
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
                                <Input type="select" name="role" className="mt-1" value={role} onChange={this._handleChange} invalid={isClickedSubmit && !role}>
                                    {
                                        isCreateNew && <option key={'role-0'} value="0">[Select Role]</option>
                                    }
                                    {
                                        roleList && Object.keys(roleList).map(key => {
                                            return (<option key={'role-' + key} value={key}>{roleList[key]}</option>)
                                        })
                                    }
                                </Input>
                                {
                                    !role &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: messages.RequiredRole }} /></FormFeedback>
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
                </Container>
            </Fragment>
        )
    }

    render() {
        const { userName } = this.props;
        const isCreateNew = !userName;
        const { isEdit } = this.state;

        return (isCreateNew || isEdit ? this._renderWhenCreateAndEdit() : this._renderWhenView())
    }
}

function mapStateToProps({ userId, userName, adminInfo }) {
    const { roleList } = adminInfo;
    return Object.assign({}, { userId, userName, roleList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminUsers, redirectToCreateNew }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);