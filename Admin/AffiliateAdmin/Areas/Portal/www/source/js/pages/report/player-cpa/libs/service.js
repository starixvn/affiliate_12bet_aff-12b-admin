import { get, post } from '../../../../modules/ajaxRequest';

export const getPlayerCpaData = async (params) => {
    const url = 'Portal/PlayerCpaReport/GetData';
    const response = await post(url, params);

    return response && response.status === 200 ? response.data : null;
}


export const getTurnoverDetail = async (memberCpaId) => {
    const url = `Portal/PlayerCpaReport/GetTurnoverDetail?memberCpaId=${memberCpaId}`;
    const response = await get(url);

    return response && response.status === 200 ? response.data : null;
}