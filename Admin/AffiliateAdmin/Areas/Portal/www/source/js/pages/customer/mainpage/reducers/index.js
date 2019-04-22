import { combineReducers } from 'redux';

import reducerRegistry from '../../../../modules/reducerRegistry';
import leftPanelReducer from '../../left-panel/reducers';
import { affCommonInfo, today } from '../../left-panel/reducers/commonReducers';

reducerRegistry.register('today', today);
reducerRegistry.register('affCommonInfo', affCommonInfo);
reducerRegistry.register('leftPanel', leftPanelReducer);

export default combineReducers({
    today,
    affCommonInfo,
    leftPanel: leftPanelReducer
});