import * as types from './types';

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
