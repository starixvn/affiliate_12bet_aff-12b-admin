import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, prevState, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](prevState, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'isBack', prevState: undefined, action: { type: types.SET_IS_BACK, data: false }, expectedResult: false },
    { name: 'param', prevState: undefined, action: { type: types.SET_PARAM, data: { userName: '' } }, expectedResult: { userName: '' } },
    { name: 'checkedUsers', prevState: undefined, action: { type: types.SET_CHECKED_USERS, data: { 1234: false } }, expectedResult: { 1234: false } },
    { name: 'gridData', prevState: undefined, action: { type: types.SET_GRID_DATA, data: [{ UserId: 123 }] }, expectedResult: [{ UserId: 123 }] },
    { name: 'userInfo', prevState: undefined, action: { type: types.SET_USER_INFO, data: { UserId: 123 } }, expectedResult: { UserId: 123 } },
    { name: 'shouldResetScrollPosition', prevState: undefined, action: { type: types.SHOULD_RESET_SCROLL_POSITION, data: false }, expectedResult: false }
]

describe('admin list reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});