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
    { name: 'setParam', type: types.SET_PARAM, data: { Id: 123 }},
    { name: 'setReportTitle', type: types.SET_REPORT_TITLE, data: 'title' }
];

describe('feedback report actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });

    it('should create action to fetchData with correct parameter', async () => {
        const expectedResponse = {
            data: {
                MainData: [
                    {
                        AffName: "12BETAFF12885",
                        RateName: "None",
                        Message: "te@te1.com",
                        SentDate: "12/14/2018 03:11:25"
                    },
                    {
                        AffName: "12BETAFF226",
                        RateName: "Very Please",
                        Message: "maia.le4@starixsoft.com",
                        SentDate: "10/07/2018 23:14:09"
                    }
                ],
                TotalItem: 2
            },
            status: 200
        };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const param = {
            FromDate: "6/28/2019",
            PageIndex: 1,
            PageSize: 20,
            RateIds: "1,2,3,4,5",
            AffName: "123abc123abc",
            ToDate: "6/28/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(result).toEqual(expectedResponse.data);
    })

    it('should create action to fetchData with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();

        const param = {
            FromDate: "6/28/2019",
            PageIndex: 1,
            PageSize: 20,
            RateIds: "1,2,3,4,5",
            AffName: "123abc123abc",
            ToDate: "6/28/2019"
        }

        const result = await actions['fetchData'](param)();

        expect(result).toEqual(null);
    })
});