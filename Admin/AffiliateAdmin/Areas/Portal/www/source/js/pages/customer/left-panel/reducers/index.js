import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { SET_AFF_NAME } from '../actions/types';
import { LEFTPANEL_RECEIVE_AFFINFO } from '../../mainpage/actions/types';

const affName = createReducer('', {
    [SET_AFF_NAME](prevState, action) {
        return action.data;
    }
});

const languageList = createReducer({}, {});

const affiliateInfo = createReducer(null, {
    [LEFTPANEL_RECEIVE_AFFINFO](prevState, action) {
        return action.data && Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    affName,
    languageList,
    affiliateInfo
});