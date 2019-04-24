import { LEFTPANEL_RECEIVE_AFFINFO, SET_AFF_COMMON_INFO } from './types';
import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';

import { loadCustomerStatisticsState } from '../../customer-statistics/modules/service';
import { setSelectedUserPanel } from '../../user-panel/actions';
import { menuKey } from '../../user-panel/modules/menu';

const receiveLeftMenuInfo = (data) => {
    return {
        type: LEFTPANEL_RECEIVE_AFFINFO,
        data
    }
}

export const setAffCommonInfo = (data) => {
    return {
        type: SET_AFF_COMMON_INFO,
        data
    }
}

export const getAffiliateInfo = (affName) => async(dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/CustomerLeftPanel/GetAffiliateInfo';
    const param = { affUsername: affName };
    const response = await post(url, param);
    const data = response && response.status === 200 && response.data ? response.data : null;

    const affCommonInfo = data && { affId: data.AffId, affName, status: data.Status };
    dispatch(setAffCommonInfo(affCommonInfo)) && affCommonInfo && loadCustomerStatisticsState();
    dispatch(receiveLeftMenuInfo(data));
    data && dispatch(setSelectedUserPanel(menuKey.statementHistory));

    dispatch(toggleLoader(false));
}