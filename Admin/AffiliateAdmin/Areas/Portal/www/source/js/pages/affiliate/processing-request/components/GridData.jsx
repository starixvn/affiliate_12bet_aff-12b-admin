import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, globalResource, statusList, colorByStatus } from '../libs/constants';
import { setPagingOption, fetchData } from '../actions';
import { resolveClientUrl, formatDateTime } from '../../../../modules/utils';
import { Claim, claimResource } from '../../../../modules/claim';
import { defaultDatetimeFormat } from '../../../../modules/constants';
import { setSelectedRequestId } from '../actions';

export class GridData extends Component {
    constructor(props) {
        super(props);

        const today = new Date(this.props.today)

        this.state = {
            todayTime: today.getTime(),
            messageNotification: ''
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param && param !== prevProps.param) {
            const { pagingOption } = this.props;
            const updateParam = Object.assign({}, param, { PageIndex: 1, PageSize: pagingOption.PageSize });

            this.props.fetchData(updateParam);
        }
    }

    handleDeleteProcess = (selectedId) => {
        this.props.setSelectedRequestId(selectedId);
        this.props.handleDelete();
    }

    handleEditProcess = (selectedId) => {
        this.props.setSelectedRequestId(selectedId);
        this.props.handleEdit(selectedId);
    }

    handleViewProcess = (selectedId) => {
        this.props.setSelectedRequestId(selectedId);
        this.props.handleView(selectedId);
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreationDate}>{headerText.CreationDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LastModified}>{headerText.LastModified}</Cell>
                <Cell header={true} colWidth={100} title={headerText.SubmittedDate}>{headerText.SubmittedDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Deadline}>{headerText.Deadline}</Cell>
                <Cell header={true} colWidth={100} title={headerText.ApprovedBy}>{headerText.ApprovedBy}</Cell>
                <Cell header={true} colWidth={100} title={headerText.ApprovalDate}>{headerText.ApprovalDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={80} title={headerText.Action}>{headerText.Action}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { todayTime } = this.state;
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, i + startIndex, todayTime));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, todayTime) {
        const { claimsPrincipal } = this.props;
        const status = dataItem.Status;
        const deadline = dataItem.Deadline;
        const deadlineDate = new Date(deadline);
        const isShowWarnning = deadlineDate.getTime() < todayTime && status === statusList.Submitted;
        const isReadOnlyClaim = new Claim(claimResource.ProcRequest, '');
        const isViewOnly = claimsPrincipal.hasClaim(isReadOnlyClaim)
            || (claimsPrincipal.hasWriteClaim(claimResource.ProcRequestMarketingRole) && (!claimsPrincipal.hasWriteClaim(claimResource.ProcRequestManagementRole)) && dataItem.Status !== 'Draft')

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>
                    <a href={resolveClientUrl(`Portal/Customer?user=${dataItem.UserName}`)} target="_top">
                        {dataItem.UserName}
                    </a>
                </Cell>

                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={dataItem.CreationDate} className="text-center">{formatDateTime(dataItem.CreationDate, defaultDatetimeFormat)}</Cell>
                <Cell title={dataItem.LastModified} className="text-center">{formatDateTime(dataItem.LastModified, defaultDatetimeFormat)}</Cell>
                <Cell title={dataItem.SubmittedDate} className="text-center">{formatDateTime(dataItem.SubmittedDate, defaultDatetimeFormat)}</Cell>
                <Cell className={isShowWarnning ? "text-danger text-center" : "text-center"} title={dataItem.Deadline}>{formatDateTime(dataItem.Deadline, defaultDatetimeFormat)}</Cell>
                <Cell title={dataItem.ApprovedBy}>{dataItem.ApprovedBy}</Cell>
                <Cell title={dataItem.ApprovalDate} className="text-center">{formatDateTime(dataItem.ApprovalDate, defaultDatetimeFormat)}</Cell>
                <Cell title={dataItem.Status} className="text-center" style={{ color: colorByStatus[status] }}>{status}</Cell>
                <Cell className="text-center" title="Action">
                    {isViewOnly
                        ? (<Button color="link" onClick={() => this.handleViewProcess(dataItem.RequestId)}>
                            <span className="oi oi-eye text-primary"></span>
                        </Button>)
                        : (<React.Fragment>
                            <Button color="link" onClick={() => this.handleEditProcess(dataItem.RequestId)}>
                                <span className="oi oi-pencil text-secondary"></span>
                            </Button>
                            <Button color="link" onClick={() => this.handleDeleteProcess(dataItem.RequestId)}>
                                <span className="oi oi-trash text-danger"></span>
                            </Button>
                        </React.Fragment>
                        )
                    }
                </Cell>
            </Row>
        );
    }

    handlePaging = (index, size) => {
        const newParam = Object.assign({}, this.props.param, {
            PageIndex: index,
            PageSize: size
        });

        this.props.fetchData(newParam);
    }

    render() {
        const { messageNotification } = this.state;
        const { gridData, shouldResetScrollPosition } = this.props;

        if (!gridData) {
            return null;
        }

        return (
            <React.Fragment>
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
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ today, gridData, pagingOption, shouldResetScrollPosition, claimsPrincipal }) {
    return { today, gridData, pagingOption, shouldResetScrollPosition, claimsPrincipal };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, fetchData, setSelectedRequestId }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
