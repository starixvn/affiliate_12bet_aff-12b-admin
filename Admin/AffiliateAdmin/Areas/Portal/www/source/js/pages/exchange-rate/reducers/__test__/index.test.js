import * as types from '../../actions/types';
import * as reducers from '../index';

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        
        expect(result).toEqual(expectedResult);
    })
}

const testCaseList = [
    { name: 'date', value: null, action: { type: types.SET_DATE, data: '09/06/2019' }, expectedResult: '09/06/2019' },
    { name: 'gridData', value: null, action: { type: types.SET_GRID_DATA, data: [{ ExchangeId: 3 }] }, expectedResult: [{ ExchangeId: 3 }] },
    { name: 'isOpenPopup', value: false, action: { type: types.SET_TOGGLE_POPUP, data: true }, expectedResult: true },
    { name: 'selectedExchangeRate', value: null, action: { type: types.SET_SELECTED_EXCHANGE_RATE, data: { ExchangeId: 2, RateType: 0, Currency: 'ABC', Rate: 0.05 } }, expectedResult: { ExchangeId: 2, RateType: 0, Currency: 'ABC', Rate: 0.05 } },
    { name: 'shouldResetScrollPosition', value: true, action: { type: types.SHOULD_RESET_SCROLL_POSITION, data: false }, expectedResult: false }
]

describe('exchange rate reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});