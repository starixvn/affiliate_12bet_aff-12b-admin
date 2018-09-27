import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import GridData from './GridData';
import ReportTitle from '../../../components/ReportTitle';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="data-content my-2">
                { this.props.reportTitle && <ReportTitle content={this.props.reportTitle} /> }
                <GridData />
            </div>
        );
    }
}

function mapStateToProps({reportTitle }) {
    return Object.assign({}, {reportTitle});
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);