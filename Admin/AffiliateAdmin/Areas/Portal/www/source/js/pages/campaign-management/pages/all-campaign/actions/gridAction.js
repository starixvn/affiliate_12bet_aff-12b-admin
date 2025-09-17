import * as types from './types';
import { post } from '@/modules/ajaxRequest';
import { toggleLoader } from '@/actions/loaderAction';
import { setPagingOption } from '@/actions/pagingAction';
import { apiUrl } from '../constants/api';
import { showSuccessToast, showErrorToast } from '@/pages/common/components/Toast';


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
        const response = await post(apiUrl.getCampaignManagementList, param);

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

export const updateCampaign = (param, callback) => async (dispatch) => {
    try {
        dispatch(toggleLoader(true));
        const response = await post(apiUrl.updateCampaign, param);
        const isSuccess = response && response.status === 200 && response.data && response.data.Code === 0;
        if (isSuccess) {
            showSuccessToast("Update Success!", `Campaign "${param.CampaignName}" has been updated successfully.`);
        }
        else {
            showErrorToast("Update Failed!", `Failed to update campaign "${updateParam.CampaignName}". Please try again.`);
        }
    }
    catch (error) {
        console.error(error);
        showErrorToast("Update Failed!", `An error occurred while updating campaign "${updateParam.CampaignName}".`);
    } finally {
        dispatch(toggleLoader(false));
        callback && callback();
    }
}