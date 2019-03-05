import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerTitle } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader, setPagingOption, fetchDetailData } from '../actions';

export class Detail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, selectedPage } = this.props;

        if (selectedPage !== prevProps.selectedPage) {
            const param = Object.assign({}, this.props.param, { PageIndex: 1, PageSize: pagingOption.PageSize, ...selectedPage });

            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchDetailData(param);

        if (data) {
            const { MainData, TotalItem } = data;

            this.setState({ gridData: MainData });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={580} title={headerTitle.WebsiteUrl}>{headerTitle.WebsiteUrl}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.TotalClicks}>{headerTitle.TotalClicks}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.UniqueClicks}>{headerTitle.UniqueClicks}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.Signup}>{headerTitle.Signup}</Cell>
                <Cell header={true} colWidth={110} title={headerTitle.Fdm}>{headerTitle.Fdm}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem));
            }
        }
        else {
            dataRows.push(
                <Row>
                    <Cell className="text-center" colSpan="5">{globalResource.NoInfo}</Cell>
                </Row>
            );
        }

        return dataRows;
    }

    _buildDataRow(dataItem) {
        return (
            <Row>
                <Cell title={dataItem.WebsiteUrl}>{dataItem.WebsiteUrl}</Cell>
                <Cell title={dataItem.TotalClick} className="text-right">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-right">{dataItem.UniqueClick}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, { SelectedPage: this.props.selectedPage, ...newPaging });
        this.fetchData(newParam);
    }

    render() {
        const { gridData } = this.state;

        if(Object.keys(this.props.selectedPage).length === 0) {
            return null;
        }
        return (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1010}
                    autoWidth={true}
                    autoHeight={false}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                    isPaging={true}
                    pageOption={this.props.pagingOption}
                    onPaging={this.handlePaging}
                />
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, selectedPage }) {
    return Object.assign({}, { pagingOption, param, selectedPage });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setPagingOption, fetchDetailData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Detail);