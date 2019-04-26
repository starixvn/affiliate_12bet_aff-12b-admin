import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { setSelectedSiteUrl, toggleLoader, fetchData } from '../actions';

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
        this.props.toggleLoader(true);

        const { affId } = this.props;
        const postData = { ...param, affId };
        const data = await this.props.fetchData(postData);

        this.setState({ gridData: data });
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key='thead'>
                <Cell header={true} colWidth={580} title={headerText.SiteUrls}>{headerText.SiteUrls}</Cell>
                <Cell header={true} colWidth={110} title={headerText.TotalClick}>{headerText.TotalClick}</Cell>
                <Cell header={true} colWidth={110} title={headerText.UniqueClick}>{headerText.UniqueClick}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Signups}>{headerText.Signups}</Cell>
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
                <Cell title={dataItem.SiteUrl}>
                    {(dataItem.SiteUrl == "Old URL" || dataItem.SiteUrl == "none") ? dataItem.SiteUrl : (<Button color="link" className="p-0" onClick={() => this.props.setSelectedSiteUrl(dataItem.SiteUrl)}>
                        {dataItem.SiteUrl}
                    </Button>)}
                </Cell>
                <Cell title={dataItem.TotalClick} className="text-right">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-right">{dataItem.UniqueClick}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return gridData && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1010}
                    autoWidth={true}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                />
            </div>
        );
    }
}


function mapStateToProps({ affCommonInfo, userPanel }) {
    const { param } = userPanel;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        affId,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setSelectedSiteUrl, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);