import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { getPageList, getCountryList } from '../../../customer/user-panel/libs/service';
import { setSelectedUserPanel, toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import * as types from '../actions/types';

export { setPagingOption, toggleLoaderUserPanel, setSelectedUserPanel, getPageList, getCountryList };

export const setSelectedPageDetailFilter = (data) => {
    return {
        type: types.SET_SELECTED_PAGE_DETAIL_FILTER,
        data
    }
}

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setShortCodeLanguageList = (data) => {
    return {
        type: types.SET_SHORT_CODE_LANGUAGE_LIST,
        data
    }
}

export const setIsGroupByCountry = (data) => {
    return {
        type: types.SET_IS_GROUP_BY_COUNTRY,
        data
    }
}

export const fetchData = (param, shortCodeLanguageList) => async () => {
    const url = 'Portal/UserPanel/LandingPageStatistics/GetLandingPageStatisticsList';
    let languageCode = param.languageCode;

    if (!languageCode) {
        var allShortCode = Object.keys(shortCodeLanguageList).map(key => key).join(',');

        languageCode = allShortCode;
    }

    const response = await post(url, Object.assign({}, param, { languageCode }));

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async () => {
    const url = 'Portal/UserPanel/LandingPageStatistics/GetLandingPageStatisticsDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}