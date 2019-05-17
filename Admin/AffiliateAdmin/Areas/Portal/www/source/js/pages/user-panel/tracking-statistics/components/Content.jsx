import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { setSelectedMonth, fetchData } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const { affId } = this.props;
        const postParam = { ...param, affId };
        const data = await this.props.fetchData(postParam);

        this.setState({ gridData: data });
        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key='thead'>
                <Cell header={true} colWidth={40} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={600} title={headerText.Month}>{headerText.Month}</Cell>
                <Cell header={true} colWidth={180} title={headerText.TotalClicks}>{headerText.TotalClicks}</Cell>
                <Cell header={true} colWidth={180} title={headerText.UniqueClicks}>{headerText.UniqueClicks}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i + 1));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.MonthText} className="text-center">
                    <Button color="link" className="p-0" onClick={() => this.props.setSelectedMonth(dataItem.MonthText)}>
                        {dataItem.MonthText}
                    </Button>
                </Cell>
                <Cell title={dataItem.TotalClick} className="text-center">{dataItem.TotalClick}</Cell>
                <Cell title={dataItem.UniqueClick} className="text-center">{dataItem.UniqueClick}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.state;

        return gridData && (
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


function mapStateToProps({ affCommonInfo, trackingStatistics }) {
    const { param } = trackingStatistics;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        affId,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, setSelectedMonth, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);