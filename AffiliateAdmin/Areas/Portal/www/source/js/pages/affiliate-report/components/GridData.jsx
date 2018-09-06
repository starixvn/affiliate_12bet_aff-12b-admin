import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { format, formatDecimal } from '../../../modules/numberFormat';
import { setPagingOption } from '../actions';
import { post } from '../../../modules/ajaxRequest';
import { allProduct } from '../modules/constants';
import { resolveClientUrl } from '../../../modules/utils';

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        if (this.props.param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        const url = resolveClientUrl('Portal/AffiliateReport/GetMainData');
        const response = await post(url, param);
        const { MainData, TotalRow } = response.data;

        this.setState({ gridData: MainData });
        this.props.setPagingOption({ TotalItem: TotalRow });
    }

    _renderHeader() {
        return [
            <Row>
                <Cell header={true} colWidth={100} rowSpan="2">Affiliate</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">Total Click</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">Unique Click</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">Signup</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">FDM</Cell>
                <Cell header={true} colWidth={50} rowSpan="2">Active Member</Cell>
                <Cell header={true} colWidth={55} rowSpan="2">Qualified Member</Cell>
                <Cell header={true} colSpan="9">Member Statistics</Cell>
                <Cell header={true} colSpan="4">Affiliate Statistics</Cell>
                <Cell header={true} colWidth={60} rowSpan="2">Month on Month</Cell>
            </Row>,
            <Row>
                <Cell header={true} colWidth={70}>Deposit</Cell>
                <Cell header={true} colWidth={70}>Withdrawal</Cell>
                <Cell header={true} colWidth={100}>Turnover</Cell>
                <Cell header={true} colWidth={100}>WinLoss</Cell>
                <Cell header={true} colWidth={70}>Bonus</Cell>
                <Cell header={true} colWidth={70}>Platform Fee</Cell>
                <Cell header={true} colWidth={70}>Operational Fee</Cell>
                <Cell header={true} colWidth={70}>Banking Fee</Cell>
                <Cell header={true} colWidth={70}>Total Fee</Cell>

                <Cell header={true} colWidth={60}>Comm%</Cell>
                <Cell header={true} colWidth={80}>Earning</Cell>
                <Cell header={true} colWidth={70}>Withdrawal</Cell>
                <Cell header={true} colWidth={90}>Balance B/F</Cell>
            </Row>
        ];
    }

    _renderBody() {
        const { gridData } = this.state;
        const { param } = this.props;

        const isHighlighProductColumn = param.ProductIds === allProduct || param.ProductIds === "";
        const dataRows = [];

        for (let i = 0; i < gridData.length; i++) {
            const dataItem = gridData[i];
            dataRows.push(
                <Row>
                    <Cell>{dataItem.UserName}</Cell>
                    <Cell className="text-right">{format(dataItem.TotalClick)}</Cell>
                    <Cell className="text-right">{format(dataItem.UniqueClick)}</Cell>
                    <Cell className="text-right">{format(dataItem.Signup)}</Cell>
                    <Cell className="text-right">{format(dataItem.Fdm)}</Cell>
                    <Cell className="text-right">{format(dataItem.ActiveMember)}</Cell>
                    <Cell className="text-right">{format(dataItem.QualifiedMember)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.Deposit)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.Withdrawal)}</Cell>
                    <Cell className={`text-right ${isHighlighProductColumn ? '' : 'highlight'}`}>{formatDecimal(dataItem.Turnover)}</Cell>
                    <Cell className={`text-right ${isHighlighProductColumn ? '' : 'highlight'}`}>{formatDecimal(dataItem.Winlost)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.Bonus)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.PlatformFee)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.OperationalFee)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.BankingFee)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.TotalFee)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.CommissionRate / 100)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.Earning)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.AffWithdrawal)}</Cell>
                    <Cell className="text-right">{formatDecimal(dataItem.BalanceBf)}</Cell>
                    <Cell className="text-center">View</Cell>
                </Row>);
        }

        return dataRows;
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };
        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, newPaging);
        this.fetchData(newParam);
    }

    render() {
        return this.state.gridData && (
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderBody()}
                    containerPadding={0}
                    isPaging={true}
                    pageOption={this.props.pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}

function mapStateToProps({ pagingOption, param }) {
    return Object.assign({}, { pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);