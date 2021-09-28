import React from 'react';
import App from '../index';
import { shallow } from 'enzyme';

jest.mock('react-dom', ()=> ({render: jest.fn()}))

describe('affiliate info - App', () => {
    beforeEach(() => {
        window.globalVariables = {
            claims: {
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": ["10000002"],
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname": ["ABC"],
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": ["Management"],
                "[Home]": ["Write"], "[Customer]": ["Write"], "[AffAccountStatus]": ["Write"],
                "[AffPassword]": ["Write"], "[SecurityQA]": ["Write"], "[AffPhone]": ["Write"],
                "[AffEmail]": ["Write"], "[AffMarket]": ["Write"], "[AffPaymentInfo]": ["Write"],
                "[AffWebURL]": ["Write"], "[AffPlayerDetail]": ["Write"], "[AffTurnover]": ["Write"],
                "[AffAdjustment]": ["Write"], "[Note]": ["Write"], "[AssignCommGroup]": ["Write"],
                "[CommissionGroup]": ["Write"], "[AffMemberName]": ["Write"], "[AffReport]": ["Write"],
                "[ExportExcel]": ["Write"], "[Affiliate]": ["Write"], "[Payment]": ["Write"],
                "[AffMember]": ["Write"], "[AffMemberDetail]": ["Write"], "[ActiveMembers]": ["Write"],
                "[Media]": ["Write"], "[HotNews]": ["Write"], "[AffEx.Rate]": ["Write"],
                "[UserControl]": ["Write"], "[ExportAffInfoExcel]": ["Write"],
                "[ExportPlayerDetailExcel]": ["Write"], "[ExportReportExcel]": ["Write"],
                "[AffAffiliate]": ["Write"], "[AffCustomerEdit]": ["Write"], "[EmailHistory]": ["Write"],
                "[AffPersonalInfo]": ["Write"], "[ViewLog]": ["Write"], "[Domain]": ["Write"],
                "[RegisteredMembers]": ["Write"], "[ProcessingRequest]": ["Write"],
                "[ProcessingRequestMarketingRole]": ["Write"], "[ProcessingRequestManagementRole]": ["Write"],
                "[Market]": ["en-GB", "en-US", "hi-IN", "id-ID", "ko-KR", "th-TH", "vi-VN", "zh-CN", "zh-TW"]
            }
        };
    })

    it('Should render correctly', () => {
        window.initialState = { param: {} };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with IsFromHome', () => {
        window.initialState = { param: { IsFromHome: true } };

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    });
})