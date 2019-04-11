import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { LEFTPANEL_RECEIVE_AFFINFO } from '../actions/types';
import { isShowLoader } from '../../../../reducers/loaderReducer';

const affName = createReducer('',{});
const languageList = createReducer({},{});

const affiliateInfo = createReducer(null, {
    [LEFTPANEL_RECEIVE_AFFINFO](prevState, action) {
        return action.data && Object.assign({}, prevState, action.data);
    }
});



export default combineReducers({
    isShowLoader,
    affName,
    languageList,
    affiliateInfo
});