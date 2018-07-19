import {
    combineReducers
} from 'redux';
import createReducer from '../../../modules/createReducer';

import {
    RECEIVE_LEFTMENU_INFO
} from '../actions/types';

const leftMenuData = createReducer({}, {
    [RECEIVE_LEFTMENU_INFO](state, { data }) {
        return data;
    }
});


export default combineReducers({
    leftMenuData
});