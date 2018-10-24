import { combineReducers } from 'redux';
import { SET_MONTHDATE } from '../actions/types';

import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const today = new Date();
const iniatialState = `${today.getMonth()}/1/${today.getFullYear()}`;

const monthDate = createReducer(iniatialState, {
    [SET_MONTHDATE](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    monthDate,
    isShowLoader
});