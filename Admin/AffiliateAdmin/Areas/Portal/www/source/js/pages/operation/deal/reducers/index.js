import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { claimsPrincipal } from '../../../../reducers/claimReducer';

import * as types from '../actions/types';

const dealTypeList = createReducer({}, {});

const param = createReducer(null, {
    [types.SET_PARAM]: (prevState, action) => {
        return action.data;
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    claimsPrincipal,
    param,
    dealTypeList
});
