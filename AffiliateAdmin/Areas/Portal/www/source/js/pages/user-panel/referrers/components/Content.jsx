import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setSiteUrl } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidMount() {
        this.fetchData(this.props.param);
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/ClickStatisticsNew/GetClickStatisticsNewAllMonths';
        const response = await post(url, param);
        if (response && response.status === 200) {
            this.setState({ gridData: response.data });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={600} title={headerText.SiteUrls}>{headerText.SiteUrls}</Cell>
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

    _buildDataRow(dataItem, index) {
        return (
            <Row>
                <Cell title={dataItem.SiteUrls}>
                    <Button color="link" className="p-0" onClick={() => this.props.setSiteUrl(dataItem.SiteUrls)}>
                        {dataItem.SiteUrls}
                    </Button>
                </Cell>
                <Cell title={dataItem.TotalClicks} className="text-right">{dataItem.TotalClicks}</Cell>
                <Cell title={dataItem.UniqueClicks} className="text-right">{dataItem.UniqueClicks}</Cell>
                <Cell title={dataItem.Signups} className="text-right">{dataItem.Signups}</Cell>
                <Cell title={dataItem.Fdm} className="text-right">{dataItem.Fdm}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
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


function mapStateToProps({ param }) {
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setSiteUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);