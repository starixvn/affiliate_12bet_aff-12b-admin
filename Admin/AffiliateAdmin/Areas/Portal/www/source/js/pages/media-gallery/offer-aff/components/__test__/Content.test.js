import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

describe('offer aff - components - Content ', () => {
    const gridData = [
        {
            FileName: "test.html",
            AffId: 25,
            EffectiveFromDate: "2021-04-01T00:00:00",
            UserName: "AFF003",
            PageId: 25,
            EffectiveToDate: "2021-04-30T00:00:00"
        },
        {
            FileName: "test2.html",
            AffId: 23,
            EffectiveFromDate: "2021-02-28T13:00:00",
            UserName: "AFF002",
            PageId: 23,
            EffectiveToDate: "2021-04-29T13:00:00"
        },
        {
            FileName: "test1.html",
            AffId: 22,
            EffectiveFromDate: "2021-03-30T02:00:00",
            UserName: "AFF001",
            PageId: 22,
            EffectiveToDate: "2021-04-28T00:00:01"
        }
    ];

    const defaultProps = {
        checkedIds: { 25: false, 23: false, 22: true },
        param: { affiliate: '', htmlFile: '' },
        pagingOption: { PageIndex: 1, PageSize: 20, Total: 0 },
        previewLink: 'https://ha.nexdev.net:7613/12BetUserAsianNew/',
        shouldResetScrollPosition: true,
        isModifiable: true,
        gridData
    };

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is null', () => {
        const props = Object.assign({}, defaultProps, { gridData: null })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with gridData is empty', () => {
        const props = Object.assign({}, defaultProps, { gridData: [] })
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call handleFetchData when param updated', async () => {
        const props = Object.assign({}, defaultProps, { param: {} })
        const result = shallow(<Content {...props} />);
        const instance = result.instance();
        instance.handleFetchData = jest.fn();

        instance.componentDidUpdate({});

        expect(instance.handleFetchData).toHaveBeenCalled();
    });

    it('componentDidUpdate - should not call handleFetchData when param is not updated', async () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.handleFetchData = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.handleFetchData).not.toHaveBeenCalled();
    });

    it('handleFetchData - should call fetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() })
        const result = shallow(<Content {...props} />);

        result.instance().handleFetchData();

        expect(props.fetchData).toHaveBeenCalled();
    });

    it('onCheckItem - should be called when click checkbox', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.onCheckItem = jest.fn();

        const dataRow = shallow(<div>{instance.buildDataRow(gridData[0], 1)}</div>);

        dataRow.find("input").at(0).simulate('change', { target: { checked: true } });

        expect(instance.onCheckItem).toHaveBeenCalledWith(true, 25);
    });

    it('onCheckItem - should call setShouldResetScrollPosition and setCheckedIds', () => {
        const props = Object.assign({}, defaultProps, { setCheckedIds: jest.fn(), setShouldResetScrollPosition: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance.onCheckItem(true, 25);

        expect(props.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(props.setCheckedIds).toHaveBeenCalledWith({ 25: true, 23: false, 22: true });
    });

    it('_handlePaging - should call setParam', () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        instance._handlePaging(2, 20);

        expect(props.setParam).toHaveBeenCalledWith({ affiliate: '', htmlFile: '', pageIndex: 2, pageSize: 20 });
    });

    it('handlePreview - should be called when click preview button', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.handlePreview = jest.fn();

        const dataRow = shallow(<div>{instance.buildDataRow(gridData[0], 1)}</div>);

        dataRow.find("Button").at(0).simulate('click');

        expect(instance.handlePreview).toHaveBeenCalledWith(25);
    });

    it('handlePreview - should call window.open', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        window.open = jest.fn();

        instance.handlePreview(25);

        expect(window.open).toHaveBeenCalledWith("https://ha.nexdev.net:7613/12BetUserAsianNew/?id=25");
    });
})