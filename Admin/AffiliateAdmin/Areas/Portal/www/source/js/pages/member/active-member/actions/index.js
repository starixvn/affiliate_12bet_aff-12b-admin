import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { globalResource } from '../../../../modules/globalResource';
import * as types from './types';

export { toggleLoader };

export const setUserNames = (data) => {
    return {
        type: types.SET_USER_NAMES,
        data
    }
}

export const setMessage = (data) => {
    return {
        type: types.SET_MESSAGE,
        data
    }
}

export const setMemberNotExist = (data) => {
    return {
        type: types.SET_MEMBER_NOT_EXIST,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setActivatedGridData = (data) => {
    return {
        type: types.SET_ACTIVATED_GRID_DATA,
        data
    }
}

export const setUnactivatedGridData = (data) => {
    return {
        type: types.SET_UNACTIVATED_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ActiveMember/GetAllMembers';
    const response = await post(url, param);
    const data = response && response.status === 200 ? response.data : {
        InactiveMemberList: [],
        ActivedMemeberList: [],
        MemberNotExist: ''
    };

    dispatch(setUnactivatedGridData(data.InactiveMemberList));
    dispatch(setActivatedGridData(data.ActivedMemeberList));
    dispatch(setMemberNotExist(data.MemberNotExist));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const activeMember = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ActiveMember/ActiveMember';
    const response = await post(url, param);

    dispatch(toggleLoader(false));
    return response && response.status === 200 ? response.data : {
        IsSuccessful: false,
        Message: globalResource.AnError
    };
}