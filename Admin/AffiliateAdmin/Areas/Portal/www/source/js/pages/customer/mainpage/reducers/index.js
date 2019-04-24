import { combineReducers } from 'redux';

import reducerRegistry from '../../../../modules/reducerRegistry';
import leftPanel from '../../left-panel/reducers';
import { affCommonInfo, today } from './commonReducer';
import { selectedUserPanel } from '../../user-panel/reducers';

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