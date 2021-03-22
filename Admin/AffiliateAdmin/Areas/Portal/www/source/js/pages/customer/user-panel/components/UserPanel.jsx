import React, { Suspense, useEffect } from 'react';
import { connect } from 'react-redux';
import Loader from '../../../../components/Loader';
import UserPanelMenu from './UserPanelMenu';
import { userPanelMenuList, userPanelPendingMenuList } from '../libs/menu';
import { userPanelMenuKey } from '../../common/libs/constants';
import { claimResource } from '../../../../modules/claim';
import { spinnerLoaderWidth, spinnerLoaderUserPanelHeight } from '../../../../modules/constants';

export const UserPanel = (props) => {
    const { selectedUserPanel, onClickMenu, isPending, isShowLoader, claimsPrincipal } = props;

    useEffect(() => {
        if (isPending && selectedUserPanel === userPanelMenuKey.statementHistory) {
            props.onClickMenu(claimsPrincipal.hasClaim(claimResource.EditAffiliate) ? userPanelMenuKey.edit : userPanelMenuKey.note);
        }
    });

    const menuList = isPending ? userPanelPendingMenuList : userPanelMenuList;
    const filterByClaimMenuList = menuList.filter(item => item.renderClaim == undefined || claimsPrincipal.hasClaim(item.renderClaim));
    const componentIndex = filterByClaimMenuList.findIndex(p => p.key === selectedUserPanel);
    const UserPanelComponent = componentIndex === -1 ? null : filterByClaimMenuList[componentIndex].component;

    return (
        <div className="user-panel-container">
            <Loader show={isShowLoader} style={{ top: spinnerLoaderUserPanelHeight, left: spinnerLoaderWidth }}/>
            <UserPanelMenu selectedUserPanel={selectedUserPanel} onClickMenu={onClickMenu} isPending={isPending} menuList={filterByClaimMenuList} />

            <div className="user-panel-content-container">
                {
                    componentIndex !== -1 &&
                    <Suspense fallback={null}>
                        <UserPanelComponent />
                    </Suspense>
                }
            </div>
        </div>
    )
}

function mapStateToProps({ isShowLoaderUserPanel, claimsPrincipal }) {
    return Object.assign({}, { isShowLoader: isShowLoaderUserPanel, claimsPrincipal });
}

export default connect(mapStateToProps)(React.memo(UserPanel));