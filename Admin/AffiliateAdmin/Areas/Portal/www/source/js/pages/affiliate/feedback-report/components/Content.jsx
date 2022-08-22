import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { toggleLoader } from '../../../../actions/loaderAction';
import ReportTitle from '../../../../components/ReportTitle';
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
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);