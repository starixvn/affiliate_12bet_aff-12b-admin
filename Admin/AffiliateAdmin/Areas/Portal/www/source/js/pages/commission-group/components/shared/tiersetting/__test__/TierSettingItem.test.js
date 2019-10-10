import React from 'react';
import TierSettingItem from '../TierSettingItem';
import { shallow } from 'enzyme';
import commissionValueList from '../../../../modules/constant';
jest.mock(commissionValueList);

describe('commission group - TierSettingItem', () => {
    const dataItem = { TierId: 1, MinAmount: 100, MaxAmount: 100000000000000, MinQm: 10, MaxQm: 1000000000, Commission: 0.01 };
    const defaultProps = {
        data: { TierId: 1, MinAmount: 0, MaxAmount: 100, MinQm: 0, MaxQm: 10, Commission: 0.01 },
        key: 'tier-1',
        isDisabledInfinity: true,
        onRemove: jest.fn(),
        onChange: jest.fn()
    };

    it('handleChange - should call onChange when change commission', () => {
        const mockEvent = { target: { name: 'Commission', value: 'a,15' } };
        const wrapper = shallow(<TierSettingItem {...defaultProps} />);

        wrapper.find('select').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChange).toHaveBeenCalledWith(1, { Commission: 0 });
    });

    it('handleCheck - should call onChange when checked checkbox', () => {
        const mockEvent = { target: { checked: true } };
        const wrapper = shallow(<TierSettingItem {...defaultProps} />);

        wrapper.find('Input[type="checkbox"]').at(0).simulate('change', mockEvent);

        expect(wrapper.instance().props.onChange).toHaveBeenCalledWith(1, { Commission: 0 });
    });

    it('handleCheck - should call onChange when unchecked checkbox', () => {
        const mockEvent = { target: { checked: false } };
        const wrapper = shallow(<TierSettingItem {...defaultProps} />);

        wrapper.find('Input[type="checkbox"]').at(1).simulate('change', mockEvent);

        expect(wrapper.instance().props.onChange).toHaveBeenCalledWith(1, { Commission: 0 });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<TierSettingItem {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with MaxAmout, MaxQm', () => {
        const props = Object.assign({}, defaultProps, { data: dataItem });
        const wrapper = shallow(<TierSettingItem {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
});