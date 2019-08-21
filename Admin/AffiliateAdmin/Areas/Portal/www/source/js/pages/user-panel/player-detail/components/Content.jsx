import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { formatDecimal } from '../../../../modules/numberFormat';
import { headerText, globalResource, selectedAllProduct, maxWidth } from '../modules/constants';
import { setPagingOption, toggleLoaderUserPanel, fetchData } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';

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
        this.props.toggleLoaderUserPanel(true);

        const { affId } = this.props;
        const postData = { ...param, affId };
        const data = await this.props.fetchData(postData);

        this.setState({ data: { gridData: data ? data.MainData : null } });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoaderUserPanel(false);
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
        const userName = htmlDecode(dataItem.UserName);

        return (
            <Row>
                <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>
                <Cell title={userName}>{userName}</Cell>
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
        const { param } = this.props;

        this.props.setPagingOption(newPaging);

        const newParam = { productId: param.productId, monthDate: param.monthDate, pageIndex, pageSize };
        this._fetchData(newParam);
    }

    render() {
        const { pagingOption } = this.props;
        const { data } = this.state;

        return data && data.gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
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

function mapStateToProps({ affCommonInfo, playerDetail }) {
    const { param, pagingOption } = playerDetail;
    const { affId } = affCommonInfo;

    return Object.assign({}, { param, pagingOption, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoaderUserPanel, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);