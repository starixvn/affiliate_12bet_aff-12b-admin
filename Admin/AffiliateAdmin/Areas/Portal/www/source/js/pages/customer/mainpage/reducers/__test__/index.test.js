﻿import { RESET_USER_PANEL } from '../../../user-panel/actions/types';
import { rootReducer } from '../index'

describe('customer - mainpage reducer', () => {
    const expectedResult = {
        adjustment: {
            affInfo: {},
        },
        affCommonInfo: null,
        clickStatistics: {
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
            selectedSiteUrl: null,
        },
        customerStatistics: {
            isShowLoader: false,
            monthDate: null,
        },
        dailyEarning: {
            param: null,
        },
        dailyEarningDetail: {
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
        },
        edit: {
            moreSiteUrlsList: null,
            userAccountInfo: null,
        },
        isShowLoaderUserPanel: false,
        landingPageStatistics: {
            pageList: [],
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
            selectedPage: null,
            shortCodeLanguageList: {},
        },
        languageList: null,
        leftPanel: {
            affName: '',
            affiliateInfo: null,
            isShowLoader: false,
            isValidUserName: true,
        },
        member: {
            canViewMemberEmail: false,
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: null,
        },
        note: {
            isShowPopup: false,
            noteInfo: {
                noteContent: '',
                noteId: 0,
            },
            noteList: [],
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
        },
        playerDetail: {
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
            productList: [],
        },
        selectedUserPanel: null,
        statementHistory: {
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
            transactionId: null,
        },
        today: '6/27/2019',
        trackingStatistics: {
            param: {},
            selectedMonth: null,
        },
        viewLog: {
            pagingOption: {
                PageIndex: 1,
                PageList: [
                  10,
                  20,
                  50,
                  100,
                  500,
                ],
                PageSize: 20,
                TotalItem: 0,
            },
            param: {},
            showIdList: {},
        },
    }
    it(`should handle rootReducer reducer`, () => {
        const expectResult = Object.assign({}, expectedResult, { isShowLoaderUserPanel: true, })
        const state = { today: '6/27/2019' };
        const result = rootReducer(state, { type: 'TOGGLE_LOADER_USER_PANEL', data: true });
        expect(result).toEqual(expectResult);
    });

    it(`should handle rootReducer reducer with action.type is RESET_USER_PANEL`, () => {
        const state = { today: '6/27/2019' };
        const result = rootReducer(state, { type: RESET_USER_PANEL, data: 'statementHistory' });
        expect(result).toEqual(expectedResult);
    });
});