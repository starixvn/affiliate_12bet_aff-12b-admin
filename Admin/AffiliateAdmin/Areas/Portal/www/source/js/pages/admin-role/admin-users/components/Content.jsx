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
import { setPagingOption, fetchData, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader } from '../actions';

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

        const result = await this.props.deleteUser(selectedUserId);

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

    _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.props.setParam(newParam);
    };

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
                <Cell header={true} colWidth={250} title={resource.LoggedIn}>{resource.LoggedIn}</Cell>
                <Cell header={true} colWidth={250} title={resource.LastRequest}>{resource.LastRequest}</Cell>
                <Cell header={true} colWidth={250} title={resource.IP}>{resource.IP}</Cell>
                <Cell header={true} colWidth={250} title={resource.Role}>{resource.Role}</Cell>
                <Cell header={true} colWidth={50} title={resource.Delete}>{resource.Delete}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
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
        const userName = htmlDecode(dataItem.UserName);
        const ipAddress = '122.53.154.194';
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.checkedUsers[dataItem.UserId]}
                        onChange={(e) => this._onCheckItem(e.target.checked, dataItem.UserId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={resource.userName}>
                    <a href={resolveClientUrl(`Portal/AdminInfo`)} target="_top">
                        {userName}
                    </a>
                </Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={ipAddress}>
                    <a href={url} target="_tagert">
                        {ipAddress}
                    </a>
                </Cell>
                <Cell className="text-center" title={dataItem.GroupName}>{dataItem.GroupName}</Cell>
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
                        isPaging={true}
                        pageOption={pagingOption}
                        onPaging={this._handlePaging}
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


function mapStateToProps({ pagingOption, param, checkedUsers, gridData, shouldResetScrollPosition }) {
    return Object.assign({}, { pagingOption, param, checkedUsers, gridData, shouldResetScrollPosition });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setCheckedUsers, setParam, setShouldResetScrollPosition, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);