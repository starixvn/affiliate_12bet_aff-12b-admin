import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { updateObjectInArray } from '../../../../modules/dataHandler';
import { headerText, globalResource, management } from '../libs/constants';
import { getPermissionInfoList, toggleLoader, setGridData, updatePermissionInfo, fakeFunctionList, setMessagePopup } from '../actions';

export class GeneralComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            expandedRoleId: 0,
            gridData: null
        }
    }

    componentDidMount() {
        this._handleGetPermissionInfoList();
    }

    _handleGetPermissionInfoList = async () => {
        this.props.toggleLoader(true);

        const permissionInfoList = await this.props.getPermissionInfoList();

        this.props.setGridData(fakeFunctionList);
        this.setState({ gridData: fakeFunctionList });

        this.props.toggleLoader(false);
    }

    _handleChange = (roleId, funcId, isChecked, isHost) => {
        const { gridData } = this.state;
        const indexItem = gridData.findIndex(item => item.RoleId === roleId && item.FuncId === funcId);
        const updateItem = Object.assign({}, gridData[indexItem], isHost ? { IsRead: !isChecked, IsHost: !isChecked } : { IsRead: !isChecked });
        const gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem);

        this.setState({ gridData: gridDataUpdate });
    }

    _handleCancel = () => {
        this.setState({
            isEdit: false,
            gridData: this.props.gridData
        });
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { gridData, expandedRoleId } = this.state;
        const permissionInfo = gridData.filter(item => item.RoleId === expandedRoleId);

        const result = await this.props.updatePermissionInfo(permissionInfo);

        if (result) {
            if (result.IsSuccessful) {
                this.props.setMessagePopup("Update Permission is successfully!");

                this.setState({ isEdit: false });
            }
            else {
                this.props.setMessagePopup(result.Message);
            }
        } else {
            this.props.setMessagePopup(globalResource.AnError);
        }

        this.props.toggleLoader(false);
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
            const { isEdit, expandedRoleId, gridData } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRowView(dataItem, gridData, expandedRoleId, isEdit));
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
                                            <a href="javascript:;" onClick={this._handleCancel}>< span className="oi oi-x text-danger"></span></a>
                                            <a href="javascript:;" onClick={this._handleSubmit}>< span className="oi oi-check text-success ml-2"></span></a>
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

    _renderCells(dataItem, isEdit, isExpand) {

        return isEdit && isExpand ? (
            <Fragment>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsRead} onChange={() => this._handleChange(dataItem.RoleId, dataItem.FuncId, dataItem.IsRead, false)} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsHost} onChange={() => this._handleChange(dataItem.RoleId, dataItem.FuncId, dataItem.IsHost, true)} />
                </Cell>
            </Fragment>
        ) : (
                <Fragment>
                    <Cell className="text-center">
                        {
                            dataItem.IsRead && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            dataItem.IsHost && <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Fragment>
            )
    }

    _buildDataRowChildren(dataItem, dataList, isEdit, isExpand) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className="pl-2" title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    {
                        this._renderCells(dataItem, isEdit, isExpand)
                    }
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
                    {
                        this._renderCells(dataItem, isEdit, isExpand)
                    }
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
    return bindActionCreators({ getPermissionInfoList, toggleLoader, setGridData, updatePermissionInfo, setMessagePopup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralComponent);