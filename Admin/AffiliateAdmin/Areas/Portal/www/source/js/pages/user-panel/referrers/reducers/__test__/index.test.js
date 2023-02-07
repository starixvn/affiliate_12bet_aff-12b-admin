import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'selectedSiteDetailFilter', value: null, action: { type: types.SET_SELECTED_SITE_DETAIL_FILTER, data: {} }, expectedResult: {} },
    { name: 'param', value: {}, action: { type: types.SET_PARAM, data: { productId: 123, content: 'abc' } }, expectedResult: { productId: 123, content: 'abc' } }
]

describe('referers reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});