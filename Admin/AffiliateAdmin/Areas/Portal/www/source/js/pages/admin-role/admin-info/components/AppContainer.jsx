import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import SubMenu from '../../common/components/SubMenu';
import Header from '../../common/components/Header';
import Content from './Content';
import { redirectToAdminUsers } from '../actions';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader, userId } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                {
                    !userId ? (
                        <div class="d-inline-flex p-3">
                            <a class="admin-role-sub-menu-back" href="javascript:;" onClick={() => this.props.redirectToAdminUsers()}><span class="oi oi-chevron-left"></span></a>
                            <strong>Create User</strong>
                        </div>
                    ) : (
                            <Fragment>
                                <SubMenu />
                                <Header />
                            </Fragment>
                        )
                }
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, userId }) {
    return Object.assign({}, { isShowLoader, userId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ redirectToAdminUsers }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);