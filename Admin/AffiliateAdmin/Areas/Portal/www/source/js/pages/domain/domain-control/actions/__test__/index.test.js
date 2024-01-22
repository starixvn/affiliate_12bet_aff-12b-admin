import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

import configureMockStore from 'redux-mock-store';
import thunk from 'redux-thunk';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setSelectedDomainId', type: types.SET_SELECTED_DOMAIN_ID, data: 123},
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: 'setGridData' },
    { name: 'setSelectedTargetCountryCode', type: types.SET_SELECTED_TARGET_COUNTRY_CODE, data: 234},
    { name: 'setAffDomainList', type: types.SET_AFF_DOMAIN_LIST, data: [{ domainName: "abc.com" }] },
    { name: 'setRedirectDomainList', type: types.SET_REDIRECT_DOMAIN_LIST, data: [{ domainName: "123.com" }] }
];

describe('domain control actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with return correct data', () => {
        const expectedResponse = {
            data: [
                {
                    DomainControlId: 123
                }
            ],
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: 'TOGGLE_LOADER' , data: true },
            { type: types.SET_GRID_DATA , data: expectedResponse.data },
            { type: types.SHOULD_RESET_SCROLL_POSITION , data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData']("en-GB")).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/GetDomainControlList', { "TargetCountryCode": "en-GB" });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchData with return error', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: 'TOGGLE_LOADER' , data: true },
            { type: types.SET_GRID_DATA , data: [] },
            { type: types.SHOULD_RESET_SCROLL_POSITION , data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData']("en-GB")).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/GetDomainControlList', { "TargetCountryCode": "en-GB" });
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchDomain with insert successfully', () => {
        const expectedAction = [
            { type: types.SET_AFF_DOMAIN_LIST , data: [] },
            { type: types.SET_REDIRECT_DOMAIN_LIST , data: [] },
        ];
        
        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchDomain']()).then(() => {
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to insertDomain with insert successfully', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            DomainId: 0,
            TargetCountry: "en-GB",
            AffDomainId: 12,
            RedirectDomain: 24,
            Status: true
        };

        const result = await actions['insertDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/InsertDomainControl', param);

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to insertDomain with insert is failed', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            DomainId: 0,
            TargetCountry: "en-GB",
            AffDomainId: 12,
            RedirectDomain: 0,
            Status: true
        };
        
        const result = await actions['insertDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/InsertDomainControl', param);
        expect(result).toEqual(null);
    })

    it('should create action to updateDomain with update successfully', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            DomainId: 123,
            TargetCountry: "en-GB",
            AffDomainId: 12,
            RedirectDomain: 24,
            Status: true
        };

        const result = await actions['updateDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/UpdateDomainControl', param);

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to updateDomain with update is failed', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            DomainId: 0,
            TargetCountry: "en-GB",
            AffDomainId: 0,
            RedirectDomain: 24,
            Status: true
        };
        
        const result = await actions['updateDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/UpdateDomainControl', param);
        expect(result).toEqual(null);
    })

    it('should create action to deleteDomain with delete successfully', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            DomainId: 123
        };

        const result = await actions['deleteDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/DeleteDomainControl', param);

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to deleteDomain with update is failed', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            DomainId: 123456
        };
        
        const result = await actions['deleteDomain'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/DeleteDomainControl', param);
        expect(result).toEqual(null);
    })

    it('should create action to getDomainControlById successfully', async () => {
        const expectedResponse = { data: { DomainId: 123, DomainName: 'abc' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getDomainControlById'](123)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/GetDomainControlById', { "domainControlId": 123 });

        expect(result).toEqual({ DomainId: 123, DomainName: 'abc' });
    })

    it('should create action to getDomainControlById failed', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getDomainControlById'](123)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/DomainControl/GetDomainControlById', { "domainControlId": 123 });
        expect(result).toEqual(null);
    })
});