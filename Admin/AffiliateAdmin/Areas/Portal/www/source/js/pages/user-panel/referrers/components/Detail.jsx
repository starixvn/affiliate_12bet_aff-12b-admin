import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { toggleLoader, setPagingOption, fetchDetailData } from '../actions';

export class Detail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, selectedSiteUrl } = this.props;

        if (selectedSiteUrl && selectedSiteUrl !== prevProps.selectedSiteUrl) {
            const param = Object.assign({}, this.props.param, { SelectedSiteUrl: selectedSiteUrl, PageIndex: 1, PageSize: pagingOption.PageSize });
            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchDetailData(param);

        this.setState({ gridData: data ? data.MainData : null });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize });

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={580} title={headerText.WebsiteUrl}>{headerText.WebsiteUrl}</Cell>
                <Cell header={true} colWidth={110} title={headerText.TotalClicks}>{headerText.TotalClicks}</Cell>
                <Cell header={true} colWidth={110} title={headerText.UniqueClicks}>{headerText.UniqueClicks}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Signup}>{headerText.Signup}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Fdm}>{headerText.Fdm}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i));
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

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
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

        const newParam = Object.assign({}, this.props.param, { SelectedSiteUrl: this.props.selectedSiteUrl, ...newPaging });
        this.fetchData(newParam);
    }

    render() {
        const { gridData } = this.state;

        return this.props.selectedSiteUrl && (
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

function mapStateToProps({ affCommonInfo, userPanel }) {
    const { pagingOption, param, selectedSiteUrl } = userPanel;

    return Object.assign({}, { 
        pagingOption,
        selectedSiteUrl,
        param: param && param.AffId ? param : { AffId: affCommonInfo.affId }
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setPagingOption, fetchDetailData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Detail);