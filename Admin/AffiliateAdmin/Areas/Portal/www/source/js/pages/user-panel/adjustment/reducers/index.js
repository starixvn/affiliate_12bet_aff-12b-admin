import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { isShowLoader } from '../../../../reducers/loaderReducer';

export const affId = createReducer(0, {});
export default combineReducers({
    isShowLoader,
    affId
});