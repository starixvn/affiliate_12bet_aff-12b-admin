import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../components/Loader';
import AddAffMktAccountPopup from './AddAffMktAccountPopup';

import { setIsShowAddAffMktAccountPopup, setAffId } from '../actions';
import { Button } from 'reactstrap';

export const AppContainer = ({ isShowLoader, isShowAddAffMktAccountPopup, setIsShowAddAffMktAccountPopup, setAffId }) => {
    const _handleToggleAddAffMktAccountPopup = () => {
        setIsShowAddAffMktAccountPopup(!isShowAddAffMktAccountPopup);
        setAffId(null);
    }

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <Filter />
            <Content />
            {isShowAddAffMktAccountPopup && <AddAffMktAccountPopup isOpen={isShowAddAffMktAccountPopup} toggle={_handleToggleAddAffMktAccountPopup} />}
        </AppLayout>
    );
};

const mapStateToProps = ({ isShowLoader, isShowAddAffMktAccountPopup }) => {
    return {
        isShowLoader,
        isShowAddAffMktAccountPopup
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setIsShowAddAffMktAccountPopup,
        setAffId
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));