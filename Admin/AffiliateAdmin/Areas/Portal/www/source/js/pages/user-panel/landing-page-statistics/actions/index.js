import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_SELECTED_PAGE, SET_PARAM } from '../actions/types';
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

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/LandingPageStatistics/GetLandingPageStatisticsList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async () => {
    const url = 'Portal/UserPanel/LandingPageStatistics/GetLandingPageStatisticsDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}