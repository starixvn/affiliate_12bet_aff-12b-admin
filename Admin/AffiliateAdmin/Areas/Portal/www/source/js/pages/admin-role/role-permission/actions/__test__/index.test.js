import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

import configureMockStore from 'redux-mock-store';
import thunk from 'redux-thunk';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setIsGeneral', type: types.SET_IS_GENERAL, data: false },
    { name: 'setRoleList', type: types.SET_ROLE_LIST, data: [{ RoleId: 123 }] },
    { name: 'setMarketIdList', type: types.SET_MARKET_ID_LIST, data: { 'en-US': 'en-US' } },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: [{ RoleId: 123 }] },
    { name: 'setMarketList', type: types.SET_MARKET_LIST, data: [{ MarketId: 'en-US' }] }
];

describe('role permission actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to getRoleList with correct parameter', () => {
        const expectedResponse = {
            data: [{
                RoleId: 12345,
                RoleName: "Management"
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: types.SET_ROLE_LIST, data: expectedResponse.data }
        ];

        const store = mockStore({ todos: [] });
        store.dispatch(actions['getRoleList']()).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetRoleList');
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getRoleList with error response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: types.SET_ROLE_LIST, data: null }
        ];

        const store = mockStore({ todos: [] });
        store.dispatch(actions['getRoleList']()).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetRoleList');
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getMarketList with correct parameter', () => {
        const expectedResponse = {
            data: [{
                MarketId: 'en-US',
                MarketName: "English"
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: types.SET_MARKET_LIST, data: expectedResponse.data },
            { type: types.SET_MARKET_ID_LIST, data: { 'en-US': 'en-US' } }
        ];

        const store = mockStore({ todos: [] });
        store.dispatch(actions['getMarketList']()).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByUser');
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getMarketList with error response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: types.SET_MARKET_LIST, data: null },
            { type: types.SET_MARKET_ID_LIST, data: {} }
        ];

        const store = mockStore({ todos: [] });
        store.dispatch(actions['getMarketList']()).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByUser');
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getPermissionInfoList with correct parameter', async () => {
        const expectedResponse = {
            data: [{
                RoleId: 12345
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getPermissionInfoList']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/GetPermissionInfoList');

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getPermissionInfoList with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getPermissionInfoList']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/GetPermissionInfoList');

        expect(result).toEqual(null);
    })

    it('should create action to getMarketListByRole with correct parameter', async () => {
        const expectedResponse = {
            data: [{
                MarketId: 'en-US'
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getMarketListByRole']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByRole');

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getMarketListByRole with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getMarketListByRole']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByRole');

        expect(result).toEqual(null);
    })

    it('should create action to updatePermissionInfo with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = { RoleId: 12345 };
        const result = await actions['updatePermissionInfo'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/UpdatePermissionInfo', param);

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updatePermissionInfo with error response', async () => {
        ajaxRequest.post = jest.fn();

        const param = { RoleId: 12345 };
        const result = await actions['updatePermissionInfo'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/UpdatePermissionInfo', param);

        expect(result).toEqual(null);
    })

    it('should create action to updateMarketInfoList with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = { MarketId: 'en-US' };
        const result = await actions['updateMarketInfoList'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/UpdateMarketInfoList', { marketInfoList: param });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateMarketInfoList with error response', async () => {
        ajaxRequest.post = jest.fn();

        const param = { MarketId: 'en-US' };
        const result = await actions['updateMarketInfoList'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/RolePermission/UpdateMarketInfoList', { marketInfoList: param });

        expect(result).toEqual(null);
    })
});