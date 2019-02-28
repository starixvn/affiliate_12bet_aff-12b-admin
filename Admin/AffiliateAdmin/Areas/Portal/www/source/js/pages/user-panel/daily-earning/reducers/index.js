import { combineReducers } from 'redux';
import { SET_PARAM } from '../actions/types';

import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

const productList = createReducer([], {})

export default combineReducers({
    productList,
    param,
    isShowLoader
});