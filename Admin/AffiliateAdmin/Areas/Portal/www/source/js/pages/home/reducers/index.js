import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { SET_GRID_DATA } from '../actions/types';

export const today = createReducer(null, {});

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    today,
    gridData,
    pagingOption,
    isShowLoader
});