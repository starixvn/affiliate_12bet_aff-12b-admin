import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

describe('exchange rate - components - Content ', () => {
    const defaultProps = {
        today: '09/06/2019',
        date: '09/06/2019',
        shouldResetScrollPosition: true,
        hasPermission: true
    }

    const gridData = [
        { ExchangeId: 9, Currency: "AUD", ActualRate: '0.70000', BuyingRate: '0.76779' },
        { ExchangeId: 3, Currency: "US$", ActualRate: '1.00000', BuyingRate: '1.0000' }
    ]

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData', () => {
        const props = Object.assign({}, defaultProps, { gridData });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData and no permission', () => {
        const props = Object.assign({}, defaultProps, { gridData, hasPermission: false });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call fetchData if param change', () => {
        const props = Object.assign({}, defaultProps, { date: '09/05/2019' });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.setState).toHaveBeenCalledWith({ isToday: false });
    });

    it('componentDidUpdate - should call fetchData if param change', () => {
        const props = Object.assign({}, defaultProps, { date: '09/06/2018' });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.setState).toHaveBeenCalledWith({ isToday: false });
    });

    it('componentDidUpdate - should not call fetchData if param not change', () => {
        const props = Object.assign({}, defaultProps, { date: '09/06/2019' });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.setState).not.toHaveBeenCalled();
    });

    it('_handleSelectedExchangeRate - should be called when click ActualRate Button', () => {
        const wrapper = shallow(<Content {...defaultProps} />);

        wrapper.instance()._handleSelectedExchangeRate = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(wrapper.instance()._handleSelectedExchangeRate).toHaveBeenCalledWith({ ExchangeId: 9, Currency: "AUD", Rate: "0.70000", RateType: 0 });
    });

    it('_handleSelectedExchangeRate - should be called when click BuyingRate Button', () => {
        const wrapper = shallow(<Content {...defaultProps} />);

        wrapper.instance()._handleSelectedExchangeRate = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(wrapper.instance()._handleSelectedExchangeRate).toHaveBeenCalledWith({ ExchangeId: 9, Currency: "AUD", Rate: "0.76779", RateType: 1 });
    });

    it('_handleSelectedExchangeRate - should call setTogglePopup and setSelectedExchangeRate and setShouldResetScrollPosition', () => {
        const setTogglePopup = jest.fn();
        const setSelectedExchangeRate = jest.fn();
        const setShouldResetScrollPosition = jest.fn();

        const props = Object.assign({}, defaultProps, { setTogglePopup, setSelectedExchangeRate, setShouldResetScrollPosition });
        const wrapper = shallow(<Content {...props} />);

        const selectedExchangeRate = { ExchangeId: 9, Currency: "AUD", Rate: 0.76779, RateType: 1 };
        wrapper.instance()._handleSelectedExchangeRate(selectedExchangeRate);

        expect(props.setTogglePopup).toHaveBeenCalledWith(true);
        expect(props.setSelectedExchangeRate).toHaveBeenCalledWith(selectedExchangeRate);
        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
    });
});