import React, { PureComponent } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource, defaultState } from '../libs/constants';
import InsertUpdateForm from './InsertUpdateForm';
import NotificationPopup from '../../../common/components/NotificationPopup';

export default class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = defaultState;
    }

    _handleChangeStatus = (selectedUserId, isOpen) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ message: `Are you sure you want to change status for this customer?`, isConfirmPopup: true, selectedUserId, isOpen });
    }

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ message: '' });

        const { selectedUserId, isOpen } = this.state;
        if (confirmResult && selectedUserId) {
            this._handleSaveStatus(selectedUserId, isOpen);
        }
    }

    _handleSaveStatus = async (selectedUserId, isOpen) => {
        const result = await this.props.changeStatus({ selectedUserId, isOpen });

        if (result && result.IsSuccessful) {
            this.setState({ message: `The status has been updated successfully!`, isConfirmPopup: false });

            this.handleFetchData();
        } else {
            this.setState({ message: !!result ? result.Message : 'Update is failed!', isConfirmPopup: false });
        }
    }

    handleFetchData = () => {
        this.props.fetchData(this.props.param);
    }

    _handleShowUserPopup = (userId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ selectedUserId: userId, isShowUserPopup: true });
    }

    _handleShowPolicyPopup = (userId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ selectedUserId: userId, isShowPolicyPopup: true });
    }

    _handleShowDepartmentPopup = (userId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ selectedUserId: userId, isShowDepartmentPopup: true });
    }

    _handleShowPasswordPopup = (userId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ selectedUserId: userId, isShowPasswordPopup: true });
    }

    handleClosePopup = () => {
        this.setState(defaultState);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={60} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Creator}>{headerText.Creator}</Cell>
                <Cell header={true} colWidth={60} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Policy}>{headerText.Policy}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Department}>{headerText.Department}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Password}>{headerText.Password}</Cell>
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
                <Cell className="text-center" colSpan="10">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const userName = htmlDecode(dataItem.UserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);
        const creator = htmlDecode(dataItem.Creator);

        return (
            <Row key={`dataRow-${index}`} className={dataItem.IsOpen ? "" : "is-closed"}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={userName}>
                    <Button color="link p-0" onClick={() => this._handleShowUserPopup(dataItem.UserId)}>
                        {
                            userName
                        }
                    </Button>
                </Cell>
                <Cell className="text-center" title={firstName}>{firstName}</Cell>
                <Cell className="text-center" title={lastName}>{lastName}</Cell>
                <Cell className="text-center" title="Change Status">
                    Open
                    <input type="checkbox" className="ml-1" onChange={() => this._handleChangeStatus(dataItem.UserId, !dataItem.IsOpen)} checked={dataItem.IsOpen} />
                </Cell>
                <Cell className="text-center" title={creator}>{creator}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDate}>{dataItem.CreatedDate}</Cell>
                <Cell className="text-center" title="Edit Account Policy">
                    <Button color="link p-0" onClick={() => this._handleShowPolicyPopup(dataItem.UserId)}>
                        Account Policy
                    </Button>
                </Cell>
                <Cell className="text-center" title="Edit Department">
                    <Button color="link p-0" onClick={() => this._handleShowDepartmentPopup(dataItem.UserId)}>
                        {dataItem.GroupName}
                    </Button>
                </Cell>
                {
                    dataItem.IsOpen ? (
                        <Cell className="text-center" title="Change Password">
                            <Button color="link p-0" onClick={() => this._handleShowPasswordPopup(dataItem.UserId)}>
                                <span className="oi oi-pencil text-secondary"></span>
                            </Button>
                        </Cell>
                    ) : (<Cell></Cell>)
                }
            </Row>
        )
    }

    render() {
        const { selectedUserId, isOpen, isShowUserPopup, isShowPolicyPopup, isShowDepartmentPopup, isShowPasswordPopup, message, isConfirmPopup } = this.state;
        const { gridData, shouldResetScrollPosition } = this.props;

        return gridData && (
            <div className="data-content">
                <div className="common-list-content table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                    <InsertUpdateForm
                        isShowPopup={isShowUserPopup}
                        selectedUserId={selectedUserId}
                        closePopup={this.handleClosePopup}
                        fetchData={this.handleFetchData}
                        toggleLoader={this.props.toggleLoader}
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