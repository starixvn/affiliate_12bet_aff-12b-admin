import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { adminRoleMenuKey } from '../../core/libs/constants';
import { redirectToAdminUsers, redirectToAdminInfo, redirectToPermissionRole } from '../actions';

export const SubMenu = (props) => {
    const { selectedMenu, userId } = props;
    const isSelectedPermissionRole = selectedMenu === adminRoleMenuKey.permissionRole;

    const _redirectToAdminUsers = () => {
        props.redirectToAdminUsers();
    }

    const _redirectToAdminInfo = () => {
        props.redirectToAdminInfo();
    }

    const _redirectToPermissionRole = () => {
        props.redirectToPermissionRole();
    }

    return (
        <div class="sub-menu-container admin-role-menu">
            <div class="d-inline-block">
                <a class="admin-role-menu-back" href="javascript:;" onClick={_redirectToAdminUsers}><span class="oi oi-chevron-left"></span></a>
            </div>
            <div class="d-inline-block">
                <ul class="nav">
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedPermissionRole ? '' : 'active'}`} href="javascript:;" onClick={_redirectToAdminInfo}>Admin Info</a>
                    </li>
                    {
                        userId && (
                            <li class="nav-item">
                                <a class={`nav-link ${isSelectedPermissionRole ? 'active' : ''}`} href="javascript:;" onClick={_redirectToPermissionRole}>Permission Role</a>
                            </li>
                        )
                    }
                </ul>
            </div>
        </div>
    )
}

function mapStateToProps({ selectedMenu, userId }) {
    return Object.assign({}, { selectedMenu, userId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ redirectToAdminUsers, redirectToAdminInfo, redirectToPermissionRole }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(SubMenu));