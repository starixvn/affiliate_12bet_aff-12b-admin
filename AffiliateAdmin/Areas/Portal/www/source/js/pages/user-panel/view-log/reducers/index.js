import { combineReducers } from 'redux';
import { SET_PARAM, SET_SHOW_GROUP } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

const today = createReducer('', {});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const isShowGroup = createReducer(false, {
    [SET_SHOW_GROUP](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    today,
    param,
    isShowGroup
});