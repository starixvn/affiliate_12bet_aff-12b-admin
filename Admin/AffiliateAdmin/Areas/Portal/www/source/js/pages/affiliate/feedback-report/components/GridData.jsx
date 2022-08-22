import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { formatDateTime } from '../../../../modules/utils';
import { resource, globalResource } from '../libs/constants';
import { setPagingOption, fetchData, setShouldResetScrollPosition, setParam } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.props.fetchData(param);
        }
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.props.setParam(newParam);
    };

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={35} title={resource.Index}>{resource.Index}</Cell>
                <Cell header={true} colWidth={125} title={resource.Affiliate}>{resource.Affiliate}</Cell>
                <Cell header={true} colWidth={150} title={resource.SentDate}>{resource.SentDate}</Cell>
                <Cell header={true} colWidth={100} title={resource.Rate}>{resource.Rate}</Cell>
                <Cell header={true} colWidth={600} title={resource.Message}>{resource.Message}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="5">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const message = htmlDecode(dataItem.Message);
        const sentDate = formatDateTime(dataItem.SentDate, "MM/dd/yyyy HH:mm:ss");

        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.AffName}>{dataItem.AffName}</Cell>
                <Cell title={sentDate} className="text-center">{sentDate}</Cell>
                <Cell title={dataItem.RateName} className="text-center">{dataItem.RateName}</Cell>
                <Cell>{message}</Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                        shouldResetScrollPosition={this.props.shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);