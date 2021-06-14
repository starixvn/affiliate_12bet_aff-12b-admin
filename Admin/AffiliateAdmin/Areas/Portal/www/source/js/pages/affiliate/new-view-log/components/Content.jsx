import React, { Fragment, PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, toggleLoader, fetchData, setSelectedAff, setDetailViewParam } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidMount() {
        const { pagingOption, mainViewParam } = this.props;

        if (mainViewParam) {
            const newParam = Object.assign({}, mainViewParam, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    componentDidUpdate(prevProps) {
        const { mainViewParam, pagingOption } = this.props;

        if (mainViewParam && mainViewParam !== prevProps.mainViewParam) {
            const newParam = Object.assign({}, mainViewParam, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchData(param);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoader(false);
    }

    _handleViewDetail = (affId, affName) => {
        const param = Object.assign({}, this.props.mainViewParam, { affId, affName });

        this.props.setDetailViewParam(param);
        this.props.setSelectedAff(affName);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={150} title={headerText.AffName}>{headerText.AffName}</Cell>
                <Cell header={true} colWidth={150} title={headerText.NumberOfAction}>{headerText.NumberOfAction}</Cell>
                <Cell header={true} colWidth={300} title={headerText.LastAction}>{headerText.LastAction}</Cell>
                <Cell header={true} colWidth={150} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Datetime}>{headerText.Datetime}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                dataRows.push(this._buildDataRow(dataList[i], i + startIndex));
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
        const affName = dataItem.AffName;
        const numberOfAction = dataItem.NumberOfAction;
        const updatedBy = htmlDecode(dataItem.Actor);
        const lastAction = htmlDecode(dataItem.LastAction);
        const dateTime = htmlDecode(dataItem.DateTimeText);

        return (
            <Fragment key={`dataRow-${index}`}>
                <Row className="row-data">
                    <Cell title={index} className="text-center">{index}</Cell>
                    <Cell title={affName} className="text-center">{affName}</Cell>
                    <Cell title={numberOfAction} className="text-center">
                        <Button color="link"
                            onClick={() => this._handleViewDetail(dataItem.AffId, affName)}>
                            {numberOfAction}
                        </Button>
                    </Cell>
                    <Cell title={lastAction} className="text-center">{lastAction}</Cell>
                    <Cell title={updatedBy} className="text-center">{updatedBy}</Cell>
                    <Cell title={dateTime} className="text-center">{dateTime}</Cell>
                </Row>
            </Fragment>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newParam = { ...this.props.mainViewParam, pageIndex, pageSize };

        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

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
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ mainViewParam, pagingOption }) {
    return Object.assign({}, { mainViewParam, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, fetchData, setSelectedAff, setDetailViewParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);