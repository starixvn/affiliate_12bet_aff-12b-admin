import { post } from '../../../modules/ajaxRequest';
import { submitFail } from "../../../modules/constants";
import { toggleLoader } from '../../../actions/loaderAction';
import { SET_DATE, SET_GRID_DATA, SET_TOGGLE_POPUP, SET_SELECTED_EXCHANGE_RATE, SHOULD_RESET_SCROLL_POSITION } from './types';

export { toggleLoader };

export const setDate = (data) => {
    return {
        type: SET_DATE,
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

export const setSelectedExchangeRate = (data) => {
    return {
        type: SET_SELECTED_EXCHANGE_RATE,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const fetchData = (date) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ExchangeRate/GetExchangeRateList';
    const response = await post(url, { date });

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const updateExchangeRate = (selectedExchangeRate) => async () => {
    const url = 'Portal/ExchangeRate/UpdateExchangeRate';
    const response = await post(url, selectedExchangeRate);

    return response && response.status === 200 ? response.data : submitFail;
}