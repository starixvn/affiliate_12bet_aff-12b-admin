import React from 'react';
import FeeSettingItem from '../FeeSettingItem';
import { shallow } from 'enzyme';
import commissionValueList from '../../../../modules/constant';
jest.mock(commissionValueList);

describe('commission group - FeeSettingItem', () => {
    const defaultProps = {
        label: "Operational Fee%",
        basedValueName: "| Gross Win Loss |",
        onChangeCommission: jest.fn(),
        data: { OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 } },
        isCreating: true
    };

    it('handleChangeComm - should call onChangeCommission when change DeductedPercentage', () => {
        const mockEvent = { target: { name: 'DeductedPercentage', value: '0.15' } };
        const wrapper = shallow(<FeeSettingItem {...defaultProps} />);

        wrapper.find('Input[type="select"][name="DeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ DeductedPercentage: 0.15 });
    });

    it('handleChangeComm - should call onChangeCommission when change NextDeductedPercentage', () => {
        const mockEvent = { target: { name: 'NextDeductedPercentage', value: '0.15' } };
        const props = Object.assign({}, defaultProps, { isCreating: false });
        const wrapper = shallow(<FeeSettingItem {...props} />);

        wrapper.find('Input[type="select"][name="NextDeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ NextDeductedPercentage: 0.15 });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<FeeSettingItem {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with isCreating is false', () => {
        const props = Object.assign({}, defaultProps, { isCreating: false });
        const wrapper = shallow(<FeeSettingItem {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
});