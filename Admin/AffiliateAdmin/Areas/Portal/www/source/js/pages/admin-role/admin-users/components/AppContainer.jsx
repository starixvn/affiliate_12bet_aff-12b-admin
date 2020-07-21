import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import { resolveClientUrl } from '../../../../modules/utils';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <h1>Admin Users</h1>
                <a href={resolveClientUrl(`Portal/AdminInfo`)} target="_top">
                    Go to Admin Info
                </a>
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);