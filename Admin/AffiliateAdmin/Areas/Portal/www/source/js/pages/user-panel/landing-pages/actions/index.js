import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_SELECTED_PAGE, SET_PARAM, SET_SHOW_DETAIL } from '../actions/types';
import { post } from '../../../../modules/ajaxRequest';

export { setPagingOption, toggleLoader };

export const setSelectedPage = (data) => {
    return {
        type: SET_SELECTED_PAGE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setShowDetail = (data) => {
    return {
        type: SET_SHOW_DETAIL,
        data
    }
}

export const fetchData = (param) => async(dispatch) => {
    const url = 'Portal/UserPanel/LandingPages/GetLandingPagesList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async(dispatch) => {
    const url = 'Portal/UserPanel/LandingPages/GetLandingPagesDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}