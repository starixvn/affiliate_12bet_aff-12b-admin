import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { setPagingOption, toggleLoader, setShowIdList, fetchData } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param } = this.props;

        if (param !== prevProps.param) {
            const newParam = Object.assign({}, param, { PageIndex: 1, PageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchData(param);

        if (data) {
            const { MainData, TotalItem } = data;

            this.setState({ gridData: MainData, shouldResetScrollPosition: true });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }

        this.props.toggleLoader(false);
    }

    handleShow = (id) => {
        const { showIdList } = this.props;
        this.setState({ shouldResetScrollPosition: false });

        this.props.setShowIdList(Object.assign({}, showIdList, { [id]: !showIdList[id] }));
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={25} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={55} title={headerText.Id}>{headerText.Id}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={100} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={110} title={headerText.Action}>{headerText.Action}</Cell>
                <Cell header={true} colWidth={500} title={headerText.Description}>{headerText.Description}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Ip}>{headerText.Ip}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                let isLink = false;
                let isHide = false;

                const dataItem = dataList[i];
                const dataPreviousItem = i == 0 ? {} : dataList[i - 1];
                const dataNextItem = i == dataList.length - 1 ? {} : dataList[i + 1];

                if (dataItem.Id == dataPreviousItem.Id) {
                    isHide = true;
                } else if (dataItem.Id == dataNextItem.Id) {
                    isLink = true;
                }

                dataRows.push(this._buildDataRow(dataItem, i + startIndex, isLink, isHide));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, isLink, isHide) {
        const id = dataItem.Id;
        const ipAddress = dataItem.Ip;
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        if (isHide && !this.props.showIdList[id]) {
            return null;
        }

        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={id} className="text-center">
                    {isLink ? <Button color="link" onClick={() => this.handleShow(id)}>
                        {id}
                    </Button> : id}
                </Cell>
                <Cell title={dataItem.DateText}>{dataItem.DateText}</Cell>
                <Cell title={dataItem.UpdatedBy} className="text-center">{dataItem.UpdatedBy}</Cell>
                <Cell title={dataItem.Description}>{dataItem.Description} <br /> ({dataItem.SpName})</Cell>
                <Cell title={dataItem.Details}>{dataItem.Details}</Cell>
                <Cell title={ipAddress} className="text-center">
                    <a target="_blank" href={url}>{ipAddress}</a>
                </Cell>
            </Row>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.fetchData(newParam);
        this.props.setShowIdList({});
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return (
            <div className="data-content">
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
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, showIdList }) {
    return Object.assign({}, { pagingOption, param, showIdList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, setShowIdList, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);