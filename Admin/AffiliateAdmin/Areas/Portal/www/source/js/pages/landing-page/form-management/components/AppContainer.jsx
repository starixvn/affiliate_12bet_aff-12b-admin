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
import { fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl } from '../actions';

export const AppContainer = (props) => {

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <ControlButtons
                label={"Add Registration Form"}
                messages={messages}
                gridData={props.gridData}
                fetchData={() => props.fetchData()}
                checkedItems={props.checkedAssets}
                setCheckedItems={props.setCheckedAssets}
                toggleLoader={props.toggleLoader}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                setSelectedItemId={props.setSelectedMediaId}
                deleteItems={(mediaIdList) => props.deleteAssets({ mediaIdList })}
            />
            <Content />
            <RegistrationForm />
            <MediaReviewPopup
                mediaDetailUrl={props.assetDetailUrl}
                setMediaDetailUrl={props.setAssetDetailUrl}
            />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, gridData, checkedAssets, assetDetailUrl }) {
    return Object.assign({}, { isShowLoader, gridData, checkedAssets, assetDetailUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));