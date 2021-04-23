import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import Content from './Content';
import DetailContent from './DetailContent';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            {
                !props.selectedAff ? <Content /> : <DetailContent />
            }
        </AppLayout>
    );
}
function mapStateToProps({ isShowLoader, selectedAff }) {
    return Object.assign({}, { isShowLoader, selectedAff });
}

export default connect(mapStateToProps)(AppContainer);