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
    { name: 'setRoleList', type: types.SET_ROLE_LIST, data: [{ RoleId: 123 }] },
    { name: 'setPermissionListByRole', type: types.SET_PERMISSION_LIST_BY_ROLE, data: [{ RoleId: 123 }] },
    { name: 'setMarketListByUser', type: types.SET_MARKET_LIST_BY_USER, data: [{ RoleId: 123 }] }
];

describe('admin info actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct parameter', async () => {
        const expectedResponse = {
            data: {
                UserId: 12345,
                Role: 1
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['fetchData'](12345)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/GetUserInfo', { userId: 12345 });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to fetchData with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['fetchData'](12345)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/GetUserInfo', { userId: 12345 });

        expect(result).toEqual(null);
    })

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
            { type: types.SET_ROLE_LIST, data: { 12345: "Management" } }
        ];

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['getRoleList']()).then(() => {
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
        return store.dispatch(actions['getRoleList']()).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetRoleList');
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getPermissionListByRole with correct parameter', () => {
        const expectedResponse = {
            data: [{
                FuncId: 12345,
                FuncName: "Home"
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: types.SET_PERMISSION_LIST_BY_ROLE, data: expectedResponse.data }
        ];

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['getPermissionListByRole'](123)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/GetPermissionListByRole', { roleId: 123 });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getPermissionListByRole with error response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: types.SET_PERMISSION_LIST_BY_ROLE, data: null }
        ];

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['getPermissionListByRole'](123)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/GetPermissionListByRole', { roleId: 123 });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getMarketListByUser with correct parameter', () => {
        const item = {
            MarketId: 12345,
            MarketName: "English"
        };

        const expectedResponse = {
            data: [item],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: types.SET_MARKET_LIST_BY_USER, data: [{ ...item, HasPermission: false }] }
        ];

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['getMarketListByUser'](123)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByUser', { userId: 123 });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getMarketListByUser with error response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: types.SET_MARKET_LIST_BY_USER, data: [] }
        ];

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['getMarketListByUser'](123)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByUser', { userId: 123 });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getMarketListByRole with correct parameter', async () => {
        const expectedResponse = {
            data: [{
                MarketId: 12345,
                MarketName: "English",
                HasPermission: true
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getMarketListByRole'](12345, true)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByRole', { roleId: 12345 });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getMarketListByRole with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getMarketListByRole'](12345)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRoleCommon/GetMarketListByRole', { roleId: 12345 });

        expect(result).toEqual(null);
    })

    it('should create action to createUser with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const userInfo = {
            userName: 'Abc'
        }
        const result = await actions['createUser'](userInfo)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/CreateUser', userInfo);

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to createUser with error response', async () => {
        ajaxRequest.post = jest.fn();

        const userInfo = {
            userName: 'Abc'
        }
        const result = await actions['createUser'](userInfo)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/CreateUser', userInfo);

        expect(result).toEqual(null);
    })

    it('should create action to updateUser with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const userInfo = {
            userName: 'Abc'
        }
        const result = await actions['updateUser'](userInfo)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/UpdateUser', userInfo);

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateUser with error response', async () => {
        ajaxRequest.post = jest.fn();

        const userInfo = {
            userName: 'Abc'
        }
        const result = await actions['updateUser'](userInfo)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/UpdateUser', userInfo);

        expect(result).toEqual(null);
    })

    it('should create action to updateStatus with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            userId: 123
        }
        const result = await actions['updateStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/UpdateStatus', param);

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to updateStatus with error response', async () => {
        ajaxRequest.post = jest.fn();

        const param = {
            userId: 123
        }
        const result = await actions['updateStatus'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminInfo/UpdateStatus', param);

        expect(result).toEqual(null);
    })
});