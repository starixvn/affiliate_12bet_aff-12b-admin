import * as actions from '../index'
import * as types from '../types'

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setReportTitle', type: types.SET_REPORT_TITLE, data: 'Affiliate Report Title' },
    { name: 'setParam', type: types.SET_PARAM, data: 'setParam' },
    { name: 'setViewLevel', type: types.SET_VIEW_LEVEL, data: 'setViewLevel' }
]

describe('affiliate report actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });
});