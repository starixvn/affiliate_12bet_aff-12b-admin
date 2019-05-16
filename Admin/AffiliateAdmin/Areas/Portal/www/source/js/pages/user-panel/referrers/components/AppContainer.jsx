import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';

const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            <Content />
            <Detail />
        </AppLayout>
    );
}

function mapStateToProps({ userPanel }) {
    const { isShowLoader } = userPanel;
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);