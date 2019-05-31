import { SET_SELECTED_USERPANEL, RESET_USER_PANEL, TOGGLE_LOADER_USER_PANEL } from './types';
import { userPanelMenuKey } from '../../common/modules/constants';
import { setParam as setDailyEarningParam } from '../../../user-panel/daily-earning/actions';
import { setParam as setDailyEarningDetailParam } from '../../../user-panel/daily-earning-detail/actions';

import { setParam as setPlayerDetailParam } from '../../../user-panel/player-detail/actions';
import { setParam as setReferrersParam } from '../../../user-panel/referrers/actions';
import { setParam as setTrackingStatisticsParam } from '../../../user-panel/tracking-statistics/actions';

export const setSelectedUserPanel = (data) => {
    return {
        type: SET_SELECTED_USERPANEL,
        data
    };
}

export const resetUserPanel = (data) => {
    return {
        type: RESET_USER_PANEL,
        data
    }
}

export const toggleLoaderUserPanel = (data) => {
    return {
        type: TOGGLE_LOADER_USER_PANEL,
        data
    }
}

export const redirectToDailyEarning = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.dailyEarning));
    dispatch(setDailyEarningParam(data));
}

export const redirectToDailyEarningDetail = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.dailyEarningDetail));
    dispatch(setDailyEarningDetailParam(data));
}

export const redirectToPlayerDetail = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.playerDetail));
    dispatch(setPlayerDetailParam(data));
}

export const redirectToTrackingStatistics = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.trackingStatistics));
    dispatch(setTrackingStatisticsParam(data));
}

export const redirectToReferrers = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.clickStatistics));
    dispatch(setReferrersParam(data));
}