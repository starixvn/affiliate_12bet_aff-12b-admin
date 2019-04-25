import { SET_SELECTED_USERPANEL } from './types';
import { loadUserPanelState } from '../modules/service';
import { menuKey } from '../modules/menu';
import { setParam } from '../../../user-panel/daily-earning/actions';
import reducerRegistry from '../../../../modules/reducerRegistry';

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
    dispatch(setParam(data));
}