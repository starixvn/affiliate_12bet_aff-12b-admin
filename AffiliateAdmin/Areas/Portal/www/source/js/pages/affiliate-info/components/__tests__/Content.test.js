import React from 'react';
import { Content } from '../Content';
import renderer from 'react-test-renderer';
import ShallowRenderer from 'react-test-renderer/shallow';
import Enzyme, { shallow } from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';

import * as ajaxRequest from '../../../../modules/ajaxRequest';
import ReportExcel from '../../modules/reportExcel';
jest.mock('../../modules/reportExcel');

Enzyme.configure({ adapter: new Adapter() });

describe('affiliate info - Content ', () => {
    const defaultProps = {
        reportTitle: '',
        param: {},
        languageList: {}
    }

    it('shouldComponentUpdate - return correct value if report title no change ', () => {
        const componentInstance = new Content(defaultProps);

        const result = componentInstance.shouldComponentUpdate({ reportTitle: 'New Title' });

        expect(result).toBeTruthy();
    });

    it('should render correctly with _buildReportTitle', () => {
        const componentInstance = new Content(defaultProps);
        const title = componentInstance._buildReportTitle('Report Title');

        const result = renderer.create((<div>{title}</div>));

        expect(result).toMatchSnapshot();
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

        it('should pass request param correctly when exporting excel', () => {
            ajaxRequest.post = jest.fn();
            const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), param: { userName: 'AFF001' } });
            const componentInstance = new Content(props);
            const expectedParam = {
                userName: 'AFF001',
                IsExportExcel: true
            }

            const expectedUrl = 'Portal/AffiliateInfo/GetAffiliateList';

            componentInstance.handleExportExcel();

            expect(ajaxRequest.post).toHaveBeenCalledWith(expectedUrl, expectedParam);
        });

        it('should call toggleLoader twice with correct parameters', async () => {
            ajaxRequest.post = jest.fn();
            const mockToggleLoader = jest.fn();
            const props = Object.assign({}, defaultProps, { toggleLoader: mockToggleLoader, param: { userName: 'AFF001' } });
            const componentInstance = new Content(props);

            await componentInstance.handleExportExcel();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(props.toggleLoader.mock.calls[0][0]).toBeTruthy();
            expect(props.toggleLoader.mock.calls[1][0]).toBeFalsy();
        });

        it('should create new instance of ReportExcel with correct parameter', async () => {
            const expectedResponse = { data: ['data 1', 'data 2'], status: 200 };
            ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

            const reportTitle = 'report title', languageList = { 'en-US': 'English' };
            const props = Object.assign({}, defaultProps, { toggleLoader: jest.fn(), reportTitle: reportTitle, languageList });
            const componentInstance = new Content(props);

            await componentInstance.handleExportExcel();

            expect(ReportExcel).toHaveBeenCalledWith(expectedResponse.data, reportTitle, languageList);
        });
    });

    it('should render correctly', () => {
        const renderer = new ShallowRenderer();

        const props = Object.assign({}, defaultProps, { reportTitle: 'Report Title' });
        const result = renderer.render(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });
});