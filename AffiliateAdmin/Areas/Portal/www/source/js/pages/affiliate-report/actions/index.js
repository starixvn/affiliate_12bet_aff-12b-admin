import { SET_REPORT_TITLE, SET_PARAM, SET_GRID_DATA } from './types';
import { post } from '../../../modules/ajaxRequest';
import { setPagingOption } from '../../../actions/pagingAction';

export { setPagingOption };

export const setReportTitle = (data) => {
    return {
        type: SET_REPORT_TITLE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const getGridData = (param) => async(dispatch) => {
    const url = 'Portal/AffiliateReport/GetMainData';
    const response = await post(url, param);
    const { MainData, TotalRow } = response.data;

    dispatch(setPagingOption({ TotalItem: TotalRow }));
    dispatch(setGridData(MainData));
}