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
    { name: 'setParam', type: types.SET_PARAM, data: { param: 'param' } }
]

describe('new view log actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct parameter', async () => {
        const expectedResponse = {
            data: {
                MainData: [
                    {
                        Action: "Setting for Commission Group",
                        DateText: "07/01/2019 12:46:38 AM",
                        OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
                        NewData: "{\"root\":{\"row\":{\"@PageName\":\"1122111\",\"@PageCode\":\"1111\"}}}",
                        Id: 6860,
                        Ip: "::1",
                        UpdatedBy: "AAwalter"
                    },
                    {
                        Action: "Payout",
                        DateText: "06/20/2019 10:25:07 PM",
                        OldData: "",
                        NewData: "{\"root\":{\"row\":{\"@PageName\":\"111111\",\"@PageCode\":\"111\"}}}",
                        Id: 10279364,
                        Ip: "10.18.200.125",
                        UpdatedBy: ""
                    },
                    {
                        Action: "Payout",
                        DateText: "06/20/2019 10:25:07 PM",
                        OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
                        NewData: "",
                        Id: 102793,
                        Ip: "10.18.200.125",
                        UpdatedBy: ""
                    },
                    {
                        Action: "Payout",
                        DateText: "06/20/2019 10:25:07 PM",
                        OldData: "",
                        NewData: "",
                        Id: 102793,
                        Ip: "10.18.200.125",
                        UpdatedBy: ""
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
            fromDate: "01/04/2016",
            logAction: 0,
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/UserPanel/ViewLog/GetViewLogList', { ...param, IsNewViewLog: true });
        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to fetchData with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            affId: 123456,
            fromDate: "01/04/2016",
            logAction: 0,
            pageIndex: 3,
            pageSize: 20,
            toDate: "07/03/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/UserPanel/ViewLog/GetViewLogList', { ...param, IsNewViewLog: true });

        expect(result).toEqual(null);
    })
});