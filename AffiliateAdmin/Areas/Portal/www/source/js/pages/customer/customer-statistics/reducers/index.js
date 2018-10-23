import { combineReducers } from 'redux';
import { SET_PARAM } from '../actions/types';

import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const monthDate = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    monthDate,
    isShowLoader
});