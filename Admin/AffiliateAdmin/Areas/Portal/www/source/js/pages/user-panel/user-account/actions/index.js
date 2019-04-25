import { post } from '../../../../modules/ajaxRequest';
import { SET_USER_ACCOUNT_INFO, SET_MORE_SITE_URLS_ARRAY } from './types'
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';

export { setPagingOption, toggleLoader };

export const setUserAccountInfo = (data) => {
    return {
        type: SET_USER_ACCOUNT_INFO,
        data
    }
}

export const setMoreSiteUrlsArray = (data) => {
    return {
        type: SET_MORE_SITE_URLS_ARRAY,
        data
    }
}

export const getUserAccountInfo = (affId) => async(dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/UserPanel/UserAccount/GetUserAccountInfo';

    const response = await post(url, {affId});

    const data = response && response.status === 200 ? response.data : null;

    const moreSiteUrls = data && data.MoreSiteUrls;
    const moreSiteUrlsArray = moreSiteUrls && moreSiteUrls != '' && moreSiteUrls.split(';');

    dispatch(setUserAccountInfo(data));
    dispatch(setMoreSiteUrlsArray(moreSiteUrlsArray));
    dispatch(toggleLoader(false));
}

export const updateUserAccount = (param) => async() => {
    const url = 'Portal/UserPanel/UserAccount/updateUserAccount';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}