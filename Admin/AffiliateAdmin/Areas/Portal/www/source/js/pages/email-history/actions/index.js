import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { setPagingOption } from '../../../actions/pagingAction';
import { SET_PARAM, SET_SHOW_POPUP } from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setShowPopup = (data) => {
    return {
        type: SET_SHOW_POPUP,
        data
    }
}

export const fetchData = (param) => async () => {
    const url = 'Portal/EmailHistory/GetEmailHistoryList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}