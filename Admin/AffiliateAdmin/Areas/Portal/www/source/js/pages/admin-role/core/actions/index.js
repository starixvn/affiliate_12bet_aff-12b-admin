import { SET_SELECTED_MENU, RESET_REDUCER, SET_USER_ID, SET_USER_NAME } from './types';

export const setSelectedMenu = (data) => {
    return {
        type: SET_SELECTED_MENU,
        data
    }
}

export const resetReducer = (data) => {
    return {
        type: RESET_REDUCER,
        data
    }
}

export const setUserId = (data) => {
    return {
        type: SET_USER_ID,
        data
    }
}

export const setUserName = (data) => {
    return {
        type: SET_USER_NAME,
        data
    }
}