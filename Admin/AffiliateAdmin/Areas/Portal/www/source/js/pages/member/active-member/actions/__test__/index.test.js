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
    { name: 'setUserNames', type: types.SET_USER_NAMES, data: "xyl157" },
    { name: 'setShowText', type: types.SET_SHOW_TEXT, data: true },
    { name: 'setMessage', type: types.SET_MESSAGE, data: "abc" },
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false },
    { name: 'setActivatedGridData', type: types.SET_ACTIVATED_GRID_DATA, data: 'setActivatedGridData' },
    { name: 'setUnactivatedGridData', type: types.SET_UNACTIVATED_GRID_DATA, data: 'setUnactivatedGridData' }
];

describe('active member actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchUnactivatedData with correct parameter', () => {
        const expectedResponse = {
            data: [
                    {
                        UserName: 'xyl157',
                    }
                ],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_UNACTIVATED_GRID_DATA, data: expectedResponse.data },
            { type: types.SET_USER_NAMES, data: 'xyl157,' },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = { userNames: 'xyl157' };
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchUnactivatedData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/GetUnactivatedMemberList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchUnactivatedData with incorrect parameter', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_UNACTIVATED_GRID_DATA, data: [] },
            { type: types.SET_USER_NAMES, data: '' },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = { userNames: 'abc' };

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchUnactivatedData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/GetUnactivatedMemberList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchActivatedData with correct parameter', () => {
        const expectedResponse = {
            data: [
                    {
                        UserName: 'xyz',
                    }
            ],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: types.SET_ACTIVATED_GRID_DATA, data: expectedResponse.data }
        ];
        const param = { userNames: 'xyz' };
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchActivatedData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/GetActivatedMemberList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchActivatedData with incorrect parameter', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: types.SET_ACTIVATED_GRID_DATA, data: [] }
        ];
        const param = { userNames: 'abc' };

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchActivatedData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/GetActivatedMemberList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to activeMember with correct parameter', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = { userNames: 'xyl157' };

        const result = await actions['activeMember'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/ActiveMember', param);

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to activeMember with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = { userNames: 'abc' };

        const result = await actions['activeMember'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ActiveMember/ActiveMember', param);
        expect(result).toEqual(null);
    })
});