import React from 'react';
import { GridData } from '../GridData';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('feedback report - components - GridData ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        param: {
            FromDate: "6/28/2019",
            PageIndex: 1,
            PageSize: 20,
            RateIds: '1',
            AffName: "",
            ToDate: "6/28/2019"
        }
    }

    const gridData = [
        {
            AffName: "12BETAFF12885",
            RateName: "None",
            Message: "te@te1.com",
            SentDate: "12/14/2018 03:11:25"
        },
        {
            AffName: "12BETAFF226",
            RateName: "Very Please",
            Message: "maia.le4@starixsoft.com",
            SentDate: "10/07/2018 23:14:09"
        }
    ]

    it('render correctly', () => {
        const result = shallow(<GridData {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData', () => {
        const result = shallow(<GridData {...defaultProps} />);
        result.setState({ gridData });

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData is empty', () => {
        const result = shallow(<GridData {...defaultProps} />);
        result.setState({ gridData: [] });

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call fetchData if param change', () => {
        const param = Object.assign({}, defaultProps.param, { RateIds: '' });
        const props = Object.assign({}, defaultProps, { param });
        const componentInstance = new GridData(props);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.fetchData).toHaveBeenCalledWith(param);
    });

    it('componentDidUpdate - should not call fetchData if param not change', () => {
        const componentInstance = new GridData(defaultProps);
        componentInstance.fetchData = jest.fn();
        componentInstance.componentDidUpdate(defaultProps);

        expect(componentInstance.fetchData).not.toHaveBeenCalled();
    });

    it('fetchData - should only call fetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), setPagingOption: jest.fn(), toggleLoader: jest.fn() });
        const componentInstance = new GridData(props);
        await componentInstance.fetchData(defaultProps.param);

        expect(props.setPagingOption).not.toHaveBeenCalled();
        expect(props.fetchData).toHaveBeenCalledWith(defaultProps.param);
    });

    it('fetchData - should call fetchData, setState and setPagingOption', async () => {
        const props = Object.assign({}, defaultProps, {
            fetchData: jest.fn(() => ({
                MainData: gridData,
                TotalItem: 2
            })), setPagingOption: jest.fn(),
            toggleLoader: jest.fn()
        });
        const componentInstance = new GridData(props);
        componentInstance.setState = jest.fn();
        await componentInstance.fetchData(defaultProps.param);

        expect(props.fetchData).toHaveBeenCalledWith(defaultProps.param);
        expect(props.setPagingOption).toHaveBeenCalledWith({ TotalItem: 2, PageIndex: 1, PageSize: 20 });
    });

    it('handlePaging - should call fetchData with correct parameters', () => {
        const setPagingOption = jest.fn();
        const setParam = jest.fn();
        const PageIndex = 2;
        const PageSize = 50;
        const paramExpect = Object.assign({}, defaultProps.param, { PageIndex, PageSize });

        const props = Object.assign({}, defaultProps, { setPagingOption, setParam });
        const componentInstance = new GridData(props);
        componentInstance.fetchData = jest.fn();

        componentInstance.handlePaging(PageIndex, PageSize);

        expect(props.setParam).toHaveBeenCalledWith(paramExpect);
    });
});