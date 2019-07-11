import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('customer member - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const today = '07/11/2019';
    const defaultProps = {
        today
    };

    it('render correctly', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('componentDidMount - should call setState', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.instance().componentDidMount();

        expect(props.setParam).toHaveBeenCalledWith({ fromDate: "1/11/2019", isGetAll: false, toDate: "7/11/2019" });
    });

    it('handleChecked - should call setState when click GetAll checkbox', () => {
        const event = {
            target: {
                checked: true,
                name: 'isGetAll'
            }
        };
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="checkbox"][name="isGetAll"]').simulate('change', event);

        expect(wrapper.setState).toHaveBeenCalledWith({ isGetAll: true });
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/25/2019")};
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.setState = jest.fn();
        
        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/25/2019", toDate: "7/11/2019" });
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "7/10/2019")};
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.setState = jest.fn();
        
        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "7/10/2019" });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const event = {
            target: {
                checked: true,
                name: 'isGetAll'
            }
        };
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="checkbox"][name="isGetAll"]').simulate('change', event);
        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ fromDate: "1/11/2019", isGetAll: false, toDate: "7/11/2019" });
    });
});