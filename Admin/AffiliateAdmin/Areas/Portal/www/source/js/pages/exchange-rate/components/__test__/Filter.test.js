import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('exchange rate - components - Filter ', () => {
    const defaultProps = {
        today: "09/06/2019",
        hasPermission: true
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('handleSearch - should be called when click Search Button', () => {
        const setDate = jest.fn();
        const fetchData = jest.fn();
        const props = Object.assign({}, defaultProps, { setDate, fetchData });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setDate).toHaveBeenCalledWith("9/6/2019");
        expect(props.fetchData).toHaveBeenCalledWith("9/6/2019");
    });

    it('should call handleChangeDate when To change', () => {
        const date = { format: jest.fn(() => "09/05/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn();
        
        wrapper.find(DatePicker).simulate('change', date);

        expect(wrapper.setState).toHaveBeenCalledWith({ date: "09/05/2019" });
    });
})