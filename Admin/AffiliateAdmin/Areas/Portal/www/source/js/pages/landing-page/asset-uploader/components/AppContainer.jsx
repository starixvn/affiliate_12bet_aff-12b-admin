import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import ControlButtons from '../../../common/components/ControlButtons';
import Content from './Content';
import MediaReviewPopup from '../../../common/components/MediaReviewPopup';
import AssetUploaderForm from './AssetUploaderForm';

import { messages } from '../libs/constants';
import { setParam, fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl } from '../actions';

export const AppContainer = (props) => {

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            <ControlButtons
                messages={messages}
                gridData={props.gridData}
                fetchData={() => props.fetchData(props.param)}
                checkedItems={props.checkedAssets}
                setCheckedItems={props.setCheckedAssets}
                toggleLoader={props.toggleLoader}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                setSelectedItemId={props.setSelectedMediaId}
                deleteItems={(mediaIdList) => props.deleteAssets({ mediaIdList })}
            />
            <Content />
            <MediaReviewPopup
                mediaDetailUrl={props.assetDetailUrl}
                setMediaDetailUrl={props.setAssetDetailUrl}
            />
            <AssetUploaderForm />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, param, gridData, checkedAssets, assetDetailUrl }) {
    return Object.assign({}, { isShowLoader, param, gridData, checkedAssets, assetDetailUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));