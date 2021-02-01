import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, prevState, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](prevState, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'roleList', prevState: undefined, action: { type: types.SET_ROLE_LIST, data: [{ RoleId: 123 }] }, expectedResult: [{ RoleId: 123 }] },
    { name: 'permissionListByRole', prevState: undefined, action: { type: types.SET_PERMISSION_LIST_BY_ROLE, data: [{ FuncId: 123 }] }, expectedResult: [{ FuncId: 123 }] },
    { name: 'marketListByUser', prevState: undefined, action: { type: types.SET_MARKET_LIST_BY_USER, data: [{ MarketId: 123 }] }, expectedResult: [{ MarketId: 123 }] }
]

describe('admin info reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});