import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'file', value: { name: 'abc.xlsx' }, action: { type: types.SET_FILE, data: null }, expectedResult: null },
    { name: 'isClickUpload', value: false, action: { type: types.SET_IS_CLICK_UPLOAD, data: true }, expectedResult: true },
    { name: 'isShowResult', value: false, action: { type: types.SET_IS_SHOW_RESULT, data: true }, expectedResult: true }
]

describe('mass adjustment reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});