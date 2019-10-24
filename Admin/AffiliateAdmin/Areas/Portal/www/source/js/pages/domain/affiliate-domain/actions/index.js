import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SHOULD_RESET_SCROLL_POSITION, SET_SELECTED_DOMAIN_ID, SET_GRID_DATA } from './types';

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

export const setSelectedDomainId = (data) => {
    return {
        type: SET_SELECTED_DOMAIN_ID,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/Domain/GetAffDomain';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const getDomainDetail = (selectedDomainId) => async () => {
    const url = 'Portal/Domain/GetDomainDetail';
    const response = await post(url, { selectedDomainId });

    return response && response.status === 200 ? response.data : {};
}

export const insertDomain = (param) => async () => {
    const url = 'Portal/Domain/insertDomain';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateDomain = (param) => async () => {
    const url = 'Portal/Domain/UpdateDomain';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const deleteDomain = (selectedDomainId) => async () => {
    const url = 'Portal/Domain/DeleteDomain';
    const response = await post(url, { selectedDomainId });

    return response && response.status === 200 ? response.data : null;
}