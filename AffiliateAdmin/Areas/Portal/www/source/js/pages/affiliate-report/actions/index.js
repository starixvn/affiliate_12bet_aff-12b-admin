import * as actionTypes from './types';
import { post } from '../../../modules/ajaxRequest';

export { setPagingOption }
from '../../../actions/pagingAction';

export const setSearchedAffiliate = (data) => {
    return {
        type: actionTypes.SET_SEARCHED_AFFILIATE,
        data
    }
}

export const setSelectedLanguage = (data) => {
    return {
        type: actionTypes.SET_SELECTED_LANGUAGE,
        data
    }
}

export const setSelectedProductList = (data) => {
    return {
        type: actionTypes.SET_SELECTED_PRODUCTLIST,
        data
    }
}

export const setFromMonth = (data) => {
    return {
        type: actionTypes.SET_FROM_MONTH,
        data
    }
}

export const setToMonth = (data) => {
    return {
        type: actionTypes.SET_TO_MONTH,
        data
    }
}

export const toggleCheckedGetAll = () => {
    return {
        type: actionTypes.SET_CHECKED_GETALL
    }
}

export const setSortBy = (data) => {
    return {
        type: actionTypes.SET_SORTBY,
        data
    }
}

export const toggleSortAscending = () => {
    return {
        type: actionTypes.SET_CHECKED_SORT_ASC
    }
}

const setGridData = (data) => {
    return {
        type: actionTypes.SET_GRID_DATA,
        data
    }
}

export const getGridData = (param) => async(dispatch) => {
    const url = 'Portal/AffiliateReport/GetMainData';
    const response = await post(url, param);
    const { MainData, TotalRow } = response.data;

    dispatch(setGridData(MainData));
}