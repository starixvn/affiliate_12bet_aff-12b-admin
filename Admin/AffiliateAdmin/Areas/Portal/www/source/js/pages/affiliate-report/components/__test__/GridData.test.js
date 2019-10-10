import React from 'react';
import { shallow } from 'enzyme';
import { GridData } from '../GridData';

import * as ajaxRequest from '../../../../modules/ajaxRequest';
jest.mock('../../modules/constants');

describe('affiliate report - GridData ', () => {
    const mainView = 1;
    const monthOnMonth = 2;

    const defaultProps = {
        pagingOption: {
            PageIndex: 1,
            PageSize: 20,
            TotalItem: 0,
            PageList: [10, 20, 50, 100, 500]
        },
        param: {},
        currentViewLevel: mainView
    };

    it('componentDidUpdate - should call setState, setPagingOption when param changed, cachedMainReport not null and currentViewLevel is Main view', () => {
        const param = {
            PageIndex: 1,
            PageSize: 20
        };
        const cachedMainReport = {
            gridData: [{ first: 1 }],
            total: 1,
            totalItem: 1
        };
        const mockSetPagingOption = jest.fn();

        const props = Object.assign({}, defaultProps, { param, setPagingOption: mockSetPagingOption });
        const componentInstance = new GridData(props);
        componentInstance.setState = jest.fn();
        componentInstance._isMainView = jest.fn(() => true);
        componentInstance.cachedMainReport = cachedMainReport;

        componentInstance.componentDidUpdate(defaultProps);
        componentInstance.setState.mock.calls[0][1]();

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance.setState).toHaveBeenCalled();
        expect(componentInstance.setState.mock.calls[0][0]).toEqual({ gridData: [{ first: 1 }], total: 1 });

        expect(componentInstance.cachedMainReport).toBeNull();
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
    });

    it('componentDidUpdate - should call fetchData when param changed, cachedMainReport is null and currentViewLevel is Main view', () => {
        const param = {
            UserName: '',
        }

        const props = Object.assign({}, defaultProps, { param });
        const componentInstance = new GridData(props);
        componentInstance.fetchData = jest.fn();
        componentInstance._isMainView = jest.fn(() => true);

        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance.fetchData).toHaveBeenCalledWith(param);
    });

    it('componentDidUpdate - should call fetchData when param changed, cachedMainReport is null and currentViewLevel is Month on Month view', () => {
        const param = { UserName: '' }

        const props = Object.assign({}, defaultProps, { param, currentViewLevel: monthOnMonth });
        const componentInstance = new GridData(props);
        componentInstance.fetchData = jest.fn();
        componentInstance._isMainView = jest.fn(() => false);

        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance.cachedMainReport).toEqual({ gridData: null, total: null, totalItem: 0 });
        expect(componentInstance.fetchData).toHaveBeenCalledWith(param);
    });

    it('componentDidUpdate - should call fetchData when param changed, cachedMainReport is not null and currentViewLevel is Month on Month view', () => {
        const param = { UserName: '' }

        const props = Object.assign({}, defaultProps, { param, currentViewLevel: monthOnMonth });
        const componentInstance = new GridData(props);
        componentInstance.cachedMainReport = { gridData: [{ data: 'data' }] };
        componentInstance.fetchData = jest.fn();
        componentInstance._isMainView = jest.fn(() => false);

        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance._isMainView).toHaveBeenCalled();
        expect(componentInstance.cachedMainReport).not.toEqual({ gridData: null, total: null, totalItem: 0 });
        expect(componentInstance.fetchData).toHaveBeenCalledWith(param);
    });

    it('_isMainView - return true with Main view', () => {
        const componentInstance = new GridData(defaultProps);
        const result = componentInstance._isMainView();

        expect(result).toBeTruthy();
    });

    it('_isMainView - return true with not Main view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth })
        const componentInstance = new GridData(props);
        const result = componentInstance._isMainView();

        expect(result).toBeFalsy();
    });

    it('fetchData - should pass request param correctly with currentViewLevel is Main view', () => {
        const expectedParam = {
            userName: 'Main view'
        }
        const expectedUrl = 'Portal/Report/GetMainData';

        ajaxRequest.post = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), param: { userName: 'Main view' } });
        const componentInstance = new GridData(props);

        componentInstance.fetchData(expectedParam);

        expect(ajaxRequest.post).toHaveBeenCalledWith(expectedUrl, expectedParam);
    });

    it('fetchData - should pass request param correctly with currentViewLevel is Month on Month view', () => {
        const expectedParam = {
            userName: 'Month on Month view'
        }
        const expectedUrl = 'Portal/Report/GetDetailData';

        ajaxRequest.post = jest.fn();
        const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), param: { userName: 'Month on Month view' }, currentViewLevel: monthOnMonth });
        const componentInstance = new GridData(props);

        componentInstance.fetchData(expectedParam);

        expect(ajaxRequest.post).toHaveBeenCalledWith(expectedUrl, expectedParam);
    });

    it('fetchData - should call correctly with correct parameters is Main view', async () => {
        const expectedResponse = { data: { MainData: [{ first: 1 }], Total: 1, TotalItem: 1 }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockToggleLoader = jest.fn();
        const mockSetPagingOption = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoader: mockToggleLoader, setPagingOption: mockSetPagingOption });
        const componentInstance = new GridData(props);
        componentInstance.setState = jest.fn();
        componentInstance._isMainView = jest.fn(() => true);
        componentInstance.cachedMainReport = 'cachedMainReport';

        const param = Object.assign({}, defaultProps.param, { PageIndex: 1, PageSize: 20 });
        await componentInstance.fetchData(param);

        expect(componentInstance._isMainView).toHaveBeenCalled();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();

        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ first: 1 }], total: 1 });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
        expect(componentInstance.cachedMainReport).toBeNull();
    });

    it('fetchData - should call correctly with correct parameters is Month on Month view', async () => {
        const expectedResponse = { data: { MainData: [{ first: 1 }], Total: 1, TotalItem: 1 }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const mockToggleLoader = jest.fn();
        const mockSetPagingOption = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoader: mockToggleLoader, setPagingOption: mockSetPagingOption });
        const componentInstance = new GridData(props);
        componentInstance.setState = jest.fn();
        componentInstance._isMainView = jest.fn(() => false);
        componentInstance.cachedMainReport = 'cachedMainReport';

        const param = Object.assign({}, defaultProps.param, { PageIndex: 1, PageSize: 20 });
        await componentInstance.fetchData(param);

        expect(componentInstance._isMainView).toHaveBeenCalled();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
        expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();

        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ first: 1 }], total: 1 });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
        expect(componentInstance.cachedMainReport).toEqual('cachedMainReport');
    });

    it('should render correctly with _renderHeader when currentViewLevel is Main view', () => {
        const componentInstance = new GridData(defaultProps);

        const renderHeader = shallow(<div>{componentInstance._renderHeader()}</div>);

        expect(renderHeader).toMatchSnapshot();
    });

    it('should render correctly with _renderHeader when currentViewLevel is Month on Month view', () => {
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const componentInstance = new GridData(props);

        const renderHeader = shallow(<div>{componentInstance._renderHeader()}</div>);

        expect(renderHeader).toMatchSnapshot();
    });

    it('_renderDataSection - should render correctly', () => {
        const dataList = [
            {
                ActiveMember: 0,
                AffAdjustment: 0,
                AffId: 927841,
                AffWithdrawal: 0,
                BalanceBf: 165,
                BankingFee: 0,
                Bonus: 0,
                CommissionRate: 0,
                Deposit: 0,
                Description: "",
                Earning: 0,
                Fdm: 0,
                MonthDate: null,
                MonthDateString: "",
                OperationalFee: 0,
                PlatformFee: 0,
                QualifiedMember: 0,
                Signup: 0,
                TotalClick: 0,
                TotalFee: 0,
                Turnover: 0,
                UniqueClick: 0,
                UserName: "12BETAFF001",
                Winlost: 0,
                Withdrawal: 0,
            }
        ];
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth });
        const componentInstance = new GridData(props);

        const renderDataSection = shallow(<div>{componentInstance._renderDataSection(dataList)}</div>);

        expect(renderDataSection).toMatchSnapshot();
    });

    it('_renderDataSection - should render correctly with ProductIds equal allProduct', () => {
        const dataList = [
            {
                ActiveMember: 0,
                AffAdjustment: 0,
                AffId: 927841,
                AffWithdrawal: 0,
                BalanceBf: 165,
                BankingFee: 0,
                Bonus: 0,
                CommissionRate: 0,
                Deposit: 0,
                Description: "",
                Earning: 0,
                Fdm: 0,
                MonthDate: null,
                MonthDateString: "",
                OperationalFee: 0,
                PlatformFee: 0,
                QualifiedMember: 0,
                Signup: 0,
                TotalClick: 0,
                TotalFee: 0,
                Turnover: 0,
                UniqueClick: 0,
                UserName: "12BETAFF001",
                Winlost: 0,
                Withdrawal: 0,
            }
        ];
        const props = Object.assign({}, defaultProps, { currentViewLevel: monthOnMonth, param: { ProductIds: '0' } });
        const componentInstance = new GridData(props);

        const renderDataSection = shallow(<div>{componentInstance._renderDataSection(dataList)}</div>);

        expect(renderDataSection).toMatchSnapshot();
    });

    it('_buildDataRow - should render correctly is Main view', () => {
        const dataItem = {
            ActiveMember: 0,
            AffAdjustment: 0,
            AffId: 0,
            AffWithdrawal: 0,
            BalanceBf: 803935.7199,
            BankingFee: 0,
            Bonus: 0,
            CommissionRate: 0,
            Deposit: 0,
            Description: "",
            Earning: 0,
            Fdm: 0,
            MonthDate: null,
            MonthDateString: "",
            OperationalFee: 0,
            PlatformFee: 0,
            QualifiedMember: 0,
            Signup: 0,
            TotalClick: 1,
            TotalFee: 0,
            Turnover: 0,
            UniqueClick: 1,
            UserName: "",
            Winlost: 0,
            Withdrawal: 0,
        };
        const index = 1;

        const componentInstance = new GridData(defaultProps);
        componentInstance._isMainView = jest.fn(() => true);

        const buildDataRow = shallow(<div>{componentInstance._buildDataRow(dataItem, 'text-right', index)}</div>);

        expect(buildDataRow).toMatchSnapshot();
    });

    it('_buildDataRow - should render correctly is Month on Month view', () => {
        const dataItem = {
            ActiveMember: 0,
            AffAdjustment: 0,
            AffId: 968222,
            AffWithdrawal: 0,
            BalanceBf: 803935.7199,
            BankingFee: 0,
            Bonus: 0,
            CommissionRate: 0,
            Deposit: 0,
            Description: "",
            Earning: 0,
            Fdm: 0,
            MonthDate: '12/26/2018',
            MonthDateString: "",
            OperationalFee: 0,
            PlatformFee: 0,
            QualifiedMember: 0,
            Signup: 0,
            TotalClick: 1,
            TotalFee: 0,
            Turnover: 0,
            UniqueClick: 1,
            UserName: "12BETAFF002",
            Winlost: 0,
            Withdrawal: 0,
        };
        const index = 1;

        const componentInstance = new GridData(defaultProps);
        componentInstance._isMainView = jest.fn(() => false);

        const buildDataRow = shallow(<div>{componentInstance._buildDataRow(dataItem, 'text-right', index)}</div>);

        expect(buildDataRow).toMatchSnapshot();
    });

    it('_buildDataRow - should call handleViewMonthOnMonth when click button view', () => {
        const dataItem = {
            ActiveMember: 0,
            AffAdjustment: 0,
            AffId: 968222,
            AffWithdrawal: 0,
            BalanceBf: 803935.7199,
            BankingFee: 0,
            Bonus: 0,
            CommissionRate: 0,
            Deposit: 0,
            Description: "",
            Earning: 0,
            Fdm: 0,
            MonthDate: null,
            MonthDateString: "",
            OperationalFee: 0,
            PlatformFee: 0,
            QualifiedMember: 0,
            Signup: 0,
            TotalClick: 1,
            TotalFee: 0,
            Turnover: 0,
            UniqueClick: 1,
            UserName: "12BETAFF002",
            Winlost: 0,
            Withdrawal: 0,
        };
        const index = 1;

        const componentInstance = new GridData(defaultProps);
        componentInstance._isMainView = jest.fn(() => true);
        componentInstance.handleViewMonthOnMonth = jest.fn();

        const buildDataRow = shallow(<div>{componentInstance._buildDataRow(dataItem, 'text-right', index)}</div>);

        buildDataRow.find('Button').simulate('click');

        expect(componentInstance.handleViewMonthOnMonth).toHaveBeenCalledWith("12BETAFF002");
    });

    it('_buildMonthCell - should return correctly with correct paramaters', () => {
        const monthDate = '11/29/2018';
        const description = '_buildMonthCell';

        const componentInstance = new GridData(defaultProps);
        const result = componentInstance._buildMonthCell(monthDate, description);

        expect(result).toEqual('11/29/2018 (_buildMonthCell)');
    });

    it('_buildMonthCell - should return correctly with Description paramater is empty', () => {
        const monthDate = '11/29/2018';
        const description = '';

        const componentInstance = new GridData(defaultProps);
        const result = componentInstance._buildMonthCell(monthDate, description);

        expect(result).toEqual('11/29/2018');
    });

    it('_buildCommonCell - should return correctly with correct paramaters', () => {
        const dataItem = {
            ActiveMember: 0,
            AffAdjustment: 0,
            AffId: 968222,
            AffWithdrawal: 0,
            BalanceBf: 803935.7199,
            BankingFee: 0,
            Bonus: 0,
            CommissionRate: 0,
            Deposit: 0,
            Description: "",
            Earning: 0,
            Fdm: 0,
            MonthDate: null,
            MonthDateString: "",
            OperationalFee: 0,
            PlatformFee: 0,
            QualifiedMember: 0,
            Signup: 0,
            TotalClick: 1,
            TotalFee: 0,
            Turnover: 0,
            UniqueClick: 1,
            UserName: "12BETAFF002",
            Winlost: 0,
            Withdrawal: 0,
        };
        const componentInstance = new GridData(defaultProps);

        const buildCommonCell = shallow(<div>{componentInstance._buildCommonCell(dataItem, 'text-right', false)}</div>);

        expect(buildCommonCell).toMatchSnapshot();
    });

    it('handleViewMonthOnMonth - should setViewLevel, setParam with correct paramaters', () => {
        const monthOnMonth = 2;
        const userName = 'handleViewMonthOnMonth';
        const mockSetViewLevel = jest.fn();
        const mockSetParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setViewLevel: mockSetViewLevel, setParam: mockSetParam });
        const componentInstance = new GridData(props);
        componentInstance.handleViewMonthOnMonth(userName);

        expect(props.setViewLevel).toHaveBeenCalledWith(monthOnMonth);
        expect(props.setParam).toHaveBeenCalledWith({ PageIndex: 1, PageSize: 20, SortDirectionType: 1, SortType: 1, UserName: "handleViewMonthOnMonth" });
    });

    it('handlePaging - should call setPagingOption and fetchData with correct parameters', () => {
        const mockSetPagingOption = jest.fn();

        const index = 1;
        const size = 20;
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };
        const paramExpect = Object.assign({}, defaultProps.param, newPaging);

        const props = Object.assign({}, defaultProps, { setPagingOption: mockSetPagingOption });
        const componentInstance = new GridData(props);
        componentInstance.fetchData = jest.fn();

        componentInstance.handlePaging(index, size);

        expect(props.setPagingOption).toHaveBeenCalledWith(newPaging);
        expect(componentInstance.fetchData).toHaveBeenCalledWith(paramExpect);
    });

    it('should render correctly', () => {
        const total = 2;
        const dataList = [
            {
                ActiveMember: 0,
                AffAdjustment: 0,
                AffId: 927841,
                AffWithdrawal: 0,
                BalanceBf: 165,
                BankingFee: 0,
                Bonus: 0,
                CommissionRate: 0,
                Deposit: 0,
                Description: "",
                Earning: 0,
                Fdm: 0,
                MonthDate: null,
                MonthDateString: "",
                OperationalFee: 0,
                PlatformFee: 0,
                QualifiedMember: 0,
                Signup: 0,
                TotalClick: 0,
                TotalFee: 0,
                Turnover: 0,
                UniqueClick: 0,
                UserName: "12BETAFF001",
                Winlost: 0,
                Withdrawal: 0,
            },
            {
                ActiveMember: 0,
                AffAdjustment: 0,
                AffId: 968222,
                AffWithdrawal: 0,
                BalanceBf: 803935.7199,
                BankingFee: 0,
                Bonus: 0,
                CommissionRate: 0,
                Deposit: 0,
                Description: "",
                Earning: 0,
                Fdm: 0,
                MonthDate: null,
                MonthDateString: "",
                OperationalFee: 0,
                PlatformFee: 0,
                QualifiedMember: 0,
                Signup: 0,
                TotalClick: 1,
                TotalFee: 0,
                Turnover: 0,
                UniqueClick: 1,
                UserName: "12BETAFF002",
                Winlost: 0,
                Withdrawal: 0,
            }
        ];

        const wrapper = shallow(<GridData {...defaultProps} />);
        wrapper.instance().setState({ gridData: dataList, total });

        expect(wrapper).toMatchSnapshot();
    });
});