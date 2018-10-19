import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

const today = createReducer('', {});
const canViewMemberEmail = createReducer(false, {});

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    isShowLoader,
    param,
    today,
    canViewMemberEmail
});