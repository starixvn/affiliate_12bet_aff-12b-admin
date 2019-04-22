import { SET_AFF_COMMON_INFO } from './types';
import customerStatisticsReducer from '../../customer-statistics/reducers';

import reducerRegistry from '../../../../modules/reducerRegistry';

export const setAffCommonInfo = (data) => {
    return {
        type: SET_AFF_COMMON_INFO,
        data
    }
}

export const loadCustomerStatisticsState = () => {
    reducerRegistry.register('customerStatistics', customerStatisticsReducer);
}

export const loadUserPanelState = (userPanelReducer) => {
    reducerRegistry.register('userPanel', userPanelReducer);
}