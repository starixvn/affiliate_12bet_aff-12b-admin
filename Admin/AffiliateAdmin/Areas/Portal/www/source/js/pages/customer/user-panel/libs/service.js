import { post } from '../../../../modules/ajaxRequest';

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
    //const response = await post('Portal/CommonSetting/GetCountryList');
    //return response && response.status === 200 ? response.data : [];

    return countryList;
}

export const countryList = [
    {
        CountryId: 0,
        CountryName: 'Unknown',
        Checked: true
    },
    {
        CountryId: 1,
        CountryName: 'Country 1',
        Checked: true
    },
    {
        CountryId: 2,
        CountryName: 'Country 2',
        Checked: true
    }, {
        CountryId: 3,
        CountryName: 'Country 3',
        Checked: true
    },
    {
        CountryId: 4,
        CountryName: 'Country 4',
        Checked: true
    },
    {
        CountryId: 5,
        CountryName: 'Country 5',
        Checked: true
    }
]
