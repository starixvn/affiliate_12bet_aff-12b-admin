import * as actions from '../index';
import * as types from '../../../core/actions/types';

import configureMockStore from 'redux-mock-store';
import thunk from 'redux-thunk';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

describe('admin role - common - actions', () => {
    it('should create action to redirectToAdminList with correct parameter', () => {

        const expectedAction = [
            { type: types.SET_SELECTED_MENU, data: 'adminList' },
            { type: types.SET_USER_ID, data: null },
            { type: types.SET_USER_NAME, data: null },
            { type: 'SET_IS_BACK', data: true }
        ];

        const store = mockStore({ todos: [] });

        store.dispatch(actions['redirectToAdminList']());

        expect(store.getActions()).toEqual(expectedAction)
    })

    it('should create action to redirectToCreateNew with correct parameter', () => {

        const expectedAction = [
            { type: types.SET_USER_ID, data: null },
            { type: types.SET_USER_NAME, data: null },
            { type: types.SET_SELECTED_MENU, data: 'adminInfo' }
        ];

        const store = mockStore({ todos: [] });

        store.dispatch(actions['redirectToCreateNew']());

        expect(store.getActions()).toEqual(expectedAction)
    })

    it('should create action to redirectToUserDetail with correct parameter', () => {

        const expectedAction = [
            { type: types.SET_USER_ID, data: 123 },
            { type: types.SET_USER_NAME, data: 'Abc' },
            { type: types.SET_SELECTED_MENU, data: 'adminInfo' }
        ];

        const store = mockStore({ todos: [] });

        store.dispatch(actions['redirectToUserDetail'](123, "Abc"));

        expect(store.getActions()).toEqual(expectedAction)
    })
});