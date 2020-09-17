import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table } from 'reactstrap';
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';

import { globalResource } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { getUserPermissionList, setUserInfo } from '../actions/index';

export class PermissionPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            data: null
        }
    }
    componentDidUpdate(prevProps) {
        const { userInfo } = this.props;
        if (userInfo && (userInfo !== prevProps.userInfo)) {
            this._handleGetUserPermissionList(userInfo.UserId);
        }
    }

    _handleGetUserPermissionList = async (userId) => {
        var data = await this.props.getUserPermissionList(userId);

        this.setState({ data });
    }

    _handleClosePopup = () => {
        this.props.setUserInfo(null);

        this.setState({ data: null });
    }

    _renderHeader() {
        return (
            <thead key="thead">
                <tr>
                    <th rowSpan="2" colSpan="2">Function</th>
                    <th colSpan="2">Individual Permission</th>
                    <th colSpan="2">Role Permision</th>
                </tr>
                <tr>
                    <th>Read</th>
                    <th>Host</th>
                    <th>Read</th>
                    <th>Host</th>
                </tr>
            </thead>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const parentFunction = dataList.filter(s => s.FuncId === s.ParentId);
            const subFunction = dataList.filter(s => s.FuncId !== s.ParentId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowChildren(dataItem, subFunction));
            }
        }
        else {
            dataRows.push(<tr key="no-info">
                <td className="text-center" colSpan="5">{globalResource.NoInfo}</td>
            </tr>);
        }

        return dataRows;
    }

    _renderColumns(dataItem) {

        return (
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
        );
    }

    _buildDataRowChildren(dataItem, dataList) {
        const funcName = htmlDecode(dataItem.FuncName);

        return (
            <Fragment>
                <tr key={`dataRow-${dataItem.FuncId}`} className="current">
                    <td colSpan="2" className="pl-2" title={funcName}>{funcName}</td>
                    {
                        this._renderColumns(dataItem)
                    }
                </tr>
                {
                    this._renderSubChildrenDataSection(dataItem, dataList)
                }
            </Fragment>
        )
    }

    _renderSubChildrenDataSection(dataItem, dataList, isRecursive) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
            const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

            for (let i = 0; i < parentFunction.length; i++) {
                const dataItem = parentFunction[i];
                dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isRecursive));
            }
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isRecursive) {

        return (
            <Fragment>
                <tr key={`dataRow-${dataItem.FuncId}`} className="current">
                    <td colSpan="2" className={isRecursive ? "pl-5" : "pl-4"} title={dataItem.FuncName}>{dataItem.FuncName}</td>
                    {
                        this._renderColumns(dataItem, isRecursive)
                    }
                </tr>
                {
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, true)
                }
            </Fragment>
        )
    }

    render() {
        const { data } = this.state;
        const { userInfo } = this.props;

        return data && (
            <Modal isOpen={!!userInfo} toggle={this._handleClosePopup} backdrop='static'>
                <ModalHeader toggle={this._handleClosePopup}>
                    <div className="text-truncate" style={{ width: 450 }}>
                        <span className="iconpopup"></span>{userInfo.UserName + " - Permission"}
                    </div>
                </ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="permission-popup-form-container">
                            <div className="permission-popup-list-content table-responsive mb-2">
                                <Table className="permission-popup-data-table">
                                    {this._renderHeader()}
                                    {this._renderDataSection(data)}
                                </Table>
                            </div>

                            <Button type="button" color="info" onClick={this._handleClosePopup}>Close</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ adminList }) {
    const { userInfo } = adminList;

    return Object.assign({}, { userInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getUserPermissionList, setUserInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PermissionPopup);