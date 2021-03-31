import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import DetailContent from './DetailContent';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Filter />
            {
                !props.selectedAff ? <Content /> : <DetailContent />
            }
        </AppLayout>
    );
}
function mapStateToProps({ selectedAff }) {
    return Object.assign({}, { selectedAff });
}

export default connect(mapStateToProps)(AppContainer);