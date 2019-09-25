jest.mock('redux');
jest.mock('react-redux');
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as actions from '../../../actions/index';

describe('exchange rate - EditExchangeRateForm - connect', () => {
    let mapStateToProps, mapDispatchToProps;
    const connectMock = jest.fn();

    beforeAll(() => {
        connect.mockImplementationOnce((mapStateToPropsFunc, mapDispatchToPropsFunc) => {
            mapStateToProps = mapStateToPropsFunc;
            mapDispatchToProps = mapDispatchToPropsFunc;
            return connectMock;
        });
        require('../../EditExchangeRateForm').default;
    });

    it('props should be mapped correctly', () => {
        const expectedResult = {
            date: '09/06/2019',
            selectedExchangeRate: { ExchangeId: 3 },
            isOpenPopup: true
        };
        const state = {
            date: '09/06/2019',
            selectedExchangeRate: { ExchangeId: 3 },
            isOpenPopup: true,
            anotherProp: "Not included"
        };

        const result = mapStateToProps(state);

        expect(result).toEqual(expectedResult);
    })

    it('bindActionCreators should be called with correct parameter', () => {
        const mockBindAction = jest.fn();
        bindActionCreators.mockImplementationOnce(mockBindAction);
        const expectedActionCreator = {
            updateExchangeRate: actions['updateExchangeRate'],
            setTogglePopup: actions['setTogglePopup'],
            toggleLoader: actions['toggleLoader'],
            fetchData: actions['fetchData']
        }

        const dispatch = {};
        mapDispatchToProps(dispatch);

        expect(mockBindAction).toHaveBeenCalledWith(expectedActionCreator, dispatch);
    })

    it('returned function of connect should be called with correct parameter', () => {
        const EditExchangeRateForm = require('../../EditExchangeRateForm').EditExchangeRateForm;

        expect(connectMock).toHaveBeenCalledWith(EditExchangeRateForm);
    })
})