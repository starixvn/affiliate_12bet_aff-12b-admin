import React from 'react';
import { ContactApp } from '../ContactApp';
import { shallow } from 'enzyme';
import ChangePasswordForm from '../../../../../common/components/ChangePasswordForm';

describe('left panel - components - general-info - ContactApp', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    it('render correctly has props is null', () => {
        const result = shallow(<ContactApp />);

        expect(result).toMatchSnapshot();
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

    it('render correctly with affiliateInfo is not null ', () => {
        const result = shallow(<ContactApp affiliateInfo={affiliateInfo} />);

        expect(result).toMatchSnapshot();
    });
});