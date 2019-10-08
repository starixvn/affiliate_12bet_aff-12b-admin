import React from 'react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

import { DatePicker } from '@9softstudio/react-datepicker';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('banner list - components - Filter ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        categoryList: [
            { Name: "Casino" },
            { Name: "Sportbook" }
        ],
        subCategoryCasinoList: [
            { Name: "Casino Bonus" },
            { Name: "Slot" }
        ],
        subCategorySportsbookList: [
            { Name: "Basketball" },
            { Name: "SB Bonus" }
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
        const event = {
            target: { value: 'Casino' }
        };

        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper).toMatchSnapshot();
    });

    it("render correctly with Sportbook's subcategory", () => {
        const event = {
            target: { value: 'Sportsbook' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ "AffType": 2, "Category": "", "Currency": "", "DeviceType": 0, "ImageType": "", "Language": "", "PageIndex": 1, "PageSize": 20, "Size": "", "SubCategory": "" });
    });

    it('setCategory - should change category when change category select', () => {
        const event = {
            target: { value: 'category' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="category"]').prop('value')).toEqual('category');
    });

    it("setSubCategory - should change category when change subCategory select", () => {
        const event = {
            target: { value: 'Casino' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.find('Input[type="select"][id="category"]').simulate('change', event);

        wrapper.find('Input[type="select"][id="subCategory"]').simulate('change', { target: { value: 'Casino Bonus' } });

        expect(wrapper.find('Input[type="select"][id="subCategory"]').prop('value')).toEqual('Casino Bonus');
    });

    it('setLanguage - should change category when change language select', () => {
        const event = {
            target: { value: 'language' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="language"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="language"]').prop('value')).toEqual('language');
    });

    it('setCurrency - should change category when change currency select', () => {
        const event = {
            target: { value: 'currency' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="currency"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="currency"]').prop('value')).toEqual('currency');
    });

    it('setImageType - should change category when change image select', () => {
        const event = {
            target: { value: 'Image' }
        };
        const props = Object.assign({}, defaultProps, { paramFromHome: { IsFromHome: true, ImageType: 'Flash' } })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="select"][id="imageType"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="imageType"]').prop('value')).toEqual('Image');
    });

    it('setSize - should change category when change size select', () => {
        const event = {
            target: { value: '300x250' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="size"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="size"]').prop('value')).toEqual('300x250');
    });

    it('setAffType - should change category when change affType select', () => {
        const event = {
            target: { value: '2' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="affType"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="affType"]').prop('value')).toEqual('2');
    });

    it('setDeviceType - should change category when change deviceType select', () => {
        const event = {
            target: { value: 'mobile' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][id="deviceType"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][id="deviceType"]').prop('value')).toEqual('mobile');
    });
})