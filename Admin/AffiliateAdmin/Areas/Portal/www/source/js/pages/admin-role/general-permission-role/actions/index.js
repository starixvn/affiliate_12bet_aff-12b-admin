import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_GRID_DATA, SET_IS_GENERAL } from './types';

export { toggleLoader };

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setIsGeneral = (data) => {
    return {
        type: SET_IS_GENERAL,
        data
    }
}

export const fetchData = (isGeneral, userId) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = `Portal/PermissionRole/${isGeneral ? 'GetFunctionList' : 'GetLanguageList'}`;
    const response = await post(url, { userId });

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(toggleLoader(false));
}