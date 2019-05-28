import React, { Suspense, useEffect  } from 'react';
import { Spinner } from 'reactstrap';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList, userPanelPendingMenuList } from '../modules/menu';
import { userPanelMenuKey } from '../../common/modules/constants';

const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu, isPending } = props;

    useEffect(() => {
        if (isPending && selectedUserPanel === userPanelMenuKey.statementHistory) {
            props.onClickMenu(userPanelMenuKey.edit);
        }
    });

    const menuList = isPending ? userPanelPendingMenuList : userPanelMenuList;
    const componentIndex = menuList.findIndex(p => p.key === selectedUserPanel);
    const UserPanelComponent = componentIndex === -1 ? null : menuList[componentIndex].component;

    return (
        <div className="user-panel-container">
            <UserPanelMenu selectedUserPanel={selectedUserPanel} onClickMenu={onClickMenu} isPending={isPending} menuList={menuList} />
 
            <div class="user-panel-content-container">
                {
                    componentIndex !== -1 &&
                    <Suspense fallback={<Spinner color="secondary" />}>
                        <UserPanelComponent />
                    </Suspense>
                }
            </div>
        </div>
    )
}

export default React.memo(UserPanel);