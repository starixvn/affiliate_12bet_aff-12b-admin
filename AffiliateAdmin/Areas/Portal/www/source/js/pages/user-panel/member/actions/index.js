import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_PARAM } from '../actions/types';

export { setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}