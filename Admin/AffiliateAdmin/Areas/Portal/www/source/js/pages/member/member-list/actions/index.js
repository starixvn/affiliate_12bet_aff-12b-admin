import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_PARAM, SET_IS_FROM_OTHER_PAGES, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setIsFromOtherPages = (data) => {
    return {
        type: SET_IS_FROM_OTHER_PAGES,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/Member/GetMemberList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const deleteMember = (memberId) => async () => {
    const url = 'Portal/Member/Deletemember';
    const response = await post(url, { memberId });

    return response && response.status === 200 ? response.data : null;
}