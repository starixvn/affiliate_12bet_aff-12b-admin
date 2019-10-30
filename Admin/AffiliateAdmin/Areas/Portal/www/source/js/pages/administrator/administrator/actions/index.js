import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';
import { getDomainList } from '../../domain/actions/index';

export { toggleLoader };

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const fetchData = (market) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = '';
    const response = await post(url, { market });

    const data = response && response.status === 200 ? response.data : [];

    dispatch(setGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const insertAdmin = (param) => async () => {
    const url = '';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateAdmin = (param) => async () => {
    const url = '';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}