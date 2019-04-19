import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';

import { SET_AFF_COMMON_INFO } from '../actions/types';
import leftPanelReducer from '../../left-panel/reducers';
//import customerStatisticsReducer from '../../customer-statistics/reducers';

export const affCommonInfo = createReducer(null, {
    [SET_AFF_COMMON_INFO](prevState, action) {
        return action.data === null ? null : {...action.data };
    }
});

export default combineReducers({
    affCommonInfo,
    leftPanel: leftPanelReducer
});