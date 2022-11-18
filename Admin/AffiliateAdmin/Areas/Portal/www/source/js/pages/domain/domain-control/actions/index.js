import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import * as types from './types';
import { getDomainList } from '../../domain/actions/index';

export { toggleLoader };

export const setSelectedDomainId = (data) => {
    return {
        type: types.SET_SELECTED_DOMAIN_ID,
        data
    }
}

export const setSelectedMarketId = (data) => {
    return {
        type: types.SET_SELECTED_MARKET_ID,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
        data
    }
}

export const setAffDomainList = (data) => {
    return {
        type: types.SET_AFF_DOMAIN_LIST,
        data
    }
}

export const setRedirectDomainList = (data) => {
    return {
        type: types.SET_REDIRECT_DOMAIN_LIST,
        data
    }
}

export const fetchData = (marketId) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/DomainControl/GetDomainControlList';
    const response = await post(url, { marketId });

    const data = response && response.status === 200 ? response.data : [];

    dispatch(setGridData(data));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(toggleLoader(false));
}

export const getDomainControlById = (domainControlId) => async () => {
    const url = 'Portal/DomainControl/GetDomainControlById';
    const response = await post(url, { domainControlId });

    return response && response.status === 200 ? response.data : null;
}

export const fetchDomain = () => async (dispatch) => {
    dispatch(setAffDomainList(await getDomainList(true)));
    dispatch(setRedirectDomainList(await getDomainList(false)));
}

export const getMarketListByDomains = (param) => async () => {
    const url = 'Portal/Domain/GetMarketList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const insertDomain = (param) => async () => {
    const url = 'Portal/DomainControl/InsertDomainControl';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateDomain = (param) => async () => {
    const url = 'Portal/DomainControl/UpdateDomainControl';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const deleteDomain = (param) => async () => {
    const url = 'Portal/DomainControl/DeleteDomainControl';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}
