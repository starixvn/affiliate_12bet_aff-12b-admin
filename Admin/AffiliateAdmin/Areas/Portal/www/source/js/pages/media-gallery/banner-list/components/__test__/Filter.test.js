import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

import { defaultPagingOption } from '../../../../../modules/constants';

describe('banner list - components - Filter ', () => {
    const deviceTypeList = [
        {
            key: 0, name: "PC / Laptop", checked: false
        },
        {
            key: 1, name: "Mobile", checked: false
        }
    ];

    const defaultProps = {
        pagingOption: defaultPagingOption,
        categoryList: [
            { categoryName: "Casino", key: "Casino", name: "Casino", checked: false },
            { categoryName: "Sportsbook", key: "Sportsbook", name: "Sportsbook", checked: false }
        ],
        subCategoryList: [
            { parentCategoryName: "Casino", key: "Slot", name: "Slot", checked: false },
            { parentCategoryName: "Sportsbook", key: "Basketball", name: "Basketball", checked: false }
        ],
        subCategoryCasinoList: [
            { CategoryName: "Casino Bonus" },
            { CategoryName: "Slot" }
        ],
        subCategorySportsbookList: [
            { CategoryName: "Basketball" },
            { CategoryName: "SB Bonus" }
        ],
        languageList: [
            { LanguageName: "English" },
            { LanguageName: "Vietnamese" }
        ],
        currencyList: [
            { key: "EURO", name: "EURO", checked: false },
            { key: "VND", name: "VND", checked: false }
        ],
        imageTypeList: [
            { Id: 0, Name: "Image"},
            { Id: 1, Name: "Flash"}
        ],
        sizeList: [
            { key: "300x250", name: "300x250", checked: false },
            { key: "500x350", name: "500x350", checked: false }
        ],
        affTypeList: [
            { key: "Asian", name: "Asian", checked: false },
            { key: "Media", name: "Media", checked: false }
        ],
        deviceTypeList,
        today: "6/14/2022",
        paramFromHome: {
            IsFromHome: false
        }
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it("render correctly with Casino's subcategory", () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ category: 'Casino' });

        expect(wrapper).toMatchSnapshot();
    });

    it("render correctly with Sportbook's subcategory", () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ category: 'Sportsbook' });

        expect(wrapper).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ "PageIndex": 1, "PageSize": 20, "fromDate": "6/1/2022", "toDate": "6/14/2022", "affType": 2, "category": "", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it('_handleChangeCategory - should change category when change category select', () => {
        const event = {
            target: { value: 'Casino' }
        };
        const props = Object.assign({}, defaultProps, { getSizeList: jest.fn(), subCategoryCasinoList: null });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="category"]').prop('value')).toEqual('Casino');
        expect(props.getSizeList).toHaveBeenCalledWith({ "affType": 2, "fromDate": "6/1/2022", "toDate": "6/14/2022", "category": "Casino", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it('_handleChangeCategory - should change category when change category select', () => {
        const event = {
            target: { value: 'Sportsbook' }
        };
        const props = Object.assign({}, defaultProps, { getSizeList: jest.fn(), subCategorySportsbookList: null });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="category"]').prop('value')).toEqual('Sportsbook');
        expect(props.getSizeList).toHaveBeenCalledWith({ "affType": 2, "fromDate": "6/1/2022", "toDate": "6/14/2022", "category": "Sportsbook", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it("_handleChange - should call getSizeList and setState", () => {
        const event = {
            target: { name: 'currency', value: 'USD' }
        };
        const props = Object.assign({}, defaultProps, { getSizeList: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState = jest.fn();

        wrapper.instance()._handleChange(event)
        expect(wrapper.setState).toHaveBeenCalledWith({"currency": "USD"});
        expect(props.getSizeList).toHaveBeenCalledWith({ "affType": 2, "fromDate": "6/1/2022", "toDate": "6/14/2022", "category": "", "currency": "USD", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it('handleChangeFromDate - should be called when From > To', () => {
        const monthDate = { format: jest.fn(() => "6/6/2022") };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn(fn => (fn instanceof Function) && fn({ toDate: "12/6/2021" }));

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleChangeToDate - should be called when To < From', () => {
        const monthDate = { format: jest.fn(() => "12/12/2020") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState = jest.fn(fn => (fn instanceof Function) && fn({ fromDate: "12/12/2020" }));

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('setSize - should change category when change size select', () => {
        const event = {
            target: { value: '300x250' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][id="size"]').simulate('change', event);

        expect(wrapper.setState).toHaveBeenCalledWith({"size": "300x250"});
    });

    it('componentDidMount - should call setParamFromHome amd _handleSearch', () => {
        const props = Object.assign({}, defaultProps, { paramFromHome: { IsFromHome: true, ImageType: 'Flash' }, setParamFromHome: jest.fn(), setParam: jest.fn() });

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        instance.componentDidMount();

        expect(props.setParamFromHome).toHaveBeenCalledWith({ IsFromHome: false });
        expect(props.setParam).toHaveBeenCalledWith({ "PageIndex": 1, "PageSize": 20, "fromDate": "6/1/2022", "toDate": "6/14/2022", "affType": 2, "category": "", "currency": "", "deviceType": 0, "imageType": "Flash", "language": "", "size": "", "subCategory": ""});
    });

    it('componentDidMount - should not call setParamFromHome and _handleSearch', () => {
        const props = Object.assign({}, defaultProps, { setParamFromHome: jest.fn(), setParam: jest.fn() });

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        instance.componentDidMount();

        expect(props.setParamFromHome).not.toHaveBeenCalled();
        expect(props.setParam).not.toHaveBeenCalled();
    });
})