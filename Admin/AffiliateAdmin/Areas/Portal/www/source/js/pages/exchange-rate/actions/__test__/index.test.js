import * as types from '../types';
import * as actions from '../index';
import * as ajaxRequest from '../../../../modules/ajaxRequest';

import thunk from 'redux-thunk';
import configureMockStore from 'redux-mock-store';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectAction = { type, data };

        expect(actions[name](data)).toEqual(expectAction);
    });
}

const actionList = [
    { name: 'setDate', type: types.SET_DATE, data: '09/06/2019' },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: { ExchangeId: 3 } },
    { name: 'setTogglePopup', type: types.SET_TOGGLE_POPUP, data: true },
    { name: 'setSelectedExchangeRate', type: types.SET_SELECTED_EXCHANGE_RATE, data: { ExchangeId: 2, RateType: 0, Currency: 'ABC', Rate: 0.05 } },
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false }
];

describe('exchange rate actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct parameter', () => {
        const expectReponse = {
            status: 200,
            data: [
                { ExchangeId: 9, Currency: "AUD", ActualRate: 0.7, BuyingRate: 0.767789 },
                { ExchangeId: 3, Currency: "US$", ActualRate: 1, BuyingRate: 1 }
            ]
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectReponse));

        const expectActions = [
            { type: 'TOGGLE_LOADER' , data: true },
            { type: 'SET_GRID_DATA' , data: expectReponse.data },
            { type: 'SHOULD_RESET_SCROLL_POSITION' , data: true },
            { type: 'TOGGLE_LOADER' , data: false },
        ];

        const date = '09/06/2019';

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](date)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ExchangeRate/GetExchangeRateList', { date });
            expect(store.getActions()).toEqual(expectActions);
        })
    })

    it('should create action to fetchData with incorrect parameter', () => {
        ajaxRequest.post = jest.fn();

        const expectActions = [
            { type: 'TOGGLE_LOADER' , data: true },
            { type: 'SET_GRID_DATA' , data: null },
            { type: 'SHOULD_RESET_SCROLL_POSITION' , data: true },
            { type: 'TOGGLE_LOADER' , data: false },
        ];

        const date = 'date';

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](date)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ExchangeRate/GetExchangeRateList', { date });
            expect(store.getActions()).toEqual(expectActions);
        })
    })

    it('should create action to updateExchangeRate with correct parameter', async () => {
        const expectResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectResponse));

        const selectedExchangeRate = {
            ExchangeId: 2,
            Rate: 0.05,
            RateType: 0
        };

        const result = await actions['updateExchangeRate'](selectedExchangeRate)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ExchangeRate/UpdateExchangeRate', selectedExchangeRate);
        expect(result).toEqual({ IsSuccessful: true, Message: '' });
    })

    it('should create action to updateExchangeRate with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();

        const selectedExchangeRate = {
            ExchangeId: 2,
            Rate: 0.05,
            RateType: 2
        };

        const result = await actions['updateExchangeRate'](selectedExchangeRate)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ExchangeRate/UpdateExchangeRate', selectedExchangeRate);
        expect(result).toEqual({ IsSuccessful: false, Message: 'Update Failed!' });
    })
})