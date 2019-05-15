import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { toggleLoader } from '../../../../actions/loaderAction';
import { formatDecimal } from '../../../../modules/numberFormat';
import { resource } from '../modules/constants';
import { selectedAllProduct, maxWidth } from '../../../../modules/constants';
import { fetchData } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            total: null,
            isSelectedAllProducts: true
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        if (param !== prevProps.param) {
            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const { affId } = this.props;
        const postData = { ...param, affId };
        const data = await this.props.fetchData(postData);

        this.setState({
            gridData: data && data.MainData,
            total: data && data.Total,
            isSelectedAllProducts: param.ProductId === selectedAllProduct
        });

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title={resource.UserName12Bet}>{resource.UserName12Bet}</Cell>
                <Cell header={true} colWidth={100} title={resource.IbcUserName}>{resource.IbcUserName}</Cell>
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
        const isTotalRow = dataList.length === 1 && !dataList[0].IbcUserName;

        if (isTotalRow) {
            dataList[0].IbcUserName = resource.Total;
        }

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, isTotalRow ? null : i + 1, productColumnClassName));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber, productColumnClassName) {
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
                    maxWidth={maxWidth}
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

function mapStateToProps({ affCommonInfo, userPanel }) {
    const { param } = userPanel;
    const { affId } = affCommonInfo;

    return Object.assign({}, { affId, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);