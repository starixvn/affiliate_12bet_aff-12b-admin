import { combineReducers } from 'redux';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

import createReducer from '../../../../modules/createReducer';
import * as types from '../actions/types';

const now = new Date();
const defaultToday = `${now.getMonth() + 1}/${now.getFullYear()}`;
const today = createReducer(defaultToday, {});

export default combineReducers({
    today,
    pagingOption,
    isShowLoader,
});
