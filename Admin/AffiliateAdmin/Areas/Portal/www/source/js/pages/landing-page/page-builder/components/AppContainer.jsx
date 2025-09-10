import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import LandingPageForm from './LandingPageForm';

const AppContainer = (props) => {
    const { isShowLoader } = props;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <LandingPageForm />
        </AppLayout>
    );
};

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));