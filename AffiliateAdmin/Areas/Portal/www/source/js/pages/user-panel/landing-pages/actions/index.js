import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_SELECTED_PAGE, SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setSelectedPage = (data) => {
    return {
        type: SET_SELECTED_PAGE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}