import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';

import {
    SET_PARAM, SET_PARAM_FROM_HOME, SET_BANNER_DETAIL_URL, SET_CHECKED_BANNERS, SET_SELECTED_BANNER_ID, SET_BANNER_ROOT_URL,
    SET_SUBCATEGORY_CASINO_LIST, SET_SUBCATEGORY_SPORTSBOOK_LIST, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_SIZE_LIST
} from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setParamFromHome = (data) => {
    return {
        type: SET_PARAM_FROM_HOME,
        data
    }
}

export const setBannerDetailUrl = (data) => {
    return {
        type: SET_BANNER_DETAIL_URL,
        data
    }
}

export const setSelectedBannerId = (data) => {
    return {
        type: SET_SELECTED_BANNER_ID,
        data
    }
}

export const setCheckedBanners = (data) => {
    return {
        type: SET_CHECKED_BANNERS,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setBannerRootUrl = (data) => {
    return {
        type: SET_BANNER_ROOT_URL,
        data
    }
}

export const setSubCategoryCasinoList = (data) => {
    return {
        type: SET_SUBCATEGORY_CASINO_LIST,
        data
    }
}

export const setSubCategorySportbooksList = (data) => {
    return {
        type: SET_SUBCATEGORY_SPORTSBOOK_LIST,
        data
    }
}

export const setSizeList = (data) => {
    return {
        type: SET_SIZE_LIST,
        data
    }
}

export const getBannerRootUrl = () => async (dispatch) => {
    const url = 'Portal/MediaGallery/GetBannerRootUrl';
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setBannerRootUrl(data));
}

export const getSubCategoryCasinoList = () => async (dispatch) => {
    const url = 'Portal/MediaGallery/GetSubCategoryCasinoList';
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setSubCategoryCasinoList(data));
}

export const getSubCategorySportsbookList = () => async (dispatch) => {
    const url = 'Portal/MediaGallery/GetSubCategorySportsbookList';
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setSubCategorySportbooksList(data));
}

export const getSizeList = (param) => async (dispatch) => {
    const url = 'Portal/MediaGallery/GetSizeList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setSizeList(data));
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    
    const url = 'Portal/MediaGallery/GetBannerList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedBanners = data ? _convertCheckedBanners(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedBanners(checkedBanners));
    dispatch(toggleLoader(false));
}

function _convertCheckedBanners(dataList) {
    const checkedBanners = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].BannerId;
        checkedBanners[itemId] = false;
    }

    return checkedBanners;
}

export const getBannerDetail = (selectedBannerId) => async () => {
    const url = 'Portal/MediaGallery/GetBannerDetail';
    const response = await post(url, { selectedBannerId });

    return response && response.status === 200 ? response.data : null;
}

export const insertBanner = (param) => async () => {
    const url = 'Portal/MediaGallery/insertBanner';

    return await handleExcute(url, param);
}

export const updateBanner = (param) => async () => {
    const url = 'Portal/MediaGallery/UpdateBanner';

    return await handleExcute(url, param);
}

const handleExcute = async (url, param) => {
    const formData = new FormData();
    for (let key in param ) {
        formData.append(key, param[key]);
    }

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : null;
}

export const deleteBanners = (BannerIdList) => async () => {
    const url = 'Portal/MediaGallery/DeleteBanners';
    const response = await post(url, { BannerIdList });

    return response && response.status === 200 ? response.data : null;
}