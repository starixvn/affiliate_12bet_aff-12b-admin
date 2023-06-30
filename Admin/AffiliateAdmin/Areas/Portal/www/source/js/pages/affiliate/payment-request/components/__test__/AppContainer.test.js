import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import PaymentRequestUpdateForm from '../../../../common/components/PaymentRequestUpdateForm';

describe('payment request - components - AppContainer ', () => {
    const defaultProps = {
        isShowLoader: true,
        transactionId: 123,
    }

    it('render correctly', () => {
        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot()
    });

    it('setTransactionId - should be called when call handleClosePopup in PaymentRequestUpdateForm', () => {
        const setTransactionId = jest.fn();
        const props = Object.assign({}, defaultProps, { setTransactionId });

        const wrapper = shallow(<AppContainer {...props} />);
        wrapper.handleClosePopup = jest.fn();

        wrapper.find(PaymentRequestUpdateForm).props().handleClosePopup();

        expect(props.setTransactionId).toHaveBeenCalledWith(null);
    });

    it('setIsClickSearch - should be called when call handleFetchData in PaymentRequestUpdateForm', () => {
        const setIsClickSearch = jest.fn();
        const props = Object.assign({}, defaultProps, { setIsClickSearch });

        const wrapper = shallow(<AppContainer {...props} />);

        wrapper.find(PaymentRequestUpdateForm).props().handleFetchData();

        expect(props.setIsClickSearch).toHaveBeenCalledWith(true);
    })
});