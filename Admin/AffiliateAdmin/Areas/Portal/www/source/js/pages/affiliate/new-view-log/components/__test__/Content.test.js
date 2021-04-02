import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('new view log - components - Content', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        mainViewParam: {
            affName: '',
            logActionIds: '',
            fromDate: "06/06/2019",
            toDate: "06/06/2019"
        },
        setPagingOption: jest.fn(),
        toggleLoader: jest.fn(),
        fetchData: jest.fn(() => gridData)
    }

    const gridData = [
        {
            AffId: 10279106,
            AffName: "12BetAff001",
            Actor: "AAwalter",
            LastAction: "User Info",
            NumberOfAction: 1,
            DateTimeText: "06/06/2019 03:42:42 AM"
        },
        {
            AffId: 10279105,
            AffName: "12BetAff002",
            Actor: "AAwalter",
            LastAction: "User Info",
            NumberOfAction: 2,
            DateTimeText: "06/06/2019 03:42:38 AM"
        },
        {
            AffId: 10279104,
            AffName: "12BetAff003",
            Actor: "AAwalter",
            LastAction: "Setting for Commission Group",
            NumberOfAction: 3,
            DateTimeText: "06/06/2019 03:42:38 AM"
        },
        {
            AffId: 10279103,
            AffName: "12BetAff004",
            Actor: "AAwalter",
            LastAction: "Setting for Commission Group",
            NumberOfAction: 4,
            DateTimeText: "06/06/2019 03:42:38 AM"
        }
    ]

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        result.instance().setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have data is empty', () => {
        const result = shallow(<Content {...defaultProps} />);

        result.instance().setState({ gridData: [] });

        expect(result).toMatchSnapshot();
    });

    it('componentDidMount - should not call fetchData', () => {
        const props = Object.assign({}, defaultProps, { mainViewParam: null });
        const componentInstance = new Content(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidMount();

        expect(componentInstance.fetchData).not.toHaveBeenCalled();
    });

    it('componentDidUpdate - should call fetchData if mainViewParam change', () => {
        const mainViewParam = Object.assign({}, defaultProps.mainViewParam, { toDate: "6/27/2017" });
        const props = Object.assign({}, defaultProps, { mainViewParam });
        const componentInstance = new Content(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.fetchData).toHaveBeenCalledWith({ ...mainViewParam, pageIndex: 1, pageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with correct parameter', async () => {
        const data = { MainData: [{ Id: 123 }], TotalItem: 1 };

        const toggleLoader = jest.fn();
        const setPagingOption = jest.fn();
        const fetchData = jest.fn(() => data);

        const props = Object.assign({}, defaultProps, { toggleLoader, setPagingOption, fetchData });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const mainViewParam = Object.assign({}, defaultProps.mainViewParam, { pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(mainViewParam);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.fetchData).toHaveBeenCalledWith(mainViewParam);
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ Id: 123 }], "shouldResetScrollPosition": true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with incorrect parameter', async () => {
        const toggleLoader = jest.fn();
        const setPagingOption = jest.fn();
        const fetchData = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoader, setPagingOption, fetchData });
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        const mainViewParam = Object.assign({}, defaultProps.mainViewParam, { memberType: '-1', pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(mainViewParam);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.fetchData).toHaveBeenCalledWith(mainViewParam);
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: null, "shouldResetScrollPosition": true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 0, PageIndex: 1, PageSize: 20 });
    });

    it('_handleViewDetail - should call setDetailViewParam and setSelectedAff', () => {
        const props = Object.assign({}, defaultProps, { setDetailViewParam: jest.fn(), setSelectedAff: jest.fn() });
        const componentInstance = new Content(props);

        componentInstance._handleViewDetail(123, "ABC");

        expect(props.setDetailViewParam).toHaveBeenCalledWith({ ...defaultProps.mainViewParam, affId: 123, affName: "ABC" });
        expect(props.setSelectedAff).toHaveBeenCalledWith("ABC");
    });

    it('_handleViewDetail - should be called when click detail button', () => {
        const componentInstance = new Content(defaultProps);
        componentInstance._handleViewDetail = jest.fn();

        const wrapper = shallow(<div>{componentInstance._buildDataRow(gridData[0], 1)}</div>);

        wrapper.find("Button").simulate('click');

        expect(componentInstance._handleViewDetail).toHaveBeenCalledWith(10279106, "12BetAff001");
    });

    it('handlePaging - should call fetchData with correct parameters', () => {
        const pageIndex = 2;
        const pageSize = 50;
        const paramExpect = Object.assign({}, defaultProps.mainViewParam, { pageIndex, pageSize });

        const componentInstance = new Content(defaultProps);
        componentInstance.fetchData = jest.fn();

        componentInstance.handlePaging(pageIndex, pageSize);

        expect(componentInstance.fetchData).toHaveBeenCalledWith(paramExpect);
    });
});