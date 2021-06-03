import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import ReportExcel from '../../libs/reportExcel';
import { DatePicker } from '@9softstudio/react-datepicker';

jest.mock('../../libs/reportExcel');

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
        getProductList,
        hasTurnOverPermission: true,
        isDisplayExportExcelBtn: true,
        sortedParam: { sortById: 5, isSortAsc: false }
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
        expect(instance.handleSearch).toHaveBeenCalled();
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
        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it('componentDidUpdate - should call setState and handleSearch', () => {
        const newParam = Object.assign({}, param, { isClickLink: true });
        const props = Object.assign({}, defaultProps, { param: newParam });
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn((obj, fn) => fn && fn());
        instance.handleSearch = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.setState).toHaveBeenCalledWith({ fromDate: "11/01/2019", toDate: "11/26/2019", isGetAll: false, selectedProduct: 0 }, expect.any(Function));
        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it('handleGetProductList - should call setState and getProductList', async () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        await instance.handleGetProductList();

        expect(instance.setState).toHaveBeenCalledWith({ productList });
        expect(defaultProps.getProductList).toHaveBeenCalled();
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/25/2019") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState(() => ({ productList }));
        wrapper.setState = jest.fn(fn => (fn instanceof Function) && fn(() => ({ fromDate: "6/25/2019", toDate: "7/12/2019" })));

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "7/10/2019") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ productList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "7/10/2019" });
    });

    it('handleChangeProduct - should call setState', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.handleChangeProduct({ target: { value: 1 }});

        expect(instance.setState).toHaveBeenCalledWith({ selectedProduct: 1 });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ productList });

        wrapper.find('Button').at(0).simulate('click');

        expect(defaultProps.setParam).toHaveBeenCalledWith({ "fromDate": "06/26/2019", "toDate": "12/26/2019", productId: 0, sortById: 5, isSortAsc: false });
    });

    it('handleExportExcel - should call exportExcel', async() => {
        const props = Object.assign({}, defaultProps, { exportExcel: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        await wrapper.instance().handleExportExcel();

        expect(props.exportExcel).toHaveBeenCalledWith({ "IsExportExcel": true, "affId": 1271808, "fromDate": "06/26/2019", "toDate": "12/26/2019", "productId": 0, sortById: 5, isSortAsc: false });
    });

    it('handleExportExcel - should call exportExcel, _getReportTitle and ReportExcel', async() => {
        const expectedResponse = { data: ['data 1', 'data 2'] };
        const props = Object.assign({}, defaultProps, { exportExcel: jest.fn(() => expectedResponse.data) })
        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance._getReportTitle = jest.fn(() => "ABC");

        await instance.handleExportExcel();

        expect(props.exportExcel).toHaveBeenCalledWith({ "IsExportExcel": true, "affId": 1271808, "fromDate": "06/26/2019", "toDate": "12/26/2019", "productId": 0, sortById: 5, isSortAsc: false });
        expect(ReportExcel).toHaveBeenCalledWith(expectedResponse.data, "ABC", true);
    });

    it('_getReportTitle - should call _getSelectedProductName', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState({ selectedProduct: 1 });
        instance._getSelectedProductName = jest.fn(() => "Sportsbook");

        const result = instance._getReportTitle();

        expect(result).toEqual("Player Detail - Sportsbook - From 06/26/2019 To 12/26/2019");
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