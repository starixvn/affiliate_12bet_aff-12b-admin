import { post } from '../../../../modules/ajaxRequest';
import { Item } from '../../../admin-role/core/libs/resetAllProps';

export const isActiveMenu = (selectedMenu, menuItem) => {
    return selectedMenu && (selectedMenu.key === menuItem.key || selectedMenu.parent === menuItem.key);
}

export const getProductList = () => async() => {
    const response = await post('Portal/CommonSetting/GetProductList');
    return response && response.status === 200 ? response.data : [];
}

export const getPageList = () => async() => {
    const response = await post('Portal/CommonSetting/GetPageList');
    return response && response.status === 200 ? response.data : [];
}

export const getTrackingCodeList = (affId) => async() => {
	const response = await post('Portal/CommonSetting/GetTrackingCodeList', { affId });
	return response && response.status === 200 ? response.data : [];
}

export const getCountryList = () => async () => {
    const response = await post('Portal/CommonSetting/GetCountryList');
    return response && response.status === 200 ? convertCountryList(response.data) : [];
}

const convertCountryList = (data) => {
    return data.map(item => ({ ...item, Checked: true }));
}
