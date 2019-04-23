import { combineReducers } from 'redux';

import reducerRegistry from '../../../modules/reducerRegistry';
import leftPanel from '../left-panel/reducers';
import { affCommonInfo, today, selectedUserPanel } from '../left-panel/reducers/commonReducers';

reducerRegistry.register('today', today);
reducerRegistry.register('selectedUserPanel', selectedUserPanel);
reducerRegistry.register('affCommonInfo', affCommonInfo);
reducerRegistry.register('leftPanel', leftPanel);

export default combineReducers({
    today,
    selectedUserPanel,
    affCommonInfo,
    leftPanel
});