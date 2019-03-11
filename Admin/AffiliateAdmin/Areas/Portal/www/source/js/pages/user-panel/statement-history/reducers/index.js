import { combineReducers } from 'redux';
import { SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

const today = createReducer(null, {});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    today,
    param
});