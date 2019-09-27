import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_PARAM, SET_BANNER_DETAIL_URL, SET_CHECKED_BANNERS, SET_SELECTED_BANNER_ID, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
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

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/BannerList/GetBannerList';
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
    const url = 'Portal/BannerList/GetBannerDetail';
    const response = await post(url, { selectedBannerId });

    return response && response.status === 200 ? response.data : {};
}

export const insertBanner = (param) => async () => {
    const url = 'Portal/BannerList/insertBanner';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateBanner = (param) => async () => {
    const url = 'Portal/BannerList/UpdateBanner';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const deleteBanners = (BannerIdList) => async () => {
    const url = 'Portal/BannerList/DeleteBanners';
    const response = await post(url, { BannerIdList });

    return response && response.status === 200 ? response.data : null;
}