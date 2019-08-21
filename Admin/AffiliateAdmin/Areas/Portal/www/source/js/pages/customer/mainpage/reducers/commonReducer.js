import createReducer from '../../../../modules/createReducer';

import { SET_AFF_COMMON_INFO } from '../actions/types';

const affCommonInfo = createReducer(null, {
    [SET_AFF_COMMON_INFO](prevState, action) {
        return {...action.data };
    }
});

const now = new Date();
const defaultToday = `${now.getMonth()}/${now.getDate()}/${now.getFullYear()}`;
const today = createReducer(defaultToday, {});
const languageList = createReducer(null, {});

export {
    affCommonInfo,
    today,
    languageList
}