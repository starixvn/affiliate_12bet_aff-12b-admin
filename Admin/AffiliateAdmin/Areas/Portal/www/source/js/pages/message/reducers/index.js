import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';

const today = createReducer(null, {});

export default combineReducers({
    today,
    isShowLoader,
    pagingOption
});