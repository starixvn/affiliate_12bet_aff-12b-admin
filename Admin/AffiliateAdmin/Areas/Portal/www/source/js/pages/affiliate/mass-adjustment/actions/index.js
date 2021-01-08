import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_FILE } from './types';

export { toggleLoader, setPagingOption };

export const setFile = (data) => {
    return {
        type: SET_FILE,
        data
    }
}

export const handleUploadFile = (file) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = '';
    const response = await post(url, file);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(toggleLoader(false));

    return data;
}