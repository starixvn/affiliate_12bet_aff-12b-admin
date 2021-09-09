import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Pager } from '@9softstudio/react-fixed-table-header';

import { headerText, globalResource, statusList, colorByStatus } from '../libs/constants';
import { setPagingOption, fetchData } from '../actions';
import { resolveClientUrl, formatDateTime } from '../../../../modules/utils';
import { Claim, claimResource } from '../../../../modules/claim';
import { defaultDatetimeFormat, defaultDateFormat } from '../../../../modules/constants';
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
            const today = new Date(todayTime)
            console.log(today);
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(this._buildDataRow(dataItem, i + startIndex, today));
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
        const isShowWarnning = deadlineDate.getTime() < todayTime
        const isViewOnly = !claimsPrincipal.hasWriteClaim(claimResource.ProcRequest)
            || (claimsPrincipal.hasWriteClaim(claimResource.ProcRequestMarketingRole) && (!claimsPrincipal.hasWriteClaim(claimResource.ProcRequestManagementRole)) && dataItem.Status !== 'Draft')

        const creationDate = dataItem.CreationDate && formatDateTime(dataItem.CreationDate, defaultDatetimeFormat);
        const lastModifiedDate = dataItem.LastModified && formatDateTime(dataItem.LastModified, defaultDatetimeFormat);
        const submittedDate = dataItem.SubmittedDate && formatDateTime(dataItem.SubmittedDate, defaultDatetimeFormat);
        const deadlineFormatedDate = dataItem.Deadline && formatDateTime(dataItem.Deadline, defaultDateFormat);
        const approvalDate = dataItem.ApprovalDate && formatDateTime(dataItem.ApprovalDate, defaultDatetimeFormat);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>
                    <a href={resolveClientUrl(`Portal/Customer?user=${dataItem.UserName}`)} target="_top">
                        {dataItem.UserName}
                    </a>
                </Cell>

                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={creationDate} className="text-center">{creationDate}</Cell>
                <Cell title={lastModifiedDate} className="text-center">{lastModifiedDate}</Cell>
                <Cell title={submittedDate} className="text-center">{submittedDate}</Cell>
                <Cell className={isShowWarnning ? "text-danger text-center" : "text-center"} title={deadlineFormatedDate}>{deadlineFormatedDate}</Cell>
                <Cell title={dataItem.ApprovedBy}>{dataItem.ApprovedBy}</Cell>
                <Cell title={approvalDate} className="text-center">{approvalDate}</Cell>
                <Cell title={dataItem.Status} className="text-center" style={{ color: colorByStatus[status] }}>{status}</Cell>
                <Cell className="text-center" title="Action">
                    {isViewOnly
                        ? (<Button color="link" title='View' onClick={() => this.handleViewProcess(dataItem.RequestId)}>
                            <span className="oi oi-eye text-primary"></span>
                        </Button>)
                        : (<React.Fragment>
                            <Button color="link" title='Edit' onClick={() => this.handleEditProcess(dataItem.RequestId)}>
                                <span className="oi oi-pencil text-secondary"></span>
                            </Button>
                            <Button color="link" title='Delete' onClick={() => this.handleDeleteProcess(dataItem.RequestId)}>
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
                        adjustedHeight = {50}
                        containerPadding={20}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
                <Pager pageOption={this.props.pagingOption} onPaging={this.handlePaging} />
            </React.Fragment>
        );
    }
}

function mapStateToProps({ today, gridData, pagingOption, shouldResetScrollPosition, claimsPrincipal, param }) {
    return { today, gridData, pagingOption, shouldResetScrollPosition, claimsPrincipal, param };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, fetchData, setSelectedRequestId }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
