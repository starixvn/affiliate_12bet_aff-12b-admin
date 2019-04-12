import Filter from './Filter';
import ReportContent from './ReportContent';
import { createContainer } from '../createContainer';
import { createReportContainer } from '../createReportContainer';

const urlParams = new URLSearchParams(window.location.search);
const view = urlParams.get('view');
const isReportView = view && view.toLowerCase() === 'report';

const option = {
    apiUrl: isReportView ? 'ThirdPartyProduct/ISoftbet/GetReportData' : 'ThirdPartyProduct/ISoftbet/Index'
}

export default isReportView ? createReportContainer(Filter, ReportContent, option) : createContainer(Filter, option);