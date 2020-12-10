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
import { claimResource } from "../../../modules/claim";
import {headerConfig} from '../modules/constants';
import { SingleHeader, HeaderRow, AggregateHeader } from "../modules/header";

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
        const header = this._buildHeader();
        const isMainView = currentViewLevel === viewLevel.main;
        const url = `Portal/Report/${isMainView ? 'GetMainData' : 'GetDetailData'}`;
        const postData = Object.assign({}, param, { IsExportExcel: true });
        const response = await post(url, postData);
        if (response && response.status === 200) {
            const reportExcel = new ReportExcel(response.data, reportTitle, header, isMainView);
            reportExcel.export();
        }
        this.props.toggleLoader(false);
    }

    _buildReportTitle() {
        const { currentViewLevel, reportTitle, claimsPrincipal } = this.props;
        const isMainView = currentViewLevel === viewLevel.main;
        const isDisplayExportExcelBtn = claimsPrincipal.hasClaim(claimResource.ExportExcel);

        const btnExport = isDisplayExportExcelBtn && (<button type="button" className="ml-auto btn btn-link btn-excel" onClick={this.handleExportExcel} title="Export Excel">
            <span className="oi icon-excel"></span>
        </button>);

        return isMainView ? (
            <div className="d-flex align-items-center">
                {reportTitle}
                {btnExport}
            </div>
        ) : (
                <div className="d-flex align-items-center">
                    <a href="javascript:;" className="btn-link font-weight-bold" onClick={this.handleClickBreadcrumb}>AFFILIATE REPORT</a>
                    {reportTitle}
                    {btnExport}
                </div>
            )
    }

    _buildHeader = () => {
        const keys = Object.keys(headerConfig);
        let cols = [];
        keys.forEach(key => {
            if (!!headerConfig[key].cols)
            {
                cols.push(this._buildAggregateHeaderCol(key, headerConfig[key]));
            }
            else
            {
                cols.push(this._buildHeaderCol(key, headerConfig[key]));
            }
        })
        return new HeaderRow(cols);
    }

    _buildHeaderCol = (key, item) => {
        const { claimsPrincipal, currentViewLevel} = this.props;
        const isMainView = currentViewLevel === viewLevel.main;
        if (!!item.isVisable && typeof item.isVisable === 'function')
        {
            return new SingleHeader(key, item.label, item.isVisable(claimsPrincipal, isMainView));
        }
        else{
            return new SingleHeader(key, item.label, true);
        }
    }

    _buildAggregateHeaderCol = (key, header) => {
        const cols = [];
        const headerCols = header.cols;
        Object.keys(headerCols).forEach(value => {
            if (!!headerCols[value].cols)
            {
                cols.push(this._buildAggregateHeaderCol(value, headerCols[value]));
            }
            else
            {
                cols.push(this._buildHeaderCol(value, headerCols[value]));
            }
        })
        return new AggregateHeader(key, header.label, cols);
    }

    render() {
        const { reportTitle } = this.props;
        const header = this._buildHeader();
        return (
            <div className="data-content my-2">
                {reportTitle && <ReportTitle content={this._buildReportTitle()} />}
                <GridData headerConfig={header}/>
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, currentViewLevel, param, claimsPrincipal }) {
    return Object.assign({}, { reportTitle, currentViewLevel, param, claimsPrincipal });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setViewLevel, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);