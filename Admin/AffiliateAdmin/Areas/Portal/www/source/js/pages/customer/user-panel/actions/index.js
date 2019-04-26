import { SET_SELECTED_USERPANEL } from './types';
import { loadUserPanelState } from '../modules/service';
import { menuKey } from '../modules/menu';
import { setParam as setDailyEarningParam } from '../../../user-panel/daily-earning/actions';
import { setParam as setDailyEarningDetailParam } from '../../../user-panel/daily-earning-detail/actions';

const activeUserPanel = (data) => {
    return {
        type: SET_SELECTED_USERPANEL,
        data
    };
}

export const setSelectedUserPanel = (data) => (dispatch) => {
    loadUserPanelState(data);
    dispatch(activeUserPanel(data))
}

export const setParamForDailyEarning = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(menuKey.dailyEarning));
    dispatch(setDailyEarningParam(data));
}

export const setParamForDailyEarningDetail = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(menuKey.dailyEarningDetail));
    dispatch(setDailyEarningDetailParam(data));
}