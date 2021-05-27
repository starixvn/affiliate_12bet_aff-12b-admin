import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Button, Modal, ModalHeader, ModalBody } from 'reactstrap';

import { messages } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { getUserPermissionList, setUserInfo } from '../actions/index';
import { renderHeader, renderColumns } from '../../common/libs/render';

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

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const parentFunction = dataList.filter(s => s.FuncId === s.ParentId);
            const subFunction = dataList.filter(s => s.FuncId !== s.ParentId);

            for (let dataItem of parentFunction) {
                dataRows.push(this._buildDataRowChildren(dataItem, subFunction));
            }
        }
        else {
            dataRows.push(<tr key="no-info">
                <td className="text-center" colSpan="6">{messages.NoInfo}</td>
            </tr>);
        }

        return dataRows;
    }

    _buildDataRowChildren(dataItem, dataList) {
        const displayName = htmlDecode(dataItem.DisplayName);

        return (
            <Fragment key={`dataRow-${dataItem.FuncId}`}>
                <tr className="current">
                    <td colSpan="4" className="pl-2" title={displayName}>{displayName}</td>
                    {
                        renderColumns(dataItem)
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

        if (dataList.length) {
            const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
            const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

            for (let dataItem of parentFunction) {
                dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isRecursive));
            }
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isRecursive) {
        const displayName = htmlDecode(dataItem.DisplayName);

        return (
            <Fragment key={`dataRow-${dataItem.FuncId}`}>
                <tr className="current">
                    <td colSpan="4" className={isRecursive ? "pl-5" : "pl-4"} title={displayName}>{displayName}</td>
                    {
                        renderColumns(dataItem)
                    }
                </tr>
                {
                    !!dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, true)
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
                                    {renderHeader()}
                                    <tbody>
                                        {this._renderDataSection(data)}
                                    </tbody>
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