import React, { Suspense } from 'react';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList } from '../modules/userPanelMenu';

const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu } = props;
    const componentIndex = userPanelMenuList.findIndex(p => p.key === selectedUserPanel);
    const UserPanelComponent = componentIndex === -1 ? null : userPanelMenuList[componentIndex].component;

    return (
        <div className="user-panel-container">
            <UserPanelMenu selectedUserPanel={selectedUserPanel} onClickMenu={onClickMenu} />

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