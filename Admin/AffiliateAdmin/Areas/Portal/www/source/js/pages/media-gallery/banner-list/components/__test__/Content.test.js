import React from 'react';
import { Content } from '../Content';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';
import { Table } from '@9softstudio/react-fixed-table-header';

describe('banner list - components - Content ', () => {
    const gridData = [
        {
            BannerId: 2485,
            Categories: ["Casino"],
            FileName: "CategoryMedia/giphy.gif",
            Language: "Chinese Simplified",
            Size: "500x500"
        },
        {
            BannerId: 2448,
            Categories: ["Number Game"],
            FileName: "CategoryMedia/step1.jpg",
            Language: "Chinese Simplified",
            Size: "1655x438"
        }
    ];

    const param = {
        AffType: 2,
        Category: "",
        Language: "",
        PageIndex: 1,
        PageSize: 20,
        Size: ""
    };

    const defaultProps = {
        pagingOption: defaultPagingOption,
        checkedBanners: { 2485: false, 2448: false },
        bannerRootUrl: null,
        shouldResetScrollPosition: true,
        param,
        gridData,
        categoryList: [
            { categoryName: "Casino", key: "Casino", name: "Casino", checked: false },
            { categoryName: "Sportsbook", key: "Sportsbook", name: "Sportsbook", checked: false }
        ],
        isModifiable: true
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [], bannerRootUrl: null })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handlePaging - should be called when onPaging in Table', () => {
        const props = Object.assign({}, defaultProps, { setPagingOption: jest.fn(), setParam: jest.fn() })
        const result = shallow(<Content {...props} />);

        result.find(Table).props().onPaging(50, 50);

        expect(props.setPagingOption).toHaveBeenCalledWith({ PageIndex: 50, PageSize: 50 });
        expect(props.setParam).toHaveBeenCalledWith({ "AffType": 2, "Category": "", "Language": "", "PageIndex": 50, "PageSize": 50, "Size": "" });
    });

    it('setSelectedBannerId - should be called when click edit button', () => {
        const props = Object.assign({}, defaultProps, { setSelectedBannerId: jest.fn(), fetchData: jest.fn(), getBannerRootUrl: jest.fn(() => 'abc/test') })
        const result = mount(<Content {...props} />);

        result.find('Button').at(0).simulate('click');

        expect(props.setSelectedBannerId).toHaveBeenCalledWith(2485);
        expect(props.fetchData).toHaveBeenCalledWith({ "AffType": 2, "Category": "", "Language": "", "PageIndex": 1, "PageSize": 20, "Size": "" });
    });

    it('setBannerDetailUrl - should be called when click image', () => {
        const props = Object.assign({}, defaultProps, { setBannerDetailUrl: jest.fn(), fetchData: jest.fn(), getBannerRootUrl: jest.fn(() => 'abc/test') })
        const result = mount(<Content {...props} />);

        result.find('a[id="link2448"]').simulate('click');

        expect(props.setBannerDetailUrl).toHaveBeenCalledWith({ "bannerId": 2448, "height": "438", "src": "null/CategoryMedia/step1.jpg?id=undefined", "width": "1655" });
        expect(props.fetchData).toHaveBeenCalledWith({ "AffType": 2, "Category": "", "Language": "", "PageIndex": 1, "PageSize": 20, "Size": "" });
    });

    it('_onCheckItem - should be called when click edit button', () => {
        const event = {
            target: {
                checked: true
            }
        };
        const props = Object.assign({}, defaultProps, { setShouldResetScrollPosition: jest.fn(), setCheckedBanners: jest.fn(), fetchData: jest.fn(), getBannerRootUrl: jest.fn(() => 'abc/test') })
        const result = mount(<Content {...props} />);

        result.find('input[type="checkbox"]').at(0).simulate('change', event);

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.setCheckedBanners).toHaveBeenCalledWith({ "2448": false, "2485": true });
        expect(props.fetchData).toHaveBeenCalledWith({ "AffType": 2, "Category": "", "Language": "", "PageIndex": 1, "PageSize": 20, "Size": "" });
    });
})