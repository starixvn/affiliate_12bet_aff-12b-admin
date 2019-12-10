import React from 'react';
import { shallow } from 'enzyme'
import { Filter } from '../Filter';
import AutoComplete from '../../../../components/auto-complete';

import MonthPicker from '@9softstudio/react-monthpicker';
import MultipleSelect from '../../../../components/multiple-select';
jest.mock('../../modules/constants');

describe('affiliate report - Filter ', () => {
    const mainView = 1;
    const monthOnMonth = 2;
    const now = new Date('12/24/2018');
    const isCheckedAllProduct = true;
    const fromMonth = {
        month: now.getMonth(),
        year: now.getFullYear()
    };

    const defaultProps = {
        productList: [],
        pagingOption: {
            PageIndex: 1,
            PageSize: 20,
            TotalItem: 0,
            PageList: [10, 20, 50, 100, 500]
        },
        languageList: {},
        today: now.getMonth() + '/' + now.getDate() + '/' + now.getFullYear(),
        currentViewLevel: mainView,
        param: {}
    };

    it('componentDidUpdate - should call setParam, setState and setReportTitle when currentViewLevel change to Main view', () => {
        const cachedMainReportParam = {
            UserName: '',
            PreferredLanguage: '',
            FromDate: '12/24/2018',
            ToDate: '12/24/2018',
            SortType: 1,
            SortDirectionType: '',
            ProductIds: '1,2'
        };
        const state = {
            userName: '',
            selectedLanguage: '',
            fromMonth: { month: now.getMonth() + 1, year: now.getFullYear() },
            toMonth: { month: now.getMonth() + 1, year: now.getFullYear() },
            sortBy: 1,
            isSortAscending: false,
            selectedProductList: '1,2',
            isGetAll: false
        };
        const expectReportTitle = 'AFFILIATE REPORT - Main view';

        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setReportTitle: mockSetReportTitle, setParam: mockSetParam });
        const componentInstance = new Filter(props);

        componentInstance.cachedMainReportParam = cachedMainReportParam;
        componentInstance.setState = jest.fn();
        componentInstance._buildReportTitle = jest.fn(() => 'AFFILIATE REPORT - Main view');
        componentInstance.componentDidUpdate({ currentViewLevel: monthOnMonth });

        expect(componentInstance.setState.mock.calls[0][0]).toEqual(state);
        componentInstance.setState.mock.calls[0][1]();

        expect(componentInstance._buildReportTitle).toHaveBeenCalledWith(true);

        expect(props.setReportTitle).toHaveBeenCalledWith(expectReportTitle);
        expect(props.setParam).toHaveBeenCalledWith(componentInstance.cachedMainReportParam);
    });

    it('componentDidUpdate - should call setParam, setState and setReportTitle when currentViewLevel change to Month on Month view', () => {
        const state = {
            userName: 'Month on Month view',
            sortBy: 1,
            isSortAscending: false
        };
        const selectedProductList = '1,2';
        const productList = [
            { ProductId: 1 },
            { ProductId: 2 }
        ];
        const expectReportTitle = 'AFFILIATE REPORT - Month on Month view';
        const mockSetReportTitle = jest.fn();
        const props = Object.assign({}, defaultProps, { productList, setReportTitle: mockSetReportTitle, param: { UserName: 'Month on Month view' }, currentViewLevel: monthOnMonth });

        const componentInstance = new Filter(props);
        componentInstance.state.selectedProductList = selectedProductList;

        componentInstance.setState = jest.fn();
        componentInstance._buildReportTitle = jest.fn(() => 'AFFILIATE REPORT - Month on Month view');
        componentInstance.componentDidUpdate({ currentViewLevel: mainView });

        expect(componentInstance.setState.mock.calls[0][0]).toEqual(state);
        componentInstance.setState.mock.calls[0][1]();

        expect(componentInstance._buildReportTitle).toHaveBeenCalledWith(true);

        expect(props.setReportTitle).toHaveBeenCalledWith(expectReportTitle);
    });

    it('_isMainView - return true with Main view', () => {
        const componentInstance = new Filter(defaultProps);
        const result = componentInstance._isMainView();

        expect(result).toBeTruthy();
    });

    it('_isMainView - return true with Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth })
        const componentInstance = new Filter(props);
        const result = componentInstance._isMainView();

        expect(result).toBeFalsy();
    });

    it('should call handlePressEnter when press Enter', () => {
        const mockedEvent = { charCode: 13 };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleClickSearch = jest.fn();

        wrapper.find('Input[type="text"][id="Affiliate"]').simulate('keyPress', mockedEvent);

        expect(instance.handleClickSearch).toHaveBeenCalled();
    });

    it('not call handlePressEnter when press keyboard which is not Enter', () => {
        const mockedEvent = { charCode: 11 };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.handleClickSearch = jest.fn();

        wrapper.find('Input[type="text"][id="Affiliate"]').simulate('keyPress', mockedEvent);

        expect(instance.handleClickSearch).not.toHaveBeenCalled();
    });

    it('should call handleChangeAffiliate when Affiliate change', () => {
        const event = { target: { value: "abc" } }

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();

        wrapper.find('Input[type="text"][id="Affiliate"]').simulate('change', event);

        expect(instance.setState).toHaveBeenCalledWith({ userName: event.target.value });
    });

    it('should call handleChangeLanguage when Language change', () => {
        const event = { target: { value: "English" } }

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();

        wrapper.find('Input[type="select"][id="Language"]').simulate('change', event);

        expect(instance.setState).toHaveBeenCalledWith({ selectedLanguage: event.target.value });
    });

    it('should call handleSelectFromMonth when From Month change with correct parameters currentMainView is Main view', () => {
        const toMonthRange = {
            min: { month: now.getMonth(), year: now.getFullYear() },
            max: { month: now.getMonth(), year: now.getFullYear() }
        };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        instance._getToMonthRange = jest.fn(() => toMonthRange);

        wrapper.find(MonthPicker).at(0).simulate('select', now.getMonth(), now.getFullYear());

        expect(instance._getToMonthRange).toHaveBeenCalledWith(fromMonth);
        expect(instance.setState).toHaveBeenCalledWith({ fromMonth });
    });

    it('should call handleSelectFromMonth when From Month change with incorrect parameters pacurrentMainView is Main view', () => {
        const toMonthRange = {
            min: { month: now.getMonth() + 1, year: now.getFullYear() },
            max: { month: now.getMonth(), year: now.getFullYear() }
        };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();
        instance._getToMonthRange = jest.fn(() => toMonthRange);

        wrapper.find(MonthPicker).at(0).simulate('select', now.getMonth(), now.getFullYear());

        expect(instance._getToMonthRange).toHaveBeenCalledWith(fromMonth);
        expect(instance.setState).toHaveBeenCalledWith({ fromMonth, toMonth: fromMonth });
    });

    it('should call handleSelectFromMonth when From Month change with currentMainView is Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.setState = jest.fn();

        wrapper.find(MonthPicker).at(0).simulate('select', now.getMonth(), now.getFullYear());

        expect(wrapper.setState).toHaveBeenCalledWith({ fromMonth });
    });

    it('should call handleSelectToMonth when To Month change', () => {
        const toMonth = {
            month: now.getMonth(),
            year: now.getFullYear()
        };
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();

        wrapper.find(MonthPicker).at(1).simulate('select', now.getMonth(), now.getFullYear());

        expect(instance.setState).toHaveBeenCalledWith({ toMonth });
    });

    it('should call handleChangeProduct when change the selected product list', () => {
        const item = {
            Checked: false,
            ProductId: 1,
            ProductName: "Sportsbook"
        };
        const selectedProductList = '3,4,5,6,7,8,9,12';
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();

        wrapper.find(MultipleSelect).simulate('change', item, selectedProductList);

        expect(instance.setState).toHaveBeenCalledWith({ selectedProductList });
    });

    it('should call handleChangeSortAscending when checkbox Get All change with currentMainView is Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ isGetAll: true });

        wrapper.find('Input[type="checkbox"]').at(0).simulate('change');

        expect(wrapper.state('isGetAll')).toEqual(false);
    });

    it('should call handleChangeSortBy when Sort By change', () => {
        const event = { target: { value: 2 } };
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();

        instance.setState = jest.fn();

        wrapper.find('Input[type="select"][id="SortBy"]').simulate('change', event);

        expect(instance.setState).toHaveBeenCalledWith({ sortBy: event.target.value });
    });

    it('should call handleChangeGetAll when checkbox A-Z change', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ isSortAscending: true });

        wrapper.find('Input[type="checkbox"]').simulate('change');

        expect(wrapper.state('isSortAscending')).toEqual(false);
    });

    it('should call handleClickSearch when click Search button with selectedProductList is empty', () => {
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setReportTitle: mockSetReportTitle, setParam: mockSetParam })

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        wrapper.setState({ selectedProductList: '' });
        instance._buildReportTitle = jest.fn(() => 'Handle click Search button');

        wrapper.find('Button').simulate('click');

        expect(instance._buildReportTitle).not.toHaveBeenCalled();
        expect(props.setReportTitle).not.toHaveBeenCalled();
        expect(props.setParam).not.toHaveBeenCalled();
    });

    it('should call handleClickSearch when click Search button with currentMainView is Main view', () => {
        const param = {
            UserName: '',
            FromDate: now.getMonth() + '/1/' + now.getFullYear(),
            ToDate: now.getMonth() + '/1/' + now.getFullYear(),
            PreferredLanguage: '',
            ProductIds: '0',
            SortType: 1,
            SortDirectionType: 0,
            PageIndex: 1,
            PageSize: 20,
            IsGetAll: false
        };

        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setReportTitle: mockSetReportTitle, setParam: mockSetParam })

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();

        instance._buildReportTitle = jest.fn(() => 'Handle click Search button');
        instance._isMainView = jest.fn(() => true);

        wrapper.find('Button').simulate('click');

        expect(instance._buildReportTitle).toHaveBeenCalledWith(true);
        expect(instance._isMainView).toHaveBeenCalled();
        expect(props.setReportTitle).toHaveBeenCalledWith('Handle click Search button');
        expect(instance.cachedMainReportParam).toEqual(param);
        expect(props.setParam).toHaveBeenCalledWith(param);
    });

    it('should call handleClickSearch when click Search button with currentMainView is Month on Month view', () => {
        const param = {
            UserName: '',
            FromDate: now.getMonth() + '/1/' + now.getFullYear(),
            ToDate: now.getMonth() + '/1/' + now.getFullYear(),
            PreferredLanguage: '',
            ProductIds: '0',
            SortType: 1,
            SortDirectionType: 1,
            PageIndex: 1,
            PageSize: 20,
            IsGetAll: false
        };
        const selectedProductList = '1,2';
        const productList = [
            { ProductId: 1 },
            { ProductId: 2 }
        ];
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { productList, setReportTitle: mockSetReportTitle, setParam: mockSetParam, currentViewLevel: monthOnMonth })

        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ selectedProductList, isSortAscending: false });
        const instance = wrapper.instance();

        instance._buildReportTitle = jest.fn(() => 'Handle click Search button');
        instance._isMainView = jest.fn(() => false);

        wrapper.find('Button').simulate('click');

        expect(instance._buildReportTitle).toHaveBeenCalledWith(true);
        expect(instance._isMainView).toHaveBeenCalled();
        expect(props.setReportTitle).toHaveBeenCalledWith('Handle click Search button');
        expect(instance.cachedMainReportParam).toBeNull;
        expect(props.setParam).toHaveBeenCalledWith(param);
    });

    it('should call handleClickSearch when click Search button with selectedProductList not equal productList', () => {
        const param = {
            UserName: '',
            FromDate: now.getMonth() + '/1/' + now.getFullYear(),
            ToDate: now.getMonth() + '/1/' + now.getFullYear(),
            PreferredLanguage: '',
            ProductIds: '1,2',
            SortType: 1,
            SortDirectionType: 1,
            PageIndex: 1,
            PageSize: 20,
            IsGetAll: false
        };
        const selectedProductList = '1,2';
        const productList = [
            { ProductId: 1 },
            { ProductId: 2 },
            { ProductId: 3 }
        ];
        const mockSetReportTitle = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { productList, setReportTitle: mockSetReportTitle, setParam: mockSetParam, currentViewLevel: monthOnMonth })

        const wrapper = shallow(<Filter {...props} />);
        wrapper.setState({ selectedProductList, isSortAscending: false });
        const instance = wrapper.instance();

        instance._buildReportTitle = jest.fn(() => 'Handle click Search button');
        instance._isMainView = jest.fn(() => false);

        wrapper.find('Button').simulate('click');

        expect(instance._buildReportTitle).toHaveBeenCalledWith(false);
        expect(instance._isMainView).toHaveBeenCalled();
        expect(props.setReportTitle).toHaveBeenCalledWith('Handle click Search button');
        expect(instance.cachedMainReportParam).toBeNull;
        expect(props.setParam).toHaveBeenCalledWith(param);
    });

    it('_buildReportTitle - return Main Report Title with correct parameter and currentMainView is Main view', () => {
        const componentInstance = new Filter(defaultProps);
        componentInstance._buildMainReportTitle = jest.fn();
        componentInstance._isMainView = jest.fn(() => true);

        componentInstance._buildReportTitle(isCheckedAllProduct);

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance._buildMainReportTitle).toHaveBeenCalledWith(isCheckedAllProduct);
    });

    it('_buildReportTitle - return Main Report Title with correct parameter and currentMainView is Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const componentInstance = new Filter(props);
        componentInstance._buildMonthOnMonthReportTile = jest.fn();
        componentInstance._isMainView = jest.fn(() => false);

        componentInstance._buildReportTitle(isCheckedAllProduct);

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance._buildMonthOnMonthReportTile).toHaveBeenCalledWith(isCheckedAllProduct);
    });

    it('_buildMainReportTitle - return corrected value with correct parameter', () => {
        const componentInstance = new Filter(defaultProps);
        componentInstance._getProductDataSource = jest.fn();

        const result = componentInstance._buildMainReportTitle(isCheckedAllProduct);

        expect(componentInstance._getProductDataSource).toHaveBeenCalled();
        expect(result).toEqual('AFFILIATE REPORT  - All languages - All Products - FROM ' + now.getMonth() + '/' + now.getFullYear() + ' TO ' + now.getMonth() + '/' + now.getFullYear());
    });

    it('_buildMainReportTitle - return corrected value with incorrect parameter', () => {
        const productList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true }
        ]

        const componentInstance = new Filter(defaultProps);
        componentInstance.state.userName = '12Bet';
        componentInstance._getProductDataSource = jest.fn(() => productList);

        const result = componentInstance._buildMainReportTitle(!isCheckedAllProduct);

        expect(componentInstance._getProductDataSource).toHaveBeenCalled();
        expect(result).toEqual("AFFILIATE REPORT - Username contains '12Bet' - All languages - Sportsbook - FROM " + now.getMonth() + '/' + now.getFullYear() + ' TO ' + now.getMonth() + '/' + now.getFullYear());
    });

    it('_buildMonthOnMonthReportTile - return corrected value with correct parameter', () => {
        const componentInstance = new Filter(defaultProps);
        componentInstance._getProductDataSource = jest.fn();

        const result = componentInstance._buildMonthOnMonthReportTile(isCheckedAllProduct);

        expect(componentInstance._getProductDataSource).toHaveBeenCalled();
        expect(result).toEqual('MONTH ON MONTH  - All Products - FROM ' + now.getMonth() + '/' + now.getFullYear() + ' TO ' + now.getMonth() + '/' + now.getFullYear());
    });

    it('_buildMonthOnMonthReportTile - return corrected value with incorrect parameter', () => {
        const productList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true }
        ]
        const componentInstance = new Filter(defaultProps);
        componentInstance.state.userName = '12Bet';
        componentInstance.state.isGetAll = true;
        componentInstance._getProductDataSource = jest.fn(() => productList);

        const result = componentInstance._buildMonthOnMonthReportTile(!isCheckedAllProduct);

        expect(componentInstance._getProductDataSource).toHaveBeenCalled();
        expect(result).toEqual('MONTH ON MONTH - 12Bet - Sportsbook - Get All Dates');
    });

    it('_getProductDataSource - return corrected value with selected all products', () => {
        const productList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true },
            { ProductId: 6, ProductName: "Keno", Checked: true },
            { ProductId: 8, ProductName: "Lottery", Checked: true }
        ]

        const expectProductList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true },
            { ProductId: 6, ProductName: "Keno", Checked: true },
            { ProductId: 8, ProductName: "Lottery", Checked: true }
        ]

        const props = Object.assign({}, defaultProps, { productList: productList })
        const componentInstance = new Filter(props);

        const result = componentInstance._getProductDataSource();

        expect(result).toEqual(expectProductList);
    });

    it('_getProductDataSource - return corrected value with not selected all products', () => {
        const productList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true },
            { ProductId: 6, ProductName: "Keno", Checked: true },
            { ProductId: 8, ProductName: "Lottery", Checked: true }
        ];

        const expectProductList = [
            { ProductId: 1, ProductName: "Sportsbook", Checked: true },
            { ProductId: 6, ProductName: "Keno", Checked: false },
            { ProductId: 8, ProductName: "Lottery", Checked: false }
        ]

        const props = Object.assign({}, defaultProps, { productList: productList })
        const componentInstance = new Filter(props);
        componentInstance.state.selectedProductList = '1';

        const result = componentInstance._getProductDataSource();

        expect(result).toEqual(expectProductList);
    });

    it('_getToMonthRange - return corrected value with correct parameter and currentMainView is Main view', () => {
        const toMonthRange = {
            min: { month: 1, year: 2018 },
            max: { month: 6, year: 2018 }
        };

        const componentInstance = new Filter(defaultProps);

        const result = componentInstance._getToMonthRange({ month: 1, year: 2018 });

        expect(result).toEqual(toMonthRange);
    });

    it('_getToMonthRange - return corrected value with maxToYear is nowYear, actualMaxMonth bigger nowMonth and currentMainView is Main view', () => {
        const date = new Date('5/24/2018');
        const today = date.getMonth() + '/' + date.getDate() + '/' + date.getFullYear();
        const toMonthRange = {
            min: { month: 2, year: 2018 },
            max: { month: 4, year: 2018 }
        };
        const props = Object.assign({}, defaultProps, { today })
        const componentInstance = new Filter(props);

        const result = componentInstance._getToMonthRange({ month: 2, year: 2018 });

        expect(result).toEqual(toMonthRange);
    });

    it('_getToMonthRange - return corrected value with correct parameter and currentMainView is Month on Month view', () => {
        const toMonthRange = {
            min: { month: 1, year: 2018 },
            max: { month: now.getMonth(), year: now.getFullYear() }
        };

        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const componentInstance = new Filter(props);

        const result = componentInstance._getToMonthRange({ month: 1, year: 2018 });

        expect(result).toEqual(toMonthRange);
    });

    it('should render correctly when currentMainView is Main view', () => {
        const languageList = [{ English: 'English' }];
        const props = Object.assign({}, defaultProps, { languageList });
        const result = shallow(<Filter {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('should call setState when select and input on AutoComplete', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.instance().setState = jest.fn();

        wrapper.find(AutoComplete).props().onSelect('selected value');
        wrapper.find(AutoComplete).props().onInput('inputed value');

        expect(wrapper.instance().setState).toHaveBeenCalledTimes(2);
        expect(wrapper.instance().setState.mock.calls[0][0]).toEqual({ userName: "selected value" });
        expect(wrapper.instance().setState.mock.calls[1][0]).toEqual({ userName: "inputed value" });
    });

    it('should render correctly when currentMainView is Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const result = shallow(<Filter {...props} />);

        expect(result).toMatchSnapshot();
    });
});