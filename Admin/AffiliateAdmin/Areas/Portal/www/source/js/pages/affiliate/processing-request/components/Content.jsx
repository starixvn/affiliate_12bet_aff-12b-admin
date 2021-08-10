import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { toggleLoader } from '../../../../actions/loaderAction';
import ReportTitle from '../../../../components/ReportTitle';
import GridData from './GridData';

import ReportExcel from '../libs/reportExcel';
import { post } from '../../../../modules/ajaxRequest';
import { claimResource } from '../../../../modules/claim';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    shouldComponentUpdate(nextprops) {
        const { reportTitle } = this.props;

        return nextprops.reportTitle !== reportTitle;
    }

    handleExportExcel = async () => {
        this.props.toggleLoader(true);
        const { param, reportTitle } = this.props;

        const url = `Portal/ProcessingRequest/GetProcessingRequestList`;
        const postData = Object.assign({}, param, { IsExportExcel: true });
        const response = await post(url, postData);
        let reportExcel = new ReportExcel({ MainData: {}, TotalItem: 0 }, reportTitle)

        if (response && response.status === 200) {
            reportExcel = new ReportExcel(response.data, reportTitle)
        }
        reportExcel.export();

        this.props.toggleLoader(false);
    }

    _buildReportTitle(reportTitle) {
        const { isDisplayExportExcelBtn } = this.props;
        return (
            <div className="d-flex align-items-center">
                {reportTitle}
                { isDisplayExportExcelBtn &&
                    <button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                        <span className="oi icon-excel"></span>
                    </button>
                }
            </div>
        );
    }

    render() {
        const { reportTitle } = this.props;

        return (
            <div className="data-content my-2">
                {reportTitle && <ReportTitle content={this._buildReportTitle(reportTitle)} />}
                <GridData />
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, param, claimsPrincipal }) {
    const isDisplayExportExcelBtn = claimsPrincipal.hasClaim(claimResource.ExportAffInfoExcel);
    return Object.assign({}, { reportTitle, param, isDisplayExportExcelBtn });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);