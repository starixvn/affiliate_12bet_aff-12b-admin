import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('statement history - components - Content', () => {
    var param = {
        fromDate: "1/11/2019",
        isBack: false,
        isGetAll: false,
        toDate: "7/11/2019",
        scrollTop: 0
    };

    const defaultProps = {
        affId: 1271808,
        pagingOption: defaultPagingOption,
        expandMonthDateList: {},
        param
    }

    const gridData = [
        {
            Amount: 0,
            Creator: "System",
            DateText: "Jun-2019",
            NormalizeDateText: "Jun-2019",
            Description: "Monthly Total",
            Status: "B/F",
            TransId: 0,
            TransType: 0,
            Children: [
                {
                    Amount: 0,
                    Creator: "System",
                    DateText: "Jun-2019",
                    NormalizeDateText: "Jun-2019",
                    Description: "Monthly Earning",
                    Status: "B/F",
                    TransId: 0,
                    TransType: 201,
                    Children: []
                }
            ]
        },
        {
            Amount: 125,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            NormalizeDateText: "Jun-2019",
            Description: "Admin Note",
            Status: "Pending",
            TransId: 1262276211,
            TransType: 202,
            Children: []
        },
        {
            Amount: -100,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            NormalizeDateText: "Jun-2019",
            Description: "",
            Status: "B/F",
            TransId: 1262276210,
            TransType: 203,
            Children: []
        },
        {
            Amount: 105,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            NormalizeDateText: "Jun-2019",
            Description: "",
            Status: "B/F",
            TransId: 1262276209,
            TransType: 0,
            Children: []
        },
        {
            Amount: 105,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            NormalizeDateText: "Jun-2019",
            Description: "",
            Status: "B/F",
            TransId: 1262276208,
            TransType: -1,
            Children: []
        }
    ]

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have data', () => {
        const result = shallow(<Content {...defaultProps} />);

        result.instance().setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have data is empty', () => {
        const result = shallow(<Content {...defaultProps} />);

        result.instance().setState({ gridData: {} });

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call fetchData if param change', () => {
        const param = Object.assign({}, defaultProps.param, { toDate: "7/10/2019" });
        const props = Object.assign({}, defaultProps, { param });
        const componentInstance = new Content(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.fetchData).toHaveBeenCalledWith({ ...param, pageIndex: 1, pageSize: 20 }, false);
    });

    it('fetchData - should call toggleLoaderUserPanel, setState and setPagingOption with correct parameter', async () => {
        const data = { MainData: [{ Id: 123 }], TotalItem: 1 };

        const toggleLoaderUserPanel = jest.fn();
        const setPagingOption = jest.fn();
        const setExpandMonthDateList = jest.fn();
        const fetchData = jest.fn(() => data);

        const props = Object.assign({}, defaultProps, { toggleLoaderUserPanel, setPagingOption, fetchData, setExpandMonthDateList });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const param = Object.assign({}, defaultProps.param, { pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(param);

        expect(props.toggleLoaderUserPanel).toHaveBeenCalledTimes(2);
        expect(props.toggleLoaderUserPanel.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoaderUserPanel.mock.calls[1][0]).toBe(false);

        const { isBack, scrollTop, ...remain } = param;
        expect(props.fetchData).toHaveBeenCalledWith({ ...remain, affId: 1271808 });
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ Id: 123 }], shouldResetScrollPosition: true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with incorrect parameter', async () => {
        const toggleLoaderUserPanel = jest.fn();
        const setPagingOption = jest.fn();
        const fetchData = jest.fn();
        const setExpandMonthDateList = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoaderUserPanel, setPagingOption, fetchData, setExpandMonthDateList });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const param = Object.assign({}, defaultProps.param, { pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(param);

        expect(props.toggleLoaderUserPanel).toHaveBeenCalledTimes(2);
        expect(props.toggleLoaderUserPanel.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoaderUserPanel.mock.calls[1][0]).toBe(false);

        const { isBack, scrollTop, ...remain } = param;
        expect(props.fetchData).toHaveBeenCalledWith({ ...remain, affId: 1271808 });
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: null, shouldResetScrollPosition: true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 0, PageIndex: 1, PageSize: 20 });
    });

    it('handleSetTransactionId - should call setState and setTransactionId when click amount payment Button', () => {
        const props = Object.assign({}, defaultProps, { setTransactionId: jest.fn() });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[1], 2)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(wrapper.setState).toHaveBeenCalledWith({ shouldResetScrollPosition: false });
        expect(props.setTransactionId).toHaveBeenCalledWith(1262276211);
    });

    it('transTypeClickHandler - should call setExpandMonthDateList when click expand Button', () => {
        const props = Object.assign({}, defaultProps, { setExpandMonthDateList: jest.fn() });
        const wrapper = shallow(<Content {...props} />);

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[0], 1)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(props.setExpandMonthDateList).toHaveBeenCalledWith({ "Jun-2019": true });
    });

    it('transTypeClickHandler - should call redirectToDailyEarning when click amount Button', () => {
        const props = Object.assign({}, defaultProps, { expandMonthDateList: { "Jun-2019": true }, redirectToDailyEarning: jest.fn(), setExpandMonthDateList: jest.fn() });
        const wrapper = shallow(<Content {...props} />);

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[0], 1)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(props.redirectToDailyEarning).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jun-2019", productId: 0, statementHistory: { expandMonthDateList: { "Jun-2019": true }, param, scrollTop: 0 } });
    });

    it('handlePaging - should call fetchData with correct parameters', () => {
        const setPagingOption = jest.fn();
        const pageIndex = 2;
        const pageSize = 50;
        const paramExpect = Object.assign({}, defaultProps.param, { pageIndex, pageSize });

        const props = Object.assign({}, defaultProps, { setPagingOption });
        const componentInstance = new Content(props);
        componentInstance.fetchData = jest.fn();

        componentInstance.handlePaging(pageIndex, pageSize);

        expect(componentInstance.fetchData).toHaveBeenCalledWith(paramExpect);
    });
});