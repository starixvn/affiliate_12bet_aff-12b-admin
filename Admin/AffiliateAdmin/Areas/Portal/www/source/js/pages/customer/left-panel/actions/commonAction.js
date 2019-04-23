import { SET_AFF_COMMON_INFO, SET_SELECTED_USERPANEL } from './types';
import customerStatisticsReducer from '../../customer-statistics/reducers';

import reducerRegistry from '../../../../modules/reducerRegistry';
import { userPanelReducer } from '../../modules/userPanelMenu';

export const setAffCommonInfo = (data) => {
    return {
        type: SET_AFF_COMMON_INFO,
        data
    }
}

export const setSelectedUserPanel = (data) => {
    return {
        type: SET_SELECTED_USERPANEL,
        data
    }
}

export const loadCustomerStatisticsState = () => {
    reducerRegistry.register('customerStatistics', customerStatisticsReducer);
}

export const loadUserPanelState = (selectedUserPanel) => {
    reducerRegistry.register('userPanel', userPanelReducer[selectedUserPanel]);
}