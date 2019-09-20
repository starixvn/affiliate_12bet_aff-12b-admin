import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import { getProductList as productList  } from '../../../customer/user-panel/modules/service';
import { SET_PRODUCT_LIST, SET_PARAM } from './types';

export { setPagingOption, toggleLoaderUserPanel }

export const setProductList = (data) => {
    return {
        type: SET_PRODUCT_LIST,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const getProductList = () => productList;

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/PlayerDetail/GetPlayerDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const exportExcel = (param) => async () => {
    const url = 'Portal/UserPanel/PlayerDetail/GetPlayerDetailList';
    const response = await post(url, { ...param, pageIndex: 1, pageSize: 20 });

    return response && response.status === 200 ? response.data : null;
}