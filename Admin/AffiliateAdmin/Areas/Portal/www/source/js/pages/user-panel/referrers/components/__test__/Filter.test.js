import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('referrers - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });
    afterEach(() => {
        jest.clearAllMocks();
    });
    const today = '07/12/2019';
    const param = {
        deviceType: 0,
        fromDate: "10/1/2018",
        isClickLink: false,
        toDate: "10/31/2018"
    };
    const countryList = [
        {
            CountryCode: "en",
            CountryName: "England",
            Checked: true
        }
    ];
    const defaultProps = {
        affId: 1271808,
        today,
        param,
        getCountryList: jest.fn(() => countryList)
    };
    const setParam = jest.fn();
    const setSelectedSiteDetailFilter = jest.fn();

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);
        result.setState({ countryList });

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call handleCountryList', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam, setParam, setSelectedSiteDetailFilter });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.handleCountryList = jest.fn();

        instance.componentDidMount();

        expect(instance.handleCountryList).toHaveBeenCalled();
    });

    it('componentDidMount - should not call handleCountryList', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam, setParam, setSelectedSiteDetailFilter });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState({ countryList: [] });
        instance.handleCountryList = jest.fn();

        instance.componentDidMount();

        expect(instance.handleCountryList).not.toHaveBeenCalled();
    });

    it('componentDidUpdate - should call setState and handleSearch', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam, setParam, setSelectedSiteDetailFilter });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();
        instance.handleSearch = jest.fn();

        instance.componentDidUpdate({ param: { } });

        expect(instance.setState).toHaveBeenCalledWith({ deviceType: 0, fromDate: "10/1/2018", toDate: "10/31/2018", countryList: null, initialSelectedCountries: "", selectedCountries: "", initialCountryList: null });
        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it('componentDidUpdate - should not call setState', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.setState).not.toHaveBeenCalled();
    });

    it('handleChange - should call setState when change deviceType select', () => {
        const event = {
            target: {
                value: 1,
                name: 'deviceType'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ countryList });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="deviceType"][id="deviceType"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ deviceType: 1 });
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/25/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ countryList });
        wrapper.setState = jest.fn(fn => (fn instanceof Function) && fn(() => ({ fromDate: "6/25/2019", toDate: "7/12/2019" })));

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "7/10/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ countryList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "7/10/2019" });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const props = Object.assign({}, defaultProps, { setParam, setSelectedSiteDetailFilter, setIsGroupByCountry: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ countryList });

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ selectedCountries: "", deviceType: 0, fromDate: "7/01/2019", toDate: "7/12/2019", affId: 1271808 });
    });
});