import React from 'react';
import { InsertForm } from '../InsertForm';
import { shallow } from 'enzyme';
import DatePicker from 'react-datepicker';
import { formatDateTime } from '../../../../../modules/utils';
import { defaultState } from '../../libs/constants';

describe('offer aff - components - InsertForm ', () => {
    const param = {
        Affiliate: '',
        HtmlFile: '',
        pageIndex: 1,
        pageSize: 20
    };

    const offerPageList = [
        { PageId: 1, FileName: "English.html" },
        { PageId: 2, FileName: "Vietnamese.html" }
    ]

    const defaultProps = {
        selectedId: 0,
        param,
        offerPageList
    };

    it('render correctly', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        result.instance().setState({
            fromDate: new Date(2021, 8, 1), lastDate: new Date(2021, 9, 0)
        });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with isSuccess false', () => {
        const props = Object.assign({}, defaultProps, { selectedId: null });
        const result = shallow(<InsertForm {...props} />);
        result.setState({ isSuccess: false, fromDate: new Date(2021, 8, 1), lastDate: new Date(2021, 9, 0) })

        expect(result).toMatchSnapshot();
    });

    it('_handleChange - should call setState', () => {
        const event = {
            target: {
                name: 'language',
                value: 'English'
            }
        };

        const result = shallow(<InsertForm {...defaultProps} />);
        result.setState = jest.fn();

        result.instance()._handleChange(event);

        expect(result.setState).toHaveBeenCalledWith({ language: 'English' });
    });

    it('_handleChangeAffNames - should call setState', () => {
        const event = {
            preventDefault: jest.fn(),
            target: {
                value: 'Aff001'
            }
        };

        const result = shallow(<InsertForm {...defaultProps} />);
        result.setState = jest.fn();

        result.instance()._handleChangeAffNames(event);

        expect(result.setState).toHaveBeenCalledWith({ affNames: 'Aff001' });
    });

    it('_handleChangeAffNames - should not call setState', () => {
        const event = {
            preventDefault: jest.fn(),
            target: {
                value: 'Aff001$%^*'
            }
        };

        const result = shallow(<InsertForm {...defaultProps} />);
        result.setState = jest.fn();

        result.instance()._handleChangeAffNames(event);

        expect(result.setState).not.toHaveBeenCalled();
    });

    it('_handleBlurAffNames - should call setState with correct affNames', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ affNames: 'AFF001,,,   AFF002,,,,,,,'});

        instance._handleBlurAffNames();

        expect(result.state('affNames')).toEqual('AFF001,AFF002');
    });

    it('_validateInputs - should return true', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance.setState({ affNames: 'AFF001', offerPage: 1 });

        const value = instance._validateInputs();

        expect(value).toBeTruthy();
    });

    it('_validateAffNames - should return true', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateAffNames('AFF001');

        expect(value).toBeTruthy();
    });

    it('_validateAffNames - should return false when affNames is empty', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateAffNames('');

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please enter Affiliate(s)!");
    });

    it('_validateAffNames - should return false when affNames contains special characters', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateAffNames('AFF001$%^#');

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Affiliate(s) must be letters and digits, separated by [Comma], [Tab] or [Enter]");
    });

    it('_validateAffNames - should return false affNames contains over 500 affs', () => {
        let affNames = '';
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        for (let i = 1; i <= 501; i++) {
            affNames = affNames.concat(',')
        }

        const value = instance._validateAffNames(affNames);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "The function can only create assignment for up to 500 affiliates per request.");
    });

    it('_validateOfferPage - should return true', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();

        const value = instance._validateOfferPage(1);

        expect(value).toBeTruthy();
    });

    it('_validateOfferPage - should return false when not select Offer Page', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        const value = instance._validateOfferPage(0);

        expect(value).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please select Offer Page HTML File!");
    });

    it('_save - should call insertOfferAff and _handleAfterUpload', async () => {
        const param = { affNameList: "Aff001", pageId: 1 };
        const props = Object.assign({}, defaultProps, { insertOfferAff: jest.fn(() => null), toggleLoader: jest.fn() })
        const result = shallow(<InsertForm {...props} />);
        const instance = result.instance();
        instance._handleAfterUpload = jest.fn();

        await instance._save(param);

        expect(props.insertOfferAff).toHaveBeenCalledWith(param);
        expect(instance._handleAfterUpload).toHaveBeenCalledWith(null);
    });

    it('_handleAfterUpload - should call _handleShowAlert when insert successfully', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: true, Message: '' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "The offer page has been assigned to the selected affiliate(s)");
    });

    it('_handleAfterUpload - should call _handleShowAlert when insert errored', () => {
        const result = shallow(<InsertForm {...defaultProps} />);
        const instance = result.instance();
        instance._handleShowAlert = jest.fn();

        instance._handleAfterUpload({ IsSuccessful: false, Message: 'errored!' });

        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "errored!");
    });

    it('_handleSubmit - should call _getParam and _save', () => {
        const expectParam = { affNameList: "Aff001", pageId: 1 };
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertForm {...props} />);
        const instance = result.instance();
        instance.setState({ affNames: "Aff001", offerPage: 1 });
        instance._save = jest.fn();

        instance._handleSubmit();

        expect(instance._save).toHaveBeenCalledWith(expectParam);
    });

    it('_handleSubmit - should not call _getParam and _save', () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const result = shallow(<InsertForm {...props} />);
        const instance = result.instance();
        instance._save = jest.fn();

        instance._handleSubmit();

        expect(instance._save).not.toHaveBeenCalled();
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn() });
        const wrapper = shallow(<InsertForm {...props} />);
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
        const wrapper = shallow(<InsertForm {...props} />);
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
        const wrapper = shallow(<InsertForm {...props} />);
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
        const wrapper = shallow(<InsertForm {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance._handleClosePopup();

        expect(props.setSelectedId).toHaveBeenCalledWith(null);
        expect(instance.setState).toHaveBeenCalledWith({ ...defaultState });
    });
})