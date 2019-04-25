import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_PRODUCT_LIST, SET_PARAM } from '../actions/types';
import { pagingOption } from '../../../../reducers/pagingReducer';

const productList = createReducer([], {
    [SET_PRODUCT_LIST]: (preState, action) => {
        return [...action.data]
    }
})

const param = createReducer(null, {
    [SET_PARAM]: (preState, action) => {
        return {...action.data };
    }
})

export default combineReducers({
    productList,
    param,
    pagingOption
})