import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('new view log - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const today = '5/5/2022';
    const defaultProps = {
        today
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('handleChange - should call setState when change Type select', () => {
        const event = {
            target: {
                type: 'select',
                value: 'UserInfo',
                name: 'logType'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="logType"][id="logType"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ logType: 'UserInfo' });
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/6/2022")};
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/6/2022", toDate: "6/6/2022" });
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "12/12/2022")};
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "12/12/2022" });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, { today: '3/5/2021', setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ fromDate: "3/1/2021", logType: 0, toDate: "3/5/2021" });
    });
});