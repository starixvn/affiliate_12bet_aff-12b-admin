import React, { Suspense } from 'react';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList, userPanelPendingMenuList, menuKey } from '../modules/menu';

const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu, isPending } = props;

    if(isPending && selectedUserPanel === menuKey.statementHistory) {
        props.onClickMenu(menuKey.edit);
    }

    const menuList = isPending ? userPanelPendingMenuList : userPanelMenuList;
    const componentIndex = menuList.findIndex(p => p.key === selectedUserPanel);
    const UserPanelComponent = componentIndex === -1 ? null : menuList[componentIndex].component;

    return (
        <div className="user-panel-container">
            <UserPanelMenu selectedUserPanel={selectedUserPanel} onClickMenu={onClickMenu} isPending={isPending} menuList={menuList}/>

            <div class="user-panel-content-container">
                {
                    componentIndex !== -1 &&
                    <Suspense fallback={<div>Loading...</div>}>
                        <UserPanelComponent />
                    </Suspense>
                }
            </div>
        </div>
    )
}

export default React.memo(UserPanel);