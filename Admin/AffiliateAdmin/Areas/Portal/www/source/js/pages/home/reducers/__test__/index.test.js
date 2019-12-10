import * as types from '../../actions/types'
import * as reducers from '../index'

const runTestReducer = ({ name, value, action, expectedResult }) => {
    it(`should handle ${name} reducer with type ${action.type}`, () => {
        const result = reducers[name](value, action);
        expect(result).toEqual(expectedResult);
    });
}

const testCaseList = [
    { name: 'generalInfo', value: null, action: { type: types.SET_GENERAL_INFO, data: { PaymentRequestNumber: 1 } }, expectedResult: { PaymentRequestNumber: 1 } },
    { name: 'depositInfo', value: null, action: { type: types.SET_DEPOSIT_INFO, data: { AllMemberDepositUpToNow: 234 } }, expectedResult: { AllMemberDepositUpToNow: 234 } },
    { name: 'pSPToday', value: true, action: { type: types.SET_PSP_TODAY, data: 123 }, expectedResult: 123 },
    { name: 'onlineList', value: null, action: { type: types.SET_ONLINE_LIST, data: [{ UserName: 'sa' }] }, expectedResult: [{ UserName: 'sa' }] }
]

describe('home reducers', () => {
    testCaseList.forEach((item) => {
        runTestReducer(item);
    })
});