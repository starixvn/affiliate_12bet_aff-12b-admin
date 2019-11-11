import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { setPagingOption } from '../../../actions/pagingAction';
import { SET_GRID_DATA } from './types';

export { toggleLoader, setPagingOption };

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const getOnlineList = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/Home/GetOnlineList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(toggleLoader(false));
}

export const getTasksWaitingForApproval = () => async () => {
    const url = 'Portal/Home/GetTasksWaitingForApproval';
    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const getBannerStatistics = () => async () => {
    const url = 'Portal/Home/GetBannerStatistics';
    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const getAffiliateStatisticsUpToNow = (param) => async () => {
    const url = 'Portal/Home/GetAffiliateStatisticsUpToNow';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const getAffiliateStatisticsThisMonth = (param) => async () => {
    const url = 'Portal/Home/GetAffiliateStatisticsThisMonth';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const getMemberStatisticsUpToNow = (param) => async () => {
    const url = 'Portal/Home/GetMemberStatisticsUpToNow';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const getMemberStatisticsThisMonth = (param) => async () => {
    const url = 'Portal/Home/GetMemberStatisticsThisMonth';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}