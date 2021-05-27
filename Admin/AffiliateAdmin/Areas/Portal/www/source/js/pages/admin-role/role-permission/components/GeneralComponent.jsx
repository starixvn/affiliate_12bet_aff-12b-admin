import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { updateObjectInArray } from '../../../../modules/dataHandler';
import { headerText, resource, messages, management } from '../libs/constants';
import NotificationPopup from '../../../common/components/NotificationPopup';
import MyLink from '../../../common/components/MyLink';
import { getPermissionInfoList, toggleLoader, setGridData, updatePermissionInfo } from '../actions';

export class GeneralComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            expandedRoleId: 0,
            gridData: null,
            messagePopup: '',
            isConfirmPopup: false
        }
    }

    componentDidMount() {
        this._handleGetPermissionInfoList();
    }

    _handleGetPermissionInfoList = async () => {
        this.props.toggleLoader(true);

        const permissionInfoList = await this.props.getPermissionInfoList();

        this.props.setGridData(permissionInfoList);
        this.setState({ gridData: permissionInfoList });

        this.props.toggleLoader(false);
    }

    _handleChangeChildrenOfItem = (gridData, data, roleId, parentId) => {
        const children = gridData.filter(item => item.RoleId == roleId && item.FuncId != parentId && item.AllParentIds.split(',').includes(parentId.toString()));

        for (let child of children) {
            const indexItem = gridData.findIndex(item => item.RoleId == roleId && item.FuncId == child.FuncId);
            const updateItem = Object.assign({}, gridData[indexItem], data);

            gridData = updateObjectInArray(gridData, indexItem, updateItem);
            gridData = this._handleChangeChildrenOfItem(gridData, data, roleId, child.FuncId);
        }

        return gridData;
    }

    _handleChangeParentsOfItem = (gridData, data, roleId, funcId, allParentIds, isChecked, isHost) => {
        const parentIds = allParentIds.split(',').filter(parentId => funcId != parentId);

        for (let parentId of parentIds) {
            const hasAnyChildIsCheck = gridData.filter(item => item.RoleId == roleId && item.FuncId != parentId && item.AllParentIds.split(',').includes(parentId.toString()) && (isHost ? item.IsHost : item.IsRead)).length;

            if (isChecked || !hasAnyChildIsCheck) {
                const indexItem = gridData.findIndex(item => item.RoleId == roleId && item.FuncId == parentId);
                const updateItem = Object.assign({}, gridData[indexItem], data);

                gridData = updateObjectInArray(gridData, indexItem, updateItem);
                gridData = this._handleChangeParentsOfItem(gridData, data, roleId, updateItem.FuncId, updateItem.AllParentIds, isChecked);
            }
        }

        return gridData;
    }

    _handleChange = (roleId, funcId, isChecked, isHost) => {
        const { gridData } = this.state;
        const data = isHost ? (isChecked ? { IsRead: true, IsHost: true } : { IsHost: false }) : (isChecked ? { IsRead: true } : { IsRead: false, IsHost: false });
        const indexItem = gridData.findIndex(item => item.RoleId == roleId && item.FuncId == funcId);
        const updateItem = Object.assign({}, gridData[indexItem], data);
        let gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem);

        gridDataUpdate = this._handleChangeChildrenOfItem(gridDataUpdate, data, roleId, funcId);
        gridDataUpdate = this._handleChangeParentsOfItem(gridDataUpdate, data, roleId, funcId, updateItem.AllParentIds, isChecked, isHost);

        this.setState({ gridData: gridDataUpdate });
    }

    _handleCancel = () => {
        this.setState({
            isEdit: false,
            gridData: this.props.gridData
        });
    }

    _handleCloseNotificationPopup = (isConfirm) => {
        this.setState({ messagePopup: '' });

        if(isConfirm) {
            this._handleSave();
        }
    }

    _handleSubmit = () => {
        this.setState({ messagePopup: messages.AreYouSureUpdateRole, isConfirmPopup: true });
    }

    _handleSave = async () => {
        this.props.toggleLoader(true);

        const { gridData, expandedRoleId } = this.state;
        const permissionInfo = gridData.filter(item => item.RoleId === expandedRoleId);
        const param = { permissionInfo, roleId: expandedRoleId };

        const result = await this.props.updatePermissionInfo(param);

        if (result) {
            if (result.IsSuccessful) {
                this.setState({ messagePopup: "Update the permission is successful!", isConfirmPopup: false, isEdit: false });
            } else {
                this.setState({ messagePopup: result.Message, isConfirmPopup: false });
            }
        } else {
            this.setState({ messagePopup: messages.AnError, isConfirmPopup: false });
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

        if (dataList.length) {
            const { isEdit, expandedRoleId, gridData } = this.state;

            for (let dataItem of dataList) {
                dataRows.push(this._buildDataRow(dataItem, gridData, expandedRoleId, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="3">{messages.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, gridData, expandedRoleId, isEdit) {
        const children = gridData && gridData.filter(f => f.RoleId === dataItem.RoleId);
        const isExpand = expandedRoleId === dataItem.RoleId;

        return (
            <Fragment key={`dataRow-${dataItem.RoleId}`}>
                <Row>
                    <Cell colSpan="3">
                        <div className="d-flex">
                            <Button className="py-0 th-wrapper" color="link" onClick={() => this.setState({ expandedRoleId: isExpand ? 0 : dataItem.RoleId, isEdit: false })}>
                                <div className="icon-container">
                                    <span className={`caret ${isExpand ? 'expand current' : 'collapsed'}`}></span>
                                </div>
                                <span>{dataItem.RoleName}</span>
                            </Button>
                            {
                                dataItem.RoleId !== management && this.props.hasWriteAdminRoleClaim && (
                                    isEdit && isExpand ? (
                                        <Fragment>
                                            <MyLink title={resource.Cancel} onClick={this._handleCancel}>
                                                < span className="oi oi-x text-danger"></span>
                                            </MyLink>

                                            <MyLink title={resource.Save} onClick={this._handleSubmit}>
                                                < span className="oi oi-check text-success ml-2"></span>
                                            </MyLink>
                                        </Fragment>
                                    ) : (
                                            children && children.length ? (
                                                <MyLink title={resource.Edit} onClick={() => this.setState({ expandedRoleId: dataItem.RoleId, isEdit: true })}>
                                                    < span className="oi oi-pencil"></span>
                                                </MyLink>
                                                ) : null
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

            for (let dataItem of parentFunction) {
                dataItem && dataRows.push(this._buildDataRowChildren(dataItem, subFunction, isEdit, isExpand));
            }
        }

        return dataRows;
    }

    _renderCells(dataItem, isEdit, isExpand) {

        return isEdit && isExpand ? (
            <Fragment>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsRead} onChange={() => this._handleChange(dataItem.RoleId, dataItem.FuncId, !dataItem.IsRead, false)} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsHost} onChange={() => this._handleChange(dataItem.RoleId, dataItem.FuncId, !dataItem.IsHost, true)} />
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
        const displayName = htmlDecode(dataItem.DisplayName);

        return (
            <Fragment key={`dataRow-${dataItem.FuncId}`}>
                <Row className="current">
                    <Cell className="pl-2" title={displayName}>{displayName}</Cell>
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

        if (dataList.length) {
            const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
            const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

            for (let dataItem of parentFunction) {
                dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isEdit, isExpand, isRecursive));
            }
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isEdit, isExpand, isRecursive) {
        const displayName = htmlDecode(dataItem.DisplayName);

        return (
            <Fragment key={`dataRow-${dataItem.FuncId}`}>
                <Row className="current">
                    <Cell className={isRecursive ? "pl-5" : "pl-4"} title={displayName}>{displayName}</Cell>
                    {
                        this._renderCells(dataItem, isEdit, isExpand)
                    }
                </Row>
                {
                    !!dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isEdit, isExpand, true)
                }
            </Fragment>
        )
    }

    render() {
        const { roleList } = this.props;
        const { messagePopup, isConfirmPopup } = this.state;

        return roleList && (
            <div className="data-content role-permission">
                <div className="table-responsive table-striped">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(roleList)}
                        shouldResetScrollPosition={false}
                    />
                </div>
                {
                    !!messagePopup && <NotificationPopup
                        message={messagePopup}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </div>
        );
    }
}


function mapStateToProps({ gridData, roleList, hasWriteAdminRoleClaim }) {
    return Object.assign({}, { gridData, roleList, hasWriteAdminRoleClaim });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getPermissionInfoList, toggleLoader, setGridData, updatePermissionInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralComponent);