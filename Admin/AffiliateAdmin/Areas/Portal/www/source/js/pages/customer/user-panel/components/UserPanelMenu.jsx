import React from 'react';
import { userPanelMenuList } from '../modules/menu';
import { isActiveMenu } from '../modules/service';
import UserPanelMenuItem from './UserPanelMenuItem';

const UserPanelMenu = (props) => {
    const { selectedUserPanel, onClickMenu } = props;
    const selectedMenuItem = userPanelMenuList.find(p => p.key === selectedUserPanel);

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
                                    userPanelMenuList.filter(item => !item.parent).map(item => {
                                        return (
                                            <UserPanelMenuItem key={item.key} item={item} 
                                                isActive={isActiveMenu(selectedMenuItem, item)}
                                                onClickMenu={onClickMenu} />
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