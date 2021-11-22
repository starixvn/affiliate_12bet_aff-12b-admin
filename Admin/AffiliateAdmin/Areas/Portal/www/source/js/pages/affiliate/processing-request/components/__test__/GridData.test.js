import React from 'react';
import { GridData } from '../GridData';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('processing request - components - GridData ', () => {
    const defaultProps = {
        today: '11/15/2021',
        pagingOption: defaultPagingOption,
        claimsPrincipal: { hasWriteClaim: jest.fn(() => true) },
        setIsClickSearch: jest.fn(),
        param: {
            Status: '',
            Username: ''
        },
        gridData
    }

    const gridData = [
        {
            "RequestId": 2036,
            "AffId": 989900,
            "UserName": "12BETAFF003",
            "CreatedBy": "AAnina",
            "CreationDate": "2021-09-07T22:53:49.117",
            "LastModified": "2021-09-09T04:53:13.573",
            "SubmittedDate": null,
            "Deadline": "2021-11-18T00:00:00",
            "ApprovedBy": null,
            "ApprovalDate": null,
            "Status": "Submitted"
        },
        {
            "RequestId": 5,
            "AffId": 989900,
            "UserName": "12BETAFF003",
            "CreatedBy": "AAsa",
            "CreationDate": "2021-08-27T07:01:00",
            "LastModified": null,
            "SubmittedDate": "2021-08-27T07:01:00",
            "Deadline": "2021-08-27T07:01:00",
            "ApprovedBy": "AAsa",
            "ApprovalDate": "2021-08-27T07:01:00",
            "Status": "Draft"
        }
    ]

    it('render correctly', () => {
        const result = shallow(<GridData {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have gridData is empty', () => {
        const props = { ...defaultProps, gridData: [] };
        const result = shallow(<GridData {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call fetchData if param change', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const componentInstance = new GridData(props);
        componentInstance.componentDidUpdate({});

        expect(props.fetchData).toHaveBeenCalledWith({ ...defaultProps.param, PageIndex: 1, PageSize: 20 });
    });

    it('componentDidUpdate - should not call fetchData if param not change', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const componentInstance = new GridData(props);
        componentInstance.componentDidUpdate(props);

        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('handleDeleteProcess - should be called when click delete Button', () => {
        const wrapper = shallow(<GridData {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleDeleteProcess = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(instance.handleDeleteProcess).toHaveBeenCalledWith(2036);
    });

    it('handleDeleteProcess - should call setSelectedRequestId and handleDelete', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequestId: jest.fn(), handleDelete: jest.fn() });
        const wrapper = shallow(<GridData {...props} />);

        wrapper.instance().handleDeleteProcess(123);

        expect(props.setSelectedRequestId).toHaveBeenCalledWith(123);
        expect(props.handleDelete).toHaveBeenCalled();
    });

    it('handleEditProcess - should be called when click edit Button', () => {
        const props = { ...defaultProps, claimsPrincipal: { hasWriteClaim: jest.fn((claim) => (claim === '[ProcessingRequestMarketingRole]' || claim === '[ProcessingRequest]') ? true : false) } };
        const wrapper = shallow(<GridData {...props} />);
        const instance = wrapper.instance();
        instance.handleEditProcess = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(1).simulate('click');

        expect(instance.handleEditProcess).toHaveBeenCalledWith(5);
    });

    it('handleEditProcess - should call setSelectedRequestId and handleEdit', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequestId: jest.fn(), handleEdit: jest.fn() });
        const wrapper = shallow(<GridData {...props} />);

        wrapper.instance().handleEditProcess(123);

        expect(props.setSelectedRequestId).toHaveBeenCalledWith(123);
        expect(props.handleEdit).toHaveBeenCalledWith(123);
    });

    it('handleViewProcess - should be called when click view Button', () => {
        const props = { ...defaultProps, claimsPrincipal: { hasWriteClaim: jest.fn(() => false) } };
        const wrapper = shallow(<GridData {...props} />);
        const instance = wrapper.instance();
        instance.handleViewProcess = jest.fn();

        const dataRow = shallow(<div>{wrapper.instance()._renderDataSection(gridData)}</div>);

        dataRow.find('Button').at(0).simulate('click');

        expect(instance.handleViewProcess).toHaveBeenCalledWith(2036);
    });

    it('handleViewProcess - should call setSelectedRequestId and handleView', () => {
        const props = Object.assign({}, defaultProps, { setSelectedRequestId: jest.fn(), handleView: jest.fn() });
        const wrapper = shallow(<GridData {...props} />);

        wrapper.instance().handleViewProcess(123);

        expect(props.setSelectedRequestId).toHaveBeenCalledWith(123);
        expect(props.handleView).toHaveBeenCalledWith(123);
    });

    it('handlePaging - should call fetchData with correct parameters', () => {
        const PageIndex = 2;
        const PageSize = 50;
        const paramExpect = Object.assign({}, defaultProps.param, { PageIndex, PageSize });

        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const componentInstance = new GridData(props);

        componentInstance.handlePaging(PageIndex, PageSize);

        expect(props.fetchData).toHaveBeenCalledWith(paramExpect);
    });
});