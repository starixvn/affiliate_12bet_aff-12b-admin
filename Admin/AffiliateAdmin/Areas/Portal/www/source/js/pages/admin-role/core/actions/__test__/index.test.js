import * as actions from '../index';
import * as types from '../types';

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setSelectedMenu', type: types.SET_SELECTED_MENU, data: '' },
    { name: 'resetReducer', type: types.RESET_REDUCER, data: 'Abc' },
    { name: 'setUserId', type: types.SET_USER_ID, data: '' },
    { name: 'setUserName', type: types.SET_USER_NAME, data: '' }
];

describe('admin role - core - actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });
});