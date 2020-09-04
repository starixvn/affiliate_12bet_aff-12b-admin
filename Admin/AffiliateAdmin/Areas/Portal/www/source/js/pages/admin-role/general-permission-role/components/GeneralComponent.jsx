import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, management } from '../libs/constants';
import { fetchData, getRoleList, fakeFunctionList } from '../actions';

export class GeneralComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            expandedRoleId: 0
        }
    }

    componentDidMount() {
        this.props.getRoleList();
        this.props.fetchData();
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={600}>{headerText.Function}</Cell>
                <Cell header={true} colWidth={200}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={200}>{headerText.Host}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { gridData } = this.props;
            const { isEdit, expandedRoleId } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRowView(dataItem, fakeFunctionList, expandedRoleId, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="3">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRowView(dataItem, gridData, expandedRoleId, isEdit) {
        const children = gridData && gridData.filter(f => f.RoleId === dataItem.RoleId);
        const isExpand = expandedRoleId === dataItem.RoleId;

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.RoleId}`}>
                    <Cell colSpan="3">
                        <div className="d-flex">
                            <Button className="py-0 th-wrapper" color="link" onClick={() => this.setState({ expandedRoleId: isExpand ? 0 : dataItem.RoleId, isEdit: false })}>
                                <div className="icon-container">
                                    <span className={`caret ${isExpand ? 'expand current' : 'collapsed'}`}></span>
                                </div>
                                <span>{dataItem.RoleName}</span>
                            </Button>
                            {
                                dataItem.RoleId !== management && (
                                    isEdit && isExpand ? (
                                        <Fragment>
                                            <a href="javascript:;" onClick={() => this.setState({ isEdit: false })}>< span className="oi oi-x text-danger"></span></a>
                                            <a href="javascript:;" onClick={() => this.setState({ isEdit: false })}>< span className="oi oi-check text-success ml-2"></span></a>
                                        </Fragment>
                                    ) : (
                                            children && children.length ? <a href="javascript:;" onClick={() => this.setState({ expandedRoleId: dataItem.RoleId, isEdit: true })}>< span className="oi oi-pencil"></span></a> : null
                                        )
                                )
                            }
                        </div>
                    </Cell>
                </Row>
                {
                    isExpand && this._renderChildrenDataSection(children, isEdit, isExpand)
                }
            </Fragment >
        )
    }

    _renderChildrenDataSection(dataList, isEdit, isExpand) {
        const dataRows = [];
        if (dataList && dataList.length) {
            const parentFunction = dataList.filter(s => s.FuncId === s.ParentId);
            const subFunction = dataList.filter(s => s.FuncId !== s.ParentId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowChildren(dataItem, subFunction, isEdit, isExpand));
            }
        }

        return dataRows;
    }

    _buildDataRowChildren(dataItem, dataList, isEdit, isExpand) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className="pl-2" title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    <Cell className="text-center">
                        {
                            isEdit && isExpand ? <input type="checkbox" checked={dataItem.IsRead} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            isEdit && isExpand ? <input type="checkbox" checked={dataItem.IsHost} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Row>
                {
                    this._renderSubChildrenDataSection(dataItem, dataList, isEdit, isExpand)
                }
            </Fragment>
        )
    }

    _renderSubChildrenDataSection(dataItem, dataList, isEdit, isExpand, isRecursive) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
            const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isEdit, isExpand, isRecursive));
            }
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isEdit, isExpand, isRecursive) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className={isRecursive ? "pl-5" : "pl-4"} title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    <Cell className="text-center">
                        {
                            isEdit && isExpand ? <input type="checkbox" checked={dataItem.IsRead} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            isEdit && isExpand ? <input type="checkbox" checked={dataItem.IsHost} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Row>
                {
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isEdit, isExpand, true)
                }
            </Fragment>
        )
    }

    render() {
        const { roleList } = this.props;

        return roleList && (
            <div className="data-content general-permission-role">
                <div className="table-responsive table-striped">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(roleList)}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ generalPermissionRole }) {
    const { gridData, roleList } = generalPermissionRole;
    return Object.assign({}, { gridData, roleList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, getRoleList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralComponent);