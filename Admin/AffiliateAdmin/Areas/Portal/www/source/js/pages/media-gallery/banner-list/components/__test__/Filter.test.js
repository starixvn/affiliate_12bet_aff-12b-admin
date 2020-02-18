import React from 'react';
import { Filter } from '../Filter';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';

describe('banner list - components - Filter ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        categoryList: [
            { CategoryName: "Casino" },
            { CategoryName: "Sportbook" }
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
            { Name: "EURO" },
            { Name: "VND" }
        ],
        imageTypeList: [
            { Name: "Image" },
            { Name: "Flash" }
        ],
        sizeList: [
            { Name: "300x250" },
            { Name: "500x350" }
        ],
        affTypeList: [
            { AffTypeId: 2, AffTypeName: 'Asian' },
            { AffTypeId: 3, AffTypeName: 'Media' }
        ],
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

        expect(props.setParam).toHaveBeenCalledWith({"PageIndex": 1, "PageSize": 20, "affType": 2, "category": "", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it('_handleChangeCategory - should change category when change category select', () => {
        const event = {
            target: { value: 'Casino' }
        };
        const props = Object.assign({}, defaultProps, { getSizeList: jest.fn(), getSubCategoryCasinoList: jest.fn(), subCategoryCasinoList: null });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="category"]').prop('value')).toEqual('Casino');
        expect(props.getSubCategoryCasinoList).toHaveBeenCalled();
        expect(props.getSizeList).toHaveBeenCalledWith({"affType": 2, "category": "Casino", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
    });

    it('_handleChangeCategory - should change category when change category select', () => {
        const event = {
            target: { value: 'Sportsbook' }
        };
        const props = Object.assign({}, defaultProps, { getSizeList: jest.fn(), getSubCategorySportsbookList: jest.fn(), subCategorySportsbookList: null });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="category"]').prop('value')).toEqual('Sportsbook');
        expect(props.getSubCategorySportsbookList).toHaveBeenCalled();
        expect(props.getSizeList).toHaveBeenCalledWith({"affType": 2, "category": "Sportsbook", "currency": "", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
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
        expect(props.getSizeList).toHaveBeenCalledWith({"affType": 2, "category": "", "currency": "USD", "deviceType": 0, "imageType": "", "language": "", "size": "", "subCategory": ""});
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
        expect(props.setParam).toHaveBeenCalledWith({"PageIndex": 1, "PageSize": 20, "affType": 2, "category": "", "currency": "", "deviceType": 0, "imageType": "Flash", "language": "", "size": "", "subCategory": ""});
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