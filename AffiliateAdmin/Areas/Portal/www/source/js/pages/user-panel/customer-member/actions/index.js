import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}