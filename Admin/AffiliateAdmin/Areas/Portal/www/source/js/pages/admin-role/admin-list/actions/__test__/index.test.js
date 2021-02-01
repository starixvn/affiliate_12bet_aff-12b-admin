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
    { name: 'setParam', type: types.SET_PARAM, data: { userName: '' } },
    { name: 'setIsBack', type: types.SET_IS_BACK, data: false },
    { name: 'setCheckedUsers', type: types.SET_CHECKED_USERS, data: { 123: true } },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: [{ UserId: 123 }] },
    { name: 'setUserInfo', type: types.SET_USER_INFO, data: { UserId: 123 } },
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false },
];

describe('admin list actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct parameter', () => {
        const expectedResponse = {
            data: [{
                UserId: 12345,
                UserName: "Abc"
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: expectedResponse.data },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: types.SET_CHECKED_USERS, data: { 12345: false } },
            { type: 'TOGGLE_LOADER', data: false }
        ];

        const param = {
            userName: ''
        }
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/GetUserList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchData with error response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: [] },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: types.SET_CHECKED_USERS, data: {} },
            { type: 'TOGGLE_LOADER', data: false }
        ];

        const param = {
            userName: ''
        }
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/GetUserList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getUserPermissionList with correct parameter', async () => {
        const expectedResponse = {
            data: [{
                MarketId: 12345,
                MarketName: "English",
                HasPermission: true
            }],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getUserPermissionList'](12345, true)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/GetUserPermissionList', { userId: 12345 });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getUserPermissionList with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getUserPermissionList'](12345)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/GetUserPermissionList', { userId: 12345 });

        expect(result).toEqual(null);
    })

    it('should create action to deleteUsers with correct parameter', async () => {
        const expectedResponse = {
            data: {
                IsSuccessful: true
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['deleteUsers']('123,456')();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/DeleteUsers', { userIds: '123,456' });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to deleteUsers with error response', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['deleteUsers']('123,456')();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AdminRole/DeleteUsers', { userIds: '123,456' });

        expect(result).toEqual(null);
    })
});