import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';
import { submitIsFailed } from '../../../../../modules/constants';

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
    { name: 'setParam', type: types.SET_PARAM, data: { Id: 123 } },
    { name: 'setSelectedId', type: types.SET_SELECTED_ID, data: 123456 },
    { name: 'setSelectedPreviewId', type: types.SET_SELECTED_PREVIEW_ID, data: 123456 },
    { name: 'setCheckedIds', type: types.SET_CHECKED_IDS, data: { 123456: false } },
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: 'setGridData' },
];

describe('offer page actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct response', () => {
        const expectedResponse = {
            data: {
                MainData: [
                    {
                        FileName: "test.html",
                        FileSize: 475,
                        FromDate: "2021-04-01T00:00:00",
                        Language: "hi-IN",
                        LanguageName: "English (India)",
                        PageId: 25,
                        ToDate: "2021-04-30T00:00:00",
                        UploadDate: "2021-03-15T00:43:51.243"
                    },
                    {
                        FileName: "test2.html",
                        FileSize: 17722,
                        FromDate: "2021-02-28T13:00:00",
                        Language: "en-US",
                        LanguageName: "English",
                        PageId: 23,
                        ToDate: "2021-04-29T13:00:00",
                        UploadDate: "2021-03-05T04:03:36.013"
                    },
                    {
                        FileName: "test1.html",
                        FileSize: 111,
                        FromDate: "2021-03-30T02:00:00",
                        Language: "hi-IN",
                        LanguageName: "English (India)",
                        PageId: 22,
                        ToDate: "2021-04-28T00:00:01",
                        UploadDate: "2021-03-04T23:14:51.38"
                    }
                ],
                TotalItem: 3
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: expectedResponse.data.MainData },
            { type: 'SET_PAGING_OPTION', data: { PageIndex: 1, PageSize: 20, TotalItem: 3 } },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: types.SET_CHECKED_IDS, data: { 22: false, 23: false, 25: false } },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = {
            pageIndex: 1,
            pageSize: 20
        }

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/GetOfferPageList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to fetchData with errored response', () => {
        ajaxRequest.post = jest.fn();

        const expectedAction = [
            { type: 'TOGGLE_LOADER', data: true },
            { type: types.SET_GRID_DATA, data: [] },
            { type: 'SET_PAGING_OPTION', data: { PageIndex: 1, PageSize: 20, TotalItem: 0 } },
            { type: types.SHOULD_RESET_SCROLL_POSITION, data: true },
            { type: types.SET_CHECKED_IDS, data: {} },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = {
            pageIndex: 1,
            pageSize: 20
        }

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/GetOfferPageList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getOfferPageDetail with correct response', async () => {
        const expectedResponse = {
            data: {
                FileName: "test.html",
                FileSize: 475,
                FromDate: "2021-04-01T00:00:00",
                Language: "hi-IN",
                LanguageName: "English (India)",
                PageId: 25,
                ToDate: "2021-04-30T00:00:00",
                UploadDate: "2021-03-15T00:43:51.243"
            }, status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const selectedId = 25;

        const result = await actions['getOfferPageDetail'](selectedId)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/GetOfferPageDetailById', { PageId: selectedId });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getOfferPageDetail with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const selectedId = 25;

        const result = await actions['getOfferPageDetail'](selectedId)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/GetOfferPageDetailById', { PageId: selectedId });
        expect(result).toEqual(submitIsFailed);
    })

    it('should create action to insertOfferPage with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            pageId: 0,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['insertOfferPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/OfferPage/InsertOfferPage');

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to insertOfferPage with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            pageId: 0,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['insertOfferPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/OfferPage/InsertOfferPage');
        expect(result).toEqual(submitIsFailed);
    })

    it('should create action to updateOfferPage with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            PageId: 123,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['updateOfferPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/OfferPage/UpdateOfferPage');

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to updateOfferPage with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            PageId: 123,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['updateOfferPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/OfferPage/UpdateOfferPage');
        expect(result).toEqual(submitIsFailed);
    })

    it('should create action to deleteOfferPages with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const selectedIds = '2485,2448';

        const result = await actions['deleteOfferPages'](selectedIds)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/DeleteOfferPages', { PageIds: selectedIds });

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to deleteOfferPages with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const selectedIds = '123456';

        const result = await actions['deleteOfferPages'](selectedIds)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/OfferPage/DeleteOfferPages', { PageIds: selectedIds });
        expect(result).toEqual(submitIsFailed);
    })
});