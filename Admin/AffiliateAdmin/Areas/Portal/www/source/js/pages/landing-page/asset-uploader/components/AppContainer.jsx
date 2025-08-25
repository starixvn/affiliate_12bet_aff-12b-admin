import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import ControlButtons from '../../../common/components/ControlButtons';
import Content from './Content';
//import EditAssetForm from './EditAssetForm';
//import AssetDetailPopup from './AssetDetailPopup';

import { messages } from '../libs/constants';
import { setParam, fetchData, toggleLoader, setSelectedAssetId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition } from '../actions';

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
                setSelectedItemId={props.setSelectedAssetId}
                deleteItems={(assetIdList) => props.deleteAssets({ assetIdList })}
            />
            <Content />
            {/*<EditAssetForm />*/}
            {/*<AssetDetailPopup />*/}
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, param, gridData, checkedAssets }) {
    return Object.assign({}, { isShowLoader, param, gridData, checkedAssets });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, fetchData, toggleLoader, setSelectedAssetId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));