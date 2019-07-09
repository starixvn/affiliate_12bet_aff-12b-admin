import React from 'react';
import { Language } from '../Language';
import { shallow } from 'enzyme';

describe('left panel - components - general-info - Language', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const affiliateInfo = {
        AffId: 1271808,
        AffLink: "http://localhost:55690/Auth/AdminLogin?chalengeStr=esh5u7&accKey=E16156C28&custName=12BETAFF226&accId=1000000026&accOnId=8405",
        AffName: "12BETAFF226",
        AffType: 2,
        BalanceBf: 9802280.5446,
        CommGroupName: "NoCommission",
        ContactApps: '[{"Name":"MSN/YM/QQ","Value":"@123","Order":0}]',
        FirstName: "bb",
        Gender: true,
        GroupId: 102,
        IsClosed: false,
        LastLoginDateText: "04/11/2019 22:24:00",
        LastLoginIp: "10.18.200.125",
        LastName: "12BETAFF226",
        MoreSiteUrl: "www.sportsbookie.com;www.gzyyy.com;www.123.com",
        NoSiteDescription: "",
        Phone: "abc",
        PreferredLanguage: "vi-VN",
        SiteUrl: "https://ha.nexdev.net:7613/12BetUserAsianNew/UserPanel/Main/#MyAccount",
        Status: "Approved"
    };

    const languageList = [
        {
            LanguageCode: "en-US",
            LanguageName: "English"
        },
        {
            LanguageCode: "vi-VN",
            LanguageName: "Vietnamese"
        }
    ]

    const defaultProps = {
        affiliateInfo,
        languageList,
        affName: '12BETAFF226'
    }

    it('render correctly with isChangingLanguage is false', () => {
        const result = shallow(<Language {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with isChangingLanguage is true', () => {
        const result = shallow(<Language {...defaultProps} />);

        result.setState({ isChangingLanguage: true });

        expect(result).toMatchSnapshot();
    });

    it('toggleChangingLanguage - should call setState when click button', () => {
        const wrapper = shallow(<Language {...defaultProps} />);
        wrapper.setState = jest.fn((val, fn) => fn());
        wrapper.instance().languageDropdown.current = jest.fn();
        wrapper.instance().languageDropdown.current.focus = jest.fn();

        wrapper.find('Button').simulate('click', true);

        expect(wrapper.setState).toHaveBeenCalledWith({ isChangingLanguage: true }, expect.any(Function));
    });

    it('toggleChangingLanguage - should call setState when onBlur select', () => {
        const wrapper = shallow(<Language {...defaultProps} />);
        wrapper.setState({ isChangingLanguage: true });
        wrapper.setState = jest.fn();

        wrapper.find('select').simulate('blur', false);

        expect(wrapper.setState).toHaveBeenCalledWith({ isChangingLanguage: false }, expect.any(Function));
    });

    it('handleChangeLanguage - should call window.confirm when change language select', () => {
        const wrapper = shallow(<Language {...defaultProps} />);
        wrapper.setState({ isChangingLanguage: true });
        window.confirm = jest.fn(() => false);

        wrapper.find('select').simulate('change');

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to change language for this affiliate?");
    });

    it('handleChangeLanguage - should call window.confirm, updateLanguage and window.alert when change language select', async () => {
        const event = {
            target: {
                value: 'en-US'
            }
        };
        const data = { IsSuccessful: false, Message: 'Change language is failed!' };
        const updateLanguage = jest.fn(() => data);

        const props = Object.assign({}, defaultProps, { updateLanguage });
        const wrapper = shallow(<Language {...props} />);
        wrapper.setState({ isChangingLanguage: true });
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();

        await wrapper.find('select').simulate('change', event);

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to change language for this affiliate?");
        expect(props.updateLanguage).toHaveBeenCalledWith({ AffId: 1271808, PreferredLanguage: "en-US" });
        expect(alert).toHaveBeenCalledWith('Change language is failed!');
    });

    it('handleChangeLanguage - should call window.confirm, updateLanguage, window.alert and getAffiliateInfo when change language select', async () => {
        const event = {
            target: {
                value: 'en-US'
            }
        };
        const data = { IsSuccessful: true, Message: '' };
        const updateLanguage = jest.fn(() => data);
        const getAffiliateInfo = jest.fn();
        const props = Object.assign({}, defaultProps, { updateLanguage, getAffiliateInfo });
        const wrapper = shallow(<Language {...props} />);
        wrapper.setState({ isChangingLanguage: true });
        window.confirm = jest.fn(() => true);
        window.alert = jest.fn();

        await wrapper.find('select').simulate('change', event);

        expect(window.confirm).toHaveBeenCalledWith("Are you sure you want to change language for this affiliate?");
        expect(props.updateLanguage).toHaveBeenCalledWith({ AffId: 1271808, PreferredLanguage: "en-US" });
        expect(alert).toHaveBeenCalledWith('The Language has been updated successfully!');
        expect(props.getAffiliateInfo).toHaveBeenCalledWith('12BETAFF226');
    });
});