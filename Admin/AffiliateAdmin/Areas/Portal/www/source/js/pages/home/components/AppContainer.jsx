import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Loader from '../../../components/Loader';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <div className="ml-4 my-2 pl-3">
                Below you see tasks that are waiting for your approval and overview of the affiliate program
            </ div>
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(React.memo(AppContainer));