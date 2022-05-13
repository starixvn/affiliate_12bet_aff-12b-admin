import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

describe('offer page - components - Content ', () => {
    const gridData = [
        {
            FileName: "test.html",
            FileSize: 475,
            EffectiveFromDate: "2021-04-01T00:00:00",
            Language: "English (India)",
            PageId: 25,
            EffectiveToDate: "2021-04-30T00:00:00",
            UploadDate: "2021-03-15T00:43:51.243"
        },
        {
            FileName: "test2.html",
            FileSize: 17722,
            EffectiveFromDate: "2021-02-28T13:00:00",
            Language: "English",
            PageId: 23,
            EffectiveToDate: "2021-04-29T13:00:00",
            UploadDate: "2021-03-05T04:03:36.013"
        },
        {
            FileName: "test1.html",
            FileSize: 111,
            EffectiveFromDate: "2021-03-30T02:00:00",
            Language: "English (India)",
            PageId: 22,
            EffectiveToDate: "2021-04-28T00:00:01",
            UploadDate: "2021-03-04T23:14:51.38"
        }
    ];

    const defaultProps = {
        checkedIds: { 25: false, 23: false, 22: true },
        param: { pageIndex: 1, pageSize: 20 },
        pagingOption: { PageIndex: 1, PageSize: 20, Total: 0 },
        previewLink: 'https://aff-12bet.nexdev.net/12BetUserAsianNew/',
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

    it('onCheckItem - should be called when click checkbox', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.onCheckItem = jest.fn();

        const dataRow = shallow(<div>{instance.buildDataRow(gridData[0], 1)}</div>);

        dataRow.find("input").at(0).simulate('change', { target: { checked: true } });

        expect(instance.onCheckItem).toHaveBeenCalledWith(true, 25);
    });

    it('handlePreview - should be called when click preview button', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        instance.handlePreview = jest.fn();

        const dataRow = shallow(<div>{instance.buildDataRow(gridData[0], 1)}</div>);

        dataRow.find("Button").at(0).simulate('click');

        expect(instance.handlePreview).toHaveBeenCalledWith(25);
    });

    it('setSelectedId - should be called when click edit button', () => {
        const props = Object.assign({}, defaultProps, { setSelectedId: jest.fn() });
        const result = shallow(<Content {...props} />);
        const instance = result.instance();

        const dataRow = shallow(<div>{instance.buildDataRow(gridData[0], 1)}</div>);

        dataRow.find("Button").at(1).simulate('click');

        expect(props.setSelectedId).toHaveBeenCalledWith(25);
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

        expect(props.setParam).toHaveBeenCalledWith({ pageIndex: 2, pageSize: 20 });
    });

    it('handlePreview - should call window.open', () => {
        const result = shallow(<Content {...defaultProps} />);
        const instance = result.instance();
        window.open = jest.fn();

        instance.handlePreview(25);

        expect(window.open).toHaveBeenCalledWith("https://aff-12bet.nexdev.net/12BetUserAsianNew/?id=25");
    });
})