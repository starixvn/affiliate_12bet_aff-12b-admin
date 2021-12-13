import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

import configureMockStore from 'redux-mock-store';
import thunk from 'redux-thunk';

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares)

const expectAction = ({ actionName, type, data }) => {
    it(`should create action to ${actionName}`, () => {
        var expectAction = { type, data };

        expect(actions[actionName](data)).toEqual(expectAction);
    })
}

const testCases = [
    { actionName: 'setReportTitle', type: types.SET_REPORT_TITLE, data: 'title'},
    { actionName: 'setParam', type: types.SET_PARAM, data: {}},
    { actionName: 'setSelectedRequestId', type: types.SET_SELECTED_REQUEST_ID, data: '123'},
    { actionName: 'setSelectedRequest', type: types.SET_SELECTED_PROC_REQUEST, data: {}},
    { actionName: 'setShouldResetScrollPosition', type: types.SET_SHOULD_RESET_SCROLL_POSITION, data: false},
    { actionName: 'setGridData', type: types.SET_GRID_DATA, data: []},
    { actionName: 'setIsClickSearch', type: types.SET_IS_CLICK_SEARCH, data: false},
    { actionName: 'setProcRequestSummary', type: types.SET_PROC_REQUEST_SUMMARY, data: {}},
    { actionName: 'setOriginalData', type: types.SET_ORGINAL_REQUEST, data: {}},
]

describe('processing request - actions - ', () => {
    testCases.forEach(item => expectAction(item))

    it('should create action to fetchData with correct response', () => {
        ajaxRequest.post = jest.fn(() => Promise.resolve({ status: 200, data: { MainData: [], TotalItem: 0, Summary: null } }));

        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: [] },
            { type: 'SET_PAGING_OPTION', data: { TotalItem: 0, PageIndex: 1, PageSize: 20 } },
            { type: types.SET_PROC_REQUEST_SUMMARY, data: null },
            { type: types.SET_SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {
            PageIndex: 1,
            PageSize: 20
        }
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/GetProcessingRequestList', param);
            expect(store.getActions()).toEqual(expectActions);
        })
    })

    it('should create action to fetchData with error response', () => {
        ajaxRequest.post = jest.fn();
        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: [] },
            { type: 'SET_PAGING_OPTION', data: { TotalItem: 0, PageIndex: 1, PageSize: 20 } },
            { type: types.SET_PROC_REQUEST_SUMMARY, data: null },
            { type: types.SET_SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {
            PageIndex: 1,
            PageSize: 20
        }
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/GetProcessingRequestList', param);
            expect(store.getActions()).toEqual(expectActions);
        })
    })

    it('should create action to getProcessingRequestDetail with correct response', () => {
        const expectData = { id: 123 };
        ajaxRequest.post = jest.fn(() => Promise.resolve({ status: 200, data: expectData }));

        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_SELECTED_PROC_REQUEST, data: expectData },
            { type: types.SET_ORGINAL_REQUEST, data: expectData },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const store = mockStore({ todos: [] });

        return store.dispatch(actions['getProcessingRequestDetail'](123)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/GetProcessingRequestDetail', { "processId": 123 });
            expect(store.getActions()).toEqual(expectActions);
            expect(data).toEqual(expectData);
        })
    })

    it('should create action to getProcessingRequestDetail with error response', () => {
        ajaxRequest.post = jest.fn();
        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_SELECTED_PROC_REQUEST, data: null },
            { type: types.SET_ORGINAL_REQUEST, data: {} },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const store = mockStore({ todos: [] });

        return store.dispatch(actions['getProcessingRequestDetail'](123)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/GetProcessingRequestDetail', { "processId": 123 });
            expect(store.getActions()).toEqual(expectActions);

            expect(data).toBeNull();
        })
    })

    it('should create action to insertProcessingRequest with correct response', () => {
        const expectData = { IsSuccessful: true };
        ajaxRequest.post = jest.fn(() => Promise.resolve({ status: 200, data: expectData }));

        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {};
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['insertProcessingRequest'](param)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/InsertProcessingRequest', param);
            expect(store.getActions()).toEqual(expectActions);
            expect(data).toEqual(expectData);
        })
    })

    it('should create action to insertProcessingRequest with error response', () => {
        ajaxRequest.post = jest.fn();
        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {};
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['insertProcessingRequest'](param)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/InsertProcessingRequest', param);
            expect(store.getActions()).toEqual(expectActions);

            expect(data).toBeNull();
        })
    })

    it('should create action to updateProcessingRequest with correct response', () => {
        const expectData = { IsSuccessful: true };
        ajaxRequest.post = jest.fn(() => Promise.resolve({ status: 200, data: expectData }));

        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {};
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['updateProcessingRequest'](param)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/UpdateProcessingRequest', param);
            expect(store.getActions()).toEqual(expectActions);
            expect(data).toEqual(expectData);
        })
    })

    it('should create action to updateProcessingRequest with error response', () => {
        ajaxRequest.post = jest.fn();
        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const param = {};
        const store = mockStore({ todos: [] });

        return store.dispatch(actions['updateProcessingRequest'](param)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/UpdateProcessingRequest', param);
            expect(store.getActions()).toEqual(expectActions);

            expect(data).toBeNull();
        })
    })

    it('should create action to deleteProcessingRequest with correct response', () => {
        const expectData = { IsSuccessful: true };
        ajaxRequest.post = jest.fn(() => Promise.resolve({ status: 200, data: expectData }));

        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const store = mockStore({ todos: [] });

        return store.dispatch(actions['deleteProcessingRequest'](123)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/DeleteProcessingRequest', { RequestId: 123 });
            expect(store.getActions()).toEqual(expectActions);
            expect(data).toEqual(expectData);
        })
    })

    it('should create action to deleteProcessingRequest with error response', () => {
        ajaxRequest.post = jest.fn();
        const expectActions = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: 'TOGGLE_LOADER', data: false },
        ]

        const store = mockStore({ todos: [] });

        return store.dispatch(actions['deleteProcessingRequest'](123)).then((data) => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ProcessingRequest/DeleteProcessingRequest', { RequestId: 123 });
            expect(store.getActions()).toEqual(expectActions);

            expect(data).toBeNull();
        })
    })
})