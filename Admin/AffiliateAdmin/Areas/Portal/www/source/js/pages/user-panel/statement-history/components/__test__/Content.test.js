import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('statement history - components - Content', () => {
    const defaultProps = {
        affId: 1271808,
        pagingOption: defaultPagingOption,
        param: {
            fromDate: "1/11/2019",
            isGetAll: false,
            toDate: "7/11/2019"
        }
    }

    const gridData = [
        {
            Amount: 0,
            Creator: "System",
            DateText: "Jun-2019",
            Description: "Monthly Earning",
            Status: "B/F",
            TransId: 0,
            TransType: 201
        },
        {
            Amount: 125,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            Description: "Admin Note",
            Status: "Pending",
            TransId: 1262276211,
            TransType: 202
        },
        {
            Amount: -100,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            Description: "",
            Status: "B/F",
            TransId: 1262276210,
            TransType: 203
        },
        {
            Amount: 105,
            Creator: "AAwalter",
            DateText: "Jun-2019",
            Description: "",
            Status: "Approved",
            TransId: 1262276209,
            TransType: 200
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

        expect(componentInstance.fetchData).toHaveBeenCalledWith({ ...param, pageIndex: 1, pageSize: 20 });
    });

    it('fetchData - should call toggleLoaderUserPanel, setState and setPagingOption with correct parameter', async () => {
        const data = { MainData: [{ Id: 123 }], TotalItem: 1 };

        const toggleLoaderUserPanel = jest.fn();
        const setPagingOption = jest.fn();
        const fetchData = jest.fn(() => data);

        const props = Object.assign({}, defaultProps, { toggleLoaderUserPanel, setPagingOption, fetchData });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const param = Object.assign({}, defaultProps.param, { pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(param);

        expect(props.toggleLoaderUserPanel).toHaveBeenCalledTimes(2);
        expect(props.toggleLoaderUserPanel.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoaderUserPanel.mock.calls[1][0]).toBe(false);

        expect(props.fetchData).toHaveBeenCalledWith({ ...param, affId: 1271808 });
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ Id: 123 }], shouldResetScrollPosition: true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with incorrect parameter', async () => {
        const toggleLoaderUserPanel = jest.fn();
        const setPagingOption = jest.fn();
        const fetchData = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoaderUserPanel, setPagingOption, fetchData });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const param = Object.assign({}, defaultProps.param, { memberType: '-1', pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(param);

        expect(props.toggleLoaderUserPanel).toHaveBeenCalledTimes(2);
        expect(props.toggleLoaderUserPanel.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoaderUserPanel.mock.calls[1][0]).toBe(false);

        expect(props.fetchData).toHaveBeenCalledWith({ ...param, affId: 1271808 });
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: null, shouldResetScrollPosition: true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 0, PageIndex: 1, PageSize: 20 });
    });

    it('handleSetTransactionId - should be called when click transId Button', () => {
        const wrapper = shallow(<Content {...defaultProps} />);

        wrapper.instance().handleSetTransactionId = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[1], 2)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(wrapper.instance().handleSetTransactionId).toHaveBeenCalledWith(1262276211);
    });

    it('handleSetTransactionId - should call setState and setTransactionId', () => {
        const props = Object.assign({}, defaultProps, { setTransactionId: jest.fn() });
        const wrapper = shallow(<Content {...props} />);
        wrapper.setState = jest.fn();

        wrapper.instance().handleSetTransactionId(1262276211);

        expect(wrapper.setState).toHaveBeenCalledWith({ shouldResetScrollPosition: false });
        expect(props.setTransactionId).toHaveBeenCalledWith(1262276211);
    });

    it('transTypeClickHandler - should call handleSetTransactionId when click amount payment Button', () => {
        const wrapper = shallow(<Content {...defaultProps} />);

        wrapper.instance().handleSetTransactionId = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[1], 2)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(wrapper.instance().handleSetTransactionId).toHaveBeenCalledWith(1262276211);
    });

    it('transTypeClickHandler - should call redirectToDailyEarning when click amount payment Button', () => {
        const props = Object.assign({}, defaultProps, { redirectToDailyEarning: jest.fn() });
        const wrapper = shallow(<Content {...props} />);

        wrapper.instance().handleSetTransactionId = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._buildDataRow(gridData[0], 1)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(props.redirectToDailyEarning).toHaveBeenCalledWith({ affId: 1271808, monthDate: "Jun-2019", productId: 0 });
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