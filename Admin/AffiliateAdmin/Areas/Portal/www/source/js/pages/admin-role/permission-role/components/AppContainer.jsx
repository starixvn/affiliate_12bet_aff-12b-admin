import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <h1>Permission Role</h1>
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);