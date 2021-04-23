import React from 'react';
import { DetailContent } from '../DetailContent';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('new view log - components - DetailContent', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        detailViewParam: {
            affId: 1271808,
            affName: '12BETAFF226',
            logActionIds: '',
            fromDate: "06/06/2019",
            toDate: "06/06/2019"
        },
        setPagingOption: jest.fn(),
        toggleLoader: jest.fn(),
        fetchDetailData: jest.fn(() => gridData)
    }

    const gridData = [
        {
            LogId: 10279106,
            UpdatedName: "AAwalter",
            OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
            NewData: "{\"root\":{\"row\":{\"@PageName\":\"1122111\",\"@PageCode\":\"1111\"}}}",
            Action: "User Info",
            Ip: "::1",
            DateText: "06/06/2019 03:42:42 AM"
        },
        {
            LogId: 10279105,
            UpdatedName: "AAwalter",
            OldData: "",
            NewData: "{\"root\":{\"row\":{\"@PageName\":\"111111\",\"@PageCode\":\"111\"}}}",
            Action: "User Info",
            Ip: "::1",
            DateText: "06/06/2019 03:42:38 AM"
        },
        {
            LogId: 10279104,
            UpdatedName: "AAwalter",
            OldData: "{\"root\":{\"row\":{\"@PageName\":\"11111\",\"@PageCode\":\"1111\"}}}",
            NewData: "",
            Action: "Setting for Commission Group",
            Ip: "::1",
            DateText: "06/06/2019 03:42:38 AM"
        },
        {
            LogId: 10279103,
            UpdatedName: "AAwalter",
            OldData: "",
            NewData: "",
            Action: "Setting for Commission Group",
            Ip: "::1",
            DateText: "06/06/2019 03:42:38 AM"
        }
    ]

    it('render correctly', () => {
        const result = shallow(<DetailContent {...defaultProps} />);

        result.instance().setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have data is empty', () => {
        const result = shallow(<DetailContent {...defaultProps} />);

        result.instance().setState({ gridData: [] });

        expect(result).toMatchSnapshot();
    });

    it('setSelectedAff - should be called when click Breadcrumb', () => {
        const props = Object.assign({}, defaultProps, { setSelectedAff: jest.fn() });
        const wrapper = shallow(<DetailContent {...props} />);
        wrapper.instance().setState({ gridData: [] });

        wrapper.find(MyLink).props().onClick();

        expect(props.setSelectedAff).toHaveBeenCalledWith('');
    });

    it('componentDidMount - should not call fetchData', () => {
        const props = Object.assign({}, defaultProps, { detailViewParam: null });
        const componentInstance = new DetailContent(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidMount();

        expect(componentInstance.fetchData).not.toHaveBeenCalled();
    });

    it('componentDidUpdate - should call fetchData if detailViewParam change', () => {
        const detailViewParam = Object.assign({}, defaultProps.detailViewParam, { toDate: "6/27/2017" });
        const props = Object.assign({}, defaultProps, { detailViewParam });
        const componentInstance = new DetailContent(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.fetchData).toHaveBeenCalledWith({ ...detailViewParam, pageIndex: 1, pageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with correct parameter', async () => {
        const data = { MainData: [{ Id: 123 }], TotalItem: 1 };

        const toggleLoader = jest.fn();
        const setPagingOption = jest.fn();
        const fetchDetailData = jest.fn(() => data);

        const props = Object.assign({}, defaultProps, { toggleLoader, setPagingOption, fetchDetailData });
        const componentInstance = new DetailContent(props);
        componentInstance.setState = jest.fn();
        const detailViewParam = Object.assign({}, defaultProps.detailViewParam, { pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(detailViewParam);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.fetchDetailData).toHaveBeenCalledWith(detailViewParam);
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: [{ Id: 123 }], "shouldResetScrollPosition": true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 1, PageIndex: 1, PageSize: 20 });
    });

    it('fetchData - should call toggleLoader, setState and setPagingOption with incorrect parameter', async () => {
        const toggleLoader = jest.fn();
        const setPagingOption = jest.fn();
        const fetchDetailData = jest.fn();

        const props = Object.assign({}, defaultProps, { toggleLoader, setPagingOption, fetchDetailData });
        const componentInstance = new DetailContent(props);
        componentInstance.setState = jest.fn();
        const detailViewParam = Object.assign({}, defaultProps.detailViewParam, { memberType: '-1', pageIndex: 1, pageSize: 20 });
        await componentInstance.fetchData(detailViewParam);

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.toggleLoader.mock.calls[0][0]).toBe(true);
        expect(props.toggleLoader.mock.calls[1][0]).toBe(false);

        expect(props.fetchDetailData).toHaveBeenCalledWith(detailViewParam);
        expect(componentInstance.setState).toHaveBeenCalledWith({ gridData: null, "shouldResetScrollPosition": true });
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 0, PageIndex: 1, PageSize: 20 });
    });

    it('handlePaging - should call fetchData with correct parameters', () => {
        const setPagingOption = jest.fn();
        const pageIndex = 2;
        const pageSize = 50;
        const paramExpect = Object.assign({}, defaultProps.detailViewParam, { pageIndex, pageSize });

        const props = Object.assign({}, defaultProps, { setPagingOption });
        const componentInstance = new DetailContent(props);
        componentInstance.fetchData = jest.fn();

        componentInstance.handlePaging(pageIndex, pageSize);

        expect(componentInstance.fetchData).toHaveBeenCalledWith(paramExpect);
    });
});