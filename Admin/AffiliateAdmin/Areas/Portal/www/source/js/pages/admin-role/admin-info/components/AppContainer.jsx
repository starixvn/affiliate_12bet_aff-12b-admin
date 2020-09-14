import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Content from './Content';
import { redirectToAdminList } from '../actions';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader, userName } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <div class="d-inline-flex p-3">
                    <a class="admin-role-sub-menu-back" href="javascript:;" onClick={() => this.props.redirectToAdminList()}><span class="oi oi-chevron-left"></span></a>
                    <strong>{ userName || "Create User"}</strong>
                </div>
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, userName }) {
    return Object.assign({}, { isShowLoader, userName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ redirectToAdminList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);