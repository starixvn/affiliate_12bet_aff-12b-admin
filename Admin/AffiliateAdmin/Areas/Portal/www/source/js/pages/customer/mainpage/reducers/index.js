import { combineReducers } from 'redux';
import { RESET_USER_PANEL } from '../../common/actions/types';

import leftPanel from '../../left-panel/reducers';
import { affCommonInfo, today } from './commonReducer';
import { selectedUserPanel } from '../../user-panel/reducers';
import { userPanelReducer } from '../../user-panel/reducers';
import customerStatistics from '../../customer-statistics/reducers';

const allReducers = combineReducers({
    today,
    selectedUserPanel,
    affCommonInfo,
    leftPanel,
    customerStatistics,
    ...userPanelReducer
});

export const rootReducer = (state, action) => {
    const newState = {...state };
    if (action.type === RESET_USER_PANEL) {
        const userPanelName = action.data;
        newState[userPanelName] = {};
    }

    return allReducers(newState, action);
};

export default rootReducer;