import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { deleteMember, fetchData, setPagingOption, toggleLoader, setShouldResetScrollPosition, setParam } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedMemberId: 0,
            message: '',
            isConfirmPopup: true
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        if(param && param !== prevProps.param) {
            this.props.fetchData(param);
        }
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

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ message: '' });

        const { selectedMemberId } = this.state;
        if (confirmResult && selectedMemberId) {
            this._deleteMember(selectedMemberId);
        }
    }

    _handleDeleteMember = (selectedMemberId) => {
        this.props.setShouldResetScrollPosition(false);
        this.setState({ message: 'Are you sure you want to remove this member from his affiliate?', isConfirmPopup: true, selectedMemberId });
    }

    _deleteMember = async (selectedMemberId) => {
        this.props.toggleLoader(true);

        const result = await this.props.deleteMember(selectedMemberId);

        if (result) {
            if (result.IsSuccessful) {
                this.setState({ message: 'Member has been removed suseccfully!', isConfirmPopup: false });

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
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={120} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={120} title={headerText.IbcUserName}>{headerText.IbcUserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={120} title={headerText.CurrentBalance}>{headerText.CurrentBalance}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Remove}>{headerText.Remove}</Cell>
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
                <Cell className="text-center" colSpan="9">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const userName = htmlDecode(dataItem.UserName);
        const ibcUserName = htmlDecode(dataItem.IbcUserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={userName}>{userName}</Cell>
                <Cell className="text-center" title={ibcUserName}>{ibcUserName}</Cell>
                <Cell className="text-center" title={firstName}>{firstName}</Cell>
                <Cell className="text-center" title={lastName}>{lastName}</Cell>
                <Cell className="text-center" title={dataItem.CurrentBalance}>{dataItem.CurrentBalance}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.AffName}>{dataItem.AffName}</Cell>
                <Cell className="text-center" title="Remove Member">
                    <Button color="link p-0" onClick={() => this._handleDeleteMember(dataItem.MemberId)}>
                        <span className="oi oi-x text-danger"></span>
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
                <div className="member-list-content table-responsive table-striped my-2">
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

function mapStateToProps({ pagingOption, param, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteMember, fetchData, setPagingOption, toggleLoader, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);