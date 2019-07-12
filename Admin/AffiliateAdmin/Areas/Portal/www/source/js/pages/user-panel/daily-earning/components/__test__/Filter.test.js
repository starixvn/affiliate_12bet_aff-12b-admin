import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

describe('daily earning - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const defaultProps = {
        param: {
            affId: 1271808,
            monthDate: "Jan-2019",
            productId: 0
        }
    }

    const productList = [
        {
            ProductId: 1,
            ProductName: 'Sportsbook'
        },
        {
            ProductId: 3,
            ProductName: 'Playtech Casino'
        },
        {
            ProductId: 4,
            ProductName: 'Number Game'
        }
    ]

    it('render correctly', async () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), getProductList: jest.fn(() => []) });
        const wrapper = await shallow(<Filter {...props} />);

        expect(props.getProductList).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jan-2019", productId: 0 });
        expect(wrapper).toMatchSnapshot();
    });

    it('render correctly with has productList', async () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), getProductList: jest.fn(() => productList) });
        const wrapper = await shallow(<Filter {...props} />);

        expect(props.getProductList).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jan-2019", productId: 0 });
        expect(wrapper).toMatchSnapshot();
    });

    it('componentDidMount - should call _submit and _fetchProductList', async () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), getProductList: jest.fn(() => productList) });
        const wrapper = await shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance._submit = jest.fn();
        instance._fetchProductList = jest.fn((fn) => fn & fn());

        await instance.componentDidMount();

        expect(props.getProductList).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jan-2019", productId: 0 });
        expect(instance._submit).toHaveBeenCalledWith(0);
        expect(instance._fetchProductList).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleChangeProduct - should call setState and _submit when change product select', async () => {
        const event = {
            target: {
                value: 1
            }
        }
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), getProductList: jest.fn(() => productList) });
        const wrapper = await shallow(<Filter {...props} />);
        wrapper.setState = jest.fn();
        wrapper.instance()._submit = jest.fn();

        wrapper.find('Input[type="select"][name="Product"][id="Product"]').simulate('change', event)

        expect(props.getProductList).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jan-2019", productId: 0 });
        expect(wrapper.setState).toHaveBeenCalledWith({ selectedProduct: 1 });
        expect(wrapper.instance()._submit).toHaveBeenCalledWith(1);
    });

    it('_submit - should be called when From change', () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), getProductList: jest.fn(() => productList) });
        const wrapper = shallow(<Filter {...props} />);
        
        wrapper.instance()._submit(1);

        expect(props.getProductList).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jan-2019", productId: 1 });
    });
});