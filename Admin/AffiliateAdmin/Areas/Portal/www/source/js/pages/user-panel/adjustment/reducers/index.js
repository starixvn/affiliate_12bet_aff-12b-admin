import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { isShowLoader } from '../../../../reducers/loaderReducer';

export const affInfo = createReducer({}, {});

export default combineReducers({
    isShowLoader,
    affInfo
});