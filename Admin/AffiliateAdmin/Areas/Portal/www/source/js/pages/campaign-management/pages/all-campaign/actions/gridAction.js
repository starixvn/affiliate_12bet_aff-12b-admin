import * as types from './types';
import { post } from '@/modules/ajaxRequest';
import { toggleLoader } from '@/actions/loaderAction';
import { setPagingOption } from '@/actions/pagingAction';

export const setGridData = (item) => {
    return {
        type: types.SET_GRID_DATA,
        data: item,
    };
};

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    try {
        dispatch(toggleLoader(true));
        const url = 'Portal/CampaignManagement/GetCampaignManagementList';
        const response = await post(url, param);

        const data = response && response.status === 200 ? response.data : null;

        dispatch(setGridData(data ? data.MainData : []));
        dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
        dispatch(setShouldResetScrollPosition(true));
    } catch (error) {
        console.error(error);
    } finally {
        dispatch(toggleLoader(false));
    }
}