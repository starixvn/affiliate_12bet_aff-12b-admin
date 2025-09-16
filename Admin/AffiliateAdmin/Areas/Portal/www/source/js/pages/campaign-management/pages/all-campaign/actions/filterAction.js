import * as types from './types';

import { post } from '@/modules/ajaxRequest';
import { convertListToMutilpleSelected } from '@/modules/utils';

export const setSelectedStatus = (item) => {
    return {
        type: types.SET_SELECTED_STATUS,
        data: item,
    };
};

export const setStatusList = (item) => {
    return {
        type: types.SET_STATUS_LIST,
        data: item,
    };
};

export const setSearchCampaign = (item) => {
    return {
        type: types.SET_SEARCH_CAMPAIGN,
        data: item,
    };
};

export const setSearchDescription = (item) => {
    return {
        type: types.SET_SEARCH_DESCRIPTION,
        data: item,
    };
};

export const setParam = (item) => {
    return {
        type: types.SET_PARAM,
        data: item,
    }
}

export const fetchFilter = () => async (dispatch) => {
    try {
        const url = 'Portal/CampaignManagement/GetAllCampaignStatus';
        const response = await post(url, {});
        console.log(response);

        const data = response && response.status === 200 ? response.data : [];
        dispatch(setSelectedStatus(data.map(item => item.StatusId).join(',')));
        dispatch(setStatusList(convertListToMutilpleSelected(data, 'StatusId', 'Status')));
    }
    catch (error) {
        console.error(error);
    }
}