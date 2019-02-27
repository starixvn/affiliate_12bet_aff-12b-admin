import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_PARAM, SET_SHOW_GROUP } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setShowGroup = (data) => {
    return {
        type: SET_SHOW_GROUP,
        data
    }
}