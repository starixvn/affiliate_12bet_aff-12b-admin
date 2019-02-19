import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_SELECTED_SITE_URL, SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setSelectedSiteUrl = (data) => {
    return {
        type: SET_SELECTED_SITE_URL,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}