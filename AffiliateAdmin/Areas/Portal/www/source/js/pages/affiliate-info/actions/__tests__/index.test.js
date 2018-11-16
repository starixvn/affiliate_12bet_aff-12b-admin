import * as actions from '../index'
import * as types from '../types'

const runTestAction = ({ name, type, data }) => {
    it(`should create action to ${name}`, () => {
        const expectedAction = { type, data };

        expect(actions[name](data)).toEqual(expectedAction);
    });
}

const actionList = [
    { name: 'setReportTitle', type: types.SET_REPORT_TITLE, data: 'Affiliate Info Title' }
]

describe('affiliate info actions', () => {
    actionList.forEach(item => {
        runTestAction(item);
    });
});