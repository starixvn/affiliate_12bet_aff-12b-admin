import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Detail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidUpdate(prevProps) {
        const { param, selectedMonth } = this.props;

        if (selectedMonth !== prevProps.selectedMonth) {
            const dateTime = new Date(selectedMonth);
            const newParam = Object.assign({}, this.props.param, { SelectedMonth: dateTime });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/TrackingStatistics/GetTrackingStatisticsByMonth';
        const response = await post(url, param);
        if (response && response.status === 200) {
            this.setState({ gridData: response.data });
        }
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={40} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={600} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={180} title={headerText.TotalClicks}>{headerText.TotalClicks}</Cell>
                <Cell header={true} colWidth={180} title={headerText.UniqueClicks}>{headerText.UniqueClicks}</Cell>
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
                    <Cell className="text-center" colSpan="4">{globalResource.NoInfo}</Cell>
                </Row>
            );
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.DateText} className="text-center">{dataItem.DateText}</Cell>
                <Cell title={dataItem.TotalClick} className="text-center">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-center">{dataItem.UniqueClick}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return this.props.selectedMonth && (
            <div className="table-responsive table-striped my-2">
                <Table
                    width={1020}
                    autoWidth={false}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(gridData)}
                    containerPadding={10}
                />
            </div>
        );
    }
}


function mapStateToProps({ param, selectedMonth }) {
    return Object.assign({}, { param, selectedMonth });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Detail);