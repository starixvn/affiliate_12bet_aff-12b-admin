import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'selectedPageDetailFilter', value: null, action: { type: types.SET_SELECTED_PAGE_DETAIL_FILTER, data: { selectedPage: "sportsbook" } }, expectedResult: { selectedPage: "sportsbook" } },
    { name: 'isGroupByCountry', value: null, action: { type: types.SET_IS_GROUP_BY_COUNTRY, data: true }, expectedResult: true },
    { name: 'param', value: null, action: { type: types.SET_PARAM, data: { Id: 123 } }, expectedResult: { Id: 123 } },
    { name: 'shortCodeLanguageList', value: {}, action: { type: types.SET_SHORT_CODE_LANGUAGE_LIST, data: { vn: 'vn' } }, expectedResult: { vn: 'vn' } }
]

describe('landing pages statistics reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});