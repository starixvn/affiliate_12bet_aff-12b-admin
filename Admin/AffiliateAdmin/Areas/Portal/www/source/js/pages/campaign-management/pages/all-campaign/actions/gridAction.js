import * as types from './types';
import { post } from '@/modules/ajaxRequest';
import { toggleLoader } from '@/actions/loaderAction';
import { setPagingOption } from '@/actions/pagingAction';
import { apiUrl } from '../constants/api';
import { showSuccessToast, showErrorToast } from '@/pages/common/components/Toast';

// Create a wrapper for post requests that disables alerts
const postWithoutAlert = async (url, data) => {
    const originalAlert = window.alert;
    window.alert = () => { }; // Temporarily disable alerts

    try {
        const result = await post(url, data);
        return result;
    } finally {
        window.alert = originalAlert; // Restore original alert
    }
};


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
        const response = await postWithoutAlert(apiUrl.updateCampaign, param);
        const isSuccess = response && response.status === 200 && response.data && response.data.Code === 0;
        if (isSuccess) {
            showSuccessToast("Update Success!", `Campaign "${param.campaignName}" has been updated successfully.`);
        }
        else {
            const errorMessage = response?.data?.Message || `Failed to update campaign "${param.campaignName}". Please try again.`;
            showErrorToast("Update Failed!", errorMessage);
        }
    }
    catch (error) {
        console.error(error);
        showErrorToast("Update Failed!", `An error occurred while updating campaign "${param.campaignName}".`);
    } finally {
        dispatch(toggleLoader(false));
        callback && callback();
    }
}

export const insertCampaign = (param, callback) => async (dispatch) => {
    try {
        dispatch(toggleLoader(true));
        const response = await postWithoutAlert(apiUrl.insertCampaign, param);
        const isSuccess = response && response.status === 200 && response.data && response.data.Code === 0;
        if (isSuccess) {
            showSuccessToast("Insert Success!", `Campaign "${param.campaignName}" has been created successfully.`);
            return { IsSuccessful: true, Message: "Campaign created successfully" };
        }
        else {
            const errorMessage = response?.data?.Message || `Failed to create campaign "${param.campaignName}". Please try again.`;
            showErrorToast("Insert Failed!", errorMessage);
        }
    }
    catch (error) {
        console.error(error);
        showErrorToast("Insert Failed!", `An error occurred while create campaign "${param.campaignName}".`);
    } finally {
        dispatch(toggleLoader(false));
        callback && callback();
    }
}