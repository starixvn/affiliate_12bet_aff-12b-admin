import { SET_PRODUCT_LIST, SET_PARAM } from './types';
import { setPagingOption } from '../../../../actions/pagingAction';

export { setPagingOption }

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