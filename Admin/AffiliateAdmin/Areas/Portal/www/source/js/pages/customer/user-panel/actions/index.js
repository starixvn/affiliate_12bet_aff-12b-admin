import { SET_SELECTED_USERPANEL } from './types';
import { userPanelMenuKey } from '../../common/modules/constants';
import { setParam as setDailyEarningParam } from '../../../user-panel/daily-earning/actions';
import { setParam as setDailyEarningDetailParam } from '../../../user-panel/daily-earning-detail/actions';

import { setParam as setPlayerDetailParam } from '../../../user-panel/player-detail/actions';
import { setParam as setReferrersParam } from '../../../user-panel/referrers/actions';

export const setSelectedUserPanel = (data) => {
    return {
        type: SET_SELECTED_USERPANEL,
        data
    };
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

export const redirectToTrackingStatistics = () => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.trackingStatistics));
}

export const redirectToReferrers = (data) => (dispatch) => {
    dispatch(setSelectedUserPanel(userPanelMenuKey.clickStatistics));
    dispatch(setReferrersParam(data));
}