import * as actionTypes from './types';

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

export const setProductList = (data) => {
    return {
        type: actionTypes.SET_PRODUCTLIST,
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