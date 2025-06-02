import * as types from './types';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { post } from '../../../../modules/ajaxRequest';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    };
}

export const fetchData = (param) => async () => {
    const url = 'Portal/Operation/GetDeals';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
} 