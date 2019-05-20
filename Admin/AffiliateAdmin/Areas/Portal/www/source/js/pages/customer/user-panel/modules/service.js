import { post } from '../../../../modules/ajaxRequest';

export const isActiveMenu = (selectedMenu, menuItem) => {
    return selectedMenu && (selectedMenu.key === menuItem.key || selectedMenu.parent === menuItem.key);
}

export const getProductList = async() => {
    const response = await post('Portal/CommonSetting/GetProductList');
    return response && response.status === 200 ? response.data : [];
}

export const getLanguageList = async() => {
    const response = await post('Portal/CommonSetting/GetLanguageList');
    return response && response.status === 200 ? response.data : [];
}

export const getPageList = async() => {
    const response = await post('Portal/CommonSetting/GetPageList');
    return response && response.status === 200 ? response.data : [];
}