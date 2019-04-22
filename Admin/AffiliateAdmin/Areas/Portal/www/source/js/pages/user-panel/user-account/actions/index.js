import { post } from '../../../../modules/ajaxRequest';
import { SET_USER_ACCOUNT_INFO } from './types'
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';

export { setPagingOption, toggleLoader };

export const setUserAccountInfo = (data) => {
    return {
        type: SET_USER_ACCOUNT_INFO,
        data
    }
}

export const getUserAccountInfo = (affId) => async(dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/UserPanel/UserAccount/GetUserAccountInfo';

    const response = await post(url, {affId});

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setUserAccountInfo(data));
    dispatch(toggleLoader(false));
}