jest.mock('redux');
jest.mock('react-redux');
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actions from '../../../actions/index';

describe('home - MemberStatistics - connect', () => {
    let mapStateToProps, mapDispatchToProps;
    const connectMock = jest.fn();

    beforeAll(() => {
        connect.mockImplementationOnce((mapStateToPropsFunc, mapDispatchToPropsFunc) => {
            mapStateToProps = mapStateToPropsFunc;
            mapDispatchToProps = mapDispatchToPropsFunc;
            return connectMock;
        });
        require('../../MemberStatistics').default;
    });

    it('props should be mapped correctly', () => {
        const expectedResult = {
            generalInfo: { PaymentRequestNumber: 1 },
            pSPToday: 123,
            depositInfo: { AllMemberDepositUpToNow: 234 }
        };

        const state = {
            generalInfo: { PaymentRequestNumber: 1 },
            pSPToday: 123,
            depositInfo: { AllMemberDepositUpToNow: 234 },
            anotherProp: "Not included"
        };

        const result = mapStateToProps(state);

        expect(result).toEqual(expectedResult);
    })

    it('bindActionCreators should be called with correct parameter', () => {
        const mockBindAction = jest.fn();
        bindActionCreators.mockImplementationOnce(mockBindAction);
        const expectedActionCreator = {
            getPSPToday: actions['getPSPToday'],
            getDepositInfo: actions['getDepositInfo']
        }

        const dispatch = {};
        mapDispatchToProps(dispatch);

        expect(mockBindAction).toHaveBeenCalledWith(expectedActionCreator, dispatch);
    })

    it('returned function of connect should be called with correct parameter', () => {
        const MemberStatistics = require('../../MemberStatistics').MemberStatistics;

        expect(connectMock).toHaveBeenCalledWith(MemberStatistics);
    })
})