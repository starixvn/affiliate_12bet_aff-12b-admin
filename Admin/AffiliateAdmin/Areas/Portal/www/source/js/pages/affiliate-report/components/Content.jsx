import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import GridData from './GridData';
import ReportTitle from '../../../components/ReportTitle';

import { setViewLevel } from '../actions';
import { toggleLoader } from '../../../actions/loaderAction';
import { viewLevel } from '../modules/constants';
import ReportExcel from '../modules/reportExcel';
import { post } from '../../../modules/ajaxRequest';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    shouldComponentUpdate(nextProps) {
        const { reportTitle } = this.props;

        return nextProps.reportTitle !== reportTitle;
    }

    handleClickBreadcrumb = () => {
        this.props.setViewLevel(viewLevel.main);
    }

    handleExportExcel = async () => {
        this.props.toggleLoader(true);
        const { param, reportTitle, currentViewLevel } = this.props;
        const isMainView = currentViewLevel === viewLevel.main;
        const url = `Portal/Report/${isMainView ? 'GetMainData' : 'GetDetailData'}`;
        const postData = Object.assign({}, param, { IsExportExcel: true });
        const response = await post(url, postData);
        if (response && response.status === 200) {
            const reportExcel = new ReportExcel(response.data, reportTitle, isMainView);
            reportExcel.export();
        }
        this.props.toggleLoader(false);
    }

    _buildReportTitle() {
        const { currentViewLevel, reportTitle } = this.props;
        const isMainView = currentViewLevel === viewLevel.main;

        return isMainView ? (
            <div className="d-flex align-items-center">
                {reportTitle}
                <button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                    <span className="oi icon-excel"></span>
                </button>
            </div>
        ) : (
                <div className="d-flex align-items-center">
                    <a href="javascript:;" className="btn-link font-weight-bold" onClick={this.handleClickBreadcrumb}>AFFILIATE REPORT</a> > {reportTitle}
                    <button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                        <span className="oi icon-excel"></span>
                    </button>
                </div>
            )
    }

    render() {
        const { reportTitle } = this.props;

        return (
            <div className="data-content my-2">
                {reportTitle && <ReportTitle content={this._buildReportTitle()} />}
                <GridData />
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, currentViewLevel, param }) {
    return Object.assign({}, { reportTitle, currentViewLevel, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setViewLevel, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);