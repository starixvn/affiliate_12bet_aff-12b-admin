import React from 'react';
import PlatformFeeItem from '../PlatformFeeItem';
import { shallow } from 'enzyme';
import commissionValueList from '../../../../modules/constant';
import MultipleSelect from "../../../../../../components/multiple-select";
jest.mock(commissionValueList);

describe('commission group - PlatformFeeItem', () => {
    const dataItem = {
        productList: [{ ProductId: 1, ProductName: 'Sportsbook', Checked: true },
        { ProductId: 3, ProductName: 'Playtech Casino', Checked: true },
        { ProductId: 4, ProductName: 'Number Game', Checked: true },
        { ProductId: 5, ProductName: 'Virtual Sports', Checked: true },
        { ProductId: 6, ProductName: 'Keno', Checked: true }
        ],
        commission: 0.12,
        nextCommission: 0.17
    }
    const defaultProps = {
        data: dataItem,
        key: `platformfee-0`,
        isInserting: true,
        onSelectProduct: jest.fn(),
        onChangeCommission: jest.fn(),
        handleClick: jest.fn(),
        btnText: '+'
    };

    it('handleChange', () => {
        const wrapper = shallow(<PlatformFeeItem {...defaultProps} />);

        wrapper.find(MultipleSelect).simulate('change', { ProductId: 1, ProductName: 'Sportsbook', Checked: true });

        expect(wrapper.instance().props.onSelectProduct).toHaveBeenCalledWith({ ProductId: 1, ProductName: 'Sportsbook', Checked: true });
    });

    it('handleChangeComm - should call onChangeCommission when change DeductedPercentage', () => {
        const mockEvent = { target: { name: 'DeductedPercentage', value: '0.15' } };
        const wrapper = shallow(<PlatformFeeItem {...defaultProps} />);

        wrapper.find('select[name="DeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ DeductedPercentage: 0.15 });
    });

    it('handleChangeComm - should call onChangeCommission when change NextDeductedPercentage', () => {
        const mockEvent = { target: { name: 'NextDeductedPercentage', value: '0.15' } };
        const props = Object.assign({}, defaultProps, { isInserting: false });
        const wrapper = shallow(<PlatformFeeItem {...props} />);

        wrapper.find('select[name="NextDeductedPercentage"]').simulate('change', mockEvent);

        expect(wrapper.instance().props.onChangeCommission).toHaveBeenCalledWith({ NextDeductedPercentage: 0.15 });
    });

    it('Should render correctly', () => {
        const wrapper = shallow(<PlatformFeeItem {...defaultProps} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('Should render correctly with isInserting is false', () => {
        const productList = [{ ProductId: 1, ProductName: 'Sportsbook', Checked: false },
        { ProductId: 3, ProductName: 'Playtech Casino', Checked: false },
        { ProductId: 4, ProductName: 'Number Game', Checked: false },
        { ProductId: 5, ProductName: 'Virtual Sports', Checked: false },
        { ProductId: 6, ProductName: 'Keno', Checked: false }
        ];
        const data = Object.assign({}, defaultProps, { productList: productList });
        const props = Object.assign({}, defaultProps, { isInserting: false, data: data });
        const wrapper = shallow(<PlatformFeeItem {...props} />);

        expect(wrapper).toMatchSnapshot();
    });
});