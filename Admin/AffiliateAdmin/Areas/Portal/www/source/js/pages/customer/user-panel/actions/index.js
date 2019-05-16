import { SET_SELECTED_USERPANEL } from './types';
import { loadUserPanelState } from '../modules/service';
import { menuKey } from '../modules/menu';
import { setParam as setDailyEarningParam } from '../../../user-panel/daily-earning/actions';
import { setParam as setDailyEarningDetailParam } from '../../../user-panel/daily-earning-detail/actions';

import { setParam as setPlayerDetailParam } from '../../../user-panel/player-detail/actions';
import { setParam as setReferrersParam } from '../../../user-panel/referrers/actions';

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

export const setParamForPlayerDetail = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(menuKey.playerDetail));
    dispatch(setPlayerDetailParam(data));
}

export const setParamForTrackingStatistics = () => (dispatch) => {
    dispatch(setSelectedUserPanel(menuKey.trackingStatistics));
}

export const setParamForReferrers = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(menuKey.clickStatistics));
    dispatch(setReferrersParam(data));
}