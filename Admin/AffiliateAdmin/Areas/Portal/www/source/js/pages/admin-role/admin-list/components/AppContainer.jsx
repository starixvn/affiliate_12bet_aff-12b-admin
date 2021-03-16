import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import ControlButtons from './ControlButtons';
import Content from './Content';
import PermissionPopup from './PermissionPopup';
import Menu from '../../common/components/SubMenu';

export const AppContainer = (props) => {
    const { isShowLoader, hasWriteAdminRoleClaim } = props;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <Menu />
            <Filter />
            {
                hasWriteAdminRoleClaim && <ControlButtons />
            }
            <Content />
            <PermissionPopup />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, hasWriteAdminRoleClaim }) {
    return Object.assign({}, { isShowLoader, hasWriteAdminRoleClaim });
}

export default connect(mapStateToProps)(React.memo(AppContainer));