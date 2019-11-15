import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { SET_ONLINE_LIST, SET_PSP_TODAY, SET_DEPOSIT_INFO, SET_GENERAL_INFO } from '../actions/types';

export const onlineList = createReducer(null, {
    [SET_ONLINE_LIST](prevState, action) {
        return action.data;
    }
});

export const pSPToday = createReducer(null, {
    [SET_PSP_TODAY](prevState, action) {
        return action.data;
    }
});

export const depositInfo = createReducer(null, {
    [SET_DEPOSIT_INFO](prevState, action) {
        return action.data;
    }
});

export const generalInfo = createReducer(null, {
    [SET_GENERAL_INFO](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    onlineList,
    pSPToday,
    depositInfo,
    generalInfo,
    pagingOption,
    isShowLoader
});