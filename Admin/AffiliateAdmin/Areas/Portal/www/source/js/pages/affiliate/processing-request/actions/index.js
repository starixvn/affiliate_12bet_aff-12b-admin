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

export const setShouldResetScrollPosition = (data) => {
    return {
        type: type.SET_SHOULD_RESET_SCROLL_POSITION,
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

    const url = 'Portal/ProcessingRequest/GetProcessingRequestList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const getBannerDetail = (selectedRequestId) => async () => {
    const url = 'Portal/ProcessingRequest/GetProcessingRequestDetail';
    const response = await post(url, { selectedRequestId });

    return response && response.status === 200 ? response.data : null;
}

export const insertBanner = (param) => async () => {
    const url = 'Portal/ProcessingRequest/InsertProcessingRequest';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateBanner = (param) => async () => {
    const url = 'Portal/ProcessingRequest/UpdateProcessingRequest';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const deleteBanners = (selectedRequestId) => async () => {
    const url = 'Portal/ProcessingRequest/DeleteProcessingRequest';
    const response = await post(url, { selectedRequestId });

    return response && response.status === 200 ? response.data : null;
}