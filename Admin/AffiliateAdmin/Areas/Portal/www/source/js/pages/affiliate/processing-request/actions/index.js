import { post } from '../../../../modules/ajaxRequest';
import * as type from './types';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';

export { toggleLoader, setPagingOption };

export const setReportTitle = (data) => {
    return {
        type: type.SET_REPORT_TITLE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: type.SET_PARAM,
        data
    }
}

export const setSelectedRequestId = (data) => {
    return {
        type: type.SET_SELECTED_REQUEST_ID,
        data
    }
}

export const setSelectedRequest = (data) => {
    return{
        type: type.SET_SELECTED_PROC_REQUEST,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: type.SET_SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: type.SET_GRID_DATA,
        data
    }
}

export const setIsClickSearch = (data) => {
    return {
        type: type.SET_IS_CLICK_SEARCH,
        data
    }
}

export const setProcRequestSummary = (data) => {
    return {
        type : type.SET_PROC_REQUEST_SUMMARY,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ProcessingRequest/GetProcessingRequestList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setProcRequestSummary(data ? data.Summary : null));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const getProcessingRequestDetail = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/ProcessingRequest/GetProcessingRequestDetail';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    dispatch(setSelectedRequest(data));
    dispatch(toggleLoader(false));
}

export const insertProcessingRequest = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/ProcessingRequest/InsertProcessingRequest';
    const response = await post(url, param);
    dispatch(toggleLoader(false));

    return response && response.status === 200 ? response.data : null;
}

export const updateProcessingRequest = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/ProcessingRequest/UpdateProcessingRequest';
    const response = await post(url, param);
    dispatch(toggleLoader(false));

    return response && response.status === 200 ? response.data : null;
}

export const deleteProcessingRequest = (selectedRequestId) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/ProcessingRequest/DeleteProcessingRequest';
    const response = await post(url, { RequestId: selectedRequestId });
    dispatch(toggleLoader(false));

    return response && response.status === 200 ? response.data : null;
}