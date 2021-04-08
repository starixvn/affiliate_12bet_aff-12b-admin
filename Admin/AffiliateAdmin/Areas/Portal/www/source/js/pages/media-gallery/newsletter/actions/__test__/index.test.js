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
    { name: 'setParam', type: types.SET_PARAM, data: { Id: 123 } },
    { name: 'setSelectedNewsLetterPageId', type: types.SET_SELECTED_NEWSLETTER_PAGE_ID, data: 123456 },
    { name: 'setSelectedPreviewPageId', type: types.SET_SELECTED_PREVIEW_PAGE_ID, data: 123456 },
    { name: 'setCheckedNewsLetterPages', type: types.SET_CHECKED_NEWSLETTER_PAGES, data: { 123456: false } },
    { name: 'setShouldResetScrollPosition', type: types.SHOULD_RESET_SCROLL_POSITION, data: false },
    { name: 'setGridData', type: types.SET_GRID_DATA, data: 'setGridData' },
];

describe('news letter actions', () => {
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
                        NewsLetterId: 25,
                        ToDate: "2021-04-30T00:00:00",
                        UploadDate: "2021-03-15T00:43:51.243"
                    },
                    {
                        FileName: "test2.html",
                        FileSize: 17722,
                        FromDate: "2021-02-28T13:00:00",
                        Language: "en-US",
                        LanguageName: "English",
                        NewsLetterId: 23,
                        ToDate: "2021-04-29T13:00:00",
                        UploadDate: "2021-03-05T04:03:36.013"
                    },
                    {
                        FileName: "test1.html",
                        FileSize: 111,
                        FromDate: "2021-03-30T02:00:00",
                        Language: "hi-IN",
                        LanguageName: "English (India)",
                        NewsLetterId: 22,
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
            { type: types.SET_CHECKED_NEWSLETTER_PAGES, data: { 22: false, 23: false, 25: false } },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = {
            pageIndex: 1,
            pageSize: 20
        }

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/GetNewsLetterList', param);
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
            { type: types.SET_CHECKED_NEWSLETTER_PAGES, data: {} },
            { type: 'TOGGLE_LOADER', data: false },
        ];
        const param = {
            pageIndex: 1,
            pageSize: 20
        }

        const store = mockStore({ todos: [] });
        return store.dispatch(actions['fetchData'](param)).then(() => {
            expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/GetNewsLetterList', param);
            expect(store.getActions()).toEqual(expectedAction)
        })
    })

    it('should create action to getNewsLetterPageDetail with correct response', async () => {
        const expectedResponse = {
            data: {
                FileName: "test.html",
                FileSize: 475,
                FromDate: "2021-04-01T00:00:00",
                Language: "hi-IN",
                LanguageName: "English (India)",
                NewsLetterId: 25,
                ToDate: "2021-04-30T00:00:00",
                UploadDate: "2021-03-15T00:43:51.243"
            }, status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const selectedNewsLetterPageId = 25;

        const result = await actions['getNewsLetterPageDetail'](selectedNewsLetterPageId)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/GetNewsLetterDetailById', { id: selectedNewsLetterPageId });

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getNewsLetterPageDetail with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const selectedNewsLetterPageId = 25;

        const result = await actions['getNewsLetterPageDetail'](selectedNewsLetterPageId)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/GetNewsLetterDetailById', { id: selectedNewsLetterPageId });
        expect(result).toEqual(null);
    })

    it('should create action to insertNewsLetterPage with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            NewsLetterId: 0,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['insertNewsLetterPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/NewsLetter/InsertNewsLetter');

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to insertNewsLetterPage with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            NewsLetterId: 0,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['insertNewsLetterPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/NewsLetter/InsertNewsLetter');
        expect(result).toEqual(null);
    })

    it('should create action to updateNewsLetterPage with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            NewsLetterId: 123,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['updateNewsLetterPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/NewsLetter/UpdateNewsLetter');

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to updateNewsLetterPage with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            NewsLetterId: 123,
            files: [{ name: 'test1.html' }]
        };

        const result = await actions['updateNewsLetterPage'](param)();

        expect(ajaxRequest.post.mock.calls[0][0]).toEqual('Portal/NewsLetter/UpdateNewsLetter');
        expect(result).toEqual(null);
    })

    it('should create action to deleteNewsLetterPages with correct response', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const newsLetterPageIds = '2485,2448';

        const result = await actions['deleteNewsLetterPages'](newsLetterPageIds)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/DeleteNewsLetters', { ids: newsLetterPageIds });

        expect(result).toEqual({ IsSuccessful: true, Message: "" });
    })

    it('should create action to deleteNewsLetterPages with errored response', async () => {
        ajaxRequest.post = jest.fn();
        const newsLetterPageIds = '123456';

        const result = await actions['deleteNewsLetterPages'](newsLetterPageIds)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/NewsLetter/DeleteNewsLetters', { ids: newsLetterPageIds });
        expect(result).toEqual(null);
    })
});