import React from 'react';
import { EditRequest } from '../EditRequest/EditRequest';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';
import * as service from '../../libs/service';

describe('processing request - components - EditRequest - ', () => {
    const selectedRequest = {
        AffId: 92034539,
        AgentName: "ss",
        ApprovalDate: null,
        ApprovedBy: null,
        BankAccountName: "",
        BankAccountNo: "",
        BankAddress: "",
        BankName: "",
        BankSwiftCode: "",
        ContactModeTime: "",
        CreatedBy: "AAlcolm",
        CreationDate: "2021-09-13T00:25:54.127",
        Deadline: "2021-09-23T00:00:00",
        LastModified: null,
        MarketId: "",
        MarketName: null,
        Player_UserName: "",
        PreferredPayment: "",
        PromotionMethod: "",
        Remark: "s",
        RequestId: 2049,
        ServerCountry: "",
        Status: "Draft",
        SubmittedDate: null,
        TargetMarket: "",
        UserName: "12BETAFF7451",
        WebAdvAff: "",
        WebRank: "",
        WebRegDate: "",
        WebSMLanguage: "",
        WebSMQuality: "",
        WebSMType: ""
    };

    const originalData = {
        AffId: 92034539,
        AgentName: "ss",
        ApprovalDate: null,
        ApprovedBy: null,
        BankAccountName: "",
        BankAccountNo: "",
        BankAddress: "",
        BankName: "",
        BankSwiftCode: "",
        ContactModeTime: "",
        CreatedBy: "AAlcolm",
        CreationDate: "2021-09-13T00:25:54.127",
        Deadline: "2021-09-24T00:00:00",
        LastModified: null,
        MarketId: "",
        MarketName: null,
        Player_UserName: "",
        PreferredPayment: "",
        PromotionMethod: "",
        Remark: "s",
        RequestId: 2049,
        ServerCountry: "",
        Status: "Draft",
        SubmittedDate: null,
        TargetMarket: "",
        UserName: "12BETAFF7451",
        WebAdvAff: "",
        WebRank: "",
        WebRegDate: "",
        WebSMLanguage: "",
        WebSMQuality: "",
        WebSMType: ""
    }

    const languageList = [
        { LanguageCode: 'vi-VN', LanguageName: 'Vietnamese' },
        { LanguageCode: 'en-GB', LanguageName: 'English (Europe)' }
    ];

    const param = {
        IsNeedInitSearch: false,
        Status: "",
        UserName: ""
    };

    const defaultProps = {
        today: '09/25/2021',
        selectedRequestId: 2049,
        selectedRequest,
        languageList,
        originalData,
        pagingOption: defaultPagingOption,
        param
    };

    it('render correctly', () => {
        const result = shallow(<EditRequest {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly when insert and clicked submit and deadline is required', () => {
        const props = Object.assign({}, defaultProps, { originalData: null, selectedRequest: { ...selectedRequest, Deadline: '', UserName: '' }, selectedRequestId: 0, setSelectedRequest: jest.fn() });
        const result = shallow(<EditRequest {...props} />);
        result.instance().setState({ isSubmit: true, isSuccess: true });

        expect(result).toMatchSnapshot();
    });

    it('render correctly when insert and clicked submit and invalid deadline', () => {
        const props = Object.assign({}, defaultProps, { originalData: { ...originalData, Status: 'Submitted' }, selectedRequest: { ...selectedRequest, UserName: '0123456789012345678901234567890' }, selectedRequestId: 0, setSelectedRequest: jest.fn() });
        const result = shallow(<EditRequest {...props} />);
        result.instance().setState({ isSubmit: true, isSuccess: true });
        service.validateDeadline = jest.fn(() => ({ isValid: true }));

        expect(result).toMatchSnapshot();
    });

    it('renderTextArea - shoud render correctly when required', () => {
        const wrapper = shallow(<EditRequest {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState({ isSubmit: true });
        service.validateDeadline = jest.fn(() => ({ isValid: true }));

        const result = instance.renderTextArea("PromotionMethod", 'Promotion Method', '', true, true)

        expect(result).toMatchSnapshot();
    });

    it('renderTextArea - shoud render correctly when invalid length', () => {
        const wrapper = shallow(<EditRequest {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState({ isSubmit: true });
        instance.validateMaxLength = jest.fn(() => true);

        const result = instance.renderTextArea("PromotionMethod", 'Promotion Method', 'abc', true, true)

        expect(result).toMatchSnapshot();
    });

    it('_renderPaymentForm - shoud render correctly when required', () => {
        const wrapper = shallow(<EditRequest {...defaultProps} />);
        const instance = wrapper.instance();

        const result = instance._renderPaymentForm("Bank", 'Bank', 'test', true, jest.fn())

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call setSelectedRequest', () => {
        const props = Object.assign({}, defaultProps, { selectedRequestId: 0, setSelectedRequest: jest.fn(), selectedRequest: { Status: 'test' } });
        const result = shallow(<EditRequest {...props} />);

        result.instance().componentDidMount(defaultProps);

        expect(props.setSelectedRequest).toHaveBeenCalledWith({ Status: 'Draft' });
    });

    it('componentWillUnmount - should call setSelectedRequest, setSelectedRequestId and setOriginalData', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequest: jest.fn(), setSelectedRequestId: jest.fn(), setOriginalData: jest.fn() });
        const result = shallow(<EditRequest {...props} />);

        result.instance().componentWillUnmount();

        expect(props.setSelectedRequest).toHaveBeenCalledWith(null);
        expect(props.setSelectedRequestId).toHaveBeenCalledWith(null);
        expect(props.setOriginalData).toHaveBeenCalledWith(null);
    });

    it('hideMessage - should not call setState and _handleGetMessageDetail', () => {
        const result = shallow(<EditRequest {...defaultProps} />);

        const instance = result.instance();
        instance.setState = jest.fn();
        instance.refAlert = { current: { scrollIntoView: jest.fn() } };
        window.setTimeout = jest.fn((fn, timeout) => (typeof(fn) === 'function') && fn())

        instance.hideMessage();

        expect(instance.setState).toHaveBeenCalledWith({ "isShowMessage": false });
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
    });

    it('handleChangePaymentMethod - should call setSelectedRequest', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequest: jest.fn(), selectedRequest: { PreferredPayment: 'abc' } });
        const result = shallow(<EditRequest {...props} />);

        result.instance().handleChangePaymentMethod({ target: { value: "Bank Transfer" } });

        expect(props.setSelectedRequest).toHaveBeenCalledWith({ PreferredPayment: 'Bank Transfer' });
    });

    it('handleChange - should call setSelectedRequest', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequest: jest.fn(), selectedRequest: {} });
        const result = shallow(<EditRequest {...props} />);

        result.instance().handleChange({ target: { name: 'PreferredPayment', value: "Bank Transfer" } });

        expect(props.setSelectedRequest).toHaveBeenCalledWith({ PreferredPayment: 'Bank Transfer' });
    });

    it('validateMaxLength - should return true', () => {
        const result = shallow(<EditRequest {...defaultProps} />);

        const isValid = result.instance().validateMaxLength('1234', 3);

        expect(isValid).toBeTruthy();
    });

    it('validateMaxLength - should return false', () => {
        const result = shallow(<EditRequest {...defaultProps} />);

        const isValid = result.instance().validateMaxLength('', 3);

        expect(isValid).toBeFalsy();
    });

    it('validateRequire - should return correct', () => {
        const result = shallow(<EditRequest {...defaultProps} />);

        const isValid = result.instance().validateRequire('abc');

        expect(isValid).toBeFalsy();
    });

    it('_handleSave - should call setState when invalid', async () => {
        const resultValidate = {
            isValid: false,
            messageList: ['abc']
        }
        const props = { ...defaultProps, toggleLoader: jest.fn() }
        const wrapper = shallow(<EditRequest {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn((obj, fn) => (typeof (fn) === 'function') && fn());
        instance.hideMessage = jest.fn();

        service.validate = jest.fn(() => resultValidate);

        await instance.handleSave();

        expect(instance.setState).toHaveBeenLastCalledWith({
            isShowMessage: !resultValidate.isValid,
            isSuccess: resultValidate.isValid,
            message: resultValidate.messageList
        }, expect.any(Function));
        expect(instance.hideMessage).toHaveBeenCalled();
    });

    it('_handleSave - should call insertProcessingRequest when insert successfullly', async () => {
        const resultValidate = {
            isValid: true,
            messageList: []
        }
        const props = {
            ...defaultProps, toggleLoader: jest.fn(), selectedRequestId: 0, insertProcessingRequest: jest.fn(() => ({ IsSuccessful: true })),
            onClose: jest.fn(), fetchData: jest.fn(), onSave: jest.fn(), setSelectedRequest: jest.fn()
        }
        const wrapper = shallow(<EditRequest {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn((obj, fn) => (typeof (fn) === 'function') && fn());
        instance.hideMessage = jest.fn();

        service.validate = jest.fn(() => resultValidate);

        await instance.handleSave();

        expect(instance.setState).toHaveBeenLastCalledWith({
            isShowMessage: true,
            isSuccess: true,
            message: [["The processing request has been saved."]]
        }, expect.any(Function));
        expect(instance.hideMessage).toHaveBeenCalled();
        expect(props.insertProcessingRequest).toHaveBeenCalled();
        expect(props.fetchData).toHaveBeenCalled();
    });

    it('_handleSave - should call updateProcessingRequest when update is failed', async () => {
        const resultValidate = {
            isValid: true,
            messageList: []
        }
        const props = {
            ...defaultProps, toggleLoader: jest.fn(), updateProcessingRequest: jest.fn(() => ({ IsSuccessful: false, Message: 'failed!' })),
            onClose: jest.fn(), fetchData: jest.fn(), onSave: jest.fn()
        }
        const wrapper = shallow(<EditRequest {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn((obj, fn) => (typeof (fn) === 'function') && fn());
        instance.hideMessage = jest.fn();

        service.validate = jest.fn(() => resultValidate);

        await instance.handleSave();

        expect(instance.setState).toHaveBeenLastCalledWith({
            isShowMessage: true,
            isSuccess: false,
            message: ['failed!']
        }, expect.any(Function));
        expect(instance.hideMessage).toHaveBeenCalled();
        expect(props.updateProcessingRequest).toHaveBeenCalled();
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    //it('_handleSave - should call _validateMessageContent and setState', async () => {
    //    const result = {
    //        isValid: true,
    //        message: ''
    //    }
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState({ isClickedEdit: true });
    //    const instance = wrapper.instance();

    //    instance._validateMessageContent = jest.fn(() => result);
    //    instance.setState = jest.fn();

    //    instance._handleSave();

    //    expect(instance._validateMessageContent).toHaveBeenCalled();
    //    expect(instance.setState).toHaveBeenCalledWith({ messageNotification: 'Are you sure you want to update this message?' });
    //});

    //it('_save - insertMessage is errored', async () => {
    //    const props = Object.assign({}, defaultProps, { selectedMessageId: 0, insertMessage: jest.fn() });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Add Failed!");
    //});

    //it('_save - insertMessage is failed', async () => {
    //    const result = { IsSuccessful: false, Message: "failed" };
    //    const props = Object.assign({}, defaultProps, { selectedMessageId: 0, insertMessage: jest.fn(() => result) });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "failed");
    //});

    //it('_save - insertMessage is successful', async () => {
    //    const result = { IsSuccessful: true, Message: "" };
    //    const props = Object.assign({}, defaultProps, { selectedMessageId: 0, insertMessage: jest.fn(() => result) });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Add Successfully!");
    //});

    //it('_save - updateMessage is errored', async () => {
    //    const props = Object.assign({}, defaultProps, { updateMessage: jest.fn() });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Update Failed!");
    //});

    //it('_save - updateMessage is failed', async () => {
    //    const result = { IsSuccessful: false, Message: "failed" };
    //    const props = Object.assign({}, defaultProps, { updateMessage: jest.fn(() => result) });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "failed");
    //});

    //it('_save - updateMessage is successful', async () => {
    //    const result = { IsSuccessful: true, Message: "" };
    //    const props = Object.assign({}, defaultProps, { updateMessage: jest.fn(() => result) });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();

    //    instance._handleShowAlert = jest.fn();
    //    instance.setState = jest.fn();

    //    await instance._save();

    //    expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Update Successfully!");
    //});

    //it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState = jest.fn((obj, fn) => fn && fn());
    //    window.setTimeout = jest.fn((fn, val) => fn && fn());
    //    const instance = wrapper.instance();
    //    instance._hideMessage = jest.fn();
    //    instance.modalRef.current = jest.fn();
    //    instance.modalRef.current.scrollTo = jest.fn();

    //    instance._handleShowAlert(false, 'Updated failed!');

    //    expect(instance.modalRef.current.scrollTo).toHaveBeenCalledWith({ top: 0, behavior: "smooth" });
    //    expect(wrapper.setState.mock.calls[0]).toEqual([{ isShowMessage: true, isSuccess: false, message: 'Updated failed!' }, expect.any(Function)]);
    //    expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
    //    expect(instance._hideMessage).toHaveBeenCalled();
    //});

    //it('_hideMessage - should call setState and _handleFetchData', async () => {
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState = jest.fn();
    //    const instance = wrapper.instance();
    //    instance._handleFetchData = jest.fn();

    //    instance._hideMessage(true);

    //    expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false, confirmResult: false });
    //    expect(instance._handleFetchData).toHaveBeenCalled();
    //});

    //it('_hideMessage - should call setState and not call _handleFetchData', async () => {
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState = jest.fn();
    //    const instance = wrapper.instance();
    //    instance._handleFetchData = jest.fn();

    //    instance._hideMessage(false);

    //    expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false, confirmResult: false });
    //    expect(instance._handleFetchData).not.toHaveBeenCalled();
    //});

    //it('_handleFetchData - should call _handleClosePopup and fetchData', async () => {
    //    const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();
    //    instance._handleClosePopup = jest.fn();

    //    instance._handleFetchData();

    //    expect(props.fetchData).toHaveBeenCalledWith(param);
    //    expect(instance._handleClosePopup).toHaveBeenCalled();
    //});

    //it('_handleCloseNotificationPopup - should call setState and _save', async () => {
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState = jest.fn();
    //    const instance = wrapper.instance();
    //    instance._save = jest.fn();

    //    instance._handleCloseNotificationPopup(true);

    //    expect(wrapper.setState).toHaveBeenCalledWith({ messageNotification: '' });
    //    expect(instance._save).toHaveBeenCalled();
    //});

    //it('_handleCloseNotificationPopup - should call setState and not call _save', async () => {
    //    const wrapper = shallow(<EditRequest {...defaultProps} />);
    //    wrapper.setState = jest.fn();
    //    const instance = wrapper.instance();
    //    instance._save = jest.fn();

    //    instance._handleCloseNotificationPopup(false);

    //    expect(wrapper.setState).toHaveBeenCalledWith({ messageNotification: '' });
    //    expect(instance._save).not.toHaveBeenCalled();
    //});

    //it('_handleClosePopup - should call setSelectedMessageId and setState', async () => {
    //    const defaultMessageContentList = Object.assign({}, messageContentList, { MessageContentEn: '' })
    //    const props = Object.assign({}, defaultProps, { setSelectedMessageId: jest.fn() });
    //    const wrapper = shallow(<EditRequest {...props} />);
    //    const instance = wrapper.instance();
    //    instance.setState = jest.fn();

    //    instance._handleClosePopup();

    //    expect(props.setSelectedMessageId).toHaveBeenCalledWith(null);
    //    expect(instance.setState).toHaveBeenCalledWith({ isClickedEdit: false, important: false, messageContentList: defaultMessageContentList });
    //});
})