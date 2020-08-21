import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { adminRoleMenuKey } from '../../core/libs/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import { redirectToAdminUsers, redirectToGeneralPermissionRole } from '../actions';

export const SubMenu = (props) => {
    const { selectedMenu } = props;
    const isSelectedPermissionRole = selectedMenu === adminRoleMenuKey.generalPermissionRole;

    const _redirectToAdminUsers = () => {
        props.redirectToAdminUsers();
    }

    const _redirectToGeneralPermissionRole = () => {
        props.redirectToGeneralPermissionRole();
    }

    return (
        <div class="sub-menu-container admin-role-menu">
            <div class="d-inline-block">
                <ul class="nav align-items-center">
                    <li class="nav-item">
                        <a href="javascript:;" onClick={_redirectToAdminUsers}>
                            <span class="icon-admin-role"></span>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedPermissionRole ? '' : 'active'}`} href="javascript:;" onClick={_redirectToAdminUsers}>Admin List</a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedPermissionRole ? 'active' : ''}`} href="javascript:;" onClick={_redirectToGeneralPermissionRole}>General Permission Role</a>
                    </li>
                </ul>
            </div>
        </div>
    )
}

function mapStateToProps({ selectedMenu }) {
    return Object.assign({}, { selectedMenu });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ redirectToAdminUsers, redirectToGeneralPermissionRole }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(SubMenu));