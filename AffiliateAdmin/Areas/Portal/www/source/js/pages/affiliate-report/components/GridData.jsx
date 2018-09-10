import React, { Component } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { format, formatDecimal } from '../../../modules/numberFormat';
import { setPagingOption } from '../actions';
import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { allProduct } from '../modules/constants';

export class GridData extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            total: null
        }
    }

    componentDidUpdate(prevProps) {
        if (this.props.param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/AffiliateReport/GetMainData';
        const response = await post(url, param);
        const { MainData, Total, TotalItem } = response.data;
        Total.UserName = 'Total (all pages)';

        this.setState({ gridData: MainData, total: Total });
        this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex });
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return [
            <Row>
                <Cell header={true} colWidth={95} rowSpan="2" title="Affiliate">Affiliate</Cell>
                <Cell header={true} colWidth={70} rowSpan="2" title="Total Click">Total Click</Cell>
                <Cell header={true} colWidth={70} rowSpan="2" title="Unique Click">Unique Click</Cell>
                <Cell header={true} colWidth={65} rowSpan="2" title="Signup">Signup</Cell>
                <Cell header={true} colWidth={50} rowSpan="2" title="FDM">FDM</Cell>
                <Cell header={true} colWidth={60} rowSpan="2" title="Active Member">Active Member</Cell>
                <Cell header={true} colWidth={60} rowSpan="2" title="Qualified Member">Qualified Member</Cell>
                <Cell header={true} colSpan="9" title="Member Statistics">Member Statistics</Cell>
                <Cell header={true} colSpan="4" title="Affiliate Statistics">Affiliate Statistics</Cell>
                <Cell header={true} colWidth={50} rowSpan="2" title="Month on Month">Month on Month</Cell>
            </Row>,
            <Row>
                <Cell header={true} colWidth={75} title="Deposit">Deposit</Cell>
                <Cell header={true} colWidth={75} title="Withdrawal">Withdrawal</Cell>
                <Cell header={true} colWidth={95} title="Turnover">Turnover</Cell>
                <Cell header={true} colWidth={95} title="WinLoss">WinLoss</Cell>
                <Cell header={true} colWidth={70} title="Bonus">Bonus</Cell>
                <Cell header={true} colWidth={70} title="Platform Fee">Platform Fee</Cell>
                <Cell header={true} colWidth={70} title="Operational Fee">Operational Fee</Cell>
                <Cell header={true} colWidth={70} title="Banking Fee">Banking Fee</Cell>
                <Cell header={true} colWidth={70} title="Total Fee">Total Fee</Cell>

                <Cell header={true} colWidth={60} title="Comm%">Comm%</Cell>
                <Cell header={true} colWidth={80} title="Earning">Earning</Cell>
                <Cell header={true} colWidth={80} title="Withdrawal">Withdrawal</Cell>
                <Cell header={true} colWidth={100} title="Balance B/F">Balance B/F</Cell>
            </Row>
        ];
    }

    _renderDataSection(dataList) {
        const { param } = this.props;

        const isAllProductIdentical = param.ProductIds === allProduct || param.ProductIds === "";
        const productColumnClassName = isAllProductIdentical ? 'text-right' : 'text-right highlight';
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, productColumnClassName));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, productColumnClassName) {
        const totalClick = format(dataItem.TotalClick);
        const uniqueClick = format(dataItem.UniqueClick);
        const signup = format(dataItem.Signup);
        const fdm = format(dataItem.Fdm);
        const activeMember = format(dataItem.ActiveMember);
        const qualifiedMember = format(dataItem.QualifiedMember);
        const deposit = formatDecimal(dataItem.Deposit);
        const withdrawal = formatDecimal(dataItem.Withdrawal);
        const turnover = formatDecimal(dataItem.Turnover);
        const winlost = formatDecimal(dataItem.Winlost);
        const bonus = formatDecimal(dataItem.Bonus);
        const platformFee = formatDecimal(dataItem.PlatformFee);
        const operationalFee = formatDecimal(dataItem.OperationalFee);
        const bankingFee = formatDecimal(dataItem.BankingFee);
        const totalFee = formatDecimal(dataItem.TotalFee);
        const commRate = formatDecimal(dataItem.CommissionRate / 100);
        const earning = formatDecimal(dataItem.Earning);
        const affWithdrawal = formatDecimal(dataItem.AffWithdrawal);
        const balanceBf = formatDecimal(dataItem.BalanceBf);

        return (
            <Row>
                <Cell title={dataItem.UserName}>{dataItem.UserName}</Cell>
                <Cell className="text-right" title={totalClick}>{totalClick}</Cell>
                <Cell className="text-right" title={uniqueClick}>{uniqueClick}</Cell>
                <Cell className="text-right" title={signup}>{signup}</Cell>
                <Cell className="text-right" title={fdm}>{fdm}</Cell>
                <Cell className="text-right" title={activeMember}>{activeMember}</Cell>
                <Cell className="text-right" title={qualifiedMember}>{qualifiedMember}</Cell>
                <Cell className="text-right" title={deposit}>{deposit}</Cell>
                <Cell className="text-right" title={withdrawal}>{withdrawal}</Cell>
                <Cell className={productColumnClassName} title={turnover}>{turnover}</Cell>
                <Cell className={productColumnClassName} title={winlost}>{winlost}</Cell>
                <Cell className="text-right" title={bonus}>{bonus}</Cell>
                <Cell className="text-right" title={platformFee}>{platformFee}</Cell>
                <Cell className="text-right" title={operationalFee}>{operationalFee}</Cell>
                <Cell className="text-right" title={bankingFee}>{bankingFee}</Cell>
                <Cell className="text-right" title={totalFee}>{totalFee}</Cell>
                <Cell className="text-right" title={commRate}>{commRate}</Cell>
                <Cell className="text-right" title={earning}>{earning}</Cell>
                <Cell className="text-right" title={affWithdrawal}>{affWithdrawal}</Cell>
                <Cell className="text-right" title={balanceBf}>{balanceBf}</Cell>
                <Cell className="text-center">{dataItem.AffId > 0 ? 'View' : ''}</Cell>
            </Row>
        )
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
        const { gridData, total } = this.state;

        return gridData && total && (
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(this.state.gridData)}
                    footer={this._renderDataSection([this.state.total])}
                    containerPadding={40}
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
    return bindActionCreators({ setPagingOption, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);