import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { post } from '../../../../modules/ajaxRequest';
import { maxWidth } from '../../../../modules/constants';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { formatDecimal } from '../../../../modules/numberFormat';
import { headerText, globalResource } from '../modules/constants';
import { setPagingOption, toggleLoader, fetchData } from '../actions';
import { selectedAllProduct } from '../modules/constants';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param, pagingOption } = this.props;
        if (param !== prevProps.param) {
            const postParam = { ...param, pageIndex: 1, pageSize: pagingOption.PageSize };
            this._fetchData(postParam);
        }
    }

    async _fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchData(param);

        this.setState({ data: { gridData: data ? data.MainData : null } });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.IbcUserName}>{headerText.IbcUserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.RegisteredDateText}>{headerText.RegisteredDateText}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Turnover}>{headerText.Turnover}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Winlost}>{headerText.Winlost}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Deposit}>{headerText.Deposit}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Bonus}>{headerText.Bonus}</Cell>
                <Cell header={true} colWidth={100} title={headerText.BankingFee}>{headerText.BankingFee}</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const { productId } = this.props;
        const productColumnClassName = productId === selectedAllProduct ? 'text-right' : 'text-right highlight';
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, startIndex + i, productColumnClassName));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="9">{globalResource.NoInfo}</Cell>
            </Row>);
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

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        }

        this.props.setPagingOption(newPaging);

        const newParam = { ...this.props.param, pageIndex, pageSize };
        this._fetchData(newParam);
    }

    render() {
        const { pagingOption } = this.props;
        const { data } = this.state;

        return data && data.gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    maxWidth={maxWidth}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(data.gridData)}
                    containerPadding={10}
                    isPaging={true}
                    pageOption={pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}

function mapStateToProps({ userPanel }) {
    const { param, pagingOption } = userPanel;

    return Object.assign({}, { param, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);