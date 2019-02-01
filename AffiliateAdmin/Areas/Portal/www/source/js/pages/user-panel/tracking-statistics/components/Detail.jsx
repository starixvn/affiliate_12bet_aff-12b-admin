import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, resource } from '../modules/constants';

import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Detail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        const { isShowStatisticsDetail, param } = this.props;

        if (param !== prevProps.param && isShowStatisticsDetail) {
            this.fetchData(this.props.param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/TrackingStatistics/GetTrackingStatisticsDetail';
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
                <Cell header={true} colWidth={1000} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={280} title={headerText.TotalClicks}>{headerText.TotalClicks}</Cell>
                <Cell header={true} colWidth={280} title={headerText.UniqueClicks}>{headerText.UniqueClicks}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if(dataList)
        {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                    <Cell className="text-center" colSpan="4">{resource.NoInfo}</Cell>
                </Row>);
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

        return this.props.isShowStatisticsDetail && (
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


    function mapStateToProps({ isShowStatisticsDetail, param }) {
        return Object.assign({}, { isShowStatisticsDetail, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Detail);