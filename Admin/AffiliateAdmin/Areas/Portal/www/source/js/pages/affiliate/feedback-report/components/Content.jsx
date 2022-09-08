import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { toggleLoader, fetchData } from '../actions';
import ReportTitle from '../../../../components/ReportTitle';
import ReportExcel from '../libs/reportExcel';
import GridData from './GridData';

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
        const newParam = Object.assign({}, param, { IsExportExcel: true });

        const data = await this.props.fetchData(newParam);

        if (data) {
            const reportExcel = new ReportExcel(data, reportTitle);
            reportExcel.export();
        }
        this.props.toggleLoader(false);
    }

    _buildReportTitle(reportTitle) {
        return (
            <div className="d-flex align-items-center">
                {reportTitle}
                <button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                    <span className="oi icon-excel"></span>
                </button>
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

function mapStateToProps({ reportTitle, param }) {
    return Object.assign({}, { reportTitle, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);