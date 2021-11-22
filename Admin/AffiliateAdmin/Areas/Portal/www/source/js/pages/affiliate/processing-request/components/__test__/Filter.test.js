import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('processing request - components - Filter - ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        IsNeedInitSearch: false,
        param: {}
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('handleClickSearch - should be called when IsNeedInitSearch is true', () => {
        const expectParam = {
            Status: '',
            IsNeedInitSearch: false,
            UserName: ''
        }

        const props = Object.assign({}, defaultProps, { setReportTitle: jest.fn(), setParam: jest.fn(), setIsClickSearch: jest.fn(), fetchData: jest.fn(), param: { IsNeedInitSearch: true, UserName: '' } });

        shallow(<Filter {...props} />);

        expect(props.setReportTitle).toHaveBeenCalledWith('PROCESSING REQUEST REPORT - ALL STATUSES');
        expect(props.setParam).toHaveBeenCalledWith(expectParam);
        expect(props.setIsClickSearch).toHaveBeenCalledWith(true);
    });

    it('handleClickSearch - should call _buildReportTitle with selected status', () => {
        const expectParam = {
            Status: 'Draft',
            IsNeedInitSearch: false,
            UserName: 'Nova001'
        }

        const props = Object.assign({}, defaultProps, { setReportTitle: jest.fn(), setParam: jest.fn(), setIsClickSearch: jest.fn(), fetchData: jest.fn() });

        const wrapper = shallow(<Filter {...props} />);
        const instance = wrapper.instance();
        instance.setState({ selectedStatus: 'Draft', userName: 'Nova001' })

        instance.handleClickSearch();

        expect(props.setReportTitle).toHaveBeenCalledWith(`PROCESSING REQUEST REPORT - USERNAME CONTAINS 'Nova001' - DRAFT`);
        expect(props.setParam).toHaveBeenCalledWith(expectParam);
        expect(props.setIsClickSearch).toHaveBeenCalledWith(true);
    });

    it("handleChange - should call setState", () => {
        const mockedEvent = { target: { name: 'selectedStatus', value: 'Draft' } }
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.handleChange(mockedEvent);

        expect(instance.setState).toHaveBeenCalledWith({ selectedStatus: 'Draft' });
    });

    it('handlePressEnter - should call handleClickSearch when press Enter', () => {
        const mockedEvent = { charCode: 13, preventDefault: jest.fn() };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleClickSearch = jest.fn();

        instance.handlePressEnter(mockedEvent);

        expect(instance.handleClickSearch).toHaveBeenCalled();
    });

    it('handlePressEnter - should not call handleClickSearch when press other keyboards', () => {
        const mockedEvent = { charCode: 123 };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleClickSearch = jest.fn();

        instance.handlePressEnter(mockedEvent);

        expect(instance.handleClickSearch).not.toHaveBeenCalled();
    });
})