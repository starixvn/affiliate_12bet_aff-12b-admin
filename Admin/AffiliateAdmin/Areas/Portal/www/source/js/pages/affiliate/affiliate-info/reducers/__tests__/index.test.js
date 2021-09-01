import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const mockColumnList = [
            {columnKey: "Index", columnName: "#", checked: true},
            {columnKey: "Status", columnName: "Status", checked: true},
            {columnKey: "FirstName", columnName: "First Name", checked: true}];

const testCaseList = [
    { name: 'commGroupList', value: {}, action: { type: types.SET_COMM_GROUP_LIST, data: { abc: 123 } }, expectedResult: { abc: 123 } },
    { name: 'languageList', value: {}, action: { type: types.SET_LANGUAGE_LIST, data: { abc: 123 } }, expectedResult: { abc: 123 } },
    { name: 'reportTitle', value: '', action: { type: types.SET_REPORT_TITLE, data: 'Specified Report Title' }, expectedResult: 'Specified Report Title' },
    { name: 'param', value: { username: 'abc' }, action: { type: types.SET_PARAM, data: { email: 'abc@gmail.com' } }, expectedResult: { username: 'abc', email: 'abc@gmail.com' } },
    { name: 'showPopupCommGroup', value: false, action: { type: types.SET_SHOW_POPUP_COMMGROUP, data: true }, expectedResult: true },
    { name: 'affId', value: 0, action: { type: types.SET_AFFID, data: 123 }, expectedResult: 123 },
    { name: 'commGroup', value: 0, action: { type: types.SET_COMM_GROUP, data: 321 }, expectedResult: 321 },
    { name: 'columnList', value: 0, action: { type: types.SET_SELECTED_COLUMN, data: mockColumnList }, expectedResult: mockColumnList },
    { name: 'isClickSearch', value: false, action: { type: types.SET_IS_CLICK_SEARCH, data: true }, expectedResult: true }
]

describe('affiliate info reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});