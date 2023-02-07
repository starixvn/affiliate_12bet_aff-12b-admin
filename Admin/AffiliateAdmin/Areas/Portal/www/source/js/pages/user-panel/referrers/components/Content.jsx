import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, maxWidth } from '../libs/constants';
import { setSelectedSiteDetailFilter, toggleLoaderUserPanel, fetchData } from '../actions';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoaderUserPanel(true);

        const data = await this.props.fetchData(param);

        this.setState({ gridData: data });
        this.props.toggleLoaderUserPanel(false);
    }

    _renderHeader() {
        return (
            <Row key='thead'>
                <Cell header={true} colWidth={370} title={headerText.SiteUrls}>{headerText.SiteUrls}</Cell>
                <Cell header={true} colWidth={210} title={headerText.Country}>{headerText.Country}</Cell>
                <Cell header={true} colWidth={110} title={headerText.TotalClick}>{headerText.TotalClick}</Cell>
                <Cell header={true} colWidth={110} title={headerText.UniqueClick}>{headerText.UniqueClick}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Signups}>{headerText.Signups}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Fdm}>{headerText.Fdm}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i));
            }
        }
        else {
            dataRows.push(
                <Row>
                    <Cell className="text-center" colSpan="6">{globalResource.NoInfo}</Cell>
                </Row>
            );
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const siteUrl = htmlDecode(dataItem.SiteUrl);

        return siteUrl.toLowerCase() == "old url" ? null : (
            <Row key={`dataRow-${index}`}>
                <Cell title={siteUrl}>
                    {siteUrl.toLowerCase() == "none" ? siteUrl : (<Button color="link" className="p-0" onClick={() => this.handleSelectedSiteDetailFilter(dataItem.SiteUrl, dataItem.CountryCode)}>
                        {siteUrl}
                    </Button>)}
                </Cell>
                <Cell title={dataItem.Country} className="text-center">{dataItem.Country}</Cell>
                <Cell title={dataItem.TotalClick} className="text-right">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-right">{dataItem.UniqueClick}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    handleSelectedSiteDetailFilter = (selectedSiteUrl, selectedCountry) => {
        this.props.setSelectedSiteDetailFilter({ selectedSiteUrl, selectedCountry });
    }

    render() {
        const { gridData } = this.state;

        return gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
                    maxWidth={maxWidth}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                />
            </div>
        );
    }
}


function mapStateToProps({ clickStatistics }) {
    const { param } = clickStatistics;

    return Object.assign({}, {
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoaderUserPanel, setSelectedSiteDetailFilter, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);