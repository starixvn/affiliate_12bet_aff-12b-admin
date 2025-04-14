import * as types from './types';
import { setPagingOption } from '../../../actions/pagingAction';
import { post } from './../../../modules/ajaxRequest';
import { getPaymentMethod } from './../../user-panel/user-account/actions/index';

export { setPagingOption, getPaymentMethod };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setIsShowAddAffMktAccountPopup = (data) => {
    return {
        type: types.SET_IS_SHOW_ADD_AF_MKT_ACCOUNT_POPUP,
        data
    }
}

export const setAffId = (data) => {
    return {
        type: types.SET_AFF_ID,
        data
    }
}

// TODO: Need Review
export const fetchData = (param) => async () => {
    const response = await post('Portal/Affiliate/GetAffiliateList', param);

    return response && response.status === 200 ? response.data : null;
}

// TODO: Need Review
export const getMarketingAccountInfo = (affId) => async () => {
    const url = 'Portal/UserPanel/UserAccount/GetUserAccountInfo';

    const response = await post(url, { affId });

    const data = response && response.status === 200 ? response.data : null;

    return data;
}