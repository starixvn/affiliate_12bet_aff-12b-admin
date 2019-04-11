import { LEFTPANEL_RECEIVE_AFFINFO } from './types';
import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';

export { toggleLoader }

const receiveLeftMenuInfo = (data) => {
    return {
        type: LEFTPANEL_RECEIVE_AFFINFO,
        data
    }
}

export const getAffiliateInfo = (affName) => async(dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/CustomerLeftPanel/GetAffiliateInfo';
    const param = { affUsername: affName };

    const response = await post(url, param);
    const data = response && response.status === 200 && response.data ? response.data : null;

    dispatch(receiveLeftMenuInfo(data));
    dispatch(toggleLoader(false));
}

export const getOnLineList = (affName) => async(dispatch) => {
    const url = 'Portal/CustomerLeftPanel/getOnLineList';
    const param = { affUsername: affName };

    const response = await post(url, param);
    return response && response.status === 200 ? response.data : null;
}

export const updateGeneralInfo = param => {
    const url = 'Portal/CustomerLeftPanel/UpdateInfo';

    return () => axios.post(url, param);
}

export const changePassword = param => {
    const url = 'Portal/CustomerLeftPanel/ChangePassword';

    return () => axios.post(url, param);
}