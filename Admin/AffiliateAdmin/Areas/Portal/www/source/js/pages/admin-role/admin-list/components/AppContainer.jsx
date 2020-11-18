import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import ControlButtons from './ControlButtons';
import Content from './Content';
import PermissionPopup from './PermissionPopup';
import Menu from '../../common/components/Menu';

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader, hasWriteUserControlClaim } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Menu />
                <Filter />
                {
                    hasWriteUserControlClaim && <ControlButtons />
                }
                <Content />
                <PermissionPopup />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, hasWriteUserControlClaim }) {
    return Object.assign({}, { isShowLoader, hasWriteUserControlClaim });
}

export default connect(mapStateToProps)(AppContainer);