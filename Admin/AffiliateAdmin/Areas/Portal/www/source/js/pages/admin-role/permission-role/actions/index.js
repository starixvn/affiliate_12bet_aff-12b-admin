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

const generalData = [
    { Id: 1, Name: 'Security Question and Answer', isView: false, isEdit: false },
    { Id: 2, Name: 'Phone Numbers', isView: true, isEdit: false },
    { Id: 3, Name: 'Email Address', isView: true, isEdit: false },
    { Id: 4, Name: '12BET Player Account', isView: true, isEdit: false },
    { Id: 5, Name: 'Website URL', isView: true, isEdit: false },
    { Id: 6, Name: 'Commission Group', isView: true, isEdit: false },
    { Id: 7, Name: 'Language', isView: true, isEdit: false },
    { Id: 8, Name: 'Add Note', isView: true, isEdit: false },
    { Id: 9, Name: 'Adjustment', isView: true, isEdit: false },
    { Id: 10, Name: 'Account status: Approved/Disapprove/Closed', isView: true, isEdit: false },
    { Id: 11, Name: 'Report', isView: true, isEdit: false },
    { Id: 12, Name: 'Export Excel Report', isView: true, isEdit: false },
]

const languageData = [
    { Id: 1, Name: 'English', isView: false, isEdit: false },
    { Id: 2, Name: 'English (Europe)', isView: true, isEdit: false },
    { Id: 3, Name: 'English (India)', isView: true, isEdit: false },
    { Id: 4, Name: 'Chinese Simplified', isView: true, isEdit: false },
    { Id: 5, Name: 'Chinese Traditional', isView: true, isEdit: false },
    { Id: 6, Name: 'Vietnamese', isView: true, isEdit: false },
    { Id: 7, Name: 'Thai', isView: true, isEdit: false },
    { Id: 8, Name: 'Indonesia', isView: true, isEdit: false },
    { Id: 9, Name: 'Korean', isView: true, isEdit: false }
]

export const fetchData = (isGeneral, userName) => (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/PermissionRole/GetFunctionList';
    //const response = await post(url, { userName });

    const response = {
        status: 200,
        data: isGeneral ? generalData : languageData
    }

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(toggleLoader(false));
}