import React from 'react';
import { userPanelMenuList } from '../modules/userPanelMenu';

const UserPanelMenu = (props) => {
    const { selectedUserPanel } = props;

    return (
        <div className="user-panel-menu-container">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tbody>
                    <tr>
                        <td width="48">
                            <span class="icon-userpanel"></span>
                        </td>
                        <td>
                            <ul class="d-inline-block menu-list">
                                {
                                    userPanelMenuList.filter(item => !item.isChild).map(item => {
                                        return (
                                            <li key={item.key} className="up-item">
                                                <a className={`upitem-link ${selectedUserPanel === item.key ? 'active' : ''}`}>
                                                    <span class="menu-text">{item.name}</span>
                                                </a>
                                            </li>
                                        )
                                    })
                                }
                            </ul>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    )
}

export default React.memo(UserPanelMenu);