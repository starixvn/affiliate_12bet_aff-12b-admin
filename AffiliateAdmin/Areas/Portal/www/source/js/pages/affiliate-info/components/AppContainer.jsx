import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import ChangePasswordForm from './ChangePasswordForm';
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
                <Content />
                <ChangePasswordForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);