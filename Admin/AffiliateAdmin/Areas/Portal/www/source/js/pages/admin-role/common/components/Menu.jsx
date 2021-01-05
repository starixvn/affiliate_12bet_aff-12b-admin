import React from 'react';
import { resolveClientUrl } from '../../../../modules/utils';

export const SubMenu = (props) => {
    const { isSelectedRolePermission } = props;

    return (
        <div className="sub-menu-container admin-role-menu">
            <div className="d-inline-block">
                <ul className="nav align-items-center">
                    <li className="nav-item">
                        <a href={resolveClientUrl("Portal/AdminRole")}>
                            <span className="icon-admin-role"></span>
                        </a>
                    </li>
                    <li className="nav-item">
                        <a className={`nav-link ${isSelectedRolePermission ? '' : 'active'}`} href={resolveClientUrl("Portal/AdminRole")}>Admin List</a>
                    </li>
                    <li className="nav-item">
                        <a className={`nav-link ${isSelectedRolePermission ? 'active' : ''}`} href={resolveClientUrl("Portal/RolePermission")}>Role Permission</a>
                    </li>
                </ul>
            </div>
        </div>
    )
}

export default React.memo(SubMenu);