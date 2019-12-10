import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row, Table } from 'reactstrap';

import Panel from './Panel';
import { getPSPToday, getDepositInfo } from '../actions';
import { formatDecimalWithCurrencySymbol, format } from '../../../modules/numberFormat';

export class MemberStatistics extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this._handleGetData();
    }

    _handleGetData = async () => {
        await this.props.getPSPToday();
        await this.props.getDepositInfo();
    }

    _renderData(generalInfo, pSPToday, isUpToNow) {
        return (
            <tbody>
                {this._buildRow('Total Deposits', formatDecimalWithCurrencySymbol(isUpToNow ? generalInfo.AllDepositNumber : generalInfo.AllDepositMonthNumber))}
                {this._buildRow('Total PSP fees', formatDecimalWithCurrencySymbol(isUpToNow ? (generalInfo.AllPSPFeeNumber + pSPToday) : (generalInfo.AllPSPFeeMonthNumber + pSPToday)))}
                {this._buildRow('Total Winlost', formatDecimalWithCurrencySymbol(isUpToNow ? generalInfo.AllWinlostNumber : generalInfo.AllWinlostMonthNumber))}
                {this._buildRow('Total Bonus', formatDecimalWithCurrencySymbol(isUpToNow ? generalInfo.AllBonusNumber : generalInfo.AllBonusMonthNumber))}
                <tr>
                    <td className="title-total"><strong>Total Net Winlost</strong></td>
                    <td className="content-total"><strong>{formatDecimalWithCurrencySymbol(isUpToNow ? generalInfo.AllNetWinlostNumber : generalInfo.AllNetWinlostMonthNumber)}</strong></td>
                </tr>
            </tbody>
        );
    }

    _buildRow(title, data) {
        return (
            <tr key={title}>
                <td className="title">{title}</td>
                <td className="content">{data}</td>
            </tr>
        )
    }

    render() {
        const { isUpToNow, generalInfo, depositInfo, pSPToday } = this.props;

        return (
            <Panel heading={`Member statistics (${isUpToNow ? 'up to now' : 'this month'})`} className={"icon-home02"}>
                <Row>
                    <Col sm={12} className="pl-4">
                        <span>We have <strong>{format(isUpToNow ? depositInfo.AllMemberDepositUpToNow : depositInfo.AllMemberDepositThisMonth)}</strong> members in the program</span>
                    </Col>
                    <Col sm={12} className="pl-4">
                        <span><strong>{format(isUpToNow ? generalInfo.AllMemberDepositNumber : generalInfo.AllMemberDepositMonthNumber)}</strong> Deposit / <strong>{format(isUpToNow ? (depositInfo.AllMemberDepositUpToNow - generalInfo.AllMemberDepositNumber) : (depositInfo.AllMemberDepositThisMonth - generalInfo.AllMemberDepositMonthNumber))}</strong> Non-Deposit</span>
                    </Col>
                    <Col sm={12}>
                        <Table className="member-statistics-table" sm={12}>
                            {this._renderData(generalInfo, pSPToday, isUpToNow)}
                        </Table>
                    </Col>
                </Row>
            </Panel>
        );
    }
}

function mapStateToProps({ generalInfo, pSPToday, depositInfo }) {
    return Object.assign({}, { generalInfo, pSPToday, depositInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getPSPToday, getDepositInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(MemberStatistics);