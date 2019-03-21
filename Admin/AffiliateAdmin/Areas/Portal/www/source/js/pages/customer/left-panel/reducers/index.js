import {
    combineReducers
} from 'redux';
import createReducer from '../../../../modules/createReducer';

import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_UPDATE_LANGUAGE_SUCCESS,
    LEFTPANEL_UPDATE_CLOSED_SUCCESS,
    LEFTPANEL_UPDATE_AFFTYPE_SUCCESS
} from '../actions/types';

const affiliateInfo = createReducer({}, {
    [LEFTPANEL_RECEIVE_AFFINFO](prevState, data) {
        return data;
    },
    [LEFTPANEL_UPDATE_LANGUAGE_SUCCESS](prevState, data) {
        const generalInfo = Object.assign({}, prevState.GeneralInfo, { PreferredLanguage: data });
        return Object.assign({}, prevState, { GeneralInfo: generalInfo });
    },
    [LEFTPANEL_UPDATE_CLOSED_SUCCESS](prevState, data) {
        const generalInfo = Object.assign({}, prevState.GeneralInfo, { Closed: data });
        return Object.assign({}, prevState, { GeneralInfo: generalInfo });
    },
    [LEFTPANEL_UPDATE_AFFTYPE_SUCCESS](prevState, data) {
        const generalInfo = Object.assign({}, prevState.GeneralInfo, { AffType: data });
        return Object.assign({}, prevState, { GeneralInfo: generalInfo });
    }
});


export default combineReducers({
    affiliateInfo
});