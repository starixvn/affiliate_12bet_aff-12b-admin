import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';
import ReportExcel from '../../modules/reportExcel';
jest.mock('../../modules/reportExcel');

describe('players detail - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });
    afterEach(() => {
        jest.clearAllMocks();
    });

    const today = '12/26/2019';
    const affId = 1271808;
    const productList = [
        {ProductId: 1, ProductName: "Sportsbook"},
        {ProductId: 4, ProductName: "Number Game"}
    ];
    const setParam = jest.fn();
    const getProductList = jest.fn(() => productList);
    const param = {
        productId: 0,
        monthDate: "11/26/2019",
        isClickLink: false,
    };
    const defaultProps = {
        today,
        param,
        affId,
        setParam,
        getProductList
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should call handleSearch and handleGetProductList', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.handleGetProductList = jest.fn();
        instance.handleSearch = jest.fn();
        instance.setState({ monthDate: new Date('11/26/2019') });

        instance.componentDidMount();

        expect(instance.handleGetProductList).toHaveBeenCalled();
        expect(instance.handleSearch).toHaveBeenCalledWith(new Date('11/26/2019'));
    });

    it('componentDidMount - should only call handleSearch', () => {
        const newParam = Object.assign({}, param, { isClickLink: true, monthDate: null });
        const props = Object.assign({}, defaultProps, { param: newParam });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState({ productList, monthDate: new Date('11/26/2019') });
        instance.handleGetProductList = jest.fn();
        instance.handleSearch = jest.fn();

        instance.componentDidMount();

        expect(instance.handleGetProductList).not.toHaveBeenCalled();
        expect(instance.handleSearch).toHaveBeenCalledWith(new Date('11/26/2019'));
    });

    it('componentDidUpdate - should call setState and handleSearch', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();
        instance.handleSearch = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.setState).toHaveBeenCalledWith({ monthDate: new Date('11/26/2019'), selectedProduct: 0 });
        expect(instance.handleSearch).toHaveBeenCalledWith(new Date('11/26/2019'));
    });

    it('handleGetProductList - should call setState and getProductList', async () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance.handleGetProductList();

        expect(instance.setState).toHaveBeenCalledWith({ productList });
        expect(defaultProps.getProductList).toHaveBeenCalled();
    });

    it('handleSelectMonth - should call setState', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.handleSelectMonth(12, 2019);

        expect(instance.setState).toHaveBeenCalledWith({ monthDate: new Date(2019, 11) });
    });

    it('handleChangeProduct - should call setState', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.handleChangeProduct({ target: { value: 1 }});

        expect(instance.setState).toHaveBeenCalledWith({ selectedProduct: 1 });
    });

    it('handleFormatMonthPicker - should return correctly', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);

        const result = wrapper.instance().handleFormatMonthPicker(12, 2019);

        expect(result).toEqual('Dec-2019');
    });

    it('handleSearch - should be called when click Search Button', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ productList });

        wrapper.find('Button').at(0).simulate('click');

        expect(defaultProps.setParam).toHaveBeenCalledWith({ monthDate: "12/1/2019", productId: 0});
    });

    it('handleExportExcel - should call exportExcel', async() => {
        const props = Object.assign({}, defaultProps, { exportExcel: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        await wrapper.instance().handleExportExcel();

        expect(props.exportExcel).toHaveBeenCalledWith({"IsExportExcel": true, "affId": 1271808, "monthDate": "12/1/2019", "productId": 0});
    });

    it('handleExportExcel - should call exportExcel, _getReportTitle and ReportExcel', async() => {
        const expectedResponse = { data: ['data 1', 'data 2'] };
        const props = Object.assign({}, defaultProps, { exportExcel: jest.fn(() => expectedResponse.data) })
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance._getReportTitle = jest.fn(() => "ABC");

        await instance.handleExportExcel();

        expect(props.exportExcel).toHaveBeenCalledWith({"IsExportExcel": true, "affId": 1271808, "monthDate": "12/1/2019", "productId": 0});
        expect(ReportExcel).toHaveBeenCalledWith(expectedResponse.data, "ABC");
    });

    it('_getReportTitle - should call handleFormatMonthPicker', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleFormatMonthPicker = jest.fn(() => "ABC");

        const result = instance._getReportTitle();

        expect(result).toEqual("Player Detail - All Products - ABC");
        expect(instance.handleFormatMonthPicker).toHaveBeenCalled();
    });

    it('_getReportTitle - should call _getSelectedProductName and handleFormatMonthPicker', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState({ selectedProduct: 1 });
        instance._getSelectedProductName = jest.fn(() => "Sportsbook");
        instance.handleFormatMonthPicker = jest.fn(() => "ABC");

        const result = instance._getReportTitle();

        expect(result).toEqual("Player Detail - Sportsbook - ABC");
        expect(instance.handleFormatMonthPicker).toHaveBeenCalled();
    });

    it('_getSelectedProductName - should return correctly', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ productList });

        const result = wrapper.instance()._getSelectedProductName(1);

        expect(result).toEqual("Sportsbook");
    });

    it('_getSelectedProductName - should return empty', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ productList });

        const result = wrapper.instance()._getSelectedProductName(2);

        expect(result).toEqual("");
    });
});