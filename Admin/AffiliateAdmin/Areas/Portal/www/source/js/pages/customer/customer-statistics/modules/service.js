import customerStatisticsReducer from '../actions';
import reducerRegistry from '../../../../modules/reducerRegistry';

export const loadCustomerStatisticsState = () => {
    reducerRegistry.register('customerStatistics', customerStatisticsReducer);
}