import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource, maxWidth } from '../libs/constants';
import { setPagingOption, toggleLoader, getOnlineList } from '../actions';

export class OnlineList extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidMount() {
        this.fetchData({ pageIndex: 1, pageSize: this.props.pagingOption.PageSize });
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, param } = this.props;

        if (param !== prevProps.param) {
            const newParam = Object.assign({}, param, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        await this.props.fetchData(param);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={35} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={125} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={125} title={headerText.LoggedIn}>{headerText.LoggedIn}</Cell>
                <Cell header={true} colWidth={125} title={headerText.LastRequest}>{headerText.LastRequest}</Cell>
                <Cell header={true} colWidth={125} title={headerText.Ip}>{headerText.Ip}</Cell>
                <Cell header={true} colWidth={125} title={headerText.Role}>{headerText.Role}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="6">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const ipAddress = dataItem.Ip;
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;
        const userName = htmlDecode(dataItem.UserName);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={id} className="text-center">{userName}</Cell>
                <Cell title={dataItem.LoggedInDateText} className="text-center">{dataItem.LoggedInDateText}</Cell>
                <Cell title={dataItem.LastRequestDateText} className="text-center">{dataItem.LastRequestDateText}</Cell>
                <Cell title={ipAddress} className="text-center">
                    <a target="_blank" href={url}>{ipAddress}</a>
                </Cell>
                <Cell title={dataItem.Role} className="text-break">{dataItem.Role}</Cell>
            </Row>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        this.fetchData({ pageIndex, pageSize });
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.props;

        return gridData && (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
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


function mapStateToProps({ pagingOption, gridData, shouldResetScrollPosition }) {
    return Object.assign({}, { pagingOption, gridData, shouldResetScrollPosition });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, getOnlineList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(OnlineList);