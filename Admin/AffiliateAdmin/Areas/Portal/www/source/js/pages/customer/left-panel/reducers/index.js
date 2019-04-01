import {
    combineReducers
} from 'redux';
import createReducer from '../../../../modules/createReducer';

import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_RECEIVE_ONLINELIST
} from '../actions/types';

const affiliateInfo = createReducer({}, {
    [LEFTPANEL_RECEIVE_AFFINFO](prevState, data) {
        return Object.assign({}, data);
    }
});

const onlineList = createReducer({}, {
    [LEFTPANEL_RECEIVE_ONLINELIST](prevState, data) {
        return Object.assign({}, data);
    }
});


export default combineReducers({
    affiliateInfo,
    onlineList
});