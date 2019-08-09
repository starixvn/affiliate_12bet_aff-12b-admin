import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';
import { setPagingOption, toggleLoaderUserPanel, fetchData, setTransactionId, redirectToDailyEarning } from '../actions';
import { headerText, globalResource, transTypeList, transaction, MONTHLY_WINLOST, PAYMENT_REQUEST, selectedAllProduct, maxWidth } from '../modules/constants';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }

        this.transTypeClickHandler = this._getClickHandlerByTransType();
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param, affId } = this.props;

        if (param !== prevProps.param || affId !== prevProps.affId) {
            const newParam = Object.assign({}, param, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoaderUserPanel(true);

        const postParam = { ...param, affId: this.props.affId };
        const data = await this.props.fetchData(postParam);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoaderUserPanel(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={35} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Type}>{headerText.Type}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Amount}>{headerText.Amount}</Cell>
                <Cell header={true} colWidth={80} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Creator}>{headerText.Creator}</Cell>
                <Cell header={true} colWidth={465} title={headerText.Description}>{headerText.Description}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _getClickHandlerByTransType() {
        return {
            [PAYMENT_REQUEST]: (dataItem) => () => {
                return this.handleSetTransactionId(dataItem.TransId);
            },
            [MONTHLY_WINLOST]: (dataItem) => () => {
                const { affId } = this.props;
                const dailyEarningParam = {
                    affId,
                    monthDate: dataItem.DateText,
                    productId: selectedAllProduct
                }

                return this.props.redirectToDailyEarning(dailyEarningParam);
            }
        }
    }

    _buildDataRow(dataItem, index) {
        const amount = formatDecimalWithCurrencySymbol(dataItem.Amount)
        const transType = dataItem.TransType;
        const clickHandler = this.transTypeClickHandler[transType];

        const isPending = dataItem.Status.toLowerCase() === 'pending';

        const status = isPending ? "Processing" : dataItem.Status;

        return (
            <Row key={`dataRow-${index}`} className={status.toLowerCase()}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.DateText} className="text-center">{dataItem.DateText}</Cell>
                <Cell title={transType} className="text-center">{transTypeList[transType] || transaction}</Cell>
                <Cell title={amount} className="text-right">
                    {clickHandler ?
                        (<Button className="p-0" color="link" onClick={clickHandler(dataItem)}>
                            {amount}
                        </Button>) :
                        amount}
                </Cell>
                <Cell title={dataItem.Status} className="text-center">{isPending ?
                    <Button className="p-0" color="link" onClick={() => this.handleSetTransactionId(dataItem.TransId)}>
                        {status}
                        </Button> : status}</Cell>
                <Cell title={dataItem.Creator} className="text-center">{dataItem.Creator}</Cell>
                <Cell title={dataItem.Description}>{dataItem.Description}</Cell>
            </Row>
        )
    }

    handleSetTransactionId = (transId) => {
        this.setState({ shouldResetScrollPosition: false });

        this.props.setTransactionId(transId);
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = { ...this.props.param, pageIndex, pageSize };

        this.props.setPagingOption(newPaging);
        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return gridData && (
            <div className="data-content payment-request">
                <div className="table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        maxWidth={maxWidth}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={10}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ affCommonInfo, statementHistory }) {
    const { pagingOption, param } = statementHistory;
    const { affId } = affCommonInfo;

    return Object.assign({}, { affId, pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoaderUserPanel, fetchData, setTransactionId, redirectToDailyEarning }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);