import React, { Component } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { formatDecimal } from '../../../../modules/numberFormat';

const totalText = 'Total';

export default class Content extends Component{
    constructor(props) {
        super(props);
    }

    _renderHeader(){
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title="12BET User Name">12BET User Name</Cell>
                <Cell header={true} colWidth={100} title="IBC User Name">IBC User Name</Cell>
                <Cell header={true} colWidth={100} title="Winlost">Winlost</Cell>
                <Cell header={true} colWidth={100} title="Deposit">Deposit</Cell>
                <Cell header={true} colWidth={100} title="Withdrawal">Withdrawal</Cell>
                <Cell header={true} colWidth={100} title="Bonus">Bonus</Cell>
                <Cell header={true} colWidth={100} title="Banking Fee">Banking Fee</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const isTotalRow = dataList.length === 1 && !dataList[0].IbcUserName;

        if(isTotalRow){
            dataList[0].IbcUserName = totalText;
        }

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, isTotalRow ? null : i + 1));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber) {
        const winlost = formatDecimal(dataItem.WinLost);
        const deposit = formatDecimal(dataItem.Deposit);
        const withdrawal = formatDecimal(dataItem.Withdrawal);
        const bonus = formatDecimal(dataItem.Bonus);
        const bankingFee = formatDecimal(dataItem.BankingFee);

        return (
            <Row>
                {rowNumber && <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>}
                {rowNumber && <Cell className="text-center" title={dataItem.UserName}>{dataItem.UserName}</Cell>}
                <Cell className="text-center" title={dataItem.IbcUserName} colSpan={!rowNumber && 3}>{dataItem.IbcUserName}</Cell>
                <Cell className="text-right" title={winlost}>{winlost}</Cell>
                <Cell className="text-right" title={deposit}>{deposit}</Cell>
                <Cell className="text-right" title={withdrawal}>{withdrawal}</Cell>
                <Cell className="text-right" title={bonus}>{bonus}</Cell>
                <Cell className="text-right" title={bankingFee}>{bankingFee}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData, total } = this.props.data;

        return gridData && total && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    footer={this._renderDataSection([total])}
                    containerPadding={10}
                />
            </div>
        );
    }
}