import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators} from 'redux';

import { headerText } from '../modules/constants';
import { toggleLoader } from '../../../actions/loaderAction';
import ReportTitle from '../../../components/ReportTitle';
import GridData from './GridData';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="data-content my-2">
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