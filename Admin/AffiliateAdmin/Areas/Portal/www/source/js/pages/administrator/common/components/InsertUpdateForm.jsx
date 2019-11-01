import React, { PureComponent } from 'react';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { headerText, globalResource, charCodes } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { getDepartmentList } from '../../department/actions/index';
import { getUserDetail, insertUser, updateUser } from '../actions';

const defaultState = {
    departmentList: [],
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    department: 0,
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}

export default class InsertUpdateForm extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            ...defaultState
        }
    }

    componentDidMount() {
        this._handleGetDepartmentList();
    }

    componentDidUpdate(prevProps) {
        const { selectedUserId } = this.props;
        if (prevProps.selectedUserId !== selectedUserId) {
            this.setState({ isInsert: selectedUserId === 0 });

            selectedUserId && this._handleGetUserDetail(selectedUserId);
        }
    }

    _handleGetDepartmentList = async () => {
        var departmentList = await getDepartmentList();

        this.setState({ departmentList });
    }

    _handleGetUserDetail = async (selectedUserId) => {
        const item = await getUserDetail(selectedUserId);
        console.log(item);
        item && this.setState({
            userName: htmlDecode(item.UserName),
            firstName: htmlDecode(item.FirstName),
            lastName: htmlDecode(item.LastName),
            department: item.GroupId
        });
    }

    _handleChange = (event) => {
        const value = event.target.value;
        const name = event.target.name;

        this.setState({ [name]: value });
    }

    _checkSpecialChar = (userName) => {
        const iChars = "!@#$%^&*()+=-[]\';,./{}|\":<>?";
        const length = userName.length;
        for (var i = 0; i < length; i++) {
            if (iChars.indexOf(userName.charAt(i)) != -1) {
                return false;
            }
        }

        return true;
    }

    _handleSubmit = async () => {
        const { userName, password, firstName, lastName, department, isInsert } = this.state;

        if (userName) {
            this._handleShowAlert(false, 'Please input user name!');
            return;
        }

        if (password) {
            this._handleShowAlert(false, 'Password is required!');
            return;
        }

        if (department) {
            this._handleShowAlert(false, 'Please choose department!');
            return;
        }

        const isValidUserName = this._checkSpecialChar(userName);

        if (isValidUserName) {
            this.props.toggleLoader(true);

            const { selectedUserId } = this.props;
            const message = isInsert ? "Administrator has been added successfully!" : "Update Successfully!";
            const userInfo = Object.assign({}, { UserId: selectedUserId, userName, password, firstName, lastName, department });
            const result = isInsert ? await this.props.insertUser(userInfo) : await this.props.updateUser(userInfo);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Failed!" : "Update Failed!");
            }

            this.props.toggleLoader(false);
        } else {
            this._handleShowAlert(false, 'User Name can not be contained the special characters. Please try again!');
        }
    }

    _handleKeyPress = (event) => {
        const charCode = event.charCode;
        if (charCode === charCodes.KeyEnter) {
            event.preventDefault();
            this._handleSubmit();
        }

        if (charCode === charCodes.KeyDoubleQuotationMark || charCode === charCodes.KeySingleQuotationMark) {
            event.preventDefault();
            this._handleShowAlert(false, 'User name can not contain this character!');
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

        if (isSuccess) {
            this.props.fetchData();
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.closePopup();

        this.setState({ ...defaultState });
    }

    render() {
        const { departmentList, userName, password, firstName, lastName, department, isInsert, isShowMessage, isSuccess, message } = this.state;
        const title = isInsert ? "Add New Account" : userName;
        const labelButton = isInsert ? "Add" : "Submit";

        return (
            <Modal isOpen={this.props.isShowPopup} toggle={this._handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="account-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autocomplete="off">
                                {
                                    isInsert && (<React.Fragment>
                                        <FormGroup row className="mb-2">
                                            <Label for="userName" sm={4} className="required">{headerText.UserName}</Label>
                                            <Col sm={8}>
                                                <Input type="text" name="userName" autocomplete="off" className="mt-2" maxlength="20" value={userName} onChange={this._handleChange}
                                                    onKeyPress={this._handleKeyPress} />
                                            </Col>
                                        </FormGroup>
                                        <FormGroup row className="mb-2">
                                            <Label for="password" sm={4} className="required">{headerText.Password}</Label>
                                            <Col sm={8}>
                                                <Input type="password" name="password" className="mt-2" maxlength="20" autocomplete="off" className="mt-2" value={password}
                                                    onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                                            </Col>
                                        </FormGroup>
                                    </React.Fragment>)
                                }
                                <FormGroup row className="mb-2">
                                    <Label for="firstName" sm={4}>{headerText.FirstName}</Label>
                                    <Col sm={8}>
                                        <Input type="text" name="firstName" maxlength="20" className="mt-2" value={firstName} onChange={this._handleChange}
                                            onKeyPress={this._handleKeyPress} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="lastName" sm={4}>{headerText.LastName}</Label>
                                    <Col sm={8}>
                                        <Input type="text" name="lastName" maxlength="20" className="mt-2" value={lastName} onChange={this._handleChange}
                                            onKeyPress={this._handleKeyPress} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row>
                                    <Label for="department" sm={4}>{headerText.Department}</Label>
                                    <Col sm={8}>
                                        <Input type="select" name="department" className="mt-2" value={department} onChange={(e) => setSearch(e.target.value)}>
                                            {
                                                isInsert && <option key={'department-0'} value="0">[Select Dept]</option>
                                            }
                                            {
                                                departmentList.map(item => {
                                                    return (<option key={'department-' + item.DepartmentId} value={item.DepartmentId}>{item.DepartmentName}</option>)
                                                })
                                            }
                                        </Input>
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit} >{labelButton}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}