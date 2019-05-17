import { combineReducers } from 'redux';
import { RESET_USER_PANEL } from '../../common/actions/types';

import reducerRegistry from '../../../../modules/reducerRegistry';
import leftPanel from '../../left-panel/reducers';
import { affCommonInfo, today } from './commonReducer';
import { selectedUserPanel } from '../../user-panel/reducers';
import { userPanelReducer } from '../../user-panel/reducers';
import createReducer from '../../../../modules/createReducer';

// reducerRegistry.register('today', today);
// reducerRegistry.register('selectedUserPanel', selectedUserPanel);
// reducerRegistry.register('affCommonInfo', affCommonInfo);
// reducerRegistry.register('leftPanel', leftPanel);

const allReducers = combineReducers({
    today,
    selectedUserPanel,
    affCommonInfo,
    leftPanel,
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

// export default combineReducers({
//     today,
//     selectedUserPanel,
//     affCommonInfo,
//     leftPanel,
//     customerStatistics,
//     ...userPanelReducer
// });