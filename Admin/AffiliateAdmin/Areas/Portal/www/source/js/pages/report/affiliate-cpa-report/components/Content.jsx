import React from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import GridData from './GridData';
import ReportTitle from '../../../../components/ReportTitle';

import { toggleLoader } from '../../../../actions/loaderAction';
import { fetchData } from '../actions';

const Title = ({ reportTitle }) => {
    return (
        <div className="d-flex align-items-center">
            {reportTitle}
            <button type="button" className="ml-auto btn btn-link btn-excel" onClick={() => console.log('export excel')} title="Export Excel">
                <span className="oi icon-excel"></span>
            </button>
        </div>
    )
}

const Content = ({ reportTitle }) => {
    return (
        <div className="data-content my-2 report-content-container">
            {
                reportTitle && (
                    <ReportTitle content={<Title reportTitle={reportTitle} />} />
                )
            }
            <GridData />
        </div>
    );
}

function mapStateToProps({ reportTitle, params }) {
    return Object.assign({}, { reportTitle, params });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);