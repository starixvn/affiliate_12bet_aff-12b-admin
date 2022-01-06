import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'selectedId', value: null, action: { type: types.SET_SELECTED_ID, data: 123 }, expectedResult: 123 },
    { name: 'selectedPreviewId', value: null, action: { type: types.SET_SELECTED_PREVIEW_ID, data: 123 }, expectedResult: 123 },
    { name: 'param', value: null, action: { type: types.SET_PARAM, data: { 123: true } }, expectedResult: { 123: true } },
    { name: 'checkedIds', value: null, action: { type: types.SET_CHECKED_IDS, data: { 123: true } }, expectedResult: { 123: true } },
    { name: 'shouldResetScrollPosition', value: true, action: { type: types.SHOULD_RESET_SCROLL_POSITION, data: false }, expectedResult: false },
    { name: 'gridData', value: null, action: { type: types.SET_GRID_DATA, data: [{ name: 'abc' }] }, expectedResult: [{ name: 'abc' }] }
]

describe('offer page - reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});