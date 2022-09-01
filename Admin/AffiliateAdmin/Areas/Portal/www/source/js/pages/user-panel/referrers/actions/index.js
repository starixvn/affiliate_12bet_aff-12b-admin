import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { getCountryList } from '../../../customer/user-panel/libs/service';
import { setSelectedUserPanel, toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import * as types from '../actions/types';

export { setPagingOption, toggleLoaderUserPanel, setSelectedUserPanel, getCountryList };

export const setSelectedSiteDetailFilter = (data) => {
    return {
        type: types.SET_SELECTED_SITE_DETAIL_FILTER,
        data
    }
}

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/Referrers/GetReferrersList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async () => {
    const url = 'Portal/UserPanel/Referrers/GetReferrersDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}