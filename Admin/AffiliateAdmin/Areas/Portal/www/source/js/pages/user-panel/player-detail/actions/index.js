import { SET_PRODUCT_LIST, SET_PARAM } from './types';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { post } from '../../../../modules/ajaxRequest';

export { setPagingOption, toggleLoader }

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

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/PlayerDetail/GetPlayerDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}