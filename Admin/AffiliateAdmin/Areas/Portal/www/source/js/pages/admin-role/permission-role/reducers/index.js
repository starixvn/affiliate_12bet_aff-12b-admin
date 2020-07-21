import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

export default combineReducers({ isShowLoader });