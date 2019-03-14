import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Loader from '../../../components/Loader';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Filter />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);