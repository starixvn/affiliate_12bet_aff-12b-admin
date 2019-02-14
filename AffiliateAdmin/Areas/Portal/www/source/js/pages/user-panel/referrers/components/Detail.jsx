import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader, setPagingOption } from '../actions';

export class Detail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param, siteUrl } = this.props;

        if (siteUrl !== prevProps.siteUrl) {
            const newParam = Object.assign({}, this.props.param, { SiteUrl: siteUrl, PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/Referrers/GetReferrersBySite';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;

            this.setState({ gridData: MainData });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={600} title={headerText.WebsiteUrl}>{headerText.WebsiteUrl}</Cell>
                <Cell header={true} colWidth={100} title={headerText.TotalClicks}>{headerText.TotalClicks}</Cell>
                <Cell header={true} colWidth={100} title={headerText.UniqueClicks}>{headerText.UniqueClicks}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Signups}>{headerText.Signups}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Fdm}>{headerText.Fdm}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1));
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

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, { SiteUrl: this.props.siteUrl, ...newPaging });
        this.props.getNoteList(newParam);
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row>
                <Cell title={dataItem.WebsiteUrl}>{dataItem.WebsiteUrl}</Cell>
                <Cell title={dataItem.TotalClicks} className="text-right">{dataItem.TotalClicks}</Cell>
                <Cell title={dataItem.UniqueClicks} className="text-right">{dataItem.UniqueClicks}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return this.props.siteUrl && (
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
                />
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, siteUrl }) {
    return Object.assign({}, { pagingOption, param, siteUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setPagingOption }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Detail);