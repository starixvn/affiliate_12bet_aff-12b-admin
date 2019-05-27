import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { SET_AFF_NAME, SET_IS_VALID_USERNAME } from '../actions/types';
import { LEFTPANEL_RECEIVE_AFFINFO } from '../../mainpage/actions/types';

const affName = createReducer('', {
    [SET_AFF_NAME](prevState, action) {
        return action.data;
    }
});

const affiliateInfo = createReducer(null, {
    [LEFTPANEL_RECEIVE_AFFINFO](prevState, action) {
        return action.data && Object.assign({}, prevState, action.data);
    }
});

const isValidUserName = createReducer(true, {
    [SET_IS_VALID_USERNAME](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    affName,
    affiliateInfo,
    isValidUserName
});