import React, { Fragment, PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import RowDetail from './RowDetail';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource, maxWidth } from '../libs/constants';
import { setPagingOption, toggleLoaderUserPanel, fetchData } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param } = this.props;

        if (param && param !== prevProps.param) {
            const newParam = Object.assign({}, param, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoaderUserPanel(true);

        const { affId } = this.props;
        const postData = { ...param, affId };
        const data = await this.props.fetchData(postData);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoaderUserPanel(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={90} title={headerText.Id}>{headerText.Id}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Date}>{headerText.Date}</Cell>
                <Cell header={true} colWidth={150} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={360} title={headerText.Action}>{headerText.Action}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Ip}>{headerText.Ip}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                dataRows.push(this._buildDataRow(dataList[i], i + startIndex));
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
        const id = dataItem.Id;
        const ipAddress = dataItem.Ip;
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        const updatedBy = htmlDecode(dataItem.UpdatedBy);
        const action = htmlDecode(dataItem.Action);
        const oldData = htmlDecode(dataItem.OldData);
        const newData = htmlDecode(dataItem.NewData);

        return (
            <Fragment key={`dataRow-${index}`}>
                <Row className="row-data">
                    <Cell title={index} className="text-center">{index}</Cell>
                    <Cell title={id} className="text-center">{id}</Cell>
                    <Cell title={dataItem.DateText} className="text-center">{dataItem.DateText}</Cell>
                    <Cell title={updatedBy} className="text-center">{updatedBy}</Cell>
                    <Cell title={action} className="text-center">{action}</Cell>
                    <Cell title={ipAddress} className="text-center">
                        <a target="_blank" href={url}>{ipAddress}</a>
                    </Cell>
                </Row>
                {
                    (oldData || newData) && (
                        <Row className="row-detail-group">
                            <Cell colSpan="6" className="p-0">
                                <RowDetail oldData={oldData} newData={newData} />
                            </Cell>
                        </Row>
                    )
                }
            </Fragment>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newParam = { ...this.props.param, pageIndex, pageSize };

        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return gridData && (
            <div className="data-content new-view-log">
                <div className="table-responsive table-striped new-view-log-table my-2">
                    <Table
                        maxWidth={maxWidth}
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


function mapStateToProps({ affCommonInfo, newViewLog }) {
    const { pagingOption, param } = newViewLog;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        affId,
        pagingOption,
        param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoaderUserPanel, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);