import reducerRegistry from '../../../../modules/reducerRegistry';
import { userPanelReducer } from './menu';

export const loadUserPanelState = (selectedUserPanel) => {
    reducerRegistry.register('userPanel', userPanelReducer[selectedUserPanel]);
}