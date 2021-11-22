import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';

import * as ajaxRequest from '../../../../../modules/ajaxRequest';
import ReportExcel from '../../libs/reportExcel';
jest.mock('../../libs/reportExcel');

describe('processing request - components - Content - ', () => {
    const dataSummary = { "Draft": 2, "Submitted": 0, "Approved": 0, "Rejected": 0 };

    const defaultProps = {
        reportTitle: 'test',
        param: {},
        isClickSearch: true,
        isDisplayExportExcelBtn: true,
        isModifiable: true,
        selectedRequestId: '',
        pagingOption: {
            PageIndex: 1,
            PageSize: 20
        },
        dataSummary
    }

    it('should render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);
        result.instance().setState({ confirmMsg: 'test' });

        expect(result).toMatchSnapshot();
    });

    it('handleAddNewRequest - should call setState', () => {
        const componentInstance = new Content(defaultProps);
        componentInstance.setState = jest.fn();

        componentInstance.handleAddNewRequest();

        expect(componentInstance.setState).toHaveBeenCalled();
    });

    it('handleEditRequest - should call getProcessingRequestDetail and setState', async () => {
        const props = { ...defaultProps, getProcessingRequestDetail: jest.fn() }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();

        await componentInstance.handleEditRequest(123);

        expect(props.getProcessingRequestDetail).toHaveBeenCalledWith(123);
        expect(componentInstance.setState).toHaveBeenCalled();
    });

    it('handleViewRequest - should call getProcessingRequestDetail and setState', async () => {
        const props = { ...defaultProps, getProcessingRequestDetail: jest.fn() }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();

        await componentInstance.handleViewRequest(123);

        expect(props.getProcessingRequestDetail).toHaveBeenCalledWith(123);
        expect(componentInstance.setState).toHaveBeenCalled();
    });

    it('handleDeleteRequest - should call setState', () => {
        const componentInstance = new Content(defaultProps);
        componentInstance.setState = jest.fn();

        componentInstance.handleDeleteRequest();

        expect(componentInstance.setState).toHaveBeenCalledWith({
            confirmMsg: 'Are you sure to delete this request?',
            isConfirmPopup: true
        });
    });

    it('_handleCloseNotificationPopup - should call _deleteRequest and setSelectedRequestId', () => {
        const props = { ...defaultProps, setSelectedRequestId: jest.fn(), selectedRequestId: 123 }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        componentInstance._deleteRequest = jest.fn();

        componentInstance._handleCloseNotificationPopup(true);

        expect(componentInstance.setState).toHaveBeenCalledWith({ confirmMsg: '' });
        expect(componentInstance._deleteRequest).toHaveBeenCalledWith(123);
        expect(props.setSelectedRequestId).toHaveBeenCalledWith(null);
    });

    it('_handleCloseNotificationPopup - should only call setSelectedRequestId', () => {
        const props = { ...defaultProps, setSelectedRequestId: jest.fn(), selectedRequestId: 123 }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();
        componentInstance._deleteRequest = jest.fn();

        componentInstance._handleCloseNotificationPopup(false);

        expect(componentInstance.setState).toHaveBeenCalledWith({ confirmMsg: '' });
        expect(componentInstance._deleteRequest).not.toHaveBeenCalled();
        expect(props.setSelectedRequestId).toHaveBeenCalledWith(null);
    });

    it('_deleteRequest - should call setState and fetchData when successfully', async () => {
        const props = { ...defaultProps, fetchData: jest.fn(), deleteProcessingRequest: jest.fn(() => ({ IsSuccessful: true })) }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();

        await componentInstance._deleteRequest(123);

        expect(componentInstance.setState).toHaveBeenCalledWith({ confirmMsg: 'The processing request has been deleted successfully.', isConfirmPopup: false });
        expect(props.fetchData).toHaveBeenCalledWith({
            PageIndex: 1,
            PageSize: 20
        });
    });

    it('_deleteRequest - should only call setState when failed', async () => {
        const props = { ...defaultProps, fetchData: jest.fn(), deleteProcessingRequest: jest.fn(() => ({ IsSuccessful: false, Message: "Failed!" })) }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();

        await componentInstance._deleteRequest(123);

        expect(componentInstance.setState).toHaveBeenCalledWith({ confirmMsg: 'Failed!', isConfirmPopup: false });
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('_deleteRequest - should only call setState when errored', async () => {
        const props = { ...defaultProps, fetchData: jest.fn(), deleteProcessingRequest: jest.fn(() => null) }
        const componentInstance = new Content(props);
        componentInstance.setState = jest.fn();

        await componentInstance._deleteRequest(123);

        expect(componentInstance.setState).toHaveBeenCalledWith({ confirmMsg: 'An error occurred while processing your request, please try again later!', isConfirmPopup: false });
        expect(props.fetchData).not.toHaveBeenCalled();
    });

    it('handleHide - should call setState', () => {
        const componentInstance = new Content(defaultProps);
        componentInstance.setState = jest.fn();

        componentInstance.handleHide();

        expect(componentInstance.setState).toHaveBeenCalledWith({ Popup: null });
    });

    it('should call handleExportExcel when clicking export excel button', () => {
        const componentInstance = new Content(defaultProps);
        componentInstance.handleExportExcel = jest.fn();

        const title = componentInstance._buildReportTitle('Report Title');
        const renderedTitle = shallow(<div>{title}</div>);

        renderedTitle.find('button').simulate('click');

        expect(componentInstance.handleExportExcel).toHaveBeenCalled();
    });

    describe('handleExportExcel ', () => {
        beforeEach(() => {
            // Clear all instances and calls to constructor and all methods:
            ReportExcel.mockClear();
        });

        it('should call toggleLoader, post and ReportExcel when exporting excel', async () => {
            const expectedResponse = { data: ['data 1', 'data 2'], status: 200 };
            ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

            const expectedUrl = 'Portal/ProcessingRequest/GetProcessingRequestList';
            const expectedParam = {
                AffId: 927841,
                IsExportExcel: true
            }

            const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), param: { AffId: 927841 } });
            const componentInstance = new Content(props);

            await componentInstance.handleExportExcel();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
            expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();

            expect(ajaxRequest.post).toHaveBeenCalledWith(expectedUrl, expectedParam);
            expect(ReportExcel).toHaveBeenLastCalledWith(expectedResponse.data, defaultProps.reportTitle);
        });

        it('should call toggleLoader twice, post but not call ReportExcel when exporting excel with response incorrect', async () => {
            const expectedResponse = { data: ['data 1', 'data 2'], status: 500 };
            ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

            const expectedUrl = 'Portal/ProcessingRequest/GetProcessingRequestList';
            const expectedParam = {
                AffId: 927841,
                IsExportExcel: true
            }
            const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), param: { AffId: 927841 } });
            const componentInstance = new Content(props);

            await componentInstance.handleExportExcel();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
            expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();

            expect(ajaxRequest.post).toHaveBeenCalledWith(expectedUrl, expectedParam);
            expect(ReportExcel).toHaveBeenCalledWith({ MainData: {}, TotalItem: 0 }, defaultProps.reportTitle);
        });
    });
});