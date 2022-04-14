import React from 'react';
import { InsertUpdateForm } from '../InsertUpdateForm';
import { shallow } from 'enzyme';
import DatePicker from 'react-datepicker';
import { formatDateTime } from '../../../../../modules/utils';
import { defaultState } from '../../libs/constants';

describe('news letter list - components - InsertUpdateForm ', () => {
    const newsLetterInfo = {
        FileName: "banner.html",
        FileSize: 7344,
        FromDate: "2021-04-01T00:00:00",
        Language: "en-US",
        LanguageName: null,
        NewsLetterId: 25,
        ToDate: "2021-04-30T00:00:00",
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
        selectedNewsLetterPageId: 0,
        param,
        languageList
    };

    it('render correctly with insert', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        result.instance().setState({
            fromDate: new Date(2021, 8, 1), toDate: new Date(2021, 9, 0)
        });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const props = Object.assign({}, defaultProps, { selectedNewsLetterPageId: 2485, getNewsLetterPageDetail: jest.fn() });
        const result = shallow(<InsertUpdateForm {...props} />);
        result.instance().setState({
            isInsert: false, isSuccess: false,
            fromDate: new Date(2021, 8, 1), toDate: new Date(2021, 9, 0)
        });

        expect(result).toMatchSnapshot();
    });

    it('_handleChangeDatePicker - should be called when change from date', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleChangeDatePicker = jest.fn();

        result.find(DatePicker).at(0).props().onChange("1/3/2021");

        expect(instance._handleChangeDatePicker).toHaveBeenCalledWith("fromDate", '1/3/2021');
    });

    it('_handleChangeDatePicker - should be called when change to date', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleChangeDatePicker = jest.fn();

        result.find(DatePicker).at(1).props().onChange("24/3/2021");

        expect(instance._handleChangeDatePicker).toHaveBeenCalledWith("toDate", '24/3/2021');
    });

    it('componentDidUpdate - should call setState and _handleGetNewsletterDetail', () => {
        const props = Object.assign({}, defaultProps, { selectedNewsLetterPageId: 25, getNewsLetterPageDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<InsertUpdateForm {...props} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetNewsletterDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).toHaveBeenCalledWith({ isInsert: false });
        expect(instance._handleGetNewsletterDetail).toHaveBeenCalledWith(25);
    });

    it('componentDidUpdate - should not call setState and _handleGetNewsletterDetail', () => {
        const props = Object.assign({}, defaultProps, { getNewsLetterPageDetail: jest.fn(() => bannerInfo) });
        const result = shallow(<InsertUpdateForm {...props} />);
        result.setState = jest.fn();

        const instance = result.instance();
        instance._handleGetNewsletterDetail = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(result.setState).not.toHaveBeenCalled();
        expect(instance._handleGetNewsletterDetail).not.toHaveBeenCalled();
    });

    it('_handleGetNewsletterDetail - should call getNewsLetterPageDetail and setState', async () => {
        const selectedNewsLetterPageId = 25;
        const props = Object.assign({}, defaultProps, { getNewsLetterPageDetail: jest.fn(() => newsLetterInfo) });
        const result = shallow(<InsertUpdateForm {...props} />);

        result.setState = jest.fn();

        await result.instance()._handleGetNewsletterDetail(selectedNewsLetterPageId);

        expect(props.getNewsLetterPageDetail).toHaveBeenCalledWith(selectedNewsLetterPageId);
        expect(result.setState).toHaveBeenCalledWith({
            fileName: "banner.html",
            fileSize: 7344,
            fromDate: new Date(newsLetterInfo.FromDate),
            language: "en-US",
            toDate: new Date(newsLetterInfo.ToDate)
        });
    });

    it('_handleChange - should call setState', () => {
        const event = {
            target: {
                name: 'language',
                value: 'English'
            }
        };

        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        result.setState = jest.fn();

        result.instance()._handleChange(event);

        expect(result.setState).toHaveBeenCalledWith({ language: 'English' });
    });

    it('_handleChangeDatePicker - should call setState', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handleChangeDatePicker("toDate", '24/3/2021');

        expect(instance.setState).toHaveBeenCalledWith({ toDate: '24/3/2021' });
    });

    it('_handFileSelect - should call setState', () => {
        const selectedFile = { name: "test.html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance._handFileSelect({ target: { files: [selectedFile] }});

        expect(instance.setState).toHaveBeenCalledWith({ selectedFile, fileName: "test.html" });
    });

    it('_validateInputs - should return true', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ selectedFile, fileName: "test.html", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') });

        const value = instance._validateInputs();

        expect(value).toBeTruthy();
    });

    it('_validateFile - should return true', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeTruthy();
    });

    it('_validateFile - should return false when invalid size', () => {
        const selectedFile = { name: "test.html", size: 4 * 1024 * 1024, type: "text/html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Max file size is 3MB, please check file again!");
    });

    it('_validateFile - should return false when invalid type', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/javascript" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(selectedFile);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select html file!");
    });

    it('_validateFile - should return false when insert but file is null', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFile(null);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select file to upload!");
    });

    it('_validateFileName - should return true', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateFileName("test.html");

        expect(value).toBeTruthy();
    });

    it('_validateFileName - should return false when invalid name length', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("test123456789123456789123456789123456789123456789.html");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "File name too long. Max length is 50!");
    });

    it('_validateFileName - should return false when invalid characters', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("test*%$.html");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "File name must not contain \\ / : * ? $ \" < > | characters and extension must be html.");
    });

    it('_validateFileName - should return false when file name is empty', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateFileName("");

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please enter file name!");
    });

    it('_insertNewsLetter - should call insertNewsLetterPage and _handleAfterUpload', async () => {
        const param = { name: "text.html" };
        const props = Object.assign({}, defaultProps, { insertNewsLetterPage: jest.fn(() => null) })
        const result = shallow(<InsertUpdateForm {...props} />);
        const instance = result.instance();
        instance._handleAfterUpload = jest.fn();

        await instance._insertNewsLetter(param);

        expect(props.insertNewsLetterPage).toHaveBeenCalledWith(param);
        expect(instance._handleAfterUpload).toHaveBeenCalledWith(null);
    });

    it('_updateNewsLetter - should call updateNewsLetterPage and _handleAfterUpload', async () => {
        const param = { name: "text.html" };
        const props = Object.assign({}, defaultProps, { updateNewsLetterPage: jest.fn(() => null) })
        const result = shallow(<InsertUpdateForm {...props} />);
        const instance = result.instance();
        instance._handleAfterUpload = jest.fn();

        await instance._updateNewsLetter(param);

        expect(props.updateNewsLetterPage).toHaveBeenCalledWith(param);
        expect(instance._handleAfterUpload).toHaveBeenCalledWith(null);
    });

    it('_save - should call _insertNewsLetter', async () => {
        const param = { name: "text.html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._insertNewsLetter = jest.fn();

        await instance._save(param);

        expect(instance._insertNewsLetter).toHaveBeenCalledWith(param);
    });

    it('_save - should call _updateNewsLetter', async () => {
        const param = { name: "text.html" };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isInsert: false });
        instance._updateNewsLetter = jest.fn();

        await instance._save(param);

        expect(instance._updateNewsLetter).toHaveBeenCalledWith(param);
    });

    it('_getParam - should return correct param', () => {
        const formatISO = "yyyy-MM-ddTHH:mm:ss.SSS";
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const state = { selectedFile, fileName: "test.html", fileSize: 2048, language: "en-US", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState(state);

        const value = instance._getParam(param);

        expect(value).toEqual({ newsLetterId: 0, file: selectedFile, fileName: state.fileName, fileSize: state.fileSize, fromDate: formatDateTime(state.fromDate, formatISO), toDate: formatDateTime(state.toDate, formatISO), language: state.language });
    });

    it('_handleAfterUpload - should call _handleShowAlert when insert successfully', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: true, Message: '' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Your newsletter has been added successfully!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when update successfully', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isInsert: false });
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: true, Message: '' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Your newsletter has been updated successfully!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when upload errored', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: false, Message: 'errored!' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "errored!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when insert failed', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload(null);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Add Failed!");
    });

    it('_handleAfterUpload - should call _handleShowAlert when update failed', () => {
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ isInsert: false });
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload(null);

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Update Failed!");
    });

    it('_handleSubmit - should call _getParam and _save', () => {
        const newsLetterInfo = { newsLetterId: 25 };
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertUpdateForm {...props} />);
        const instance = result.instance();
        instance.setState({ selectedFile, fileName: "test.html", fromDate: new Date('2/28/2021'), toDate: new Date('4/1/2021') });
        instance._getParam = jest.fn(() => newsLetterInfo);
        instance._save = jest.fn();

        instance._handleSubmit();

        expect(instance._getParam).toHaveBeenCalled();
        expect(instance._save).toHaveBeenCalledWith(newsLetterInfo);
    });

    it('_handleSubmit - should not call _getParam and _save', () => {
        const selectedFile = { name: "test.html", size: 2048, type: "text/html" };
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertUpdateForm {...props} />);
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
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSubmit).toHaveBeenCalled();
    });

    it('_handleKeyPress - should not call _handleSubmit', () => {
        const event = { charCode: 123, preventDefault: jest.fn() };
        const result = shallow(<InsertUpdateForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleSubmit = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSubmit).not.toHaveBeenCalled();
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<InsertUpdateForm {...props} />);
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
        const wrapper = shallow(<InsertUpdateForm {...props} />);
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
        const wrapper = shallow(<InsertUpdateForm {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(false);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).not.toHaveBeenCalled();
        expect(instance._handleClosePopup).not.toHaveBeenCalled();
    });

    it('_handleClosePopup - should call setSelectedNewsLetterPageId and setState', async () => {
        const props = Object.assign({}, defaultProps, { setSelectedNewsLetterPageId: jest.fn() });
        const wrapper = shallow(<InsertUpdateForm {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance._handleClosePopup();

        expect(props.setSelectedNewsLetterPageId).toHaveBeenCalledWith(null);
        expect(instance.setState).toHaveBeenCalledWith({ ...defaultState });
    });
})