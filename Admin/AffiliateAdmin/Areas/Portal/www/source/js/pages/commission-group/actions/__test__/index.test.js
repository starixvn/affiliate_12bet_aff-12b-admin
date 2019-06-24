import * as actions from '../index';
import * as types from '../types';
import { defaultPagingOption } from '../../../../modules/constants';
import * as ajaxRequest from '../../../../modules/ajaxRequest';

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const runTestActionReturnPost = ({ name, url}) => {
    it(`should create action to ${name}`, () => {
        ajaxRequest.post = jest.fn();
        actions[name](defaultPagingOption)();

        expect(ajaxRequest.post).toHaveBeenCalledWith(url, defaultPagingOption);
    });
}

const actionList = [
    { name: 'setGridData', type: types.SET_GRIDDATA, data: 'setGridData' },
    { name: 'setPagingOption', type: types.SET_PAGING_OPTION, data: 'setPagingOption' },
    { name: 'setSelectedGroup', type: types.SET_SELECTED_GROUP, data: 'setSelectedGroup' },
    { name: 'setSearchedGroupName', type: types.SET_SEARCHED_GROUPNAME, data: 'setSearchedGroupName' }
];

const actionListReturnPost = [
    { name: 'insertCommissionGroup', url: 'Portal/CommissionGroup/CreateCommissionGroup' },
    { name: 'updateCommissionGroup', url: 'Portal/CommissionGroup/UpdateCommissionGroup' },
    { name: 'deleteCommGroup', url: 'Portal/CommissionGroup/DeleteCommissionGroup' }
];

describe('commission group actions', () => {
    it('should create action to fetchGroupSettingData', () => {
        ajaxRequest.post = jest.fn();
        actions['fetchGroupSettingData'](defaultPagingOption);

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/CommissionGroup/GetCommissionGroupList', defaultPagingOption);
    })

    actionList.forEach(item => {
        runTestAction(item);
    });

    actionListReturnPost.forEach(item => {
        runTestActionReturnPost(item);
    });

});