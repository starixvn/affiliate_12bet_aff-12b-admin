import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
import Loader from '../../../../components/Loader';

const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            <Content />
            {props.selectedMonth && <Detail />}
        </AppLayout>
    );
}

function mapStateToProps({ userPanel }) {
    const { isShowLoader, selectedMonth } = userPanel;
    return Object.assign({}, { isShowLoader, selectedMonth });
}

export default connect(mapStateToProps)(React.memo(AppContainer));