import { combineReducers } from 'redux';
import { SET_PRODUCT_LIST, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

export const productList = createReducer([], {
    [SET_PRODUCT_LIST]: (preState, action) => {
        return [...action.data]
    }
})

export const param = createReducer({}, {
    [SET_PARAM]: (prevState, action) => {
        return { ...action.data };
    }
})

export default combineReducers({
    productList,
    param,
    pagingOption
})