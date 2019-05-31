import { post } from '../../../../modules/ajaxRequest';
import { toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import { getProductList } from '../../../customer/user-panel/modules/service';
import { setSelectedUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { SET_PARAM } from './types';

export { toggleLoaderUserPanel, getProductList, setSelectedUserPanel, redirectToDailyEarning };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const fetchData = (param) => async() => {
    const url = 'Portal/UserPanel/StatementHistory/GetDailyEarningDetail';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}