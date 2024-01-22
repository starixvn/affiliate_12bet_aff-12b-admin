import React from 'react'
import { shallow } from 'enzyme'
import App from '../index'

describe('media gallery - banner list - index', () => {

    it('Should render correctly', () => {
        window.initialState = {
            categoryList: [{ "CategoryName": "Sportsbook", "SubCategoryName": "" }, { "CategoryName": "Casino", "SubCategoryName": "" }, { "CategoryName": "Number Game", "SubCategoryName": "" }, { "CategoryName": "Racebook", "SubCategoryName": "" }],
            languageList: [{ "LanguageCode": "en-GB", "LanguageName": "English (Europe)" }, { "LanguageCode": "en-US", "LanguageName": "English" }, { "LanguageCode": "hi-IN", "LanguageName": "English (India)" }, { "LanguageCode": "id-ID", "LanguageName": "Indonesian" }, { "LanguageCode": "ko-KR", "LanguageName": "Korean" }, { "LanguageCode": "th-TH", "LanguageName": "Thai" }, { "LanguageCode": "vi-VN", "LanguageName": "Vietnamese" }, { "LanguageCode": "zh-CN", "LanguageName": "Chinese Simplified" }, { "LanguageCode": "zh-TW", "LanguageName": "Chinese Traditional" }],
            bannerRootUrl: "https://aff-12bet.nexdev.net/12BetBanner",
            sizeList: [{ "Id": 1, "Name": "100x100" }, { "Id": 2, "Name": "140x55" }, { "Id": 3, "Name": "399x399" }, { "Id": 4, "Name": "480x50" }, { "Id": 5, "Name": "721x529" }, { "Id": 6, "Name": "960x1280" }, { "Id": 7, "Name": "1140x386" }, { "Id": 8, "Name": "2339x1421" }, { "Id": 9, "Name": "3040x1342" }],
            affTypeList: [{ "AffTypeId": 2, "AffTypeName": "Asian" }, { "AffTypeId": 3, "AffTypeName": "Media" }, { "AffTypeId": 4, "AffTypeName": "European" }],
            paramFromHome: { "ImageType": null, "IsFromHome": false }
        }

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
        }

        const wrapper = shallow(<App />);

        expect(wrapper).toMatchSnapshot();
    })
})