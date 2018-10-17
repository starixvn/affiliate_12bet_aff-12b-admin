import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

const currentDate = createReducer('', {});

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    isShowLoader,
    param,
    currentDate
});