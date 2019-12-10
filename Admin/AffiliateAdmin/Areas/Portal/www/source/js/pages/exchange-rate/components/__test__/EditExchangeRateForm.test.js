import React from 'react';
import { EditExchangeRateForm } from '../EditExchangeRateForm';
import { shallow } from 'enzyme';

describe('exchange rate - components - EditExchangeRateForm ', () => {
    const defaultProps = {
        date: '09/06/2019',
        isOpenPopup: true
    }
    const selectedExchangeRate = {
        ExchangeId: 9,
        Currency: "AUD",
        Rate: "0.767789",
        RateType: 1
    }
    it('render correctly', () => {
        const result = shallow(<EditExchangeRateForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have selectedExchangeRate', () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const result = shallow(<EditExchangeRateForm {...props} />);
    
        expect(result).toMatchSnapshot();
    });

    it('render correctly with have selectedExchangeRate and RateType is US$Rate', () => {
        const exchangeRate = Object.assign({}, selectedExchangeRate, { RateType: 0 })
        const props = Object.assign({}, defaultProps, { selectedExchangeRate: exchangeRate });
        const result = shallow(<EditExchangeRateForm {...props} />);
    
        expect(result).toMatchSnapshot();
    });

    it('render correctly with have selectedExchangeRate and isSuccess false ', () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const result = shallow(<EditExchangeRateForm {...props} />);

        result.setState({ isSuccess: false });    

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have selectedExchangeRate but invalid rate', () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const result = shallow(<EditExchangeRateForm {...props} />);
    
        result.setState({ rate: "0." });
    
        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call setState if selectedExchangeRate change', () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const componentInstance = new EditExchangeRateForm(props);
        componentInstance.setState = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.setState).toHaveBeenCalledWith({ rate: "0.767789" });
    });

    it('_handleChange - should call setState with correct rate', async () => {
        const event = {
            target: {
                name: 'rate',
                value: '123456'
            }
        }
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('change', event);

        expect(wrapper.setState).toHaveBeenCalledWith({ rate: '123,456' });
    });

    it('_handleChange - should call setState with correct rate but no format', async () => {
        const event = {
            target: {
                name: 'rate',
                value: '0.12345'
            }
        }
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('change', event);

        expect(wrapper.setState).toHaveBeenCalledWith({ rate: '0.12345' });
    });

    it('_handleChange - should call _handleShowAlert with rate exceed limit', async () => {
        const event = {
            target: {
                name: 'rate',
                value: '922337203685479'
            }
        }
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance._handleShowAlert = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('change', event);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Exchange rate exceeds the allowable limit.');
    });

    it('_handleChange - should not call setState with invalid rate', async () => {
        const event = {
            target: {
                name: 'rate',
                value: 'abc'
            }
        }
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('change', event);

        expect(wrapper.setState).not.toHaveBeenCalledWith();
    });

    it('_handleKeyPress - should call handleSubmit when press Enter', () => {
        const event = { charCode: 13, preventDefault: jest.fn() };
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance._handleSubmit = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('keyPress', event);

        expect(instance._handleSubmit).toHaveBeenCalled();
    });

    it('_handleKeyPress - should call _handleShowAlert when press non digit Key', () => {
        const event = { charCode: 32, preventDefault: jest.fn() };
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance.handleSubmit = jest.fn();
        instance._handleShowAlert = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('keyPress', event);

        expect(instance.handleSubmit).not.toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Please input digit only.');
    });

    it('_handleKeyPress - should call _handleShowAlert when press non digit Key 2', () => {
        const event = { charCode: 58, preventDefault: jest.fn() };
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance.handleSubmit = jest.fn();
        instance._handleShowAlert = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('keyPress', event);

        expect(instance.handleSubmit).not.toHaveBeenCalled();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Please input digit only.');
    });

    it('_handleKeyPress - should not call handleSubmit and _handleShowAlert when press digit Key', () => {
        const event = { charCode: 2 };
        const props = Object.assign({}, defaultProps, { selectedExchangeRate });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance.handleSubmit = jest.fn();
        instance._handleShowAlert = jest.fn();

        wrapper.find('Input[type="text"][name="rate"][id="rate"]').simulate('keyPress', event);

        expect(instance.handleSubmit).not.toHaveBeenCalled();
        expect(instance._handleShowAlert).not.toHaveBeenCalled();
    });

    it('handleSubmit - should call toggleLoader and _handleShowAlert with rate is required', async () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate, toggleLoader: jest.fn() });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        const instance = wrapper.instance();
        instance._handleShowAlert = jest.fn();

        await wrapper.find('Button').at(0).simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Exchange rate is required.');
    });

    it('handleSubmit - should call toggleLoader and setState with amount is invalid', async () => {
        const props = Object.assign({}, defaultProps, { selectedExchangeRate, toggleLoader: jest.fn() });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState({ rate: '0' });
        const instance = wrapper.instance();
        instance._handleShowAlert = jest.fn();

        await wrapper.find('Button').at(0).simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Exchange rate must be greater than zero.');
    });

    it('handleSubmit - should call toggleLoader, updateExchangeRate and _handleShowAlert with correct rate', async () => {
        const result = { IsSuccessful: false, Message: 'Update failed!' }
        const updateExchangeRate = jest.fn(() => result);
        const props = Object.assign({}, defaultProps, { updateExchangeRate, selectedExchangeRate, toggleLoader: jest.fn() });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState({ rate: '0.05' });
        const instance = wrapper.instance();
        instance._handleShowAlert = jest.fn();

        await wrapper.find('Button').at(0).simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.updateExchangeRate).toHaveBeenCalledWith({ Currency: "AUD", ExchangeId: 9, Rate: 0.05, RateType: 1 });
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'Update failed!');
    });

    it('handleSubmit - should call toggleLoader, updateExchangeRate and _handleShowAlert with correct amount', async () => {
        const result = { IsSuccessful: true, Message: '' }
        const updateExchangeRate = jest.fn(() => result);
        const toggleLoader = jest.fn();
        const props = Object.assign({}, defaultProps, { updateExchangeRate, selectedExchangeRate, toggleLoader });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState({ rate: '0.05' });

        const instance = wrapper.instance();
        instance._handleShowAlert = jest.fn();

        await wrapper.find('Button').at(0).simulate('click');

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.updateExchangeRate).toHaveBeenCalledWith({ Currency: "AUD", ExchangeId: 9, Rate: 0.05, RateType: 1 });
        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, 'Updated successfully!');
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const wrapper = shallow(<EditExchangeRateForm {...defaultProps} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, val) => fn && fn());
        const instance = wrapper.instance();
        instance._hideMessage = jest.fn();

        instance._handleShowAlert(false, 'Updated failed!')

        expect(wrapper.setState.mock.calls[0]).toEqual([{ isShowMessage: true, isSuccess: false, message: 'Updated failed!' }, expect.any(Function)]);
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(instance._hideMessage).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState and not call setTogglePopup', async () => {
        const props = Object.assign({}, defaultProps, { setTogglePopup: jest.fn(), fetchData: jest.fn() });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState = jest.fn();

        wrapper.instance()._hideMessage()

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(props.setTogglePopup).not.toHaveBeenCalled();
    });

    it('_hideMessage - should call setState, fetchData and setTogglePopup', async () => {
        const props = Object.assign({}, defaultProps, { setTogglePopup: jest.fn(), fetchData: jest.fn() });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);
        wrapper.setState = jest.fn();

        wrapper.instance()._hideMessage(true)

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).toHaveBeenCalledWith('09/06/2019');
        expect(props.setTogglePopup).toHaveBeenCalledWith(false);
    });

    it('setTogglePopup - should be called when click Cancel button', () => {
        const setTogglePopup = jest.fn();
        const props = Object.assign({}, defaultProps, { selectedExchangeRate, setTogglePopup });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);

        wrapper.find('Button').at(1).simulate('click');
    
        expect(props.setTogglePopup).toHaveBeenCalledWith(false);
    });

    it('setTogglePopup - should be called when click toggle in Modal', () => {
        const setTogglePopup = jest.fn();
        const props = Object.assign({}, defaultProps, { selectedExchangeRate, setTogglePopup });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);

        wrapper.find('Modal').props().toggle();
    
        expect(props.setTogglePopup).toHaveBeenCalledWith(false);
    });

    it('setTogglePopup - should be called when click toggle in ModalHeader', () => {
        const setTogglePopup = jest.fn();
        const props = Object.assign({}, defaultProps, { selectedExchangeRate, setTogglePopup });
        const wrapper = shallow(<EditExchangeRateForm {...props} />);

        wrapper.find('ModalHeader').props().toggle();
    
        expect(props.setTogglePopup).toHaveBeenCalledWith(false);
    });
})