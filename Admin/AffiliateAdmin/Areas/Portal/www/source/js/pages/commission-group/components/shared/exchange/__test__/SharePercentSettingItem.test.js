import React from 'react';
import SharePercentSettingItem from '../SharePercentSettingItem';
import { shallow } from 'enzyme';
import commissionValueList from '../../../../modules/constant';
jest.mock(commissionValueList);

describe('commission group - SharePercentSettingItem', () => {
    const defaultProps = {
        label: "Win Loss Share%",
        onChangeCommission: jest.fn(),
        data: { DeductionId: 13, ProductIds: 19, DeductedPercentage: 0 },
        isCreating: true
    };

    it('handleChangeComm - should call onChangeCommission when change SharePercentage', () => {
        const mockEvent = { target: { name: 'DeductedPercentage', value: '0.15' } };
        const wrapper = shallow(<SharePercentSettingItem {...defaultProps} />);

        wrapper.find('Input[type="select"][name="DeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ DeductedPercentage: 0.15 });
    });

    it('handleChangeComm - should call onChangeCommission when change NextSharePercentage', () => {
        const mockEvent = { target: { name: 'NextDeductedPercentage', value: '0.15' } };
        const props = Object.assign({}, defaultProps, { isCreating: false });
        const wrapper = shallow(<SharePercentSettingItem {...props} />);

        wrapper.find('Input[type="select"][name="NextDeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ NextDeductedPercentage: 0.15 });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<SharePercentSettingItem {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with isCreating is false', () => {
        const props = Object.assign({}, defaultProps, { isCreating: false });
        const wrapper = shallow(<SharePercentSettingItem {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
});