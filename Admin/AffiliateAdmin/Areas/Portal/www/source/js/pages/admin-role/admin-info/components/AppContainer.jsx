import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Content from './Content';
import MyLink from '../../../common/components/MyLink';
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
                <div className="d-inline-flex p-3">
                    <MyLink className={"admin-role-sub-menu-back"} onClick={() => this.props.redirectToAdminList()}>
                        <span className="oi oi-chevron-left"></span>
                    </MyLink>
                    <strong>{userName || "Create User"}</strong>
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