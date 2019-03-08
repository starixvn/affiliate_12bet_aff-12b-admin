import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { isShowLoader } from '../../../../reducers/loaderReducer';

export const affInfor = createReducer({}, {});

export default combineReducers({
    isShowLoader,
    affInfor
});