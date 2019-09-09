import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Loader from '../../../components/Loader';
import Filter from './Filter';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(React.memo(AppContainer));