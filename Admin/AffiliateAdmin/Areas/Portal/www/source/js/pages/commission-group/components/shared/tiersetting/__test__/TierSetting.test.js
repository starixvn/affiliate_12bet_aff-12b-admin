import React from 'react';
import { TierSetting } from '../TierSetting';
import TierSettingItem from '../TierSettingItem';
import { shallow } from 'enzyme';
jest.mock('../../../../modules/constant');

describe('commission group - TierSetting', () => {
    const defaultSelectedGroup = {
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        Description: "A B C",
        GroupId: 0,
        GroupName: "ABC",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
        QmTurnover: 100,
        TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100, MinQm: 0, MaxQm: 10, Commission: 0 },
        { TierId: 2, MinAmount: 100, MaxAmount: 100000000000000, MinQm: 10, MaxQm: 1000000000, Commission: 0 }]
    }
    const defaultProps = {
        selectedGroup: defaultSelectedGroup
    };

    it('handleAdd - should call setSelectedGroup when click button Add Tier', () => {
        const expectParam = {
            TierSettingList: [
                { Commission: 0, MaxAmount: 100, MaxQm: 10, MinAmount: 0, MinQm: 0, TierId: 1 },
                { Commission: 0, MaxAmount: 100000000000000, MaxQm: 1000000000, MinAmount: 100, MinQm: 10, TierId: 2 },
                { Commission: 0, MaxAmount: 100000000000001, MaxQm: 1000000001, MinAmount: 100000000000001, MinQm: 1000000001, TierId: 3 }
            ]
        };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);

        wrapper.find('button').simulate('click')
        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    });

    it('handleAdd - should call setSelectedGroup when click button Add Tier with TierSettingList is null', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { TierSettingList: [] });
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);

        wrapper.find('button').simulate('click')
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ TierSettingList: [{ Commission: 0, MaxAmount: 0, MaxQm: 0, MinAmount: 0, MinQm: 0, TierId: 1 }] });
    });

    it('handleRemove - Should be called when click remove', () => {
        const wrapper = shallow(<TierSetting {...defaultProps} />);
        wrapper.instance().handleRemove = jest.fn();

        wrapper.find(TierSettingItem).at(1).simulate('remove', 2);

        expect(wrapper.instance().handleRemove).toHaveBeenCalled();
    });

    it('handleRemove - Should call setSelectedGroup', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);
        wrapper.instance().handleRemove(1);

        expect(props.setSelectedGroup).toHaveBeenCalledWith({ TierSettingList: [{ TierId: 1, Commission: 0, MaxAmount: 100, MaxQm: 10, MinAmount: 0, MinQm: 0 }] })
    });

    it('handleChangeInput - Should call setSelectedGroup', () => {
        const expectParam = {
            TierSettingList: [
                { Commission: 0, MinAmount: 10, MaxAmount: 500, MinQm: 10, MaxQm: 50, TierId: 1 },
                { Commission: 0, MaxAmount: 100000000000000, MaxQm: 1000000000, MinAmount: 100, MinQm: 10, TierId: 2 }
            ]
        };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);
        wrapper.find(TierSettingItem).at(1).simulate('change', 1, { MinAmount: 10, MaxAmount: 500, MinQm: 10, MaxQm: 50 });

        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    });

    it('handleChangeInput - Should call setSelectedGroup with tierItemValue is incorrect', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);
        wrapper.find(TierSettingItem).at(1).simulate('change', 1, { MinAmount: 'abc', MaxAmount: 0, MinQm: 0, MaxQm: 0 });

        expect(props.setSelectedGroup).not.toHaveBeenCalled();
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<TierSetting {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with TierSettingList is null', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { TierSettingList: [] });
        const props = Object.assign({}, defaultProps, { selectedGroup: selectedGroup });
        const wrapper = shallow(<TierSetting {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
})