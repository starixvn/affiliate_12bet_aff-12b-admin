import { post } from '../../../../modules/ajaxRequest';
import { userPanelMenuKey as menuKey } from '../../common/libs/constants';

export const isActiveMenu = (selectedMenu, menuItem) => {
    return selectedMenu && (selectedMenu.key === menuItemenu.key || selectedMenu.parent === menuItemenu.key);
}

export const getProductList = () => async() => {
    const response = await post('Portal/CommonSetting/GetProductList');
    return response && response.status === 200 ? response.data : [];
}

export const getCountryList = () => async () => {
    const response = await post('Portal/CommonSetting/GetCountryList');
    return response && response.status === 200 ? convertCountryList(response.data) : [];
}

export const getAllTargetLanguageList = () => async () => {
    const response = await post('Portal/CommonSetting/GetAllTargetLanguageList');
    return response && response.status === 200 ? response.data : [];
}

const convertCountryList = (data) => {
    return data.map(item => ({ ...item, Checked: true }));
}

export const isValidMenuByRenderClaim = (claimsPrincipal, menu, isPending) => {
    if (menu.key === menuKey.edit) {

    }
    return menu.renderClaim == undefined || claimsPrincipal.hasClaim(menu.renderClaim) || (Array.isArray(menu.renderClaim) && menu.renderClaimenu.some(claim => claimsPrincipal.hasClaim(claim)));
}