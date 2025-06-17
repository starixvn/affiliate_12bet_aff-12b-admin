import * as types from './types';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { post } from '../../../../modules/ajaxRequest';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    };
}

export const setDealInfo = (data) => {
    return {
        type: types.SET_DEAL_INFO,
        data
    };
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
        data
    };
}

export const setIsSubmitted = (data) => {
    return {
        type: types.SET_IS_SUBMITTED,
        data
    };
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/Operation/GetDeals';
    const response = await post(url, param);

    if (response && response.status === 200) {
        const data = response.data;
        dispatch(setGridData(data ? data.MainData : null));
        dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    }

    dispatch(toggleLoader(false));
}

export const getDealDetail = (groupId) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/Operation/GetDealDetail';
    const response = await post(url, { groupId });

    let data = response && response.status === 200 ? response.data : null;

    dispatch(setDealInfo(data));

    dispatch(toggleLoader(false));
}

export const addCPADeal = (param) => async () => {
    const url = 'Portal/Operation/AddCpaDeal';
    return await post(url, param);
}

export const deleteDeal = (dealId) => async () => {
    const url = 'Portal/Operation/DeleteDeal';
    return await post(url, { dealId });
}