import { SET_SELECTED_MENU, RESET_REDUCER } from './types';

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