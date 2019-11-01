import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_PARAM } from './types';

export { toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

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

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    console.log(param);
    const url = 'Portal/Administrator/GetAdminList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : [];

    dispatch(setGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const insertAdmin = (param) => async () => {
    const url = 'Portal/Administrator/InsertAdmin';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateAdmin = (param) => async () => {
    const url = 'Portal/Administrator/UpdateAdmin';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}