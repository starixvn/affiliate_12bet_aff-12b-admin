import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'mainViewParam', value: null, action: { type: types.SET_MAIN_VIEW_PARAM, data: { username: 'abc' } }, expectedResult: { username: 'abc' } },
    { name: 'detailViewParam', value: null, action: { type: types.SET_DETAIL_VIEW_PARAM, data: { username: 'abc' } }, expectedResult: { username: 'abc' } },
    { name: 'selectedAff', value: '', action: { type: types.SET_SELECTED_AFF, data: 'abc' }, expectedResult: 'abc' }
]

describe('new view log reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});