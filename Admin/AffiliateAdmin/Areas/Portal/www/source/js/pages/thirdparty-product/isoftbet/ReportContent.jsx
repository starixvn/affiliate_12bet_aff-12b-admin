import React, { PureComponent } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { formatDecimal } from '../../../modules/numberFormat';

export default class ReportContent extends PureComponent {
    constructor(props) {
        super(props);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100}>PlayerId</Cell>
                <Cell header={true} colWidth={30}>Currency</Cell>
                <Cell header={true} colWidth={100}>SessionId</Cell>
                <Cell header={true} colWidth={40}>GameId</Cell>
                <Cell header={true} colWidth={100}>RoundId</Cell>
                <Cell header={true} colWidth={80}>Time</Cell>
                <Cell header={true} colWidth={100}>Turnover</Cell>
                <Cell header={true} colWidth={100}>Winloss</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i + 1));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber) {
        return (
            <Row>
                <Cell className="text-center">{rowNumber}</Cell>
                <Cell>{dataItem.PlayerId}</Cell>
                <Cell>{dataItem.Currency}</Cell>
                <Cell>{dataItem.SessionId}</Cell>
                <Cell>{dataItem.GameId}</Cell>
                <Cell>{dataItem.RoundId}</Cell>
                <Cell>{new Date(dataItem.Time).toLocaleString()}</Cell>
                <Cell className="text-right">{formatDecimal(dataItem.Turnover)}</Cell>
                <Cell className="text-right">{formatDecimal(dataItem.Winloss)}</Cell>
            </Row>
        )
    }

    _buildTotalRow(total){
        return (
            <Row>
                <Cell className="text-center" colSpan="7">Total</Cell>
                <Cell className="text-right">{formatDecimal(total.Turnover)}</Cell>
                <Cell className="text-right">{formatDecimal(total.Winloss)}</Cell>
            </Row>
        )
    }

    render() {
        const { data } = this.props;
        if (!data || !data.MainData) {
            return null;
        }

        const { MainData, Total } = data;

        return (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(MainData)}
                    footer={this._buildTotalRow(Total)}
                    containerPadding={20}
                />
            </div>
        )
    }
}