import { post } from '../../../../modules/ajaxRequest';
import { userPanelMenuKey as menuKey } from '../../common/libs/constants';
import { Claim, claimResource } from '../../../../modules/claim';

export const isActiveMenu = (selectedMenu, menuItem) => {
    return selectedMenu && (selectedMenu.key === menuItem.key || selectedMenu.parent === menuItem.key);
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

export const isValidMenuByRenderClaim = (claimsPrincipal, menu, isPendingAff) => {
    if (isPendingAff && menu.key === menuKey.edit) {
        return claimsPrincipal.hasClaim(menu.renderClaim) || claimsPrincipal.hasClaim(new Claim(claimResource.ManagePendingAff))
    }
    return menu.renderClaim == undefined || claimsPrincipal.hasClaim(menu.renderClaim) || (Array.isArray(menu.renderClaim) && menu.renderClaim.some(claim => claimsPrincipal.hasClaim(claim)));
}