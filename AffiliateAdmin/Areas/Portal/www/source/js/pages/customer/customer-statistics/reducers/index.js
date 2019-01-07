import { combineReducers } from 'redux';
import { SET_MONTHDATE } from '../actions/types';

import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const today = new Date();
const defaultMonthDate = `${today.getMonth()}/1/${today.getFullYear()}`;

const monthDate = createReducer(defaultMonthDate, {
    [SET_MONTHDATE](prevState, action) {
        return action.data;
    }
})

export const affId = createReducer(0, {});
export const affName = createReducer('', {});

export default combineReducers({
    monthDate,
    isShowLoader,
    affId,
    affName
});