import React from 'react';
import { adminRoleMenuKey as menuKey } from './constants';
import { resetAllProps } from './resetAllProps';

const AdminUsers = React.lazy(() =>
    import( /* webpackChunkName: "adminUsers" */ '../../admin-users/components/AppContainer'));
const AdminInfo = React.lazy(() =>
    import( /* webpackChunkName: "adminInfo" */ '../../admin-info/components/AppContainer'));
const PermissionRole = React.lazy(() =>
    import( /* webpackChunkName: "permissionRole" */ '../../permission-role/components/AppContainer'));

export const adminRoleMenuList = [
    { key: menuKey.adminUsers, component: resetAllProps(AdminUsers, menuKey.adminUsers) },
    { key: menuKey.adminInfo, component: resetAllProps(AdminInfo, menuKey.adminInfo) },
    { key: menuKey.permissionRole, component: resetAllProps(PermissionRole, menuKey.permissionRole) }
]