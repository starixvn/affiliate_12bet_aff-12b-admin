import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, prevState, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](prevState, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'isGeneral', prevState: undefined, action: { type: types.SET_IS_GENERAL, data: false }, expectedResult: false },
    { name: 'roleList', prevState: undefined, action: { type: types.SET_ROLE_LIST, data: [{ RoleId: 123 }] }, expectedResult: [{ RoleId: 123 }] },
    { name: 'marketIdList', prevState: undefined, action: { type: types.SET_MARKET_ID_LIST, data: { 1234: 1234 } }, expectedResult: { 1234: 1234 } },
    { name: 'gridData', prevState: undefined, action: { type: types.SET_GRID_DATA, data: [{ RoleId: 123 }] }, expectedResult: [{ RoleId: 123 }] },
    { name: 'marketList', prevState: undefined, action: { type: types.SET_MARKET_LIST, data: [{ MarketId: 123 }] }, expectedResult: [{ MarketId: 123 }] }
]

describe('role permission reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});