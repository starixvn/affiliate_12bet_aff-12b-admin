import React from 'react';
import { DomainPopup } from '../DomainPopup';
import { shallow } from 'enzyme';

describe('domain - components - DomainPopup ', () => {
    const emptyDomain = {
        domainId: 0,
        domainName: '',
        excludedTargetCountries: [],
        isSSL: false
    };

    const selectedDomain = {
        domainId: 123,
        domainName: 'abc.com',
        excludedTargetCountries: ['en-US'],
        isSSL: true
    };

    const targetCountryList = [
        {
            TargetCountryCode: 'en-US',
            TargetCountryName: 'English',
            Status: 0
        },
        {
            TargetCountryCode: 'th-TH',
            TargetCountryName: 'Thailand',
            Status: 1
        },
        {
            TargetCountryCode: 'zh-CN',
            TargetCountryName: 'Chinese',
            Status: 2
        }
    ]

    const defaultProps = {
        selectedDomainId: 123,
        isAffDomain: true,
        getTargetCountryListByDomain: jest.fn(() => targetCountryList),
        getDomainById: jest.fn(() => selectedDomain)
    };

    it('render correctly with insert', () => {
        const props = Object.assign({}, defaultProps, { selectedDomainId: 0 });
        const result = shallow(<DomainPopup {...props} />);
        result.setState({ selectedDomain: emptyDomain });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with update', () => {
        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ isSuccess: false, selectedDomain, targetCountryList, usingTargetCountries: ["en-US"] });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with not show popup', () => {
        const props = Object.assign({}, defaultProps);
        const result = shallow(<DomainPopup {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate should call _handleUpdateState', () => {
        const props = Object.assign({}, defaultProps, { selectedDomainId: 234 });
        const result = shallow(<DomainPopup {...props} />);

        const instance = result.instance();
        instance._handleUpdateState = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance._handleUpdateState).toHaveBeenCalledWith(234, true);
    });

    it('_handleUpdateState - with insert - should call getTargetCountryListByDomain and setState', async () => {
        const targetCountryByDomain = [{ TargetCountryCode: 'en-US', TargetCountryName: "English" }];
        const props = Object.assign({}, defaultProps, { getTargetCountryListByDomain: jest.fn(() => targetCountryByDomain), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(0, false);

        expect(props.getTargetCountryListByDomain).toHaveBeenCalledWith({ "affDomainId": 0, "redirectDomainId": 0 });
        expect(instance.setState).toHaveBeenCalledWith({ "isInsert": true, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English" }], "selectedDomain": { "excludedTargetCountries": [], "domainId": 0, "domainName": "", "isSSL": false } });
    });

    it('_handleUpdateState - with insert - should call getTargetCountryListByDomain and setState with empty target country', async () => {
        const props = Object.assign({}, defaultProps, { getTargetCountryListByDomain: jest.fn(), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(0, false);

        expect(props.getTargetCountryListByDomain).toHaveBeenCalledWith({ "affDomainId": 0, "redirectDomainId": 0 });
        expect(instance.setState).toHaveBeenCalledWith({ "isInsert": true, "targetCountryList": undefined, "selectedDomain": { "excludedTargetCountries": [], "domainId": 0, "domainName": "", "isSSL": false } });
    });

    it('_handleUpdateState - with update - should call toggleLoader, getDomainById, getTargetCountryListByDomain and setState', async () => {
        const domain = { DomainId: 123, DomainName: "abc.com", IsSSL: true };
        const targetCountryByDomain = [{ TargetCountryCode: 'en-US', TargetCountryName: "English", Status: 0 }, { TargetCountryCode: 'th-TH', TargetCountryName: "Thailand", Status: 2 }];
        const props = Object.assign({}, defaultProps, { getTargetCountryListByDomain: jest.fn(() => targetCountryByDomain), getDomainById: jest.fn(() => domain), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(123, true);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.getDomainById).toHaveBeenCalledWith(123, true);
        expect(props.getTargetCountryListByDomain).toHaveBeenCalledWith({ "affDomainId": 123 });
        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": false, "isInsert": false, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 0 }, { TargetCountryCode: 'th-TH', TargetCountryName: "Thailand", Status: 2 }], "selectedDomain": { "excludedTargetCountries": [], "domainId": 123, "domainName": "abc.com", "isSSL": true }, "usingTargetCountries": ["th-TH"] });
    });

    it('_handleUpdateState - with update - should call toggleLoader, getDomainById, getTargetCountryListByDomain and setState with has blocked target country', async () => {
        const domain = { DomainId: 123, DomainName: "abc.com", IsSSL: true };
        const targetCountryByDomain = [{ TargetCountryCode: 'en-US', TargetCountryName: "English", Status: 1 }];
        const props = Object.assign({}, defaultProps, { getTargetCountryListByDomain: jest.fn(() => targetCountryByDomain), getDomainById: jest.fn(() => domain), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(123, true);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.getDomainById).toHaveBeenCalledWith(123, true);
        expect(props.getTargetCountryListByDomain).toHaveBeenCalledWith({ "affDomainId": 123 });
        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": true, "isInsert": false, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 1 }], "selectedDomain": { "excludedTargetCountries": ["en-US"], "domainId": 123, "domainName": "abc.com", "isSSL": true }, "usingTargetCountries": [] });
    });

    it('_handleUpdateState - with update - should call toggleLoader, getDomainById, getTargetCountryListByDomain and setState with empty target country', async () => {
        const domain = { DomainId: 123, DomainName: "abc.com", IsSSL: true };
        const props = Object.assign({}, defaultProps, { getTargetCountryListByDomain: jest.fn(), getDomainById: jest.fn(() => domain), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(123, false);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.getDomainById).toHaveBeenCalledWith(123, false);
        expect(props.getTargetCountryListByDomain).toHaveBeenCalledWith({ "redirectDomainId": 123 });
        expect(instance.setState).toHaveBeenCalledWith({ "isInsert": false, "selectedDomain": { "excludedTargetCountries": "", "domainId": 123, "domainName": "abc.com", "isSSL": true } });
    });

    it('_handleUpdateState - with update - should only call toggleLoader, getDomainById', async () => {
        const props = Object.assign({}, defaultProps, { getDomainById: jest.fn(), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance._handleUpdateState(123, false);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(props.getDomainById).toHaveBeenCalledWith(123, false);
        expect(instance.setState).not.toHaveBeenCalled();
    });

    it('_handleChange should be called when change domain name', () => {
        const event = {
            target: {
                type: 'text',
                name: 'domainName',
                value: '123'
            }
        };
        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain });
        result.setState = jest.fn();

        result.find('Input[type="text"][name="domainName"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ selectedDomain: { "domainId": 123, "domainName": "123", excludedTargetCountries: ['en-US'], "isSSL": true } });
    });

    it('_handleChange should be called when change checkbox', () => {
        const event = {
            target: {
                type: 'checkbox',
                name: 'isSSL',
                checked: false
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain });
        result.setState = jest.fn();

        result.find('input[type="checkbox"][name="isSSL"]').simulate('change', event);

        expect(result.setState).toHaveBeenCalledWith({ selectedDomain: { "domainId": 123, "domainName": "abc.com", excludedTargetCountries: ['en-US'], "isSSL": false } });
    });

    it('_handleChangeTargetCountry - should be called when change target country checkbox', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: true
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList });
        result.instance()._handleChangeTargetCountry = jest.fn();
        result.find('input[type="checkbox"][id="en-US"]').simulate('change', event);

        expect(result.instance()._handleChangeTargetCountry).toHaveBeenCalledWith({ "target": { "checked": true, "type": "checkbox" } }, "en-US");
    });

    it('_handleChangeTargetCountry - should not call setState', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: true
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList, usingTargetCountries: ["en-US"] });
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeTargetCountry(event, "en-US");

        expect(instance.setState).not.toHaveBeenCalled();
    });

    it('_handleChangeTargetCountry - should call setState', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: true
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList, usingTargetCountries: "en-US" });
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeTargetCountry(event, "th-TH");

        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": false, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 0 }, { "TargetCountryCode": "th-TH", "TargetCountryName": "Thailand", "Status": 1 }, { "TargetCountryCode": "zh-CN", "TargetCountryName": "Chinese", "Status": 2 }], "selectedDomain": { "excludedTargetCountries": ["th-TH"], "domainId": 123, "domainName": "abc.com", "isSSL": true } });
    });

    it('_handleChangeTargetCountry - should call setState with uncheck', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: false
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList, usingTargetCountries: ["en-US"] });
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeTargetCountry(event, "th-TH");

        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": false, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 0 }, { "TargetCountryCode": "th-TH", "TargetCountryName": "Thailand", "Status": 0 }, { "TargetCountryCode": "zh-CN", "TargetCountryName": "Chinese", "Status": 2 }], "selectedDomain": { "excludedTargetCountries": [], "domainId": 123, "domainName": "abc.com", "isSSL": true } });
    });

    it('_handleChangeAllTargetCountries - should not call setState with not have target countrys', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: false
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeAllTargetCountries(event);

        expect(instance.setState).not.toHaveBeenCalled();
    });

    it('_handleChangeAllTargetCountries - should call setState', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: true
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList });
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeAllTargetCountries(event);

        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": true, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 1 }, { "TargetCountryCode": "th-TH", "TargetCountryName": "Thailand", "Status": 1 }, { "TargetCountryCode": "zh-CN", "TargetCountryName": "Chinese", "Status": 2 }], "selectedDomain": { "excludedTargetCountries": ["en-US", "th-TH"], "domainId": 123, "domainName": "abc.com", "isSSL": true } });
    });

    it('_handleChangeAllTargetCountries - should call setState with uncheck', () => {
        const event = {
            target: {
                type: 'checkbox',
                checked: false
            }
        };

        const result = shallow(<DomainPopup {...defaultProps} />);
        result.setState({ selectedDomain, targetCountryList });
        const instance = result.instance();
        instance.setState = jest.fn();
        instance._handleChangeAllTargetCountries(event);

        expect(instance.setState).toHaveBeenCalledWith({ "isBlockAllTargetCountries": false, "targetCountryList": [{ "TargetCountryCode": "en-US", "TargetCountryName": "English", "Status": 0 }, { "TargetCountryCode": "th-TH", "TargetCountryName": "Thailand", "Status": 0 }, { "TargetCountryCode": "zh-CN", "TargetCountryName": "Chinese", "Status": 2 }], "selectedDomain": { "excludedTargetCountries": [], "domainId": 123, "domainName": "abc.com", "isSSL": true } });
    });

    it('_handleSubmit - should be called toggleLoader and _handleShowAlert with domain is empty', async () => {
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), setSelectedDomainId: emptyDomain });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState({ selectedDomain: emptyDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Please input domain name!");
    });

    it('_handleSubmit - should be called toggleLoader, insertDomain and _handleShowAlert with with insert successfully', async () => {
        const result = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { insertDomain: jest.fn(() => result), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Add Successfully!");
    });

    it('_handleSubmit - should be called toggleLoader, insertDomain and _handleShowAlert with with insert is failed', async () => {
        const result = { IsSuccessful: false, Message: "Adding has a error" };
        const props = Object.assign({}, defaultProps, { insertDomain: jest.fn(() => result), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Adding has a error");
    });

    it('_handleSubmit - should be called toggleLoader, insertDomain and _handleShowAlert with with insert is errored', async () => {
        const props = Object.assign({}, defaultProps, { insertDomain: jest.fn(), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Add Domain is failed!");
    });

    it('_handleSubmit - should be called toggleLoader, updateDomain and _handleShowAlert with with update successfully', async () => {
        const result = { IsSuccessful: true, Message: "" };
        const props = Object.assign({}, defaultProps, { updateDomain: jest.fn(() => result), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        wrapper.setState({ isInsert: false });
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Update Successfully!");
    });

    it('_handleSubmit - should be called toggleLoader, updateDomain and _handleShowAlert with with update is failed', async () => {
        const result = { IsSuccessful: false, Message: "Update has a error" };
        const props = Object.assign({}, defaultProps, { updateDomain: jest.fn(() => result), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        wrapper.setState({ isInsert: false });
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Update has a error");
    });

    it('_handleSubmit - should be called toggleLoader, updateDomain and _handleShowAlert with with update is errored', async () => {
        const props = Object.assign({}, defaultProps, { updateDomain: jest.fn(), toggleLoader: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        wrapper.setState({ isInsert: false });
        const instance = wrapper.instance();
        instance.setState({ selectedDomain });
        instance._handleShowAlert = jest.fn();
        instance.setState = jest.fn();

        await instance._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Update Domain is failed!");
    });

    it('_handleShowAlert - should call setState, setTimeout and _hideMessage', async () => {
        const wrapper = shallow(<DomainPopup {...defaultProps} />);
        wrapper.setState = jest.fn((obj, fn) => fn && fn());
        window.setTimeout = jest.fn((fn, val) => fn && fn());

        const instance = wrapper.instance();
        instance._hideMessage = jest.fn();

        instance._handleShowAlert(false, 'Updated failed!');

        expect(wrapper.setState.mock.calls[0]).toEqual([{ isShowMessage: true, isSuccess: false, message: 'Updated failed!' }, expect.any(Function)]);
        expect(window.setTimeout).toHaveBeenCalledWith(expect.any(Function), 3000);
        expect(instance._hideMessage).toHaveBeenCalled();
    });

    it('_hideMessage - should call setState, fetchData and setSelectedDomain', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(true);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).toHaveBeenCalledWith(true);
        expect(instance._handleClosePopup).toHaveBeenCalledWith();
    });

    it('_hideMessage - should call setState and not call fetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const wrapper = shallow(<DomainPopup {...props} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();
        instance._handleClosePopup = jest.fn();

        instance._hideMessage(false);

        expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('_handlePressEnter - should call _handleSubmit when press enter in domain name textbox', () => {
        const event = {
            charCode: 13,
            preventDefault: jest.fn()
        };
        const wrapper = shallow(<DomainPopup {...defaultProps} />);
        wrapper.setState({ selectedDomain });
        wrapper.instance()._handleSubmit = jest.fn();

        wrapper.find('Input[type="text"]').simulate('keyPress', event);

        expect(event.preventDefault).toHaveBeenCalledWith();
        expect(wrapper.instance()._handleSubmit).toHaveBeenCalledWith();
    });

    it('_handlePressEnter - should not call _handleSubmit when press other keys', () => {
        const event = {
            charCode: 16,
            preventDefault: jest.fn()
        };
        const wrapper = shallow(<DomainPopup {...defaultProps} />);
        wrapper.setState({ selectedDomain });
        wrapper.find('Input[type="text"]').simulate('keyPress', event);

        expect(event.preventDefault).not.toHaveBeenCalled();
    });

    it('_handleClosePopup - should call setSelectedDomain', () => {
        const props = Object.assign({}, defaultProps, { setSelectedDomainId: jest.fn() })
        const wrapper = shallow(<DomainPopup {...props} />);

        wrapper.instance()._handleClosePopup();

        expect(props.setSelectedDomainId).toHaveBeenCalledWith(null);
    });
})