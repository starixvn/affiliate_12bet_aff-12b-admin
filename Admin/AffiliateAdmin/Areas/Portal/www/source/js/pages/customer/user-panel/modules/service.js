import reducerRegistry from '../../../../modules/reducerRegistry';
import { userPanelReducer } from './menu';
import { post } from '../../../../modules/ajaxRequest';

export const loadUserPanelState = (selectedUserPanel) => {
    // const reducer = selectedUserPanel ? userPanelReducer[selectedUserPanel] : null;
    // reducerRegistry.register('userPanel', reducer);
}

export const isActiveMenu = (selectedMenu, menuItem) => {
    return selectedMenu && (selectedMenu.key === menuItem.key || selectedMenu.parent === menuItem.key);
}

export const getProductList = async() => {
    const response = await post('Portal/CommonSetting/GetProductList');
    return response && response.status === 200 ? response.data : [];
}