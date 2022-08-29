import React from 'react';
import { Content } from '../Content';
import { shallow } from 'enzyme';
import { defaultPagingOption } from '../../../../../modules/constants';
import ReportExcel from '../../libs/reportExcel';
jest.mock('../../libs/reportExcel');

describe('feedback report - components - Content ', () => {
    const defaultProps = {
        param: {
            FromDate: "6/28/2019",
            PageIndex: 1,
            PageSize: 20,
            RateIds: '1',
            AffName: "",
            ToDate: "6/28/2019"
        },
        reportTitle: ""
    }

    it('render correctly', () => {
        const result = shallow(<Content {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with have reportTitle', () => {
        const props = Object.assign({}, defaultProps, { reportTitle: "Report Title" });
        const result = shallow(<Content {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('shouldComponentUpdate - should return correct', () => {
        const props = Object.assign({}, defaultProps, { reportTitle: "Report Title" });
        const componentInstance = new Content(defaultProps);
        const result = componentInstance.shouldComponentUpdate(props);

        expect(result).toBeTruthy();
    });

    it('handleExportExcel - should call fetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(), toggleLoader: jest.fn() });
        const componentInstance = new Content(props);
        await componentInstance.handleExportExcel(defaultProps);

        expect(props.fetchData).toHaveBeenCalled();
        expect(ReportExcel).not.toHaveBeenCalled();
        expect(props.toggleLoader).toHaveBeenCalled();
    });

    it('handleExportExcel - should call fetchData and ReportExcel', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn(() => []), toggleLoader: jest.fn() });
        const componentInstance = new Content(props);
        await componentInstance.handleExportExcel(defaultProps);

        expect(props.fetchData).toHaveBeenCalled();
        expect(ReportExcel).toHaveBeenCalled();
        expect(props.toggleLoader).toHaveBeenCalled();
    });
});