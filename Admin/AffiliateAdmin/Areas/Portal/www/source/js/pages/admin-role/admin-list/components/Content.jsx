import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import CheckboxSelectAll from '../../../common/components/CheckboxSelectAll';
import { NotificationPopup } from '../../../common/components/NotificationPopup';
import MyLink from '../../../common/components/MyLink';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { resource, globalResource } from '../libs/constants';
import { fetchData, setUserInfo, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader, deleteUsers, redirectToUserDetail } from '../actions';

export class Content extends PureComponent {

    constructor(props) {
        super(props);

        this.state = {
            selectedUserId: 0,
            message: '',
            isConfirmPopup: true
        }
    }

    componentDidUpdate(prevProps) {
        const { param, isBack } = this.props;
        if (param && param !== prevProps.param || isBack && isBack != prevProps.isBack) {
            this.props.fetchData(param);
        }
    }

    _onCheckItem = (isChecked, userId) => {
        const newCheckedUsers = Object.assign({}, this.props.checkedUsers, { [userId]: isChecked });

        this.props.setCheckedUsers(newCheckedUsers);
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ message: '' });

        const { selectedUserId } = this.state;
        if (confirmResult && selectedUserId) {
            this._deleteUser(selectedUserId);
        }
    }

    _handleDeleteUser = (selectedUserId, selectedUserName) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ message: `Are you sure? <br /> By confirm this action, user <strong className="text-danger">${selectedUserName}</strong> will be deleted?`, isConfirmPopup: true, selectedUserId });
    }

    _deleteUser = async (selectedUserId) => {
        this.props.toggleLoader(true);

        const result = await this.props.deleteUsers(selectedUserId);

        if (result) {
            if (result.IsSuccessful) {
                this.setState({ message: 'User has been deleted successfully!', isConfirmPopup: false });

                this.props.fetchData(this.props.param);
            }
            else {
                this.setState({ message: result.Message, isConfirmPopup: false });
            }
        } else {
            this.setState({ messagePopup: globalResource.AnError, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _handleRedirectToUserDetail = (userId, userName) => {
        this.props.redirectToUserDetail(userId, userName);
    }

    _handleShowPermission = (userId, userName) => {
        this.props.setUserInfo({ UserId: userId, UserName: userName });
    }

    _renderHeader() {
        return (
            <Row key="thead">
                {
                    this.props.hasWriteUserControlClaim && (
                        <Cell header={true} colWidth={50}>
                            <CheckboxSelectAll
                                checkedList={this.props.checkedUsers}
                                setCheckedList={this.props.setCheckedUsers}
                                setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                            />
                        </Cell>
                    )
                }
                <Cell header={true} colWidth={50} title={resource.Index}>{resource.Index}</Cell>
                <Cell header={true} colWidth={200} title={resource.UserName}>{resource.UserName}</Cell>
                <Cell header={true} colWidth={200} title={resource.FirstName}>{resource.FirstName}</Cell>
                <Cell header={true} colWidth={200} title={resource.LastName}>{resource.LastName}</Cell>
                <Cell header={true} colWidth={200} title={resource.Creator}>{resource.Creator}</Cell>
                <Cell header={true} colWidth={200} title={resource.LastLoginDate}>{resource.LastLoginDate}</Cell>
                <Cell header={true} colWidth={200} title={resource.LastLoginIp}>{resource.LastLoginIp}</Cell>
                <Cell header={true} colWidth={200} title={resource.Role}>{resource.Role}</Cell>
                <Cell header={true} colWidth={100} title={resource.Status}>{resource.Status}</Cell>
                <Cell header={true} colWidth={100} title={resource.Action}>{resource.Action}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const { hasWriteUserControlClaim } = this.props;

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1, hasWriteUserControlClaim));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={hasWriteUserControlClaim ? "9" : "8"}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, hasWriteUserControlClaim) {
        const userId = dataItem.UserId;
        const userName = htmlDecode(dataItem.UserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);
        const creator = htmlDecode(dataItem.Creator);
        const roleName = htmlDecode(dataItem.RoleName);
        const ipAddress = dataItem.LastLoginIp;
        const status = dataItem.IsActive ? 'Open' : 'Closed';
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        return (
            <Row key={`dataRow-${index}`}>
                {
                    hasWriteUserControlClaim && (
                        <Cell className="text-center">
                            <input type="checkbox" checked={this.props.checkedUsers[userId] || false}
                                onChange={(e) => this._onCheckItem(e.target.checked, userId)} />
                        </Cell>
                    )
                }
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={resource.ViewAccountInfo}>
                    <MyLink onClick={() => this._handleRedirectToUserDetail(userId, userName)}>
                        {userName}
                    </MyLink>
                </Cell>
                <Cell className="text-center" title={firstName}>{firstName}</Cell>
                <Cell className="text-center" title={lastName}>{lastName}</Cell>
                <Cell className="text-center" title={creator}>{creator}</Cell>
                <Cell className="text-center" title={dataItem.LastLoginDateText}>{dataItem.LastLoginDateText}</Cell>
                <Cell className="text-center" title={ipAddress}>
                    {
                        ipAddress && <a href={url} target="_tagert">
                            {ipAddress}
                        </a>
                    }
                </Cell>
                <Cell className="text-center" title={roleName}>{roleName}</Cell>
                <Cell className="text-center" title={status}>{status}</Cell>
                <Cell className="text-center">
                    <Button color="link p-0" title={resource.ViewPermission} onClick={() => this._handleShowPermission(dataItem.UserId, dataItem.UserName)}>
                        <span className="oi oi-eye"></span>
                    </Button>
                    {
                        hasWriteUserControlClaim && (
                            <Button color="link p-0" title={resource.DeleteAccount} onClick={() => this._handleDeleteUser(dataItem.UserId, userName)}>
                                <span className="oi oi-trash text-danger pl-3"></span>
                            </Button>
                        )
                    }
                </Cell>
            </Row>
        )
    }

    render() {
        const { gridData, shouldResetScrollPosition } = this.props;
        const { message, isConfirmPopup } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="admin-user-content table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
                {
                    !!message && <NotificationPopup
                        message={message}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </div>
        );
    }
}


function mapStateToProps({ adminList, hasWriteUserControlClaim }) {
    const { isBack, param, checkedUsers, shouldResetScrollPosition, gridData } = adminList;
    return Object.assign({}, { isBack, param, checkedUsers, gridData, shouldResetScrollPosition, hasWriteUserControlClaim });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setUserInfo, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader, deleteUsers, redirectToUserDetail }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);