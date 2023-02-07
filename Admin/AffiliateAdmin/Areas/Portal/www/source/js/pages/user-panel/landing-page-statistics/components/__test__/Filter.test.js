import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('landing page statistics - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const today = '07/12/2019';
    const languageList = [
        {
            LanguageCode: "en-US",
            LanguageName: "English"
        },
        {
            LanguageCode: "vi-VN",
            LanguageName: "Vietnamese"
        },
        {
            LanguageCode: "zh-TW",
            LanguageName: "Chinese Traditional"
        },
        {
            LanguageCode: "abc",
            LanguageName: "ABC"
        }
    ];
    const shortCodeLanguageList = ["en-US", "vi-VN", "zh-TW", "abc"];
    const pageList = [
        {
            PageCode: "sportsbook",
            PageId: 1,
            PageName: "Sportsbook"
        },
        {
            PageCode: "casino",
            PageId: 18,
            PageName: "Casino"
        },
        {
            PageCode: "keno",
            PageId: 21,
            PageName: "Keno"
        }
    ];
    const countryList = [
        {
            CountryCode: "en",
            CountryName: "England",
            Checked: true
        }
    ];
    const defaultProps = {
        today,
        affId: 1271808,
        languageList,
        shortCodeLanguageList,
        setShortCodeLanguageList: jest.fn(),
        getPageList: jest.fn(() => pageList),
        getCountryList: jest.fn(() => countryList)
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);
        result.setState({ pageList, countryList });

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call handleGetPageList and handleCountryList', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleGetPageList = jest.fn();
        instance.handleCountryList = jest.fn();

        instance.componentDidMount();

        expect(instance.handleGetPageList).toHaveBeenCalled();
        expect(instance.handleCountryList).toHaveBeenCalled();
        expect(defaultProps.getPageList).toHaveBeenCalled();
        expect(defaultProps.getCountryList).toHaveBeenCalled();
    });

    it('componentDidMount - should not call handleGetPageList and handleCountryList', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList });
        const instance = wrapper.instance();
        instance.handleGetPageList = jest.fn();
        instance.handleCountryList = jest.fn();

        instance.componentDidMount();

        expect(instance.handleGetPageList).not.toHaveBeenCalled();
        expect(instance.handleCountryList).not.toHaveBeenCalled();
    });

    it('handleChange - should call setState when change page select', () => {
        const event = {
            target: {
                value: 'casino',
                name: 'selectedPage'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="selectedPage"][id="selectedPage"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ selectedPage: 'casino' });
    });

    it('handleChange - should call setState when change languageCode select', () => {
        const event = {
            target: {
                value: 'en',
                name: 'languageCode'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList, groupBy: "Language" });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="languageCode"][id="languageCode"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ languageCode: 'en' });
    });

    it('handleChange - should call setState when change deviceType select', () => {
        const event = {
            target: {
                value: 1,
                name: 'deviceType'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="deviceType"][id="deviceType"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ deviceType: 1 });
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/25/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList });
        wrapper.setState = jest.fn(fn => (fn instanceof Function) && fn(() => ({ fromDate: "6/25/2019", toDate: "7/12/2019" })));

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "7/10/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ pageList, countryList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "7/10/2019" });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const setSelectedPageDetailFilter = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam, setSelectedPageDetailFilter, setIsGroupByCountry: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ pageList, countryList });

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ groupBy: "Country", selectedCountries: "", affId: 1271808, deviceType: 0, fromDate: "7/01/2019", selectedPage: "", toDate: "7/12/2019" });
        expect(props.setSelectedPageDetailFilter).toHaveBeenCalledWith(null);
    });
});