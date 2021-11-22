import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'reportTitle', value: '', action: { type: types.SET_REPORT_TITLE, data: 'abc' }, expectedResult: 'abc' },
    { name: 'param', value: null, action: { type: types.SET_PARAM, data: { username: 'abc' } }, expectedResult: { username: 'abc' } },
    { name: 'selectedRequestId', value: 0, action: { type: types.SET_SELECTED_REQUEST_ID, data: 123 }, expectedResult: 123 },
    { name: 'selectedRequest', value: null, action: { type: types.SET_SELECTED_PROC_REQUEST, data: { abc: 123 } }, expectedResult: { abc: 123 } },
    { name: 'shouldResetScrollPosition', value: true, action: { type: types.SET_SHOULD_RESET_SCROLL_POSITION, data: false }, expectedResult: false },
    { name: 'gridData', value: null, action: { type: types.SET_GRID_DATA, data: [] }, expectedResult: [] },
    { name: 'isClickSearch', value: false, action: { type: types.SET_IS_CLICK_SEARCH, data: true }, expectedResult: true },
    { name: 'dataSummary', value: null, action: { type: types.SET_PROC_REQUEST_SUMMARY, data: { test: 123 } }, expectedResult: { test: 123 } },
    { name: 'languageList', value: null, action: { type: types.SET_LANGUAGE_LIST, data: [] }, expectedResult: [] },
    { name: 'originalData', value: null, action: { type: types.SET_ORGINAL_REQUEST, data: { test: 123 } }, expectedResult: { test: 123 } },
]

describe('processing request - reducers - ', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});