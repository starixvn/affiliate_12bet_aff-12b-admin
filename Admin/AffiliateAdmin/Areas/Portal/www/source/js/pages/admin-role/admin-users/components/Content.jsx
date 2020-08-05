import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import CheckboxSelectAll from '../../../common/components/CheckboxSelectAll';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { resource } from '../libs/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import { fetchData, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader, deleteUsers } from '../actions';

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
        const { param } = this.props;
        if (param && param !== prevProps.param) {
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

    _handleDeleteUser = (selectedUserId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ message: 'Are you sure you want to delete this user?', isConfirmPopup: true, selectedUserId });
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
        }

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={40}>
                    <CheckboxSelectAll
                        checkedList={this.props.checkedUsers}
                        setCheckedList={this.props.setCheckedUsers}
                        setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                    />
                </Cell>
                <Cell header={true} colWidth={40} title={resource.Index}>{resource.Index}</Cell>
                <Cell header={true} colWidth={250} title={resource.UserName}>{resource.UserName}</Cell>
                <Cell header={true} colWidth={250} title={resource.CreatedDate}>{resource.CreatedDate}</Cell>
                <Cell header={true} colWidth={250} title={resource.LastLoginDate}>{resource.LastLoginDate}</Cell>
                <Cell header={true} colWidth={250} title={resource.LastLoginIp}>{resource.LastLoginIp}</Cell>
                <Cell header={true} colWidth={250} title={resource.Role}>{resource.Role}</Cell>
                <Cell header={true} colWidth={50} title={resource.Delete}>{resource.Delete}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="8">{resource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const userId = dataItem.UserId;
        const userName = htmlDecode(dataItem.UserName);
        const ipAddress = dataItem.LastLoginIp;
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.checkedUsers[userId]}
                        onChange={(e) => this._onCheckItem(e.target.checked, userId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={resource.userName}>
                    <a href={resolveClientUrl(`Portal/AdminInfo?userId=${userId}`)} target="_top">
                        {userName}
                    </a>
                </Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.LastRequestDateText}>{dataItem.LastRequestDateText}</Cell>
                <Cell className="text-center" title={ipAddress}>
                    {
                        ipAddress && <a href={url} target="_tagert">
                            {ipAddress}
                        </a>
                    }
                </Cell>
                <Cell className="text-center" title={dataItem.RoleName}>{dataItem.RoleName}</Cell>
                <Cell className="text-center" title="Delete User">
                    <Button color="link p-0" onClick={() => this._handleDeleteUser(dataItem.UserId)}>
                        <span className="oi oi-trash text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    render() {
        const { pagingOption, gridData, shouldResetScrollPosition } = this.props;
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


function mapStateToProps({ adminUsers }) {
    const { pagingOption, param, checkedUsers, shouldResetScrollPosition, gridData } = adminUsers;
    return Object.assign({}, { pagingOption, param, checkedUsers, gridData, shouldResetScrollPosition });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader, deleteUsers }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);