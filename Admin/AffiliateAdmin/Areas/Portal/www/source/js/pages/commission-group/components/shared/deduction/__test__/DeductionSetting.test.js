import React from 'react';
import { DeductionSetting } from '../DeductionSetting';
import { shallow } from 'enzyme';
import FeeSettingItem from '../FeeSettingItem';
jest.mock('../../../../modules/constant');

describe('commission group - DeductionSetting', () => {
    const defaultProps = {
        selectedGroup: {
            BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
            Description: "A B C",
            GroupId: 0,
            GroupName: "ABC",
            OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
            QmTurnover: 100,
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
        }
    };

    it('handleSelectOperationalFee  - should call setSelectedGroup when change Operational Fee%', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<DeductionSetting {...props} />);

        wrapper.find(FeeSettingItem).at(0).props().onChangeCommission();
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 } });
    });

    it('handleSelectBankingFee  - should call setSelectedGroup when change Banking Fee%', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<DeductionSetting {...props} />);

        wrapper.find(FeeSettingItem).at(1).props().onChangeCommission();
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 } });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<DeductionSetting {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });
})