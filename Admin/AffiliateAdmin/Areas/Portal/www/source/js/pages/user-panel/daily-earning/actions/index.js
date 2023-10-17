import { post } from '../../../../modules/ajaxRequest';
import { getProductList } from '../../../customer/user-panel/libs/service';
import { SET_PARAM } from './types';
import { setSelectedUserPanel, toggleLoaderUserPanel, redirectToDailyEarningDetail, redirectToStatementHistory } from '../../../customer/user-panel/actions';

export { toggleLoaderUserPanel, redirectToDailyEarningDetail, setSelectedUserPanel, getProductList, redirectToStatementHistory };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/StatementHistory/GetDailyEarning';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}