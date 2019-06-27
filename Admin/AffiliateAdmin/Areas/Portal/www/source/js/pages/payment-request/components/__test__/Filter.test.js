import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../modules/ajaxRequest';
import DatePicker from '@9softstudio/react-datepicker';

describe('payment request - components - Content ', () => {
    const defaultProps = {
        today: "6/27/2019",
        param: {}
    }

    const param = {
        FromDate: "6/25/2019",
        IsGetAll: false,
        PageIndex: 1,
        PageSize: 20,
        SearchBy: 2,
        Status: null,
        ToDate: "6/26/2019",
        Username: null
    }

    const gridData = [
        {
            AffName: "12BETAFF226",
            Amount: 125,
            CompletedDateText: "06/20/2019 22:45:23",
            Creator: "AAwalter",
            Status: "Approved",
            TransDateText: "06/20/2019 22:25:07",
            TransId: 1262276211
        },
        {
            AffName: "12BETAFF226",
            Amount: 105,
            CompletedDateText: "06/19/2019 00:20:18",
            Creator: "AAwalter",
            Status: "Pending",
            TransDateText: "06/18/2019 22:14:31",
            TransId: 1262276209
        }
    ]

    it('render correctly with SearchBy Username', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with SearchBy Others', () => {
        const props = Object.assign({}, defaultProps, { param })
        const result = shallow(<Filter {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ FromDate: "6/26/2019", IsGetAll: false, SearchBy: 1, Status: undefined, ToDate: "6/27/2019", Username: null });
    });

    it("should call handleChange when change Search By's value", () => {
        const mockedEvent = { target: { name: 'searchBy', value: 1 } }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][name="searchBy"]').simulate('change', mockedEvent);

        expect(wrapper.state('searchBy')).toBe(1);
    });

    it('handlePressEnter - should be called when press Enter', () => {
        const mockedEvent = { charCode: 13 };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleSearch = jest.fn();

        wrapper.find('Input[type="text"][name="userName"]').simulate('keyPress', mockedEvent);

        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it("should call handleChange when change Status's value", () => {
        const mockedEvent = { target: { name: 'status', value: 1 } }
        const props = Object.assign({}, defaultProps, { param })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][name="status"]').simulate('change', mockedEvent);

        expect(wrapper.state('status')).toBe(1);
    });

    it('should call handleChange when click checkbox Get All', () => {
        const props = Object.assign({}, defaultProps, { param })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="checkbox"]').simulate('change');

        expect(wrapper.state('checkbox')).toEqual(true);
    });

    it('should call handleChangeFromDate when From change', () => {
        const monthDate = "6/25/2019";
        const props = Object.assign({}, defaultProps, { param })
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        
        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(instance.setState).toHaveBeenCalled();
    });

    it('should call handleChangeToDate when To change', () => {
        const monthDate = "6/26/2019";
        const props = Object.assign({}, defaultProps, { param })
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        
        wrapper.find(DatePicker).at(1).simulate('select', monthDate);

        expect(instance.setState).toHaveBeenCalled();
    });
})