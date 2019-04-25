import React, { PureComponent } from 'react';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { formatDecimal } from '../../../../modules/numberFormat';
import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { resolveClientUrl } from '../../../../modules/utils';
import { resource } from '../modules/constants';
import { selectedAllProduct } from '../../../../modules/constants';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            total: null,
            isSelectedAllProducts: true
        }
    }

    componentDidMount() {
        this.fetchData(this.props.param);
    }

    componentDidUpdate(prevProps) {
        if (this.props.param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/StatementHistory/GetDailyEarning';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, Total } = response.data;

            this.setState({ gridData: MainData, total: Total, isSelectedAllProducts: param.ProductId === selectedAllProduct });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title={resource.Date}>{resource.Date}</Cell>
                <Cell header={true} colWidth={100} title={resource.Winlost}>{resource.Winlost}</Cell>
                <Cell header={true} colWidth={100} title={resource.Deposit}>{resource.Deposit}</Cell>
                <Cell header={true} colWidth={100} title={resource.Withdrawal}>{resource.Withdrawal}</Cell>
                <Cell header={true} colWidth={100} title={resource.Bonus}>{resource.Bonus}</Cell>
                <Cell header={true} colWidth={100} title={resource.BankingFee}>{resource.BankingFee}</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList, productColumnClassName) {
        const dataRows = [];
        const isTotalRow = dataList.length === 1 && !dataList[0].Date;

        if (isTotalRow) {
            dataList[0].Date = resource.Total;
        }

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, isTotalRow ? null : i + 1, productColumnClassName));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber, productColumnClassName) {
        const winlost = formatDecimal(dataItem.MemberWinLost);
        const deposit = formatDecimal(dataItem.Deposit);
        const withdrawal = formatDecimal(dataItem.Withdrawal);
        const bonus = formatDecimal(dataItem.Bonus);
        const bankingFee = formatDecimal(dataItem.BankingFee);

        const { param } = this.props;
        const queryString = `AffId=${param.AffId}&AffName=${param.AffName}&ProductId=${param.ProductId}&MonthDate=${dataItem.Date}`;
        const dateLink = resolveClientUrl('Portal/UserPanel/StatementHistory/DailyEarningDetail') + '?' + queryString;

        return (
            <Row>
                {rowNumber && <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>}
                <Cell className="text-center" title={dataItem.Date} colSpan={!rowNumber && 2}>
                    {rowNumber ? (<a href={dateLink}>{dataItem.Date}</a>) : dataItem.Date}
                </Cell>
                <Cell className={productColumnClassName} title={winlost}>{winlost}</Cell>
                <Cell className="text-right" title={deposit}>{deposit}</Cell>
                <Cell className="text-right" title={withdrawal}>{withdrawal}</Cell>
                <Cell className="text-right" title={bonus}>{bonus}</Cell>
                <Cell className="text-right" title={bankingFee}>{bankingFee}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData, total, isSelectedAllProducts } = this.state;
        const productColumnClassName = isSelectedAllProducts ? 'text-right' : 'text-right highlight';

        return gridData && total && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData, productColumnClassName)}
                    footer={this._renderDataSection([total], productColumnClassName)}
                    containerPadding={10}
                />
            </div>
        );
    }
}

function mapStateToProps({ userPanel }) {
    const { param } = userPanel;
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);