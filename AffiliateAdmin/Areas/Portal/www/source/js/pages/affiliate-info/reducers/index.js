import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { SET_REPORT_TITLE, SET_PARAM } from '../actions/types';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';

const commGroupList = createReducer({}, {});

const languageList = createReducer({}, {});

const reportTitle = createReducer('', {
    [SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    pagingOption,
    reportTitle,
    param,
    commGroupList,
    languageList,
    isShowLoader
});