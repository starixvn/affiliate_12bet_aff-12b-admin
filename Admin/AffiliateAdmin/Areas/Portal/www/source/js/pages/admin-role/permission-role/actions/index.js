import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_PARAM, SET_CHECKED_ITEMS, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_IS_GENERAL } from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setCheckedItems = (data) => {
    return {
        type: SET_CHECKED_ITEMS,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

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

export const fetchData = (isGeneral, param) => (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/PermissionRole/GetFunctionList';
    //const response = await post(url, param);

    const response = {
        status: 200,
        data: {
            MainData: isGeneral ? generalData : languageData,
            TotalItem: isGeneral ? 12 : 9
        }
    }

    const data = response && response.status === 200 ? response.data : null;
    const checkedItems = data ? _convertCheckedItems(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : null));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedItems(checkedItems));
    dispatch(toggleLoader(false));
}

function _convertCheckedItems(dataList) {
    const checkedItems = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].Id;
        checkedItems[itemId] = false;
    }

    return checkedItems;
}