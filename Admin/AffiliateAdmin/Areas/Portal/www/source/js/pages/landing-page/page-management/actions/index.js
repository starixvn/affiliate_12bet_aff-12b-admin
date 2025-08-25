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

export const setLandingPageDetailUrl = (data) => {
    return {
        type: types.SET_LANDING_PAGE_DETAIL_URL,
        data
    }
}

export const setSelectedLandingPageId = (data) => {
    return {
        type: types.SET_SELECTED_LANDING_PAGE_ID,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/PageManagement/GetPageManagementList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data : []));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}
