import React from 'react';
import { ContactNumber } from '../ContactNumber';
import { shallow } from 'enzyme';
import { messages } from '../../../libs/constants';

describe('left panel - components - general-info - ContactNumber', () => {
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
        PreferredContactNumber: "vi-VN",
        SiteUrl: "https://aff-12bet.nexdev.net/12BetUserAsianNew/UserPanel/Main/#MyAccount",
        Status: "Approved"
    };

    const defaultProps = {
        affiliateInfo,
        isStatic: false,
        affName: '12BETAFF226'
    }

    it('render correctly', () => {
        const result = shallow(<ContactNumber {...defaultProps} />);
        result.setState({ messageNotification: 'message' });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with isStatic is true', () => {
        const props = Object.assign({}, defaultProps, { isStatic: true });
        const result = shallow(<ContactNumber {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleOnClick - should call setState when click button', () => {
        const wrapper = shallow(<ContactNumber {...defaultProps} />);
        wrapper.setState = jest.fn();

        wrapper.find('Button').simulate('click', true);

        expect(wrapper.setState).toHaveBeenCalledWith({ isOpen: true });
    });

    it('handleCloseNotification - should call setState', async () => {
        const wrapper = shallow(<ContactNumber {...defaultProps} />);
        wrapper.setState = jest.fn();
        const instance = wrapper.instance();

        await instance._handleCloseNotification();

        expect(wrapper.setState).toHaveBeenCalledWith({ notificationMessage: '' });
    });

    it('_toogle - should call setState', async () => {
        const wrapper = shallow(<ContactNumber {...defaultProps} />);
        wrapper.setState = jest.fn(fn => fn && fn({ isOpen: true }));

        await wrapper.instance()._toogle();

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });


    it('_handleSubmit - when submit successfully', async () => {
        const expectedResult = { IsSuccessful: true, Message: '' };
        const props = Object.assign({}, defaultProps, { toggleLoaderLeftPanel: jest.fn(), updateContactNumber: jest.fn((obj) => expectedResult), getAffiliateInfo: jest.fn() });
        const wrapper = shallow(<ContactNumber {...props} />);
        const instance = wrapper.instance();

        wrapper.setState({ isChangingContactNumber: true });
        wrapper.setState = jest.fn();

        await instance._handleSubmit(123);

        expect(props.getAffiliateInfo).toHaveBeenCalledWith('12BETAFF226');
        expect(wrapper.setState).toHaveBeenCalledWith({ notificationMessage: messages.contactNumberUpdatedSuccess });
    });

    it('_handleSubmit - when submit failed', async () => {
        const expectedResult = { IsSuccessful: false, Message: 'failed' };
        const props = Object.assign({}, defaultProps, { toggleLoaderLeftPanel: jest.fn(), updateContactNumber: jest.fn((obj) => expectedResult), getAffiliateInfo: jest.fn() });
        const wrapper = shallow(<ContactNumber {...props} />);
        const instance = wrapper.instance();

        wrapper.setState({ isChangingContactNumber: true });
        wrapper.setState = jest.fn();

        await instance._handleSubmit(123);

        expect(props.getAffiliateInfo).not.toHaveBeenCalled();
        expect(wrapper.setState).toHaveBeenCalledWith({ notificationMessage: expectedResult.Message });
    });
});