import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <h1>Department List</h1>
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(React.memo(AppContainer));