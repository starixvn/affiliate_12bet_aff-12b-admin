import React, { Suspense, useEffect } from 'react';
import { connect } from 'react-redux';
import Loader from '../../../../components/Loader';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList, userPanelPendingMenuList } from '../libs/menu';
import { userPanelMenuKey } from '../../common/libs/constants';
import { spinnerLoaderWidth, spinnerLoaderUserPanelHeight } from '../../../../modules/constants';

export const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu, isWaitingApproval, isShowLoader, claimsPrincipal, isPending } = props;
    const menuList = isWaitingApproval ? userPanelPendingMenuList : userPanelMenuList;

    useEffect(() => {
        if (isWaitingApproval && selectedUserPanel === userPanelMenuKey.statementHistory) {
            const firstMenu = menuList.find(m => m.renderClaim == undefined || claimsPrincipal.hasClaim(m.renderClaim) || (Array.isArray(m.renderClaim) && m.renderClaim.some(claim => claimsPrincipal.hasClaim(claim))));

            firstMenu && props.onClickMenu(firstMenu.key);
        }
    });

    const filterByClaimMenuList = menuList.filter(m => m.renderClaim == undefined || claimsPrincipal.hasClaim(m.renderClaim) || (Array.isArray(m.renderClaim) && m.renderClaim.some(claim => claimsPrincipal.hasClaim(claim))));
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
    const isPending = affCommonInfo && affCommonInfo.status && affCommonInfo.status.toLowerCase() === 'pending';

    return Object.assign({}, { isShowLoader: isShowLoaderUserPanel, claimsPrincipal, isPending });
}

export default connect(mapStateToProps)(React.memo(UserPanel));