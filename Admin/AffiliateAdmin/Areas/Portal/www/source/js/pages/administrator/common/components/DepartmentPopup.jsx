import React, { PureComponent } from 'react';

import Filter from './Filter';
import Content from './Content';
import EditPolicyForm from './EditPolicyForm';
import SelectAll from '../../../common/components/SelectAll';

import { Button, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { fetchData } from '../libs/services';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { userInfo } from 'os';

const defaultState = {
    data: null,
    checkedUsers: {},
    userInfoList: {},
    param: null,
    userNames: '',
    selectedUserIds: '',
    isShowPolicyPopup: false,
    shouldResetScrollPosition: true,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}

export default class DepartmentPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            ...defaultState
        }
    }

    componentDidUpdate(prevProps) {
        const { groupId } = this.props;
        if (groupId && groupId !== prevProps.groupId) {
            this.getData({ groupId, userName: '', firstLetter: '' });
        }
    }

    handleSetParam = (param) => {
        const { groupId } = this.props;
        this.setState({ param: { ...param, groupId } });
    }

    handleFetchData = (param) => {
        const { groupId } = this.props;
        this.getData({ ...param, groupId });
    }

    getData = async (param) => {
        const result = await fetchData(param);

        this.setState({ ...result });
    }

    _handleClosePopup = () => {
        this.props.closePopup();
    }

    _handleUpdate = () => {
        const { checkedUsers, userInfoList } = this.state;

        const userIdList = this._getCheckedItemIdList(checkedUsers);

        if (userIdList.length) {
            const userNames = userIdList.map(userId => {
                return htmlDecode(userInfoList[userId]);
            });

            this.setState({ selectedUserIds: userIdList.toString(), userNames: userNames.toString(), isShowPolicyPopup: true });
        } else {
            this._handleShowAlert(false, 'Please choose at least one account.');
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
            this._handleClosePopup();
        }
    }

    _getCheckedItemIdList = (checkedUsers) => {
        return Object.keys(checkedUsers).filter(key => checkedUsers[key]);
    }

    setShouldResetScrollPosition = (shouldResetScrollPosition) => {
        this.setState({ shouldResetScrollPosition });
    }

    render() {
        const { isShowMessage, isSuccess, message, param, data, checkedUsers, shouldResetScrollPosition, selectedUserIds, userNames, isShowPolicyPopup } = this.state;
        const { isAdmin, name, isShowPopup } = this.props;
        const title = isAdmin ? `Department - ${name}` : `${name}`;

        return (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} size="xl" backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="department-popup-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Filter
                                setParam={this.handleSetParam}
                                fetchData={this.handleFetchData}
                                isPopup={true}
                            />
                            {
                                data && data.length && (
                                    <React.Fragment>
                                        <div className="my-2">
                                            <Button outline type="button" onClick={this._handleUpdate}>Update</Button>
                                        </div>
                                        <SelectAll
                                            checkedList={checkedUsers}
                                            setCheckedList={(newCheckedUsers) => this.setState({ checkedUsers: newCheckedUsers })}
                                            setShouldResetScrollPosition={this.setShouldResetScrollPosition}
                                        />
                                    </React.Fragment>
                                )
                            }
                            <Content
                                param={param}
                                gridData={data}
                                shouldResetScrollPosition={shouldResetScrollPosition}
                                setShouldResetScrollPosition={this.setShouldResetScrollPosition}
                                fetchData={this.getData}
                                isPopup={true}
                                checkedUsers={checkedUsers}
                                setCheckedUsers={(newCheckedUsers) => this.setState({ checkedUsers: newCheckedUsers })}
                            />
                            <EditPolicyForm
                                selectedId={selectedUserIds}
                                name={userNames}
                                isAccountPolicy={true}
                                isShowPopup={isShowPolicyPopup}
                                isGetByIdList={true}
                                closePopup={() => this.setState({ isShowPolicyPopup: false, selectedUserIds: '', userNames: '' })}
                            />
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}