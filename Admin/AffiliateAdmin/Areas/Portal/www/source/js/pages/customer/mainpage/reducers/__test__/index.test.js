﻿import { RESET_USER_PANEL } from '../../../user-panel/actions/types';
import { rootReducer } from '../index';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('customer - mainpage reducer', () => {
    const expectedResult = {
        adjustment: {
            affInfo: {},
        },
        affCommonInfo: null,
        clickStatistics: {
            pagingOption: defaultPagingOption,
            param: {},
            selectedSiteDetailFilter: null,
        },
        customerStatistics: {
            monthDate: null,
        },
        dailyEarning: {
            param: null,
        },
        dailyEarningDetail: {
            pagingOption: defaultPagingOption,
            param: {},
        },
        edit: {
            hasWriteEditAffClaim: false,
            moreSiteUrlsList: null,
            userAccountInfo: null,
        },
        hasTurnOverPermission: false,
        isShowLoaderUserPanel: false,
        landingPageStatistics: {
            pageList: [],
            pagingOption: defaultPagingOption,
            param: {},
            isGroupByCountry: true,
            selectedPageDetailFilter: null,
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
            pagingOption: defaultPagingOption,
            param: null,
        },
        note: {
            isShowPopup: false,
            noteInfo: {
                noteContent: '',
                noteId: 0,
            },
            noteList: [],
            pagingOption: defaultPagingOption,
        },
        playerDetail: {
            pagingOption: defaultPagingOption,
            param: {},
            productList: [],
            sortedParam: null,
        },
        selectedUserPanel: null,
        statementHistory: {
            pagingOption: defaultPagingOption,
            param: {},
            transactionId: null,
        },
        today: '6/27/2019',
        trackingStatistics: {
            param: {},
            selectedMonth: null,
        },
        viewLog: {
            pagingOption: defaultPagingOption,
            param: null,
            showIdList: {},
        },
        claimsPrincipal: {}
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