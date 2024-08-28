import React, { Suspense, useEffect } from 'react';
import { connect } from 'react-redux';
import Loader from '../../../../components/Loader';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList, userPanelPendingMenuList } from '../libs/menu';
import { isValidMenuByRenderClaim } from '../libs/service';
import { userPanelMenuKey } from '../../common/libs/constants';
import { spinnerLoaderWidth, spinnerLoaderUserPanelHeight } from '../../../../modules/constants';

export const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu, isWaitingApproval, isShowLoader, claimsPrincipal, isPendingAff } = props;
    const menuList = isWaitingApproval ? userPanelPendingMenuList : userPanelMenuList;

    useEffect(() => {
        if (isWaitingApproval && selectedUserPanel === userPanelMenuKey.statementHistory) {
            const firstMenu = menuList.find(m => isValidMenuByRenderClaim(claimsPrincipal, m, isPendingAff));

            firstMenu && props.onClickMenu(firstMenu.key);
        }
    });

    const filterByClaimMenuList = menuList.filter(m => isValidMenuByRenderClaim(claimsPrincipal, m, isPendingAff));
    const selectedMenu = filterByClaimMenuList.find(p => p.key === selectedUserPanel);
    const UserPanelComponent = !selectedMenu ? null : selectedMenu.component;

    return (
        <div className="user-panel-container">
            <Loader show={isShowLoader} style={{ top: spinnerLoaderUserPanelHeight, left: spinnerLoaderWidth }}/>
            <UserPanelMenu selectedUserPanel={selectedUserPanel} onClickMenu={onClickMenu} isWaitingApproval={isWaitingApproval} menuList={filterByClaimMenuList} />

            <div className="user-panel-content-container">
                {
                    UserPanelComponent &&
                    <Suspense fallback={null}>
                        <UserPanelComponent />
                    </Suspense>
                }
            </div>
        </div>
    )
}

function mapStateToProps({ isShowLoaderUserPanel, claimsPrincipal, affCommonInfo }) {
    const isPendingAff = affCommonInfo && affCommonInfo.isPendingAff;

    return Object.assign({}, { isShowLoader: isShowLoaderUserPanel, claimsPrincipal, isPendingAff });
}

export default connect(mapStateToProps)(React.memo(UserPanel));