import {
    combineReducers
} from 'redux';
import createReducer from '../../../modules/createReducer';

import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_UPDATE_LANGUAGE_SUCCESS
} from '../actions/types';

const leftMenuData = createReducer({}, {
    [LEFTPANEL_RECEIVE_AFFINFO](state, { data }) {
        return data;
    },
    [LEFTPANEL_UPDATE_LANGUAGE_SUCCESS](state, { data }) {
        const generalInfo = Object.assign({}, state.GeneralInfo, { PreferredLanguage: data });
        return Object.assign({}, state, { GeneralInfo: generalInfo });
    }
});


export default combineReducers({
    leftMenuData
});