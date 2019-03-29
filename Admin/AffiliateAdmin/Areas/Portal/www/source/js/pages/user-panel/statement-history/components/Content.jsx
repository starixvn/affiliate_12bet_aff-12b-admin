import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, transTypeList, transaction, MONTHLY_WINLOST, PAYMENT_REQUEST } from '../modules/constants';
import { setPagingOption, toggleLoader, fetchData, setTransactionId } from '../actions';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';
import { resolveClientUrl } from '../../../../modules/utils';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param } = this.props;

        if (param !== prevProps.param) {
            const newParam = Object.assign({}, param, { PageIndex: 1, PageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchData(param);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize });

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={25} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Type}>{headerText.Type}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Amount}>{headerText.Amount}</Cell>
                <Cell header={true} colWidth={65} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Creator}>{headerText.Creator}</Cell>
                <Cell header={true} colWidth={500} title={headerText.Description}>{headerText.Description}</Cell>
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

    _buildDataRow(dataItem, index) {
        const { param } = this.props;
        const amount = formatDecimalWithCurrencySymbol(dataItem.Amount)
        const transType = dataItem.TransType;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.DateText} className="text-center">{dataItem.DateText}</Cell>
                <Cell title={transType} className="text-center">{transTypeList[transType] || transaction}</Cell>
                <Cell title={amount} className="text-right">
                    {transType === PAYMENT_REQUEST ? <Button className="p-0" color="link" onClick={() => this.handleSetTransactionId(dataItem.TransId)}>
                        {amount}
                    </Button> : (transType === MONTHLY_WINLOST ?
                        <a href={`${resolveClientUrl('Portal/UserPanel/StatementHistory/DailyEarning')}` +
                            `?affId=${param.AffId}&affName=${param.AffName}&monthDate=${dataItem.DateText}`} target="fContent">
                            {amount}
                        </a> : amount)
                    }
                </Cell>
                <Cell title={dataItem.Status} className="text-center">{dataItem.Status === 'pending' ?
                    <Button className="p-0" color="link" onClick={() => this.handleSetTransactionId(dataItem.TransId)}>
                        Processing
                        </Button> : dataItem.Status}</Cell>
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
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
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


function mapStateToProps({ pagingOption, param }) {
    return Object.assign({}, { pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, fetchData, setTransactionId }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);