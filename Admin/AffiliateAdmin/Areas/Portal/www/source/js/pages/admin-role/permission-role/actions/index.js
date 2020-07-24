import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_PARAM, SET_CHECKED_FUNCTIONS, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setCheckedFunctions = (data) => {
    return {
        type: SET_CHECKED_FUNCTIONS,
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

const exampleData = [
    { FunctionId: 1, FunctionName: 'Security Question and Answer', isView: false, isEdit: false },
    { FunctionId: 2, FunctionName: 'Phone Numbers', isView: true, isEdit: false },
    { FunctionId: 3, FunctionName: 'Email Address', isView: true, isEdit: false },
    { FunctionId: 4, FunctionName: '12BET Player Account', isView: true, isEdit: false },
    { FunctionId: 5, FunctionName: 'Website URL', isView: true, isEdit: false },
    { FunctionId: 6, FunctionName: 'Commission Group', isView: true, isEdit: false },
    { FunctionId: 7, FunctionName: 'Language', isView: true, isEdit: false },
    { FunctionId: 8, FunctionName: 'Add Note', isView: true, isEdit: false },
    { FunctionId: 9, FunctionName: 'Adjustment', isView: true, isEdit: false },
    { FunctionId: 10, FunctionName: 'Account status: Approved/Disapprove/Closed', isView: true, isEdit: false },
    { FunctionId: 11, FunctionName: 'Report', isView: true, isEdit: false },
    { FunctionId: 12, FunctionName: 'Export Excel Report', isView: true, isEdit: false },
]

const response = {
    status: 200,
    data: {
        MainData: exampleData,
        TotalItem: 12
    }
}

export const fetchData = (param) => (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/PermissionRole/GetFunctionList';
    //const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedFunctions = data ? _convertCheckedFunctions(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : null));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedFunctions(checkedFunctions));
    dispatch(toggleLoader(false));
}

function _convertCheckedFunctions(dataList) {
    const checkedFunctions = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].FunctionId;
        checkedFunctions[itemId] = false;
    }

    return checkedFunctions;
}