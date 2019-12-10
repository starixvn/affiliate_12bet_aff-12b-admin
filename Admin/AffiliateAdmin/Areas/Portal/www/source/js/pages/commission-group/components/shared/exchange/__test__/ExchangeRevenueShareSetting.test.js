import React from 'react';
import { ExchangeRevenueShareSetting } from '../ExchangeRevenueShareSetting';
import { shallow } from 'enzyme';
import SharePercentSettingItem from '../SharePercentSettingItem';
jest.mock('../../../../modules/constant');

describe('commission group - ExchangeRevenueShareSetting', () => {
    const defaultProps = {
        selectedGroup: {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            ExchangeWinLossShare: { DeductionId: 13, ProductIds: 19, DeductedPercentage: 0.1 },
            ExchangeCommissionShare: { DeductionId: 14, ProductIds: 19, DeductedPercentage: 0.05 },
            Description: "A B C",
            GroupId: 0,
            GroupName: "ABC",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
            QmTurnover: 100,
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
        }
    };

    it('handleSelectOperationalFee  - should call setSelectedGroup when change Win Loss Share%', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<ExchangeRevenueShareSetting {...props} />);

        wrapper.find(SharePercentSettingItem).at(0).props().onChangeCommission();
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ ExchangeWinLossShare: { DeductedPercentage: 0.1, DeductionId: 13, ProductIds: 19 } });
    });

    it('handleSelectBankingFee  - should call setSelectedGroup when change Commission Share%', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<ExchangeRevenueShareSetting {...props} />);

        wrapper.find(SharePercentSettingItem).at(1).props().onChangeCommission();
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ ExchangeCommissionShare: { DeductedPercentage: 0.05, DeductionId: 14, ProductIds: 19 } });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<ExchangeRevenueShareSetting {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });
})