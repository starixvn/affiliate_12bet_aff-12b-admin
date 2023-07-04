import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoaderUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { getAffiliateInfo, toggleLoader } from '../../../customer/mainpage/actions/loadingDataAction';
import { SET_PARAM, TRANSACTION_ID } from '../actions/types';

export { setPagingOption, toggleLoaderUserPanel, getAffiliateInfo, redirectToDailyEarning, toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setTransactionId = (data) => {
    return {
        type: TRANSACTION_ID,
        data
    }
}

export const fetchData = (param) => async() => {
    const url = 'Portal/UserPanel/StatementHistory/GetStatementHistoryList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}