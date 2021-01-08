import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setFile } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {

        return (
            <div className="data-content my-2">
                Mass Adjustment
            </div>
        );
    }
}

function mapStateToProps({  }) {
    return Object.assign({}, { });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setFile, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);