import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table } from 'reactstrap';
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';

import { globalResource } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { getRoleByUser, setUserInfo } from '../actions/index';

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
            this._handleGetRoleByUser(userInfo.UserId);
        }
    }

    _handleGetRoleByUser = async (userId) => {
        var data = await this.props.getRoleByUser({ userId });

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
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem));
            }
        }
        else {
            dataRows.push(<tr key="no-info">
                <td className="text-center" colSpan="5">{globalResource.NoInfo}</td>
            </tr>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem) {
        const funcDescription = htmlDecode(dataItem.FunctionDescription);

        return (
            <tr key={`dataRow-${funcDescription}`}>
                <td className="text-center" title={funcDescription} colSpan="2">{funcDescription}</td>
                <td className="text-center">
                    {
                        !dataItem.IndividualPermission ? null : <span className="oi oi-check text-primary"></span>
                    }
                </td>
                <td className="text-center">
                    {
                        dataItem.IndividualPermission === 3 ? <span className="oi oi-check text-primary"></span> : null
                    }
                </td>
                <td className="text-center">
                    {
                        !dataItem.RolePermission ? null : <span className="oi oi-check text-primary"></span>
                    }
                </td>
                <td className="text-center">
                    {
                        dataItem.RolePermission === 3 ? <span className="oi oi-check text-primary"></span> : null
                    }
                </td>
            </tr>
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

function mapStateToProps({ adminUsers }) {
    const { userInfo } = adminUsers;

    return Object.assign({}, { userInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getRoleByUser, setUserInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PermissionPopup);