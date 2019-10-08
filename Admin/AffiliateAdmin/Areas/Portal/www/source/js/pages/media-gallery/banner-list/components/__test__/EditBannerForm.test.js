import React from 'react';
import { EditBannerForm } from '../EditBannerForm';
import { shallow } from 'enzyme';

describe('banner list - components - EditBannerForm ', () => {
    const bannerInfo = {
        BannerId: 2485,
        Category: "Baccarat",
        Currency: "EURO",
        DeviceType: "PC / Laptop",
        FileName: "CategoryMedia/Casino/Baccarat/giphy.gif",
        ImageType: "Image",
        Language: "Chinese Simplified",
        Size: "500x500",
        AffType: 2
    };

    const param = {
        AffType: 2,
        Category: "",
        Currency: "",
        DeviceType: 0,
        ImageType: "",
        Language: "",
        PageIndex: 1,
        PageSize: 20,
        Size: "",
        SubCategory: ""
    };

    const defaultProps = {
        selectedBannerId: 0,
        param,
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
        ]
    };

    it('render correctly with insert', () => {
        const result = shallow(<EditBannerForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: 2485, getBannerDetail: jest.fn() });
        const result = shallow(<EditBannerForm {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with not show popup', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: null });
        const result = shallow(<EditBannerForm {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate should call setState', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: 2485, getBannerDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        result.instance().componentDidUpdate(defaultProps);

        expect(result.setState).toHaveBeenCalledWith({ isInsert: false });
    });

    it('componentDidUpdate should call setState and _handleGetBannerDetail', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: 2485, getBannerDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<EditBannerForm {...props} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetBannerDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).toHaveBeenCalledWith({ isInsert: false });
        expect(instance._handleGetBannerDetail).toHaveBeenCalledWith(2485);
    });

    it('componentDidUpdate should not call setState and _handleGetBannerDetail', () => {
        const result = shallow(<EditBannerForm {...defaultProps} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetBannerDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).not.toHaveBeenCalled();
        expect(instance._handleGetBannerDetail).not.toHaveBeenCalled();
    });

    it('_handleGetBannerDetail should call getBannerDetail and setState', async () => {
        const selectedBannerId = 2485;
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Baccarat", "currency": "EURO", "deviceType": 0, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "" });
    });

    it('_handleGetBannerDetail should call getBannerDetail and setState  with casino category', async () => {
        const selectedBannerId = 2485;
        const banner = Object.assign({}, bannerInfo, { Category: 'Slot' })
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => banner) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Casino", "currency": "EURO", "deviceType": 0, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "Slot" });
    });

    it('_handleGetBannerDetail should call getBannerDetail and setState with sportsbook category', async () => {
        const selectedBannerId = 2485;
        const banner = Object.assign({}, bannerInfo, { Category: 'Basketball' })
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => banner) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Sportsbook", "currency": "EURO", "deviceType": 0, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "Basketball" });
    });

    it('_handleFile should be called when change file', () => {
        const event = {
            target: {
                files: [{ name: 'test.jpg' }]
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="file"][id="file"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({"file": {"name": "test.jpg"}, "inputFileName": "test.jpg", "isDisabled": false});
    });

    it('_handleFile should be called when change file but not chose', () => {
        const event = {
            target: {
                files: null
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="file"][id="file"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({"file": null, "inputFileName": "", "isDisabled": true});
    });

    it('_handleFile should be called when change file but chose invalid file', () => {
        const event = {
            target: {
                files: [{ name: 'abc.pdf' }]
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);
        result.instance()._handleShowAlert = jest.fn();

        result.setState = jest.fn();

        result.find('Input[type="file"][id="file"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({"file": {"name": "abc.pdf"}, "inputFileName": "abc.pdf", "isDisabled": true});
        expect(result.instance()._handleShowAlert).toHaveBeenCalledWith(false, "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'" );
    });

    it('_handleFile should be called when change file but file name empty', () => {
        const event = {
            target: {
                files: [{ name: '' }]
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);
        result.instance()._handleShowAlert = jest.fn();

        result.setState = jest.fn();

        result.find('Input[type="file"][id="file"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({"file": {"name": ""}, "inputFileName": "", "isDisabled": true});
        expect(result.instance()._handleShowAlert).toHaveBeenCalledWith(false, "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'" );
    });

    it('_handleChange should be called when change category select to Casino', () => {
        const event = {
            target: {
                name: 'category',
                value: 'Casino'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ category: 'Casino' });
    });

    it('_handleChange should be called when change category select to Sportbook', () => {
        const event = {
            target: {
                name: 'category',
                value: 'Sportsbook'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ category: 'Sportsbook' });
    });

    it('_handleChange should be called when change language select', () => {
        const event = {
            target: {
                name: 'language',
                value: 'English'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="language"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ language: 'English' });
    });

    it('_handleChange should be called when change currency select', () => {
        const event = {
            target: {
                name: 'currency',
                value: 'VND'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="currency"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ currency: 'VND' });
    });

    it('_handleChange should be called when change deviceType select', () => {
        const event = {
            target: {
                name: 'deviceType',
                value: 1
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="deviceType"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ deviceType: 1 });
    });

    it('_handleSubmit - should be called toggleLoader and _handleShowAlert when insert not have file', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select banner to upload.");
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, val) => fn && fn());

        const instance = wrapper.instance();
        instance._hideMessage = jest.fn();

        instance._handleShowAlert(false, 'Updated failed!');

        expect(wrapper.setState.mock.calls[0]).toEqual([{ isShowMessage: true, isSuccess: false, message: 'Updated failed!' }, expect.any(Function)]);
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(instance._hideMessage).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState, fetchData and _handleClosePopup', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(true);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).toHaveBeenCalledWith(param);
        expect(instance._handleClosePopup).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState and not call _handleFetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(false);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(instance._handleClosePopup).not.toHaveBeenCalled();
    });

    it('_handleClosePopup - should call setSelectedBannerId and setState', async () => {
        const props = Object.assign({}, defaultProps, { setSelectedBannerId: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance._handleClosePopup();

        expect(props.setSelectedBannerId).toHaveBeenCalledWith(null);
        expect(instance.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Casino", "currency": "EURO", "deviceType": 0, "file": null, "imageType": "Image", "inputFileName": "", "isDisabled": true, "isInsert": true, "isShowMessage": false, "isSuccess": true, "language": "Chinese Simplified", "message": "", "subCategory": "Baccarat" });
    });
})