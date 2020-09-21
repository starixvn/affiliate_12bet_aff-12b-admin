import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Col, Button, Form, FormGroup, Label, Input, FormFeedback, Alert, Row } from 'reactstrap';
import { messages, initialState, resources } from '../libs/constants';
import { validateUserName, validateNewPassword } from '../../../../modules/utils';
import { convertMarketListByRole, getSelectedMarketName, validateLanguage } from '../libs/utils';
import { globalResource } from '../../../../modules/globalResource';
import { fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminList, deleteUsers, updateStatus, getMarketListByUser, getMarketListByRole, getPermissionListByRole, setPermissionListByRole } from '../actions';
import ChangePasswordForm from '../../../common/components/ChangePasswordForm';
import { NotificationPopup } from '../../../common/components/NotificationPopup';
import PersonalPermissionTable from './PersonalPermissionTable';
import MultipleSelect from '../../../../components/multiple-select';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        const { userId } = this.props;

        this.state = {
            ...initialState,
            isCreateNew: !userId
        }
    }

    componentDidMount() {
        const { userId } = this.props;

        this.props.getRoleList();
        userId && this._handleFetchData(userId);
    }

    _handleFetchData = async (userId) => {
        this.props.toggleLoader(true);

        const userInfo = await this.props.fetchData(userId);

        if (userInfo) {

            const { AdminInfo, MarketList, UserPermissionList } = userInfo;

            const convertMarket = convertMarketListByRole(MarketList, true);

            this.setState({
                newUserName: AdminInfo.UserName,
                firstName: AdminInfo.FirstName,
                lastName: AdminInfo.LastName,
                role: AdminInfo.RoleId,
                language: getSelectedMarketName(MarketList),
                initialRole: AdminInfo.RoleId,
                isActive: AdminInfo.IsActive,
                initialUserInfo: AdminInfo,
                initialMarketList: convertMarket,
                marketListByRole: convertMarket,
                initialPermissionList: UserPermissionList
            });

            this.props.setPermissionListByRole(UserPermissionList);
        } else {
            this._handleShowAlert(false, globalResource.AnError);
        }

        this.props.toggleLoader(false);
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value })
    }

    _handleChangeRole = (event) => {
        const role = event.target.value;

        this.setState({ role });

        const { initialRole } = this.state;

        if (role == initialRole) {
            const { initialMarketList, initialPermissionList } = this.state;

            this.setState({ marketListByRole: initialMarketList });
            this.props.setPermissionListByRole(initialPermissionList);
        } else {
            this.props.getPermissionListByRole(role);
            this._handleUpdateMarketList(role);
        }
    }

    _handleUpdateMarketList = async (roleId) => {
        const { itemDefault } = this.state;
        const marketList = await this.props.getMarketListByRole(roleId, itemDefault.HasPermission);

        this.setState({ marketListByRole: marketList });
    }

    _handleSubmit = async (isValid) => {

        this.setState({ isClickedSubmit: true });

        if (isValid) {
            const { userId, permissionListByRole } = this.props;
            const { newUserName, newPassword, role, isActive, marketListByRole } = this.state;
            const param = { userId: userId || 0, userName: newUserName, password: newPassword, role, isClosed: !isActive, permissionInfo: permissionListByRole, marketList: marketListByRole };

            const data = userId ? await this.props.updateUser(param) : await this.props.createUser(param);

            if (data) {
                this._handleShowAlert(data.IsSuccessful, data.IsSuccessful ? (userId ? "Update user is successful!" : "Create new user is successful!") : data.Message);
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

        isSuccess && this.props.redirectToAdminList();
    }

    _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this._handleSubmit(isValid);
        }
    }

    _handleCancel = (isCreateNew) => {
        if (isCreateNew) {
            this.props.redirectToAdminList();
        } else {
            const { initialUserInfo, initialMarketList, initialPermissionList } = this.state;

            this.setState({
                isClickedSubmit: false,
                newUserName: initialUserInfo.UserName,
                firstName: initialUserInfo.FirstName,
                lastName: initialUserInfo.LastName,
                role: initialUserInfo.RoleId,
                isActive: initialUserInfo.IsActive,
                isEdit: false,
                marketList: initialMarketList
            });

            this.props.setPermissionListByRole(initialPermissionList);
        }
    }

    _toggleChangingPassword = (isChanging) => {
        this.setState({ isChangingPassword: isChanging });
    }

    _handleDeleteUser = () => {
        this.setState({ messagePopup: `Are you sure? <br /> By confirm this action, user <strong class="text-danger">${this.props.userName}</strong> will be deleted?`, isConfirmPopup: true, isDeleteUser: true });
    }

    _handleChangeStatus = () => {
        this.setState({ messagePopup: `Are you sure? <br /> By confirm this action, user <strong class="text-danger">${this.props.userName}</strong> will be deactivated?`, isConfirmPopup: true, isDeleteUser: false });
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messagePopup: '' });

        const { userId } = this.props;
        const { isDeleteUser, isActive, isAfterDeleted } = this.state;

        if (confirmResult && userId && isDeleteUser) {
            this._deleteUser(userId);
        }

        if (confirmResult && userId && !isDeleteUser) {
            this._updateStatus(userId, isActive);
        }

        if (isAfterDeleted) {
            this.props.redirectToAdminList();
        }
    }

    _handleChangeMarket = (item, selectedKey, isItemDefault) => {
        if (isItemDefault) {
            this.setState({ itemDefault: item }, () => item.HasPermission && this._handleUpdateMarketList(this.state.role));
        } else {
            const { marketListByRole } = this.state;

            this.setState({ marketListByRole: this.handleChangeMultipleSelected(marketListByRole, selectedKey) });
        }
    }

    handleChangeMultipleSelected = (initialList, selectedKey) => {
        const selectedList = typeof (selectedKey) === "string" ? selectedKey.split(',').filter(p => !!p) : selectedKey;

        return initialList && initialList.map(item => {
            const { MarketId, MarketName } = item;
            const HasPermission = selectedList.indexOf(MarketId.toString()) !== -1;

            return {
                MarketId,
                MarketName,
                HasPermission
            };
        })
    }

    _deleteUser = async (userId) => {
        this.props.toggleLoader(true);

        const result = await this.props.deleteUsers(userId);

        if (result) {
            const messagePopup = result.IsSuccessful ? 'User has been deleted successfully!' : result.Message;

            this.setState({ messagePopup, isConfirmPopup: false, isAfterDeleted: true });
        } else {
            this.setState({ messagePopup: globalResource.AnError, isDeleteUser: false, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _updateStatus = async (userId, isActive) => {
        this.props.toggleLoader(true);

        const result = await this.props.updateStatus({ userId, isClosed: isActive });

        if (result) {
            const messagePopup = result.IsSuccessful ? "User's status has been updated successfully!" : result.Message;

            result.IsSuccessful && this.setState({ isActive: !isActive });
            this.setState({ messagePopup, isConfirmPopup: false });
        } else {
            this.setState({ messagePopup: globalResource.AnError, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _renderWhenView() {
        const { roleList, userId } = this.props;
        const { newUserName, firstName, lastName, role, language, isActive, isShowMessage, isSuccess, message, isChangingPassword } = this.state;

        return (
            <Fragment>
                <div className="mb-2">
                    <Button outline type="button" color="danger" onClick={this._handleDeleteUser}> Delete < span className="oi oi-trash ml-2"></span></Button>
                    <Button outline type="button" className="ml-2" onClick={() => this.setState({ isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                </div>
                <Container fluid={true} className="admin-info-container pt-2">
                    <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                        <div dangerouslySetInnerHTML={{ __html: message }}></div>
                    </Alert>
                    <Row>
                        <Col sm={6}>
                            <strong>General Information</strong>
                            <Form tag="form" className="mt-2">
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.UserName}</Label>
                                    <Col>
                                        <Label sm={12}>{"AA" + newUserName}</Label>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.FirstName}</Label>
                                    <Col>
                                        <Label sm={12}>{firstName}</Label>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.LastName}</Label>
                                    <Col>
                                        <Label sm={12}>{lastName}</Label>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.Password}</Label>
                                    <Col>
                                        <Button color="link" className="btn-password" onClick={() => this._toggleChangingPassword(true)}>
                                            <span sm="auto">{resources.Change}</span>
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
                                        <Label sm="auto">{resources.Role}</Label>
                                        <Col>
                                            <Label sm={12}>{roleList[role]}</Label>
                                        </Col>
                                    </FormGroup>
                                }
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.Language}</Label>
                                    <Col>
                                        <Label sm={12}>{language}</Label>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label sm="auto">{resources.AccountStatus}</Label>
                                    <Col>
                                        <input type="checkbox" name="isActive" className="account-status" checked={isActive} onChange={this._handleChangeStatus} />
                                    </Col>
                                </FormGroup>
                            </Form >
                        </Col>
                        <Col sm={6}>
                            <PersonalPermissionTable />
                        </Col>
                    </Row>
                </Container>
            </Fragment>
        )
    }

    _renderWhenCreateAndEditUser() {
        const { roleList } = this.props;
        const { isClickedSubmit, newUserName, firstName, lastName, newPassword, role, isShowMessage, isSuccess, message,
            isCreateNew, isEdit, isActive, itemDefault, marketListByRole } = this.state;

        const validatedPasswordResult = validateNewPassword(newPassword);
        const validatedUserNameResult = validateUserName(newUserName);
        const isValidLanguage = validateLanguage(marketListByRole);
        const isValid = validatedUserNameResult.IsValid && !!firstName && !!lastName && !!role && isValidLanguage && (isCreateNew ? validatedPasswordResult.IsValid : true);

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
                    <Row>
                        <Col sm={6}>
                            <strong>General Information</strong>
                            <Form tag="form" autocomplete="off" className="mt-2">
                                <FormGroup row className="mb-1">
                                    <Label for="newUserName" sm="auto" className="required">{resources.UserName}</Label>
                                    <Col sm={5} className="row-user">
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
                                    <Col sm={5}>
                                        <Input type="text" className="input-required mt-2" placeholder="Enter First Name" name="firstName" id="firstName"
                                            value={firstName} onKeyPress={this._handlePressEnter} onChange={this._handleChange} invalid={isClickedSubmit && !firstName} />
                                        {!firstName &&
                                            <FormFeedback><div dangerouslySetInnerHTML={{ __html: "First Name is required!" }} /></FormFeedback>}
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-1">
                                    <Label for="lastName" sm="auto" className="required">{resources.LastName}</Label>
                                    <Col sm={5}>
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
                                                <Col sm={5}>
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
                                    <Label for="role" sm="auto" className="required">{resources.Role}</Label>
                                    <Col sm={5}>
                                        <Input type="select" name="role" className="mt-2 select-role" value={role} onChange={this._handleChangeRole} invalid={isClickedSubmit && !role}>
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
                                    marketListByRole && (
                                        <FormGroup row className={`mb-1 ${isClickedSubmit && !isValidLanguage ? 'multiple-select-wrapper is-invalid' : ''}`}>
                                            <Label for="market" sm="auto" className="required">{resources.Language}</Label>
                                            <Col sm={5} className="mt-2">
                                                <MultipleSelect
                                                    dataSource={marketListByRole}
                                                    keyField="MarketId"
                                                    valueField="MarketName"
                                                    statusField="HasPermission"
                                                    maxDisplayItemCount={1}
                                                    noneSelectedLabel="Please Select"
                                                    itemDefaultOption={itemDefault}
                                                    isDisableOption={itemDefault.HasPermission}
                                                    onChange={this._handleChangeMarket} />
                                            </Col>
                                        </FormGroup>
                                    )
                                }
                                <FormGroup row className="mb-1">
                                    <Label sm="auto" className="pt-2">{resources.AccountStatus}</Label>
                                    <Col>
                                        <input type="checkbox" className="mt-2" checked={isActive} onChange={() => this.setState({ isActive: !isActive })} />
                                    </Col>
                                </FormGroup>
                            </Form>
                        </Col>
                        <Col sm={6}>
                            <PersonalPermissionTable
                                isCreateNew={isCreateNew}
                                isEdit={isEdit} />
                        </Col>
                    </Row>
                </Container>
            </Fragment>
        )
    }

    render() {
        const { isCreateNew, isEdit, messagePopup, isConfirmPopup } = this.state;

        return (
            <Fragment>
                {
                    isCreateNew || isEdit ? this._renderWhenCreateAndEditUser() : this._renderWhenView()
                }
                {
                    !!messagePopup && <NotificationPopup
                        message={messagePopup}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </Fragment>
        );
    }
}

function mapStateToProps({ userId, userName, adminInfo }) {
    const { roleList, permissionListByRole, marketListByUser } = adminInfo;
    return Object.assign({}, { userId, userName, roleList, permissionListByRole, marketListByUser });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, createUser, updateUser, toggleLoader, getRoleList, redirectToAdminList, deleteUsers, updateStatus, getMarketListByUser, getMarketListByRole, getPermissionListByRole, setPermissionListByRole }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);