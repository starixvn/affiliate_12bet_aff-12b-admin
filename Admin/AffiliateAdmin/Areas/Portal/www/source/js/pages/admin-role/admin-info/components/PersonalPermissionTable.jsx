import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setRolePermission, getRolePermission } from '../actions';

export class PersonalPermissionTable extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidUpdate(prevProps) {
        const { roleId } = this.props;

        if (roleId && roleId !== prevProps.roleId) {
            this.props.getRolePermission(roleId);
        }
    }

    _renderHeader() {

        return [
            <Row key="theard">
                <Cell header={true} colWidth={300} rowSpan="2" title={headerText.Function}>{headerText.Function}</Cell>
                <Cell header={true} colWidth={200} colSpan="2" title={headerText.IndividualPermission}>{headerText.IndividualPermission}</Cell>
                <Cell header={true} colWidth={200} colSpan="2" title={headerText.RolePermission}>{headerText.RolePermission}</Cell>
            </Row>,
            <Row key="sub-theard">
                <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
            </Row>
        ];
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isCreateNew, isEdit } = this.props;

            const parentFunction = dataList.filter(s => s.FuncId === s.ParentId);
            const subFunction = dataList.filter(s => s.FuncId !== s.ParentId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowChildren(dataItem, subFunction, isCreateNew, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="5">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _renderCells(dataItem, isCreateNew, isEdit) {

        return isEdit || isCreateNew ? (
            <Fragment>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsUserRead} onChange={() => this._handleChange(dataItem.FuncId, dataItem.IsUserRead, false)} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsUserHost} onChange={() => this._handleChange(dataItem.FuncId, dataItem.IsUserHost, true)} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleRead} disabled />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleHost} disabled />
                </Cell>
            </Fragment>
        ) : (
                <Fragment>
                    <Cell className="text-center">
                        {
                            dataItem.IsUserRead && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            dataItem.IsUserHost && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                    <Cell className="text-center">
                        {
                            dataItem.IsRoleRead && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            dataItem.IsRoleHost && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Fragment>
            )
    }

    _buildDataRowChildren(dataItem, dataList, isCreateNew, isEdit) {
        const funcName = htmlDecode(dataItem.FuncName);

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className="pl-2" title={funcName}>{funcName}</Cell>
                    {
                        this._renderCells(dataItem, isCreateNew, isEdit)
                    }
                </Row>
                {
                    this._renderSubChildrenDataSection(dataItem, dataList, isCreateNew, isEdit)
                }
            </Fragment>
        )
    }

    _renderSubChildrenDataSection(dataItem, dataList, isCreateNew, isEdit, isRecursive) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
            const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isCreateNew, isEdit, isRecursive));
            }
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isCreateNew, isEdit, isRecursive) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className={isRecursive ? "pl-5" : "pl-4"} title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    {
                        this._renderCells(dataItem, isCreateNew, isEdit)
                    }
                </Row>
                {
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isCreateNew, isEdit, true)
                }
            </Fragment>
        )
    }

    render() {
        const { rolePermission } = this.props;

        return rolePermission && (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
                    <Table
                        maxWidth={700}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(rolePermission)}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ adminInfo }) {
    const { rolePermission, isCreateNew, isEdit } = adminInfo;
    return Object.assign({}, { rolePermission, isCreateNew, isEdit });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setRolePermission, getRolePermission }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PersonalPermissionTable);