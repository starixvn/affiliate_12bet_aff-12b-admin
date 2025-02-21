import React from 'react';
import { QmTurnoverSetting } from '../QmTurnoverSetting';
import { shallow } from 'enzyme';

describe('commission group - QmTurnoverSetting', () => {
    const defaultSelectedGroup = {
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        Description: "A B C",
        GroupId: 0,
        GroupName: "ABC",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
        QmTurnover: 100,
        TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }]
    };
    const defaultProps = {
        selectedGroup: defaultSelectedGroup
    };

    it('handleChange - should call setSelectedGroup when change QmTurnover', () => {
        const mockEvent = { target: { name: 'QmTurnover', value: '150,000' } };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<QmTurnoverSetting {...props} />);

        wrapper.find('Input[id="QmTurnover"]').simulate('change', mockEvent)
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ QmTurnover: 150000 });
    });

    it('handleChange - not call setSelectedGroup when change QmTurnover with value not isValidNumber', () => {
        const mockEvent = { target: { name: 'QmTurnover', value: '15abc,000' } };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<QmTurnoverSetting {...props} />);

        wrapper.find('Input[id="QmTurnover"]').simulate('change', mockEvent)
        expect(props.setSelectedGroup).not.toHaveBeenCalled();
    });

    it('handleChange - should call setSelectedGroup when change QmTurnover', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { GroupId: 123, QmTurnover: null });
        const mockEvent = { target: { name: 'QmTurnover', value: '250,000' } };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { selectedGroup: selectedGroup, setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<QmTurnoverSetting {...props} />);

        wrapper.find('Input[id="QmTurnover"]').simulate('change', mockEvent)
        expect(props.setSelectedGroup).toHaveBeenCalledWith({ QmTurnover: 250000 });
    });

    it('handleChange - not call setSelectedGroup when change QmTurnover with value not isValidNumber', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { GroupId: 123, QmTurnover: null });
        const mockEvent = { target: { name: 'QmTurnover', value: '250,000,ABC' } };
        const mockSetSelectedGroup = jest.fn();
        const props = Object.assign({}, defaultProps, { selectedGroup: selectedGroup, setSelectedGroup: mockSetSelectedGroup });
        const wrapper = shallow(<QmTurnoverSetting {...props} />);

        wrapper.find('Input[id="QmTurnover"]').simulate('change', mockEvent)
        expect(props.setSelectedGroup).not.toHaveBeenCalled();
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<QmTurnoverSetting {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with GroupId different Zero and QmTurnover is null', () => {
        const selectedGroup = Object.assign({}, defaultSelectedGroup, { GroupId: 123, QmTurnover: null });
        const props = Object.assign({}, defaultProps, { selectedGroup: selectedGroup });
        const wrapper = shallow(<QmTurnoverSetting {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
})