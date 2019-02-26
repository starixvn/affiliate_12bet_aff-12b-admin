import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption, toggleLoader } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: []
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param } = this.props;

        if (param !== prevProps.param) {
            const newParam = Object.assign({}, param, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);
        const url = 'Portal/UserPanel/ViewLog/GetViewLogList';
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
            <Row key="theard">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={40} title={headerText.Id}>{headerText.Id}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={110} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Action}>{headerText.Action}</Cell>
                <Cell header={true} colWidth={500} title={headerText.Description}>{headerText.Description}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Ip}>{headerText.Ip}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if(dataList.length)
        {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                    <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
                </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Id} className="text-center">{dataItem.Id}</Cell>
                <Cell title={dataItem.DateText}>{dataItem.DateText}</Cell>
                <Cell title={dataItem.UpdatedBy} className="text-center">{dataItem.UpdatedBy}</Cell>
                <Cell title={dataItem.Description}>{dataItem.Description} <br /> ({dataItem.SpName})</Cell>
                <Cell title={dataItem.Details}>{dataItem.Details}</Cell>
                <Cell title={dataItem.Ip} className="text-center">
                    <a target="_blank" href="http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&amp;token=&amp;toolhandler_redirect=0&amp;ip=${dataItem.Ip}&amp;j=1">&nbsp;{dataItem.Ip}</a>
                </Cell>
            </Row>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.param, newPaging);
        console.log(newParam);
        this.fetchData(newParam);
    };

    render() {
        return (
            <div className="data-content">
                <div className="table-responsive table-striped">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(this.state.gridData)}
                        containerPadding={10}
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
    return bindActionCreators({ setPagingOption, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);