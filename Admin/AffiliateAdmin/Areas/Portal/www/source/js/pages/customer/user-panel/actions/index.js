import { SET_SELECTED_USERPANEL } from './types';
import { loadUserPanelState } from '../modules/service';

export const setSelectedUserPanel = (data) => (dispatch) => {
    loadUserPanelState(data);

    dispatch({
        type: SET_SELECTED_USERPANEL,
        data
    })
}