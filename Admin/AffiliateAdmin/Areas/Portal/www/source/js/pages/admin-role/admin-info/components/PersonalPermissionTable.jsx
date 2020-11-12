import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Form, FormGroup, Label } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { globalResource } from '../libs/constants';
import { updateObjectInArray } from '../../../../modules/dataHandler';
import { setPermissionListByRole } from '../actions';
import { renderHeader, renderColumns } from '../../common/libs/render';

export class PersonalPermissionTable extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isSetByDefault: false
        }
    }

    componentDidUpdate(prevProps) {
        const { isSetByDefault } = this.state;
        const { permissionListByRole } = this.props;

        if (isSetByDefault && JSON.stringify(permissionListByRole) !== JSON.stringify(prevProps.permissionListByRole)) {
            this._updatePermissionListByRole();
        }
    }

    _handleChangeChildrenOfItem = (gridData, data, parentId) => {
        const children = gridData.filter(item => item.FuncId != parentId && item.AllParentIds.split(',').includes(parentId.toString()));

        for (let i = 0; i < children.length; i++) {
            const child = children[i];
            const indexItem = gridData.findIndex(item => item.FuncId == child.FuncId);
            const updateItem = Object.assign({}, gridData[indexItem], data);

            gridData = updateObjectInArray(gridData, indexItem, updateItem);
            gridData = this._handleChangeChildrenOfItem(gridData, data, child.FuncId);
        }

        return gridData;
    }

    _handleChangeParentsOfItem = (gridData, data, funcId, allParentIds, isChecked, isHost) => {
        const parentIds = allParentIds.split(',').filter(parentId => funcId != parentId);

        for (let i = 0; i < parentIds.length; i++) {
            const parentId = parentIds[i];
            const hasAnyChildIsCheck = gridData.filter(item => item.FuncId != parentId && item.AllParentIds.split(',').includes(parentId.toString()) && (isHost ? item.IsHost : item.IsRead)).length;

            if (isChecked || !hasAnyChildIsCheck) {
                const indexItem = gridData.findIndex(item => item.FuncId == parentId);
                const updateItem = Object.assign({}, gridData[indexItem], data);

                gridData = updateObjectInArray(gridData, indexItem, updateItem);
                gridData = this._handleChangeParentsOfItem(gridData, data, updateItem.FuncId, updateItem.AllParentIds, isChecked);
            }
        }

        return gridData;
    }

    _handleChange = (funcId, isChecked, isHost) => {
        const { permissionListByRole } = this.props;
        const data = isHost ? (isChecked ? { IsRead: true, IsHost: true } : { IsHost: false }) : (isChecked ? { IsRead: true } : { IsRead: false, IsHost: false });
        const indexItem = permissionListByRole.findIndex(item => item.FuncId == funcId);
        const updateItem = Object.assign({}, permissionListByRole[indexItem], data);

        let permissionListByRoleUpdate = updateObjectInArray(permissionListByRole, indexItem, updateItem);

        permissionListByRoleUpdate = this._handleChangeChildrenOfItem(permissionListByRoleUpdate, data, funcId);
        permissionListByRoleUpdate = this._handleChangeParentsOfItem(permissionListByRoleUpdate, data, funcId, updateItem.AllParentIds, isChecked, isHost);

        this.props.setPermissionListByRole(permissionListByRoleUpdate);
    }

    _handleSetByDefault = (isSetByDefault) => {
        this.setState({ isSetByDefault });

        if (isSetByDefault) {
            this._updatePermissionListByRole()
        }
    }

    _updatePermissionListByRole = () => {
        const { permissionListByRole } = this.props;

        const newPermissionListByRole = permissionListByRole.map(item => {
            return Object.assign({}, item, { IsRead: item.IsRoleRead, IsHost: item.IsRoleHost });
        });

        this.props.setPermissionListByRole(newPermissionListByRole);
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
            dataRows.push(<tr key="no-info">
                <td className="text-center" colSpan="5">{globalResource.NoInfo}</td>
            </tr>);
        }

        return dataRows;
    }

    _renderCells(dataItem, isCreateNew, isEdit) {
        const { isSetByDefault } = this.state;

        return isEdit || isCreateNew ? (
            <Fragment>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsRead} disabled={isSetByDefault} onChange={() => this._handleChange(dataItem.FuncId, !dataItem.IsRead, false)} />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsHost} disabled={isSetByDefault} onChange={() => this._handleChange(dataItem.FuncId, !dataItem.IsHost, true)} />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleRead} disabled />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleHost} disabled />
                </td>
            </Fragment>
        ) : renderColumns(dataItem);
    }

    _buildDataRowChildren(dataItem, dataList, isCreateNew, isEdit) {
        const funcName = htmlDecode(dataItem.FuncName);

        return (
            <Fragment>
                <tr key={`dataRow-${dataItem.FuncId}`} className="current">
                    <td colSpan="3" className="pl-2" title={funcName}>{funcName}</td>
                    {
                        this._renderCells(dataItem, isCreateNew, isEdit)
                    }
                </tr>
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
                <tr key={`dataRow-${dataItem.FuncId}`} className="current">
                    <td colSpan="3" className={isRecursive ? "pl-5" : "pl-4"} title={dataItem.FuncName}>{dataItem.FuncName}</td>
                    {
                        this._renderCells(dataItem, isCreateNew, isEdit)
                    }
                </tr>
                {
                    !!dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isCreateNew, isEdit, true)
                }
            </Fragment>
        )
    }

    render() {
        const { isSetByDefault } = this.state;
        const { permissionListByRole, isEdit, isCreateNew } = this.props;

        return permissionListByRole && (
            <div className="data-content personal-permission-table">
                <strong className="title-personal">Personal Permission</strong>
                {
                    (isEdit || isCreateNew) && (
                        <Form inline tag="div" className="float-right">
                            <FormGroup>
                                <input type="checkbox" checked={isSetByDefault} onChange={() => this._handleSetByDefault(!isSetByDefault)} />
                                <Label sm="auto" className="pt-2 text-primary">Set to Role's Default</Label>
                            </FormGroup>
                        </Form>
                    )
                }
                <div className="table-responsive table-striped my-3">
                    <Table className="permission-popup-data-table">
                        {renderHeader()}
                        {this._renderDataSection(permissionListByRole)}
                    </Table>
                </div>
            </div>
        );
    }
}


function mapStateToProps({ adminInfo }) {
    const { permissionListByRole } = adminInfo;
    return Object.assign({}, { permissionListByRole });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPermissionListByRole }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PersonalPermissionTable);