import React from 'react';
import { MemberStatistics } from '../MemberStatistics';
import { shallow } from 'enzyme';

describe('home - components - MemberStatistics', () => {
    const defaultProps = {
        generalInfo: {
            AllAffiliateMonthNumber: 0,
            AllAffiliateNumber: 3113,
            AllBonusMonthNumber: 0,
            AllBonusNumber: 509.1923,
            AllDepositMonthNumber: 0,
            AllDepositNumber: 5830693.0114,
            AllMemberDepositMonthNumber: 2516,
            AllMemberDepositNumber: 8847,
            AllNetWinlostMonthNumber: -76.9964,
            AllNetWinlostNumber: 1776017.4515,
            AllPSPFeeMonthNumber: 0,
            AllPSPFeeNumber: -57.124,
            AllWinlostMonthNumber: 58.3306,
            AllWinlostNumber: -3453885.1957,
            ApprovedAffiliateMonthNumber: 0,
            ApprovedAffiliateNumber: 236,
            BannerNumber: 32,
            ClosedAffiliateMonthNumber: 0,
            ClosedAffiliateNumber: 837,
            DisapprovedAffiliateMonthNumber: 0,
            DisapprovedAffiliateNumber: 1016,
            FlashBannerNumber: 9,
            ImageBannerNumber: 23,
            PaymentRequestNumber: 1,
            PendingAffiliateMonthNumber: 0,
            PendingAffiliateNumber: 1024,
            TextBannerNumber: 13
        },
        pSPToday: 0,
        depositInfo: {
            AllMemberDepositThisMonth: 0,
            AllMemberDepositUpToNow: 17049
        },
        isUpToNow: true,
        getPSPToday: jest.fn(),
        getDepositInfo: jest.fn()
    }

    it('render correctly', () => {
        const result = shallow(<MemberStatistics {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with this month', () => {
        const props = Object.assign({}, defaultProps, { isUpToNow: false });
        const result = shallow(<MemberStatistics {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleGetData - Should call getPSPToday and getDepositInfo', async () => {
        const result = shallow(<MemberStatistics {...defaultProps} />);

        await result.instance()._handleGetData();

        expect(defaultProps.getPSPToday).toHaveBeenCalled();
        expect(defaultProps.getDepositInfo).toHaveBeenCalled();
    });
});