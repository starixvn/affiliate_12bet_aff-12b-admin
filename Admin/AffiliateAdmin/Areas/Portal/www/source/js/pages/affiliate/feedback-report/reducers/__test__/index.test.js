import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'param', value: null, action: { type: types.SET_PARAM, data: { AffId: 123456 } }, expectedResult: { AffId: 123456 } },
    { name: 'reportTitle', value: null, action: { type: types.SET_REPORT_TITLE, data: 'Title' }, expectedResult: 'Title' }
]

describe('feedback report reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});