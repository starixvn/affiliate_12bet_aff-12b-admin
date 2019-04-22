import { combineReducers } from 'redux';
import { SET_USER_ACCOUNT_INFO } from '../actions/types'
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const affId = createReducer(null, {});

const userAccountInfo = createReducer(null, {
    [SET_USER_ACCOUNT_INFO](prevState, action) {
        return  Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    isShowLoader,
    affId,
    userAccountInfo
});