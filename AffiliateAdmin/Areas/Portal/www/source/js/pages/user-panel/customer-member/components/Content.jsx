import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { post } from '../../../../modules/ajaxRequest';
import { defaultPagingOption } from '../../../../modules/constants';

import { resource } from '../modules/constants';
import { toggleLoader } from '../actions';

export class Content extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            pagingOption: Object.assign({}, defaultPagingOption)
        }
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.props.param !== prevProps.param) {
            const pagingParam = {
                PageIndex: this.props.param.PageIndex,
                PageSize: this.state.pagingOption.PageSize
            }

            this._setStateAndFetchData(pagingParam);
        }
    }

    fetchData = async (param) => {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/CustomerMember/GetCustomerMember';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;
            const { pagingOption } = this.state;

            this.setState({
                data: MainData,
                pagingOption: Object.assign({}, pagingOption, { TotalItem: TotalItem })
            });
        }

        this.props.toggleLoader(false);
    }

    handlePaging = (index, size) => {
        const pagingParam = {
            PageIndex: index,
            PageSize: size
        }

        this._setStateAndFetchData(pagingParam);
    }

    _setStateAndFetchData = (pagingParam) => {
        const { pagingOption } = this.state;
        this.setState({
            pagingOption: Object.assign({}, pagingOption, pagingParam)
        });

        const param = Object.assign({}, this.props.param, pagingParam);
        this.fetchData(param);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={100} title={resource.UserName}>{resource.UserName}</Cell>
                <Cell header={true} colWidth={100} title={resource.FirstName}>{resource.FirstName}</Cell>
                <Cell header={true} colWidth={100} title={resource.LastName}>{resource.LastName}</Cell>
                {this.props.canViewMemberEmail && (<Cell header={true} colWidth={100} title={resource.EmailAddress}>{resource.EmailAddress}</Cell>)}
                <Cell header={true} colWidth={50} title={resource.Currency}>{resource.Currency}</Cell>
                <Cell header={true} colWidth={100} title={resource.RegisterDate}>{resource.RegisterDate}</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList, canViewMemberEmail) {
        const { pagingOption } = this.state;
        const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, startIndex + i, canViewMemberEmail));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber) {
        return (
            <Row>
                <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName}</Cell>
                <Cell title={dataItem.FirstName}>{dataItem.FirstName}</Cell>
                <Cell title={dataItem.LastName}>{dataItem.LastName}</Cell>
                {this.props.canViewMemberEmail && (<Cell title={dataItem.Email}>{dataItem.Email}</Cell>)}
                <Cell className="text-center" title={dataItem.CurrencyName}>{dataItem.CurrencyName}</Cell>
                <Cell className="text-center" title={dataItem.RegisteredDateString}>{dataItem.RegisteredDateString}</Cell>
            </Row>
        )
    }

    render() {
        const { data, pagingOption } = this.state;

        return data && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(data)}
                    containerPadding={10}
                    isPaging={true}
                    pageOption={pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        )
    }
}

function mapStateToProps({ param, canViewMemberEmail }) {
    return Object.assign({}, { param, canViewMemberEmail });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);

