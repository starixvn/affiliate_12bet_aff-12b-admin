import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Loader from '../../../../components/Loader';
import Content from './Content';
import ViewDealWrapper from '../../../operation/view-deal';

import { toggleDealPopup } from '../actions';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            <Content />
            {props.isShowDealPopup && <ViewDealWrapper dealId={props.dealId} toggle={props.toggleDealPopup} />}
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, isShowDealPopup, dealId }) {
    return Object.assign({}, { isShowLoader, isShowDealPopup, dealId });
}

const mapDispatchToProps = (dispatch) => {
    return {
        toggleDealPopup: () => dispatch(toggleDealPopup())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));