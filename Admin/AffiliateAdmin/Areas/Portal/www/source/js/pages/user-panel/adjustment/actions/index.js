import { post } from '../../../../modules/ajaxRequest';
import { toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import { getAffiliateInfo } from '../../../customer/mainpage/actions/loadingDataAction';

export { toggleLoaderUserPanel, getAffiliateInfo }

export const getBalanceBroughtForward = (param) => async () => {
    const url = 'Portal/UserPanel/Adjustment/GetBalanceBroughtForward';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : 0;
}

const submitFail = {
    IsSuccessful: false,
    Message: 'Update Failed!'
}

export const handleSubmit = (param) => async () => {
    const url = 'Portal/UserPanel/Adjustment/InsertAdjustment';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : submitFail;
}
