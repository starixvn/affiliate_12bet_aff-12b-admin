import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { formatDateTime } from '../../../../modules/utils';
import { defaultDatetimeFormat } from '../../../../modules/constants';
import { resource, globalResource } from '../libs/constants';
import { setPagingOption, fetchData, setParam, toggleLoader } from '../actions';

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
        const data = await this.props.fetchData(param);

        if (data) {
            const { MainData, TotalItem } = data;

            this.setState({ gridData: MainData });
            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
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
                <Cell header={true} colWidth={125} title={resource.AffName}>{resource.AffName}</Cell>
                <Cell header={true} colWidth={150} title={resource.SentDate}>{resource.SentDate}</Cell>
                <Cell header={true} colWidth={100} title={resource.RateName}>{resource.RateName}</Cell>
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
        const sentDate = formatDateTime(dataItem.SentDate, defaultDatetimeFormat);

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
        const { gridData } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="table-responsive table-striped">
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
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param }) {
    return Object.assign({}, { pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setParam, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);