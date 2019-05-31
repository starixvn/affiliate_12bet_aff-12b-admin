import React from 'react';
import { connect } from 'react-redux';
import Loader from '../../../../components/Loader';
import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';
import { spinnerLoaderWidth } from '../../../../modules/constants';

const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} style={{ left: spinnerLoaderWidth }} />
            <Content />
        </AppLayout>
    );
}

function mapStateToProps({ customerStatistics }) {
    const { isShowLoader } = customerStatistics;
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(React.memo(AppContainer));