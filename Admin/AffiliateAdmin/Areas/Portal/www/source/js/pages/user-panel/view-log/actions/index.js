import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_PARAM, SET_SHOW_ID_LIST } from '../actions/types';
import { post } from '../../../../modules/ajaxRequest';

export { setPagingOption, toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setShowIdList = (data) => {
    return {
        type: SET_SHOW_ID_LIST,
        data
    }
}

export const fetchData = (param) => async(dispatch) => {
    const url = 'Portal/UserPanel/ViewLog/GetViewLogList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}