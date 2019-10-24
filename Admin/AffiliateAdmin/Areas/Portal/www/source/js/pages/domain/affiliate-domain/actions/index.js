import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';

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

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = '';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}
