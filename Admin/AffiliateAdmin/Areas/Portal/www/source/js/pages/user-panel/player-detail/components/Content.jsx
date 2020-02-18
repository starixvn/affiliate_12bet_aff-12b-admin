import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { formatDecimal } from '../../../../modules/numberFormat';
import { headerText, globalResource, selectedAllProduct, maxWidth, SortByIdList } from '../modules/constants';
import { setPagingOption, toggleLoaderUserPanel, fetchData } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';
import SortingCell from './SortingCell';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            isSortAsc: false,
            sortingColumn: 'Turnover'
        }
    }

    componentDidUpdate(prevProps) {
        const { param, pagingOption } = this.props;
        const isDifferent = param !== prevProps.param;

        if (isDifferent) {
            const postParam = { ...param, pageIndex: 1, pageSize: pagingOption.PageSize };
            this._fetchData(postParam);
        }

        if (param.isClickLink || isDifferent) {
            this.setState({ isSortAsc: false, sortingColumn: 'Turnover' });
        }
    }

    async _fetchData(param) {
        this.props.toggleLoaderUserPanel(true);

        const { affId } = this.props;
        const postData = { ...param, affId };
        const data = await this.props.fetchData(postData);

        this.setState({ data: data ? data.MainData : null });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoaderUserPanel(false);
    }

    handleClickHeader = (selectedColumn) => {
        const { isSortAsc, sortingColumn } = this.state;
        const { param, pagingOption } = this.props;
        const postParam = { ...param, pageIndex: pagingOption.PageIndex, pageSize: pagingOption.PageSize, sortById: SortByIdList[selectedColumn] };

        if (sortingColumn === selectedColumn) {
            this.setState({ isSortAsc: !isSortAsc });
            this._fetchData({ ...postParam, isSortAsc: !isSortAsc });

        } else {
            this.setState({ isSortAsc: false, sortingColumn: selectedColumn });
            this._fetchData({ ...postParam, isSortAsc: false });
        }
    }
    _renderHeader() {
        const { isSortAsc, sortingColumn } = this.state;

        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Username'} title={headerText.UserName} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'IbcUsername'} title={headerText.IbcUserName} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'RegisteredDate'} title={headerText.RegisteredDate} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Turnover'} title={headerText.Turnover} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Winloss'} title={headerText.Winloss} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Deposit'} title={headerText.Deposit} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Withdrawal'} title={headerText.Withdrawal} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'Bonus'} title={headerText.Bonus} handleClickHeader={this.handleClickHeader} />
                <SortingCell isSortAsc={isSortAsc} sortingColumn={sortingColumn} columnName={'BankingFee'} title={headerText.BankingFee} handleClickHeader={this.handleClickHeader} />
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const { param } = this.props;
        const productColumnClassName = param.productId === selectedAllProduct ? 'text-right' : 'text-right highlight';
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
        const winloss = formatDecimal(dataItem.Winloss);
        const bonus = formatDecimal(dataItem.Bonus);
        const deposit = formatDecimal(dataItem.Deposit);
        const withdrawal = formatDecimal(dataItem.Withdrawal);
        const bankingFee = formatDecimal(dataItem.BankingFee);
        const userName = htmlDecode(dataItem.UserName);

        return (
            <Row>
                <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>
                <Cell title={userName}>{userName}</Cell>
                <Cell title={dataItem.IbcUserName}>{dataItem.IbcUserName}</Cell>
                <Cell className="text-center" title={dataItem.RegisteredDateText}>{dataItem.RegisteredDateText}</Cell>
                <Cell className={productColumnClassName} title={turnover}>{turnover}</Cell>
                <Cell className={productColumnClassName} title={winloss}>{winloss}</Cell>
                <Cell className="text-right" title={deposit}>{deposit}</Cell>
                <Cell className="text-right" title={withdrawal}>{withdrawal}</Cell>
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

        return data && (
            <div className="table-responsive table-striped my-2 player-detail">
                <Table
                    maxWidth={maxWidth}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(data)}
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