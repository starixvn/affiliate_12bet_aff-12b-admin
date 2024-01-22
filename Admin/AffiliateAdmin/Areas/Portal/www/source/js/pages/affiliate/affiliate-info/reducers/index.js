import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import * as type from '../actions/types';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { showPopupPassword } from '../../../common/reducers';
import { claimsPrincipal } from '../../../../reducers/claimReducer';

export const groupId = createReducer(0, {});

export const targetCountryList = createReducer({}, {
    [type.SET_TARGET_COUNTRY_LIST](prevState, action) {
        return action.data;
    }
});

export const commGroupList = createReducer({}, {
    [type.SET_COMM_GROUP_LIST](prevState, action){
        return action.data;
    }
});

export const languageList = createReducer({}, {
    [type.SET_LANGUAGE_LIST](prevState, action){
        return action.data;
    }
});

export const reportTitle = createReducer('', {
    [type.SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [type.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const showPopupCommGroup = createReducer(false, {
    [type.SET_SHOW_POPUP_COMMGROUP](prevState, action) {
        return action.data;
    }
})

export const affId = createReducer(0, {
    [type.SET_AFFID](prevState, action) {
        return action.data;
    }
})

export const commGroup = createReducer(0, {
    [type.SET_COMM_GROUP](prevState, action) {
        return action.data;
    }
})

export const columnList = createReducer(0, {
    [type.SET_SELECTED_COLUMN](prevState, action) {
        return [...action.data];
    }
})

export const isClickSearch = createReducer(false, {
    [type.SET_IS_CLICK_SEARCH](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    pagingOption,
    reportTitle,
    param,
    targetCountryList,
    commGroupList,
    languageList,
    isShowLoader,
    showPopupPassword,
    showPopupCommGroup,
    affId,
    commGroup,
    groupId,
    columnList,
    claimsPrincipal,
    isClickSearch
});