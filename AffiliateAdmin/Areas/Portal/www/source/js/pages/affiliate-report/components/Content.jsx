import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Breadcrumb, BreadcrumbItem, Button } from 'reactstrap';
import GridData from './GridData';
import ReportTitle from '../../../components/ReportTitle';

import { setViewLevel } from '../actions';
import { viewLevel } from '../modules/constants';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    handleClickBreadcrumb = () => {
        this.props.setViewLevel(viewLevel.main);
    }

    render() {
        const { currentViewLevel, reportTitle } = this.props;
        const isMainView = currentViewLevel === viewLevel.main;

        return (
            <div className="data-content my-2">
                {
                    !isMainView && (
                        <Breadcrumb>
                            <BreadcrumbItem><a href="javascript:;" className="btn-link" onClick={this.handleClickBreadcrumb}>Main</a></BreadcrumbItem>
                            <BreadcrumbItem active={!isMainView}>Month On Month</BreadcrumbItem>
                        </Breadcrumb>
                    )
                }

                {reportTitle && <ReportTitle content={reportTitle} />}
                <GridData />
            </div>
        );
    }
}

function mapStateToProps({ reportTitle, currentViewLevel }) {
    return Object.assign({}, { reportTitle, currentViewLevel });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setViewLevel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);