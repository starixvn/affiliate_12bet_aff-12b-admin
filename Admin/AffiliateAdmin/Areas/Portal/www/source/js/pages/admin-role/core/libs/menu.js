import React from 'react';
import { adminRoleMenuKey as menuKey } from './constants';
import { resetAllProps } from './resetAllProps';

const AdminList = React.lazy(() =>
    import( /* webpackChunkName: "adminList" */ '../../admin-list/components/AppContainer'));
const AdminInfo = React.lazy(() =>
    import( /* webpackChunkName: "adminInfo" */ '../../admin-info/components/AppContainer'));


export const adminRoleMenuList = [
    { key: menuKey.adminList, component: resetAllProps(AdminList, menuKey.adminList) },
    { key: menuKey.adminInfo, component: resetAllProps(AdminInfo, menuKey.adminInfo) },
]