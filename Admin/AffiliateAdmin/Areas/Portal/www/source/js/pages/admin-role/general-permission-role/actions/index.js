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

export const roleList = [
    { RoleId: 1, RoleName: 'Management' },
    { RoleId: 2, RoleName: 'Exe. Settlement' },
]

export const fakeFunctionList = [
    { RoleId: 1, RoleName: 'Management', ParentId: 1, FuncId: 1, FuncName: 'Home', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 2, FuncName: 'Customer', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 3, FuncName: 'Statement History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 4, FuncName: 'Adjusment', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 5, FuncName: 'Affiliate', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 6, FuncName: 'Affiliate Info', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 7, FuncName: 'Payment Request', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 8, FuncName: 'Email History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 9, FuncName: 'Revenue Group', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 10, FuncName: 'Admin Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 11, FuncName: 'Admin List', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 12, FuncName: 'General Permission Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 12, FuncId: 13, FuncName: 'General', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 12, FuncId: 14, FuncName: 'Language', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 15, FuncName: 'Admin Info', Policy: 3, IsRead: true, IsHost: true },

    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 1, FuncId: 1, FuncName: 'Home', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 2, FuncName: 'Customer', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 3, FuncName: 'Statement History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 4, FuncName: 'Adjusment', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 5, FuncName: 'Affiliate', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 6, FuncName: 'Affiliate Info', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 7, FuncName: 'Payment Request', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 8, FuncName: 'Email History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 9, FuncName: 'Revenue Group', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 10, FuncName: 'Admin Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 11, FuncName: 'Admin List', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 12, FuncName: 'General Permission Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 12, FuncId: 13, FuncName: 'General', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 12, FuncId: 14, FuncName: 'Language', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 15, FuncName: 'Admin Info', Policy: 3, IsRead: true, IsHost: true },
]