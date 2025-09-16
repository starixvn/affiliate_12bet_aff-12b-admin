import { combineReducers } from 'redux';

import * as filterReducer from './filterReducer';
import * as gridReducer from './gridReducer';
import { pagingOption } from '@/reducers/pagingReducer';
import { isShowLoader } from '@/reducers/loaderReducer';

export default combineReducers(
    Object.assign({}, {
    ...filterReducer,
    ...gridReducer,
    pagingOption,
    isShowLoader,
}));    