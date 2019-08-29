import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { SET_PARAM, SET_GRID_DATA, SET_TOGGLE_POPUP, SET_SELECTED_CURRENTCY, SHOULD_RESET_SCROLL_POSITION } from './types';

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setTogglePopup = (data) => {
    return {
        type: SET_TOGGLE_POPUP,
        data
    }
}

export const setSelectedCurrency = (data) => {
    return {
        type: SET_SELECTED_CURRENTCY,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ExchangeRate/GetExchangeRateList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const updateExchangeRate = (selectedCurrency) => async () => {
    const url = 'Portal/ExchangeRate/UpdateCurrency';
    const response = await post(url, selectedCurrency);

    return response && response.status === 200 ? response.data : null;
}