import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, globalResource, statusList, colorByStatus } from '../libs/constants';
import { setPagingOption, fetchData } from '../actions';
import { resolveClientUrl } from '../../../../modules/utils';
import { NotificationPopup } from '../../../common/components/NotificationPopup';
import { claimResource } from '../../../../modules/claim';

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

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreationDate}>{headerText.CreationDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LastModified}>{headerText.LastModified}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Sumitted}>{headerText.Sumitted}</Cell>
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
        const status = dataItem.Status;
        const deadline = dataItem.Deadline;
        const deadlineDate = new Date(deadline);
        const isShowWarnning = deadlineDate.getTime() < todayTime && status === statusList.Submitted;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-right" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>
                    <a href={resolveClientUrl(`Portal/Customer?user=${dataItem.UserName}`)} target="_top">
                        {dataItem.UserName}
                    </a>
                </Cell>

                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={dataItem.CreationDate}>{dataItem.CreationDate}</Cell>
                <Cell title={dataItem.LastModified}>{dataItem.LastModified}</Cell>
                <Cell title={dataItem.Sumitted}>{dataItem.Sumitted}</Cell>
                <Cell className={isShowWarnning ? "text-danger" : "" } title={dataItem.Deadline}>{dataItem.Deadline}</Cell>
                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={dataItem.ApprovedBy}>{dataItem.ApprovedBy}</Cell>
                <Cell title={dataItem.ApprovalDate}>{dataItem.ApprovalDate}</Cell>
                <Cell title={dataItem.Status} style={{ color: colorByStatus[status] }}>{status}</Cell>
                <Cell className="text-center" title="Change Password">
                    {
                        isApproved && (<Button color="link" onClick={() => this.handleChangePassword(dataItem.AffId)}>
                            <span className="oi oi-pencil text-secondary"></span>
                        </Button>)
                    }
                </Cell>
                }
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
                {
                    !!messageNotification && <NotificationPopup
                        message={messageNotification}
                        isConfirmPopup={false}
                        onClose={() => this.setState({ messageNotification: '' })}
                    />
                }
            </React.Fragment>
        );
    }
}

function mapStateToProps({ today, gridData, pagingOption, shouldResetScrollPosition }) {
    return { today, gridData, pagingOption, shouldResetScrollPosition };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GridData);
