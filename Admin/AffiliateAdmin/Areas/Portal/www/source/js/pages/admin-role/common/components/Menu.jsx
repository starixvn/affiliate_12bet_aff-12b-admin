import React from 'react';
import { resolveClientUrl } from '../../../../modules/utils';

export const SubMenu = (props) => {
    const { isSelectedRolePermission } = props;

    return (
        <div class="sub-menu-container admin-role-menu">
            <div class="d-inline-block">
                <ul class="nav align-items-center">
                    <li class="nav-item">
                        <a href={resolveClientUrl("Portal/AdminRole")}>
                            <span class="icon-admin-role"></span>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedRolePermission ? '' : 'active'}`} href={resolveClientUrl("Portal/AdminRole")}>Admin List</a>
                    </li>
                    <li class="nav-item">
                        <a class={`nav-link ${isSelectedRolePermission ? 'active' : ''}`} href={resolveClientUrl("Portal/RolePermission")}>Role Permission</a>
                    </li>
                </ul>
            </div>
        </div>
    )
}

export default React.memo(SubMenu);