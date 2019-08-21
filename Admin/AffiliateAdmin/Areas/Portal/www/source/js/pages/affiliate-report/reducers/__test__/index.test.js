import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'reportTitle', value: undefined, action: { type: types.SET_REPORT_TITLE, data: 'Specified Report Title' }, expectedResult: 'Specified Report Title' },
    { name: 'param', value: { username: 'abc' }, action: { type: types.SET_PARAM, data: { email: 'abc@gmail.com' } }, expectedResult: { username: 'abc', email: 'abc@gmail.com' } },
    { name: 'currentViewLevel', value: undefined, action: { type: types.SET_VIEW_LEVEL, data: 2 }, expectedResult: 2 }
]

describe('affiliate report reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});