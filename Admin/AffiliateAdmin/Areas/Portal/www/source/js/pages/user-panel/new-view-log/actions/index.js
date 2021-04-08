import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoaderUserPanel } from '../../../customer/user-panel/actions';
import { SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoaderUserPanel };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const getLogActionList = () => async () => {
    const url = 'Portal/UserPanel/ViewLog/GetLogActionList';

    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const fetchData = (param) => async() => {
    const url = 'Portal/UserPanel/ViewLog/GetViewLogList';

    const response = await post(url, { ...param, IsNewViewLog: true });

    return response && response.status === 200 ? response.data : null;
}