import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import * as types from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setReportTitle = (data) => {
    return {
        type: types.SET_REPORT_TITLE,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    const url = 'Portal/FeedbackReport/GetFeedbackReportList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}