import React from 'react';
import { EditBannerForm } from '../EditBannerForm';
import { shallow } from 'enzyme';

describe('banner list - components - EditBannerForm ', () => {
    const bannerInfo = {
        BannerId: 2485,
        Category: "Baccarat",
        Currency: "EURO",
        DeviceType: 1,
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
            { CategoryName: "Casino" },
            { CategoryName: "Sportsbook" }
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
        ]
    };

    it('render correctly with insert', () => {
        const result = shallow(<EditBannerForm {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: 2485, getBannerDetail: jest.fn() });
        const result = shallow(<EditBannerForm {...props} />);
        result.setState({ isInsert: false, category: "Sportsbook", isSuccess: false, inputFileName: "test.gif" });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with not show popup', () => {
        const props = Object.assign({}, defaultProps, { selectedBannerId: null });
        const result = shallow(<EditBannerForm {...props} />);
        result.setState({ category: "" })

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
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Baccarat", "currency": "EURO", "deviceType": 1, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "" });
    });

    it('_handleGetBannerDetail should call getBannerDetail and not call setState', async () => {
        const selectedBannerId = 2485;
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => null) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).not.toHaveBeenCalled();
    });

    it('_handleGetBannerDetail should call getBannerDetail and setState  with casino category', async () => {
        const selectedBannerId = 2485;
        const banner = Object.assign({}, bannerInfo, { Category: 'Slot' })
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => banner) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Casino", "currency": "EURO", "deviceType": 1, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "Slot" });
    });

    it('_handleGetBannerDetail should call getBannerDetail and setState with sportsbook category', async () => {
        const selectedBannerId = 2485;
        const banner = Object.assign({}, bannerInfo, { Category: 'Basketball' })
        const props = Object.assign({}, defaultProps, { getBannerDetail: jest.fn(() => banner) });
        const result = shallow(<EditBannerForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetBannerDetail(selectedBannerId);

        expect(props.getBannerDetail).toHaveBeenCalledWith(selectedBannerId);
        expect(result.setState).toHaveBeenCalledWith({ "affType": 2, "category": "Sportsbook", "currency": "EURO", "deviceType": 1, "imageType": "Image", "inputFileName": "giphy.gif", "language": "Chinese Simplified", "subCategory": "Basketball" });
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

    it('_handleChange should be called when change category select to Sportsbook', () => {
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

    it('_handleChange should be called when change category select to others', () => {
        const event = {
            target: {
                name: 'category',
                value: 'Number Game'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="select"][id="category"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledTimes(2);
        expect(result.setState.mock.calls[0][0]).toEqual({ category: 'Number Game' });
        expect(result.setState.mock.calls[1][0]).toEqual({ subCategory: '' });
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

    it('_handleChange should be not called when change input file name', () => {
        const event = {
            target: {
                name: 'inputFileName',
                value: 'should_call_handleShowAlert_with_invalid_lenght_test.png'
            }
        };

        const result = shallow(<EditBannerForm {...defaultProps} />);

        result.setState = jest.fn();

        result.find('Input[type="text"][id="inputFileName"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ inputFileName: 'should_call_handleShowAlert_with_invalid_lenght_test.png' });
    });

    it('_handleSubmit - should call toggleLoader and _handleShowAlert when insert not has file', async () => {
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

    it('_handleSubmit - should call toggleLoader and _handleShowAlert when update not has input file name', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ isInsert: false });
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "You must enter a file name for banner.");
    });

    it('_handleSubmit - should call toggleLoader and _handleShowAlert when update not has file but has input file name', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ isInsert: false, inputFileName: "test.gif" });
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        instance._validateInputFile = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._validateInputFile).toHaveBeenCalledWith("test.gif");
    });

    it('_handleSubmit - should call toggleLoader and _handleShowAlert', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ file: { name: "test.pdf" } });
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        instance._handleShowAlert = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'");
    });

    it('_handleSubmit - should call toggleLoader and _save', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ file: { name: "test.gif" } });
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        instance._save = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._save).toHaveBeenCalled();
    });

    it('_handleSubmit - should call toggleLoader and _validateInputFile', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ file: { name: "test.gif" }, inputFileName: 'test.png' });
        const instance = wrapper.instance();

        instance._validateInputFile = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._validateInputFile).toHaveBeenCalledWith('test.png');
    });

    it('_validateInputFile - should call _save', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._save = jest.fn();

        await instance._validateInputFile('test.png');

        expect(instance._save).toHaveBeenCalled();
    });

    it('_validateInputFile - should call _handleShowAlert', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._validateInputFile('test.pdf');

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'");
    });

    it('_validateInputFile - should call _handleShowAlert with invalid lenght', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._validateInputFile('should_call_handleShowAlert_with_invalid_lenght_test.png');

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "The maximum length of file name is 50. Please choose another name.");
    });

    it('_save - should call insertBanner and _handleAfterSave', async () => {
        const expectedResult = { IsSuccessful: true, Message: ''};
        const props = Object.assign({}, defaultProps, { insertBanner: jest.fn(() => expectedResult) });
        const wrapper = shallow(<EditBannerForm {...props} />);
        const instance = wrapper.instance();

        instance._handleAfterSave = jest.fn();

        await instance._save();

        expect(props.insertBanner).toHaveBeenCalledWith({"affType": 2, "bannerId": 0, "category": "Casino", "currency": "EURO", "deviceType": 0, "file": null, "imageType": "Image", "inputFileName": "", "language": "Chinese Simplified", "subCategory": "Baccarat"});
        expect(instance._handleAfterSave).toHaveBeenCalledWith(true, expectedResult);
    });

    it('_save - should call updateBanner and _handleAfterSave', async () => {
        const expectedResult = { IsSuccessful: true, Message: ''};
        const props = Object.assign({}, defaultProps, { updateBanner: jest.fn(() => expectedResult) });
        const wrapper = shallow(<EditBannerForm {...props} />);
        wrapper.setState({ isInsert: false });
        const instance = wrapper.instance();

        instance._handleAfterSave = jest.fn();

        await instance._save();

        expect(props.updateBanner).toHaveBeenCalledWith({"affType": 2, "bannerId": 0, "category": "Casino", "currency": "EURO", "deviceType": 0, "file": null, "imageType": "Image", "inputFileName": "", "language": "Chinese Simplified", "subCategory": "Baccarat"});
        expect(instance._handleAfterSave).toHaveBeenCalledWith(false, expectedResult);
    });

    it('_handleAfterSave - should call _handleShowAlert with the add is error', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(true, null);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "The add is failed!");
    });

    it('_handleAfterSave - should call _handleShowAlert with the update is error', async () => {
        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(false, null);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "The update is failed!");
    });

    it('_handleAfterSave - should call _handleShowAlert with add successfully', async () => {
        const result = { IsSuccessful: true, Message: ''};

        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(true, result);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Your banner has been added successfully!");
    });

    it('_handleAfterSave - should call _handleShowAlert with update successfully', async () => {
        const result = { IsSuccessful: true, Message: ''};

        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(false, result);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Your banner has been updated successfully!");
    });

    it('_handleAfterSave - should call _handleShowAlert with the add is failed', async () => {
        const result = { IsSuccessful: false, Message: 'the add is failed!'};

        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(true, result);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'the add is failed!');
    });

    it('_handleAfterSave - should call _handleShowAlert with the update is failed', async () => {
        const result = { IsSuccessful: false, Message: 'the update is failed!'};

        const wrapper = shallow(<EditBannerForm {...defaultProps} />);
        const instance = wrapper.instance();

        instance._handleShowAlert = jest.fn();

        await instance._handleAfterSave(false, result);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, 'the update is failed!');
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