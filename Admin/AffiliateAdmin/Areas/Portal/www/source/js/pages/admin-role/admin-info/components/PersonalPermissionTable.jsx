import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Form, FormGroup, Label } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { updateObjectInArray } from '../../../../modules/dataHandler';
import { setPermissionListByRole } from '../actions';

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

    _handleChange = (funcId, isChecked, isHost) => {
        const { permissionListByRole } = this.props;
        const data = isHost ? (isChecked ? { IsHost: !isChecked } : { IsRead: !isChecked, IsHost: !isChecked }) : (isChecked ? { IsRead: !isChecked, IsHost: !isChecked } : { IsRead: !isChecked });
        const indexItem = permissionListByRole.findIndex(item => item.FuncId === funcId);
        const updateItem = Object.assign({}, permissionListByRole[indexItem], data);
        const permissionListByRoleUpdate = updateObjectInArray(permissionListByRole, indexItem, updateItem);

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

    _renderHeader() {

        return (
            <thead key="thead">
                <tr>
                    <th rowSpan="2" colSpan="3">{headerText.Function}</th>
                    <th colSpan="2">{headerText.IndividualPermission}</th>
                    <th colSpan="2">{headerText.RolePermission}</th>
                </tr>
                <tr>
                    <th>{headerText.Read}</th>
                    <th>{headerText.Host}</th>
                    <th>{headerText.Read}</th>
                    <th>{headerText.Host}</th>
                </tr>
            </thead>
        );
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
                    <input type="checkbox" checked={dataItem.IsRead} disabled={isSetByDefault} onChange={() => this._handleChange(dataItem.FuncId, dataItem.IsRead, false)} />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsHost} disabled={isSetByDefault} onChange={() => this._handleChange(dataItem.FuncId, dataItem.IsHost, true)} />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleRead} disabled />
                </td>
                <td className="text-center">
                    <input type="checkbox" checked={dataItem.IsRoleHost} disabled />
                </td>
            </Fragment>
        ) : (
                <Fragment>
                    <td className="text-center">
                        {
                            dataItem.IsRead && <span className="oi oi-check text-primary"></span>
                        }
                    </td>

                    <td className="text-center">
                        {
                            dataItem.IsHost && <span className="oi oi-check text-primary"></span>
                        }
                    </td>
                    <td className="text-center">
                        {
                            dataItem.IsRoleRead && <span className="oi oi-check text-primary"></span>
                        }
                    </td>

                    <td className="text-center">
                        {
                            dataItem.IsRoleHost && <span className="oi oi-check text-primary"></span>
                        }
                    </td>
                </Fragment>
            )
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
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isCreateNew, isEdit, true)
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
                        {this._renderHeader()}
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