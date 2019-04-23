import createReducer from '../../../../modules/createReducer';

import { SET_AFF_COMMON_INFO, SET_SELECTED_USERPANEL } from '../actions/types';

const affCommonInfo = createReducer(null, {
    [SET_AFF_COMMON_INFO](prevState, action) {
        return action.data === null ? null : {...action.data };
    }
});

const selectedUserPanel = createReducer(null, {
    [SET_SELECTED_USERPANEL](prevState, action) {
        return action.data;
    }
});

const now = new Date();
const defaultToday = `${now.getMonth()}/${now.getDate()}/${now.getFullYear()}`;
const today = createReducer(defaultToday, {});

export {
    affCommonInfo,
    today,
    selectedUserPanel
}