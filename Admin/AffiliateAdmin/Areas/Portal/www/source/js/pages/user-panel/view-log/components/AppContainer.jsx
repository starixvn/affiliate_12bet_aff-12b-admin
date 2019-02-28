import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);