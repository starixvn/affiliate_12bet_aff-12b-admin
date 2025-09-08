import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../../common/components/ControlButtons';
import Content from './Content';
import RegistrationForm from './RegistrationForm';
import MediaReviewPopup from '../../../common/components/MediaReviewPopup';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedMediaId, setCheckedForms, deleteForms, setShouldResetScrollPosition, setFormDetailUrl } from '../actions';
import Filter from './Filter';

export const AppContainer = (props) => {

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <ControlButtons
                label={"Add Registration"}
                messages={messages}
                gridData={props.gridData}
                fetchData={() => props.fetchData()}
                checkedItems={props.checkedForms}
                setCheckedItems={props.setCheckedForms}
                toggleLoader={props.toggleLoader}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                setSelectedItemId={props.setSelectedMediaId}
                deleteItems={(mediaIdList) => props.deleteForms({ mediaIdList })}
            />
            <Filter />
            <Content />
            <RegistrationForm />
            <MediaReviewPopup
                mediaDetailUrl={props.formDetailUrl}
                setMediaDetailUrl={props.setFormDetailUrl}
            />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, gridData, checkedForms, formDetailUrl }) {
    return Object.assign({}, { isShowLoader, gridData, checkedForms, formDetailUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedMediaId, setCheckedForms, deleteForms, setShouldResetScrollPosition, setFormDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));