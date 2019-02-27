import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption, toggleLoader, setShowGroup } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            idObject: {},
            gridData: [],
            shouldResetScrollPosition: true
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

            this.setState({ gridData: MainData, shouldResetScrollPosition: true });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }
        this.props.toggleLoader(false);
    }

    handleShowGroup = (id) => {
        const { idObject } = this.state;
        this.setState({ shouldResetScrollPosition: false });

        if(this.props.isShowGroup) {
            if(idObject.lenght == 0) {
                this.props.setShowGroup(false);
            } else {
                var isContain = false;
                Object.keys(idObject).map(idItem => {
                    if(id == idItem) {
                        isContain = true;
                    }  
                });

                isContain ? this.setState({ idObject: Object.assign({}, delete idObject.id) }) : this.setState({ idObject: Object.assign({}, idObject, { id }) });
            }
        } else {
            this.props.setShowGroup(true);
            this.setState({ idObject: Object.assign({}, idObject, { id }) });
        }
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

        if(dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                var isGroup = false;
                var isHide = false;
                const dataItem = dataList[i];
                const dataPreviousItem = i == 0 ? {} : dataList[i - 1];
                const dataNextItem = i == dataList.length - 1 ? {} : dataList[i + 1];

                if ( dataItem.Id == dataPreviousItem.Id) {
                    isHide = true;
                }

                if ( dataItem.Id != dataPreviousItem.Id && dataItem.Id == dataNextItem.Id ) {
                    isGroup = true;
                }

                dataRows.push(this._buildDataRow(dataItem, i + startIndex, isGroup, isHide));
            }
        }
        else {
            dataRows.push(<Row>
                    <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
                </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, isGroup, isHide) {
        var url = "http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=&token=&toolhandler_redirect=0&ip={dataItem.Ip}j=1";
        var isContain = false;
        Object.keys(idObject).map(idItem => {
            if(dataItem.Id == idItem) {
                isContain = true;
            }  
        })


        return (
            (isHide ? (this.props.isShowGroup && isContain): true) && <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Id} className="text-center">
                    { isGroup ? <Button color="link" onClick={() => this.handleShowGroup(dataItem.Id)}>
                                    {dataItem.Id}
                               </Button> : dataItem.Id }
                </Cell>
                <Cell title={dataItem.DateText}>{dataItem.DateText}</Cell>
                <Cell title={dataItem.UpdatedBy} className="text-center">{dataItem.UpdatedBy}</Cell>
                <Cell title={dataItem.Description}>{dataItem.Description} <br /> ({dataItem.SpName})</Cell>
                <Cell title={dataItem.Details}>{dataItem.Details}</Cell>
                <Cell title={dataItem.Ip} className="text-center">
                    <a target="_blank" href={url.replace('{dataItem.Ip}', dataItem.Ip)}>{dataItem.Ip}</a>
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


function mapStateToProps({ pagingOption, param, isShowGroup }) {
    return Object.assign({}, { pagingOption, param, isShowGroup });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, setShowGroup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);