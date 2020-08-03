import React from 'react';
import { adminRoleMenuKey as menuKey } from './constants';
import { resetAllProps } from './resetAllProps';

const AdminUsers = React.lazy(() =>
    import( /* webpackChunkName: "adminUsers" */ '../../admin-users/components/AppContainer'));
const AdminInfo = React.lazy(() =>
    import( /* webpackChunkName: "adminInfo" */ '../../admin-info/components/AppContainer'));
const PermissionRole = React.lazy(() =>
    import( /* webpackChunkName: "permissionRole" */ '../../permission-role/components/AppContainer'));

export const userPanelMenuList = [
    { key: menuKey.adminUsers, name: 'Admin Users', component: resetAllProps(AdminUsers, menuKey.adminUsers), parent: null },
    { key: menuKey.adminInfo, name: 'Admin Info', component: resetAllProps(AdminInfo, menuKey.adminInfo), parent: null },
    { key: menuKey.permissionRole, name: 'Permission Role', component: resetAllProps(PermissionRole, menuKey.permissionRole), parent: null }
]