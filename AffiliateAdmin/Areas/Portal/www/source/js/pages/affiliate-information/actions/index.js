import {
    RECEIVE_LEFTMENU_INFO
} from './types';
import axios from 'axios';

import {
    resolveClientUrl
} from '../../../modules/utils';

const receiveLeftMenuInfo = (data) => {
    return {
        type: RECEIVE_LEFTMENU_INFO,
        data
    }
}

export const getLeftMenuInfo = userName => async dispatch => {
    const url = resolveClientUrl('Portal/Affiliate/GetInfo?username=' + userName);
    const response = await axios.get(url);
    dispatch(receiveLeftMenuInfo(response.data));
}

export default {
    receiveLeftMenuInfo
}