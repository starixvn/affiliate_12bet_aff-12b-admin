import React from 'react';
import { PlatformFeeSetting } from '../PlatformFeeSetting';
import { shallow } from 'enzyme';
import PlatformFeeItem from "../PlatformFeeItem";
jest.mock('../../../../modules/constant');

describe('commission group - assign private group', () => {
    const defaultSelectedGroup = {
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        Description: "",
        GroupId: 0,
        GroupName: "",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" },
        { DeductedPercentage: 0.12, DeductionId: 11, ProductIds: "1,3,4,5,6,7,8" }],
        QmTurnover: 100,
        TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
    };
    const defaultProps = {
        selectedGroup: defaultSelectedGroup,
        affInfo: {
            AffId: 927841,
            AffType: 4,
            Email: "walter.tran1@starixsoft.com",
            FirstName: "媒体册 获取以更新吧！",
            GroupId: 102,
            MailOption: false,
            PlayerUsername: "qatest01",
            PreferredLanguage: "hi-IN",
            UserName: "12BETAFF001"
        },
        deductionList: [
            { DeductionId: 10, DeductionName: "Platform Fee", DeductedPercentage: 0.17 },
            { DeductionId: 11, DeductionName: "Operational Fee", DeductedPercentage: 0.07 },
            { DeductionId: 12, DeductionName: "Banking Fee", DeductedPercentage: 0.02 }
        ],
        productList: [
            { ProductId: 1, ProductName: "Sportsbook" },
            { ProductId: 3, ProductName: "Playtech Casino" },
            { ProductId: 4, ProductName: "Number Game" },
            { ProductId: 5, ProductName: "Virtual Sports" },
            { ProductId: 6, ProductName: "Keno" }
        ]
    };

    it('handleAdd - should call setSelectedGroup', () => {
        const expectParam = {
            PlatformFeeList: [
                { DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" },
                { DeductedPercentage: 0.12, DeductionId: 11, ProductIds: "1,3,4,5,6,7,8" },
                { DeductedPercentage: 0.17, DeductionId: 10, NextDeductedPercentage: 0.17, ProductIds: "" }
            ]
        }
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);

        wrapper.find(PlatformFeeItem).at(0).props().handleClick();

        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    })

    it('handleRemove - should be called when click remove', () => {
        const wrapper = shallow(<PlatformFeeSetting {...defaultProps} />);
        wrapper.instance().handleRemove = jest.fn();
        wrapper.find(PlatformFeeItem).at(1).props().handleClick();

        expect(wrapper.instance().handleRemove).toHaveBeenCalledWith(1);
    })

    it('handleRemove - should call setSelectedGroup', () => {
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);
        wrapper.instance().handleRemove(1);

        expect(props.setSelectedGroup).toHaveBeenCalledWith({ PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6,1,3,4,5,6,7,8" }] });
    })

    it('handleSelect - should be called when change product', () => {
        const wrapper = shallow(<PlatformFeeSetting {...defaultProps} />);
        wrapper.instance().handleSelect = jest.fn();
        wrapper.find(PlatformFeeItem).at(0).props().onSelectProduct({ ProductId: 1, ProductName: "Sportsbook" });

        expect(wrapper.instance().handleSelect).toHaveBeenCalledWith(0, { ProductId: 1, ProductName: "Sportsbook" });
    })

    it('handleSelect - should call setSelectedGroup with checked', () => {
        const expectParam = {
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6,1" },
            { DeductedPercentage: 0.12, DeductionId: 11, ProductIds: "3,4,5,6,7,8" }]
        };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);
        wrapper.instance().handleSelect(0, { ProductId: 1, ProductName: "Sportsbook", Checked: true });

        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    })

    it('handleSelect - should call setSelectedGroup with not checked', () => {
        const expectParam = {
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "3,4,5,6,1" },
            { DeductedPercentage: 0.12, DeductionId: 11, ProductIds: "1,3,4,5,6,7,8" }]
        };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);
        wrapper.instance().handleSelect(0, { ProductId: 1, ProductName: "Sportsbook", Checked: false });

        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    })

    it('handleSelect - should call setSelectedGroup with PlatformFeeList is null', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { PlatformFeeList: [] });
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup, selectedGroup: selectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);
        wrapper.instance().handleSelect(0, { ProductId: 1, ProductName: "Sportsbook", Checked: true });

        expect(props.setSelectedGroup).toHaveBeenCalledWith({ PlatformFeeList: [] });
    })

    it('handleChangeCommission - should be called when change commission', () => {
        const wrapper = shallow(<PlatformFeeSetting {...defaultProps} />);
        wrapper.instance().handleChangeCommission = jest.fn();
        wrapper.find(PlatformFeeItem).at(0).props().onChangeCommission(0.15);

        expect(wrapper.instance().handleChangeCommission).toHaveBeenCalledWith(0, 0.15);
    })

    it('handleChangeCommission - should call setSelectedGroup', () => {
        const expectParam = {
            PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" },
            { DeductedPercentage: 0.12, DeductionId: 11, ProductIds: "1,3,4,5,6,7,8" }]
        };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<PlatformFeeSetting {...props} />);
        wrapper.instance().handleChangeCommission(0, 0.15);

        expect(props.setSelectedGroup).toHaveBeenCalledWith(expectParam);
    })

    it('should render correctly', () => {
        const wrapper = shallow(<PlatformFeeSetting {...defaultProps} />);
        expect(wrapper).toMatchSnapshot();
    })
})