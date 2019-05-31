import { SET_MONTHDATE, TOGGLE_LOADER_CUSTOMER_STATISTICS } from './types';
import { post } from '../../../../modules/ajaxRequest';

export const setMonthDate = (data) => {
    return {
        type: SET_MONTHDATE,
        data
    }
}

export const toggleLoaderCustomerStatistics = (data) => {
    return {
        type: TOGGLE_LOADER_CUSTOMER_STATISTICS,
        data
    }
}

export const fetchData = (param) => async () => {
    const url = 'Portal/CustomerStatistics/GetCustomerStatistics';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}