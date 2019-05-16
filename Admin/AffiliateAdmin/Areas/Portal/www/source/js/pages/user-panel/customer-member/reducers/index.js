import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

const canViewMemberEmail = createReducer(false, {});

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return { ...action.data };
    }
})

export default combineReducers({
    isShowLoader,
    pagingOption,
    param,
    canViewMemberEmail
});