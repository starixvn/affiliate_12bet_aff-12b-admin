import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import ControlButtons from './ControlButtons';
import Content from './Content';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <div className="admin-role-menu">
                    <span class="icon-admin-role"></span>
                    <strong>Admin Users</strong>
                </div>
                <Filter />
                <ControlButtons />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);