import customerStatisticsReducer from '../reducers';
import reducerRegistry from '../../../../modules/reducerRegistry';

export const loadCustomerStatisticsState = () => {
    reducerRegistry.register('customerStatistics', customerStatisticsReducer);
}