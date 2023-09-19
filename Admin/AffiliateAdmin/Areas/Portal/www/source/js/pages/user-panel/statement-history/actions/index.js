import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoaderUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { getAffiliateInfo, toggleLoader } from '../../../customer/mainpage/actions/loadingDataAction';
import * as types from '../actions/types';

export { setPagingOption, toggleLoaderUserPanel, getAffiliateInfo, redirectToDailyEarning, toggleLoader };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setTransactionId = (data) => {
    return {
        type: types.TRANSACTION_ID,
        data
    }
}

export const setExpandMonthDateList = (data) => {
    return {
        type: types.SET_EXPAND_MONTH_DATE_LIST,
        data
    }
}

export const fetchData = (param) => async() => {
    const url = 'Portal/UserPanel/StatementHistory/GetStatementHistoryList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}