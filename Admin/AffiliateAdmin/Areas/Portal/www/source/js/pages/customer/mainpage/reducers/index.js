import { combineReducers } from 'redux';
import { RESET_USER_PANEL } from '../../user-panel/actions/types';

import leftPanel from '../../left-panel/reducers';
import { affCommonInfo, today, languageList, hasTurnOverPermission } from './commonReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import { userPanelReducer, selectedUserPanel, isShowLoaderUserPanel } from '../../user-panel/reducers';
import customerStatistics from '../../customer-statistics/reducers';
import { isShowLoader } from '../../../../reducers/loaderReducer';

const allReducers = combineReducers({
    today,
    isShowLoader,
    languageList,
    hasTurnOverPermission,
    claimsPrincipal,
    selectedUserPanel,
    isShowLoaderUserPanel,
    affCommonInfo,
    leftPanel,
    customerStatistics,
    ...userPanelReducer
});

export const rootReducer = (state, action) => {
    if (action.type === RESET_USER_PANEL) {
        const newState = { ...state };

        const userPanelName = action.data;
        newState[userPanelName] = {};

        return allReducers(newState, action);
    }

    return allReducers(state, action);
};

export default rootReducer;