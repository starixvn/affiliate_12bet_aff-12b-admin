import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { adminRoleMenuKey } from '../../core/libs/constants';
import { redirectToAdminList, redirectToRolePermission } from '../actions';

export const SubMenu = (props) => {
    const { selectedMenu } = props;
    const isSelectedRolePermission = selectedMenu === adminRoleMenuKey.rolePermission;

    const _redirectToAdminList = () => {
        props.redirectToAdminList();
    }

    const _redirectToRolePermission = () => {
        props.redirectToRolePermission();
    }

    return (
        <div class="sub-menu-container admin-role-menu">
            <div class="d-inline-block">
                <ul class="nav align-items-center">
                    <li class="nav-item">
                        <a href="javascript:;" onClick={_redirectToAdminList}>
                            <span class="icon-admin-role"></span>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedRolePermission ? '' : 'active'}`} href="javascript:;" onClick={_redirectToAdminList}>Admin List</a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedRolePermission ? 'active' : ''}`} href="javascript:;" onClick={_redirectToRolePermission}>Role Permission</a>
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
    return bindActionCreators({ redirectToAdminList, redirectToRolePermission }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(SubMenu));