import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_MAIN_VIEW_PARAM, SET_DETAIL_VIEW_PARAM, SET_SELECTED_AFF } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setMainViewParam = (data) => {
    return {
        type: SET_MAIN_VIEW_PARAM,
        data
    }
}

export const setDetailViewParam = (data) => {
    return {
        type: SET_DETAIL_VIEW_PARAM,
        data
    }
}

export const setSelectedAff = (data) => {
    return {
        type: SET_SELECTED_AFF,
        data
    }
}

export const getLogActionList = () => async () => {
    const url = 'Portal/UserPanel/ViewLog/GetLogActionList';

    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const fetchData = (param) => async() => {
    const url = 'Portal/ViewLog/GetNewViewLogList';

    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async () => {
    const url = 'Portal/ViewLog/GetNewViewLogDetailList';

    const response = await post(url, param,);

    return response && response.status === 200 ? response.data : null;
}