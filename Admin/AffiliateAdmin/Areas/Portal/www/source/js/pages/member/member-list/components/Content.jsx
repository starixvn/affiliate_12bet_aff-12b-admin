import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { updateMemberStatus, fetchData, setShouldResetScrollPosition, setParam } from '../actions';
import { claimResource } from '../../../../modules/claim';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedMemberId: 0,
            message: '',
            isRemoved: false,
            isConfirmPopup: true
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        if (param && param !== prevProps.param) {
            this.props.fetchData(param);
        }
    }

    _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setParam(newParam);
    };

    _handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ message: '' });

        const { selectedMemberId } = this.state;
        if (confirmResult && selectedMemberId) {
            this._updateMemberStatus(selectedMemberId);
        }
    }

    _handleChangeStatus = (selectedMemberId, isRemoved) => {
        this.props.setShouldResetScrollPosition(false);

        const message = isRemoved ? 'Are you sure you want to remove the selected member from this affiliate?' : 'Do you want to reinstate the selected member for this affiliate?';
        this.setState({ message, isConfirmPopup: true, selectedMemberId, isRemoved });
    }

    _updateMemberStatus = async (selectedMemberId) => {
        const { isRemoved } = this.state;

        const result = await this.props.updateMemberStatus(selectedMemberId, isRemoved);

        if (result) {
            if (result.IsSuccessful) {
                const message = `The selected member has been ${isRemoved ? 'removed' : 'reinstated'} successfully!`;
                this.setState({ message, isConfirmPopup: false });

                this.props.fetchData(this.props.param);
            }
            else {
                this.setState({ message: result.Message, isConfirmPopup: false });
            }
        }
    }

    _renderHeader() {
        const { isModifiable, isShowMemberName } = this.props;
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={120} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={120} title={headerText.IbcUserName}>{headerText.IbcUserName}</Cell>
                { isShowMemberName && <Cell header={true} colWidth={100} title={headerText.FirstName}>{headerText.FirstName}</Cell>}
                { isShowMemberName && <Cell header={true} colWidth={100} title={headerText.LastName}>{headerText.LastName}</Cell>}
                <Cell header={true} colWidth={110} title={headerText.CurrentBalance}>{headerText.CurrentBalance}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
                { isModifiable && <Cell header={true} colWidth={40} title={headerText.Remove}>{headerText.Remove}</Cell>}
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = this.props;

            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            const { isShowMemberName, isModifiable } = this.props;

            dataRows.push(<Row>
                <Cell className="text-center" colSpan={isShowMemberName ? `${6 + Number(isModifiable)}` : `${4 + Number(isModifiable)}`}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const { isModifiable, isShowMemberName } = this.props;

        const userName = htmlDecode(dataItem.UserName);
        const ibcUserName = htmlDecode(dataItem.IbcUserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);
        const isRemoved = dataItem.IsRemoved;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={userName}>{userName}</Cell>
                <Cell className="text-center" title={ibcUserName}>{ibcUserName}</Cell>
                { isShowMemberName && <Cell className="text-center" title={firstName}>{firstName}</Cell>}
                { isShowMemberName && <Cell className="text-center" title={lastName}>{lastName}</Cell>}
                <Cell className="text-center" title={dataItem.CurrentBalance}>{dataItem.CurrentBalance}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.AffName}>{dataItem.AffName}</Cell>
                { isModifiable &&
                    <Cell className="text-center" title={isRemoved ? 'Reinstate the member' : 'Remove the member'}>
                        <input type="checkbox" checked={isRemoved} onChange={() => this._handleChangeStatus(dataItem.MemberId, !isRemoved)} />
                    </Cell>
                }
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

function mapStateToProps({ pagingOption, param, shouldResetScrollPosition, gridData, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MemberDetail);
    const isShowMemberName = claimsPrincipal.hasClaim(claimResource.MemberName);
    return Object.assign({}, { pagingOption, param, shouldResetScrollPosition, gridData, isModifiable, isShowMemberName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateMemberStatus, fetchData, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);