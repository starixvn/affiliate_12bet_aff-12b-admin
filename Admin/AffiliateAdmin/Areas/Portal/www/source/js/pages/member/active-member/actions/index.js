import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_USER_NAMES, SET_SHOW_TEXT, SHOULD_RESET_SCROLL_POSITION, SET_ACTIVATED_GRID_DATA, SET_UNACTIVATED_GRID_DATA } from './types';

export const setUserNames = (data) => {
    return {
        type: SET_USER_NAMES,
        data
    }
}

export const setShowText = (data) => {
    return {
        type: SET_SHOW_TEXT,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setActivatedGridData = (data) => {
    return {
        type: SET_ACTIVATED_GRID_DATA,
        data
    }
}

export const setUnactivatedGridData = (data) => {
    return {
        type: SET_UNACTIVATED_GRID_DATA,
        data
    }
}

export const fetchUnactivatedData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ActiveMember/GetUnactivedMemberList';
    const response = await post(url, param);
    const data = response && response.status === 200 ? response.data : [];

    dispatch(setUnactivatedGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));

    return data.length;
}

export const fetchActivatedData = (param) => async (dispatch) => {
    const url = 'Portal/ActiveMember/GetActivedMemberList';
    const response = await post(url, param);
    const data = response && response.status === 200 ? response.data : [];

    dispatch(setActivatedGridData(data));

    return data.length;
}

export const ActiveMember = (memberIds) => async () => {
    const url = 'Portal/ActiveMember/ActiveMember';
    const response = await post(url, { memberIds });

    return response && response.status === 200 ? response.data : null;
}