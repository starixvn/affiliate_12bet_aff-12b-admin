import React from 'react';
import { connect } from 'react-redux';
import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Content from './Content';

const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Content />
        </AppLayout>
    );
}

function mapStateToProps({ userPanel }) {
    const { isShowLoader } = userPanel;
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(React.memo(AppContainer));