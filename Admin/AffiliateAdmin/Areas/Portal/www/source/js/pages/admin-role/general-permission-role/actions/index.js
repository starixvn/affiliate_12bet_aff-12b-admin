import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_GRID_DATA, SET_IS_GENERAL } from './types';

export { toggleLoader };

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setIsGeneral = (data) => {
    return {
        type: SET_IS_GENERAL,
        data
    }
}

const fakeLanguageList = [
    { RoleId: 1, RoleName: 'Management', IsES: true, IsEU: true, IsEI: true, IsCS: true, IsCH: true, IsVN: true, IsTH: true, IsID: true, IsKO: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', IsES: true, IsEU: false, IsEI: false, IsCS: true, IsCH: false, IsVN: false, IsTH: true, IsID: false, IsKO: false },
    { RoleId: 3, RoleName: 'Exe. Risk Team', IsES: true, IsEU: false, IsEI: true, IsCS: true, IsCH: false, IsVN: true, IsTH: true, IsID: false, IsKO: true },
    { RoleId: 4, RoleName: 'Exe. Marketing', IsES: true, IsEU: true, IsEI: false, IsCS: false, IsCH: false, IsVN: false, IsTH: false, IsID: true, IsKO: false },
    { RoleId: 5, RoleName: 'Asst. Marketing', IsES: true, IsEU: true, IsEI: true, IsCS: true, IsCH: true, IsVN: true, IsTH: false, IsID: true, IsKO: false }
]

export const fetchData = () => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = `Portal/PermissionRole/GetLanguageList`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(toggleLoader(false));
}

export const fetchLanguageData = () => (dispatch) => {
    dispatch(toggleLoader(true));

    // const url = `Portal/PermissionRole/GetLanguageList`;
    // const response = await post(url);

    // const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(fakeLanguageList));
    dispatch(toggleLoader(false));
}