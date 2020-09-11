import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert } from 'reactstrap';
import { messages, initialState, resources } from '../libs/constants';
import { validateUserName, validateNewPassword } from '../../../../modules/utils';
import { globalResource } from '../../../../modules/globalResource';
import { fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminUsers, redirectToCreateNew, deleteUsers, updateStatus, setIsEdit } from '../actions';
import ChangePasswordForm from '../../../common/components/ChangePasswordForm';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

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
                firstName: userInfo.FirstName,
                lastName: userInfo.LastName,
                role: userInfo.RoleId,
                isActive: userInfo.IsActive,
                userInfo
            });
        } else {
            this.props.redirectToCreateNew();
            this._handleShowAlert(false, globalResource.AnError);
        }

        this.props.toggleLoader(false);
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _handleClickNext = () => {
        const isValidUserInfo = this._validateUserInfo();

        this.setState({ isClickedSubmit: true });

        isValidUserInfo && this.props.setIsInfomation(false);

    }

    _handleSubmit = async () => {
        const isValidUserInfo = this._validateUserInfo();

        this.setState({ isClickedSubmit: true });

        if (isValidUserInfo) {
            const { userId } = this.props;
            const { newUserName, newPassword, confirmPassword, role, isActive } = this.state;
            const param = { userId: userId || 0, userName: newUserName, password: newPassword, confirmPassword, role, isActive };
            const data = userId ? await this.props.updateUser(param) : await this.props.createUser(param);

            if (data) {
                this._handleShowAlert(data.IsSuccessful, data.IsSuccessful ? (userId ? "Update user is successfully!" : "Create new user is successfully!") : data.Message);
            } else {
                this._handleShowAlert(false, userId ? "Update user is failed!" : "Create new user is failed!");
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

        isSuccess && this.props.redirectToAdminUsers();
    }

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit(isValid);
        }
    }

    _handleCancel = (isCreateNew) => {
        if (isCreateNew) {
            this.props.redirectToAdminUsers();
        } else {
            const { userInfo } = this.state;

            this.setState({
                isClickedSubmit: false,
                newUserName: userInfo.UserName,
                firstName: userInfo.FirstName,
                lastName: userInfo.LastName,
                role: userInfo.RoleId,
                isActive: userInfo.IsActive,
            });
        }
    }

    _toggleChangingPassword = (isChanging) => {
        this.setState({ isChangingPassword: isChanging });
    }

    _handleDeleteUser = () => {
        this.setState({ messagePopup: 'Are you sure you want to delete this user?', isConfirmPopup: true });
    }

    _handleChangeStatus = (isActive) => {
        this.setState({ messagePopup: 'Are you sure you want to update the status of this user?', isConfirmPopup: true, isActive });
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messagePopup: '' });

        const { userId } = this.props;
        const { isDeleteUser, isActive } = this.setState;
        if (confirmResult && userId && isDeleteUser) {
            this._deleteUser(userId);
        }
        if (confirmResult && userId && !isDeleteUser) {
            this._updateStatus(userId, !isActive);
        }

        if (!confirmResult && isDeleteUser) {
            this.props.redirectToAdminUsers();
        }
    }

    _deleteUser = async (userId) => {
        this.props.toggleLoader(true);

        const result = await this.props.deleteUsers(userId);

        if (result) {
            const messagePopup = result.IsSuccessful ? 'User has been deleted successfully!' : result.Message;

            this.setState({ messagePopup, isConfirmPopup: false });
        } else {
            this.setState({ isDeleteUser: false, messagePopup: globalResource.AnError, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _updateStatus = async (userId, isActive) => {
        this.props.toggleLoader(true);

        const result = await this.props.updateStatus({ userId, isActive });

        if (result) {
            const messagePopup = result.IsSuccessful ? "User's status has been update successfully!" : result.Message;

            result.IsSuccessful && this.setState({ isActive });
            this.setState({ messagePopup, isConfirmPopup: false });
        } else {
            this.setState({ messagePopup: globalResource.AnError, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _renderWhenView() {
        const { roleList, userId } = this.props;
        const { newUserName, firstName, lastName, role, isActive, isShowMessage, isSuccess, message,
            isChangingPassword, messagePopup, isConfirmPopup } = this.state;

        return (
            <Fragment>
                <div className="mb-2">
                    <Button outline type="button" color="danger" onClick={this._handleDeleteUser}> Delete < span className="oi oi-trash ml-2"></span></Button>
                    <Button outline type="button" className="ml-2" onClick={() => this.props.setIsEdit(true)}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                </div>
                <Container fluid={true} className="admin-info-container pt-2">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Form tag="form" className="my-4">
                        <FormGroup row className="mb-1">
                            <Label sm="auto">{resources.UserName}</Label>
                            <Col sm={2}>
                                <Label sm="auto">{"AA" + newUserName}</Label>
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label sm="auto">{resources.FirstName}</Label>
                            <Col sm={2}>
                                <Label sm="auto">{firstName}</Label>
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label sm="auto">{resources.LastName}</Label>
                            <Col sm={2}>
                                <Label sm="auto">{lastName}</Label>
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label sm="auto">{resources.Password}</Label>
                            <Col sm={2}>
                                <Button color="link" className="btn-password" onClick={() => this._toggleChangingPassword(true)}>
                                    <span sm="auto">Change</span>
                                </Button>
                                {isChangingPassword && <ChangePasswordForm
                                    showPopupPassword={isChangingPassword}
                                    selectedId={userId}
                                    isAdmin={true}
                                    setShowPopupPassword={this._toggleChangingPassword}
                                />}
                            </Col>
                        </FormGroup>
                        {
                            roleList && <FormGroup row className="mb-1">
                                <Label sm="auto">Role</Label>
                                <Col sm={2}>
                                    <Label sm="auto">{roleList[role]}</Label>
                                </Col>
                            </FormGroup>
                        }
                        <FormGroup row className="mb-1">
                            <Label sm="auto">Account Status</Label>
                            <Col sm={2}>
                                <input type="checkbox" name="isActive" className="account-status" checked={isActive} onChange={this._handleChangeStatus} />
                            </Col>
                        </FormGroup>
                    </Form >
                </Container>
                {
                    !!messagePopup && <NotificationPopup
                        message={messagePopup}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </Fragment>
        )
    }

    _renderWhenCreateAndEditUser() {
        const { isCreateNew, roleList } = this.props;
        const { isClickedSubmit, newUserName, firstName, lastName, newPassword, role, isShowMessage, isSuccess, message } = this.state;

        const validatedPasswordResult = validateNewPassword(newPassword);
        const validatedUserNameResult = validateUserName(newUserName);
        const isValid = validatedUserNameResult.IsValid && !!firstName && !!lastName && (isCreateNew ? (validatedPasswordResult.IsValid && !!role) : true);

        return (
            <Fragment>
                <div className="mb-2">
                    <Fragment>
                        <Button outline type="button" color="danger" onClick={() => this._handleCancel(false)}>Cancel<span className="oi oi-x ml-2"></span></Button>
                        <Button outline type="button" className="ml-2" disabled={isClickedSubmit ? !isValid : false} onClick={() => this._handleSubmit(isValid)}>Save<span className="oi oi-task ml-2"></span></Button>
                    </Fragment>
                </div>
                <Container fluid={true} className="admin-info-container pt-2">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Form tag="form" className="my-4" autocomplete="off">
                        <FormGroup row className="mb-1">
                            <Label for="newUserName" sm="auto" className="required">{resources.UserName}</Label>
                            <Col sm={2} className="row-user">
                                <Input type="text" className="refix" disabled value="AA" />
                                <div className="user-name-group">
                                    <Input type="text" className="input-required user-name" placeholder="Enter User Name" name="newUserName" id="newUserName"
                                        value={newUserName} onKeyPress={this._handlePressEnter} onChange={this._handleChange} invalid={isClickedSubmit && !validatedUserNameResult.IsValid} />
                                    {!validatedUserNameResult.IsValid &&
                                        <FormFeedback className="invalid-user-name"><div dangerouslySetInnerHTML={{ __html: validatedUserNameResult.Message }} /></FormFeedback>}
                                </div>
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label for="firstName" sm="auto" className="required">{resources.FirstName}</Label>
                            <Col sm={2}>
                                <Input type="text" className="input-required mt-2" placeholder="Enter First Name" name="firstName" id="firstName"
                                    value={firstName} onKeyPress={this._handlePressEnter} onChange={this._handleChange} invalid={isClickedSubmit && !firstName} />
                                {!firstName &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: "First Name is required!" }} /></FormFeedback>}
                            </Col>
                        </FormGroup>
                        <FormGroup row className="mb-1">
                            <Label for="lastName" sm="auto" className="required">{resources.LastName}</Label>
                            <Col sm={2}>
                                <Input type="text" className="input-required mt-2" placeholder="Enter Last Name" name="lastName" id="lastName"
                                    value={lastName} onKeyPress={this._handlePressEnter} onChange={this._handleChange} invalid={isClickedSubmit && !lastName} />
                                {!lastName &&
                                    <FormFeedback><div dangerouslySetInnerHTML={{ __html: "Last Name is required!" }} /></FormFeedback>}
                            </Col>
                        </FormGroup>
                        {
                            isCreateNew && (
                                <Fragment>
                                    <FormGroup row className="mb-1">
                                        <Label for="newPassword" sm="auto" className="required">{resources.Password}</Label>
                                        <Col sm={2}>
                                            <Input type="password" className="input-required mt-2" placeholder="Enter Default Password" name="newPassword" id="newPassword" autocomplete="false"
                                                value={newPassword} onKeyPress={this._handlePressEnter} onChange={this._handleChange} invalid={isClickedSubmit && !validatedPasswordResult.IsValid} />
                                            {validatedPasswordResult.Message &&
                                                <FormFeedback><div dangerouslySetInnerHTML={{ __html: validatedPasswordResult.Message }} /></FormFeedback>}
                                        </Col>
                                    </FormGroup>
                                </Fragment>
                            )
                        }
                        <FormGroup row className="mb-1">
                            <Label for="role" sm="auto" className="required">Role</Label>
                            <Col sm={2}>
                                <Input type="select" name="role" className="mt-2 select-role" value={role} onChange={this._handleChange} invalid={isClickedSubmit && !role}>
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
                        {
                            isCreateNew && <FormGroup row className="mb-1">
                                <Label sm="auto" className="pt-2">Account Status</Label>
                                <Col sm={2}>
                                    <input type="checkbox" className="mt-2" checked disabled />
                                </Col>
                            </FormGroup>
                        }
                    </Form>
                </Container>
            </Fragment>
        )
    }

    render() {
        const { isCreateNew, isEdit } = this.props;

        return isCreateNew || isEdit ? this._renderWhenCreateAndEditUser() : this._renderWhenView()
    }
}

function mapStateToProps({ userId, userName, adminInfo }) {
    const { roleList, isCreateNew, isEdit } = adminInfo;
    return Object.assign({}, { userId, userName, roleList, isCreateNew, isEdit });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminUsers, redirectToCreateNew, deleteUsers, updateStatus, setIsEdit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);