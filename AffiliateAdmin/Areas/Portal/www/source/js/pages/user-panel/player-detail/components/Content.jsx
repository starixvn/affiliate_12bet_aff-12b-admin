import React, { Component } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { formatDecimal } from '../../../../modules/numberFormat';

export default class Content extends Component {
    constructor(props) {
        super(props);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title="User Name">User Name</Cell>
                <Cell header={true} colWidth={100} title="IBC User Name">IBC User Name</Cell>
                <Cell header={true} colWidth={100} title="Registered Date">Registered Date</Cell>
                <Cell header={true} colWidth={100} title="Turnover">Turnover</Cell>
                <Cell header={true} colWidth={100} title="Winlost">Winlost</Cell>
                <Cell header={true} colWidth={100} title="Deposit">Deposit</Cell>
                <Cell header={true} colWidth={100} title="Bonus">Bonus</Cell>
                <Cell header={true} colWidth={100} title="Banking Fee">Banking Fee</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList, productColumnClassName) {
        const dataRows = [];

        const { pagingOption } = this.props;
        const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, startIndex + i, productColumnClassName));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber, productColumnClassName) {
        const turnover = formatDecimal(dataItem.Turnover);
        const winlost = formatDecimal(dataItem.Winlost);
        const deposit = formatDecimal(dataItem.Deposit);
        const bonus = formatDecimal(dataItem.Bonus);
        const bankingFee = formatDecimal(dataItem.BankingFee);

        return (
            <Row>
                <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName}</Cell>
                <Cell title={dataItem.IbcUserName}>{dataItem.IbcUserName}</Cell>
                <Cell className="text-center" title={dataItem.RegisteredDateText}>{dataItem.RegisteredDateText}</Cell>
                <Cell className={productColumnClassName} title={turnover}>{turnover}</Cell>
                <Cell className={productColumnClassName} title={winlost}>{winlost}</Cell>
                <Cell className="text-right" title={deposit}>{deposit}</Cell>
                <Cell className="text-right" title={bonus}>{bonus}</Cell>
                <Cell className="text-right" title={bankingFee}>{bankingFee}</Cell>
            </Row>
        )
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        }

        this.props.onPaging(newPaging);
    }

    render() {
        const { pagingOption, data, productColumnClassName } = this.props;
        const { gridData } = data;

        return gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData, productColumnClassName)}
                    containerPadding={10}
                    isPaging={true}
                    pageOption={pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}