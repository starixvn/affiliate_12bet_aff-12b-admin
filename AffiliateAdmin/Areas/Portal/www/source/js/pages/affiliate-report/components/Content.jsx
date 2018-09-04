import React, { Component } from "react";

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import GridData from './GridData';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="data-content">
                <div className="my-1">
                    <GridData />
                </div>
            </div>
        );
    }
}

function mapStateToProps({ }) {
    return Object.assign({}, {});
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);