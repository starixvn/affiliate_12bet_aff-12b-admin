import * as actions from '../index';
import * as types from '../types';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setMainViewParam', type: types.SET_MAIN_VIEW_PARAM, data: { param: 'param' } },
    { name: 'setDetailViewParam', type: types.SET_DETAIL_VIEW_PARAM, data: { param: 'param' } },
    { name: 'setSelectedAff', type: types.SET_SELECTED_AFF, data: '123' }
]

describe('new view log - actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to getLogActionList with correct response', async () => {
        const expectedResponse = {
            data: [
                {
                    ActionName: "Setting for Commission Group",
                    ActionId: 6860
                },
                {
                    ActionName: "Payout",
                    ActionId: 10279364
                }
            ],
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = await actions['getLogActionList']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetLogActionList');
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to getLogActionList with response error', async () => {
        ajaxRequest.post = jest.fn();

        const result = await actions['getLogActionList']()();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetLogActionList');
        expect(result).toEqual(null);
    })

    it('should create action to fetchData with correct response', async () => {
        const expectedResponse = {
            data: {
                MainData: [
                    {
                        AffId: 10279106,
                        Actor: "AAwalter",
                        LastAction: "User Info",
                        NumberOfAction: 1,
                        DateTimeText: "06/06/2019 03:42:42 AM"
                    },
                    {
                        AffId: 10279105,
                        Actor: "AAwalter",
                        LastAction: "User Info",
                        NumberOfAction: 2,
                        DateTimeText: "06/06/2019 03:42:38 AM"
                    },
                    {
                        AffId: 10279104,
                        Actor: "AAwalter",
                        LastAction: "Setting for Commission Group",
                        NumberOfAction: 3,
                        DateTimeText: "06/06/2019 03:42:38 AM"
                    },
                    {
                        AffId: 10279103,
                        Actor: "AAwalter",
                        LastAction: "Setting for Commission Group",
                        NumberOfAction: 4,
                        DateTimeText: "06/06/2019 03:42:38 AM"
                    }
                ],
                Total: null,
                TotalItem: 4
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            username: '',
            fromDate: "01/04/2016",
            logActionIds: '',
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetNewViewLogList', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to fetchData with response error', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            username: '',
            fromDate: "01/04/2016",
            logActionIds: '',
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetNewViewLogList', param);

        expect(result).toEqual(null);
    })

    it('should create action to fetchDetailData with correct response', async () => {
        const expectedResponse = {
            data: {
                MainData: [
                    {
                        LogId: 10279106,
                        UpdatedName: "AAwalter",
                        OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
                        NewData: "{\"root\":{\"row\":{\"@PageName\":\"1122111\",\"@PageCode\":\"1111\"}}}",
                        Action: "User Info",
                        Ip: "::1",
                        DateText: "06/06/2019 03:42:42 AM"
                    },
                    {
                        LogId: 10279105,
                        UpdatedName: "AAwalter",
                        OldData: "",
                        NewData: "{\"root\":{\"row\":{\"@PageName\":\"111111\",\"@PageCode\":\"111\"}}}",
                        Action: "User Info",
                        Ip: "::1",
                        DateText: "06/06/2019 03:42:38 AM"
                    },
                    {
                        LogId: 10279104,
                        UpdatedName: "AAwalter",
                        OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
                        NewData: "",
                        Action: "Setting for Commission Group",
                        Ip: "::1",
                        DateText: "06/06/2019 03:42:38 AM"
                    },
                    {
                        LogId: 10279103,
                        UpdatedName: "AAwalter",
                        OldData: "",
                        NewData: "",
                        Action: "Setting for Commission Group",
                        Ip: "::1",
                        DateText: "06/06/2019 03:42:38 AM"
                    }
                ],
                Total: null,
                TotalItem: 4
            },
            status: 200
        };

        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            affId: 1271808,
            username: '12BETAFF001',
            fromDate: "01/04/2016",
            logActionIds: '',
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchDetailData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetNewViewLogDetailList', param);
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to fetchDetailData with response error', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            affId: 123456,
            username: '12BETAFF001',
            fromDate: "01/04/2016",
            logActionIds: '',
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchDetailData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/ViewLog/GetNewViewLogDetailList', param);

        expect(result).toEqual(null);
    })
});