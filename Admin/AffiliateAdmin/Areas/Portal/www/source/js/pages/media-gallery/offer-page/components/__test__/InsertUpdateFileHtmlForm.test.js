import React from 'react';
import { InsertUpdateFileHtmlForm } from '../InsertUpdateFileHtmlForm';
import { shallow } from 'enzyme';
import DatePicker from 'react-datepicker';
import { formatDateTime } from '../../../../../modules/utils';
import { defaultState } from '../../libs/constants';

describe('offer page - components - InsertUpdateFileHtmlForm ', () => {
    const offerPageInfo = {
        FileName: "banner.html",
        FileSize: 7344,
        EffectiveFromDate: "2021-04-01T00:00:00",
        Language: "English",
        PageId: 25,
        EffectiveToDate: "2021-04-30T00:00:00",
        UploadDate: "2021-03-24T03:56:56.047"
    };

    const param = {
        pageIndex: 1,
        pageSize: 20
    };

    const languageList = [
        { LanguageCode: "en-US", LanguageName: "English" },
        { LanguageCode: "vi-VN", LanguageName: "Vietnamese" }
    ]

    const defaultProps = {
        selectedId: 0,
        param,
        languageList
    };

    it('render correctly with insert', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        result.instance().setState({
            fromDate: new Date(2021, 8, 1), toDate: new Date(2021, 9, 0)
        });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const props = Object.assign({}, defaultProps, { selectedId: 2485, getOfferPageDetail: jest.fn() });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        result.instance().setState({
            isInsert: false, isSuccess: false,
            fromDate: new Date(2021, 8, 1), toDate: new Date(2021, 9, 0)
        });

        expect(result).toMatchSnapshot();
    });

    it('_handleChangeDatePicker - should be called when change from date', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleChangeDatePicker = jest.fn();

        result.find(DatePicker).at(0).props().onChange("1/3/2021");

        expect(instance._handleChangeDatePicker).toHaveBeenCalledWith("fromDate", '1/3/2021');
    });

    it('_handleChangeDatePicker - should be called when change to date', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleChangeDatePicker = jest.fn();

        result.find(DatePicker).at(1).props().onChange("24/3/2021");

        expect(instance._handleChangeDatePicker).toHaveBeenCalledWith("toDate", '24/3/2021');
    });

    it('componentDidUpdate - should call setState and _handleGetItemDetail', () => {
        const props = Object.assign({}, defaultProps, { selectedId: 25, getOfferPageDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetItemDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).toHaveBeenCalledWith({ isInsert: false });
        expect(instance._handleGetItemDetail).toHaveBeenCalledWith(25);
    });

    it('componentDidUpdate - should not call setState and _handleGetItemDetail', () => {
        const props = Object.assign({}, defaultProps, { getOfferPageDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetItemDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).not.toHaveBeenCalled();
        expect(instance._handleGetItemDetail).not.toHaveBeenCalled();
    });

    it('_handleGetItemDetail - should call getOfferPageDetail and setState', async () => {
        const selectedId = 25;
        const props = Object.assign({}, defaultProps, { getOfferPageDetail: jest.fn(() => offerPageInfo) });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        await instance._handleGetItemDetail(selectedId);

        expect(props.getOfferPageDetail).toHaveBeenCalledWith(selectedId);
        expect(instance.setState).toHaveBeenCalledWith({
            fileName: "banner.html",
            fileSize: 7344,
            fromDate: new Date(offerPageInfo.EffectiveFromDate),
            language: "English",
            toDate: new Date(offerPageInfo.EffectiveToDate)
        });
    });

    it('_handleChange - should call setState', () => {
        const event = {
            target: {
                name: 'language',
                value: 'English'
            }
        };

        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        result.setState = jest.fn();

        result.instance()._handleChange(event);

        expect(result.setState).toHaveBeenCalledWith({ language: 'English' });
    });

    it('_handleChangeDatePicker - should call setState', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleChangeDatePicker("toDate", '24/3/2021');

        expect(instance.setState).toHaveBeenCalledWith({ toDate: '24/3/2021' });
    });

    it('_handFileSelect - should call setState', () => {
        const selectedFile = { name: "test.html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handFileSelect({ target: { files: [selectedFile] }});

        expect(instance.setState).toHaveBeenCalledWith({ selectedFile, fileName: "test.html" });
    });

    it('_validateInputs - should return true', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ selectedFile, fileName: "test.html", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') });

        const value = instance._validateInputs();

        expect(value).toBeTruthy();
    });

    it('_validateFile - should return true', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeTruthy();
    });

    it('_validateFile - should return false when invalid size', () => {
        const selectedFile = { name: "test.html", size: 4 * 1024 * 1024, type: "text/html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Max file size is 3MB, please check file again!");
    });

    it('_validateFile - should return false when invalid type', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/javascript" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select html file!");
    });

    it('_validateFile - should return false when insert but file is null', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(null);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select file to upload!");
    });

    it('_validateFileName - should return true', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateFileName("test.html");

        expect(value).toBeTruthy();
    });

    it('_validateFileName - should return false when invalid name length', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("test123456789123456789123456789123456789123456789.html");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "File name too long. Max length is 50!");
    });

    it('_validateFileName - should return false when invalid characters', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("test*%$.html");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "File name must not contain \\ / : * ? $ \" < > | characters and extension must be html.");
    });

    it('_validateFileName - should return false when file name is empty', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please enter file name!");
    });

    it('_insertNewItem - should call insertOfferPage and _handleAfterUpload', async () => {
        const param = { name: "text.html" };
        const props = Object.assign({}, defaultProps, { insertOfferPage: jest.fn(() => null) })
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = result.instance();
        instance._handleAfterUpload = jest.fn();

        await instance._insertNewItem(param);

        expect(props.insertOfferPage).toHaveBeenCalledWith(param);
        expect(instance._handleAfterUpload).toHaveBeenCalledWith(null);
    });

    it('_updateItem - should call updateOfferPage and _handleAfterUpload', async () => {
        const param = { name: "text.html" };
        const props = Object.assign({}, defaultProps, { updateOfferPage: jest.fn(() => null) })
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = result.instance();
        instance._handleAfterUpload = jest.fn();

        await instance._updateItem(param);

        expect(props.updateOfferPage).toHaveBeenCalledWith(param);
        expect(instance._handleAfterUpload).toHaveBeenCalledWith(null);
    });

    it('_save - should call _insertNewItem', async () => {
        const param = { name: "text.html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._insertNewItem = jest.fn();

        await instance._save(param);

        expect(instance._insertNewItem).toHaveBeenCalledWith(param);
    });

    it('_save - should call _updateItem', async () => {
        const param = { name: "text.html" };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isInsert: false });
        instance._updateItem = jest.fn();

        await instance._save(param);

        expect(instance._updateItem).toHaveBeenCalledWith(param);
    });

    it('_getParam - should return correct param', () => {
        const formatISO = "yyyy-MM-ddTHH:mm:ss.SSS";
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const state = { selectedFile, fileName: "test.html", fileSize: 2048, language: "en-US", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState(state);

        const value = instance._getParam(param);

        expect(value).toEqual({ pageId: 0, file: selectedFile, fileName: state.fileName, fileSize: state.fileSize, effectiveFrom: formatDateTime(state.fromDate, formatISO), effectiveTo: formatDateTime(state.toDate, formatISO), language: state.language });
    });

    it('_handleAfterUpload - should call _handleShowAlert when insert successfully', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: true, Message: '' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "The offer page has been added successfully!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when update successfully', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isInsert: false });
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: true, Message: '' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "The offer page has been updated successfully!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when upload errored', () => {
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: false, Message: 'errored!' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "errored!");
    });

    it('_handleSubmit - should call _getParam and _save', () => {
        const offerInfo = { pageId: 25 };
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = result.instance();
        instance.setState({ selectedFile, fileName: "test.html", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') });
        instance._getParam = jest.fn(() => offerInfo);
        instance._save = jest.fn();

        instance._handleSubmit();

        expect(instance._getParam).toHaveBeenCalled();
        expect(instance._save).toHaveBeenCalledWith(offerInfo);
    });

    it('_handleSubmit - should not call _getParam and _save', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = result.instance();
        instance.setState({ selectedFile, fileName: "test.html", fromDate: new Date('4/28/2021'), toDate: new Date('4/1/2021') });
        instance._getParam = jest.fn();
        instance._save = jest.fn();

        instance._handleSubmit();

        expect(instance._getParam).not.toHaveBeenCalled();
        expect(instance._save).not.toHaveBeenCalled();
    });

    it('_handleKeyPress - should call _handleSubmit', () => {
        const event = { charCode: 13, preventDefault: jest.fn() };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSubmit).toHaveBeenCalled();
    });

    it('_handleKeyPress - should not call _handleSubmit', () => {
        const event = { charCode: 123, preventDefault: jest.fn() };
        const result = shallow(<InsertUpdateFileHtmlForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSubmit).not.toHaveBeenCalled();
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<InsertUpdateFileHtmlForm {...props} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, val) => fn && fn());

        const instance = wrapper.instance();
        instance._hideMessage = jest.fn();

        instance._handleShowAlert(false, 'Updated failed!');

        expect(wrapper.setState.mock.calls[0]).toEqual([{ isShowMessage: true, isSuccess: false, message: 'Updated failed!' }, expect.any(Function)]);
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(instance._hideMessage).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState, fetchData and fetchData', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<InsertUpdateFileHtmlForm {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(true);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).toHaveBeenCalledWith(param);
        expect(instance._handleClosePopup).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState and not call fetchData', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<InsertUpdateFileHtmlForm {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(false);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(instance._handleClosePopup).not.toHaveBeenCalled();
    });

    it('_handleClosePopup - should call setSelectedId and setState', async () => {
        const props = Object.assign({}, defaultProps, { setSelectedId: jest.fn() });
        const wrapper = shallow(<InsertUpdateFileHtmlForm {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance._handleClosePopup();

        expect(props.setSelectedId).toHaveBeenCalledWith(null);
        expect(instance.setState).toHaveBeenCalledWith({ ...defaultState });
    });
})