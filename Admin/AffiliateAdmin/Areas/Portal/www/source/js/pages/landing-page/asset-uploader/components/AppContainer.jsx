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

import { messageDelete, messageSwitchStatus } from '../libs/constants';
import { assetStatus } from '../../libs/constants';
import { setParam, fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl, setNotification, toggleAssetStatus } from '../actions';
import NotificationPopup from '../../../common/components/NotificationPopup';

export const AppContainer = (props) => {
    const _getCheckedItemIdList = () => {
        const { checkedAssets } = props;
        if (!checkedAssets || typeof checkedAssets !== 'object') {
            return [];
        }

        return Object.keys(checkedAssets).filter(key => {
            const item = checkedAssets[key];
            return item && item.isChecked;
        });
    }

    const _handleSwitchStatus = async () => {
        const itemIdList = _getCheckedItemIdList();

        if(itemIdList.length === 0) {
            props.setNotification({
                message: messageSwitchStatus.PleaseSelect,
                isConfirmPopup: false
            });
        }
        else {
            const firstSelectedItem = itemIdList[0];
            const firstItem = props.gridData.find(item => item.MediaId == firstSelectedItem);
            const newStatus = firstItem.StatusId === assetStatus.active ? assetStatus.inactive : assetStatus.active;

            props.setNotification({
                message: messageSwitchStatus.AreYouSureSwitchStatus,
                isConfirmPopup: true,
                onConfirm: async () => {
                    const result = await props.toggleAssetStatus({ mediaIds: itemIdList.join(','), status: newStatus });
                    if (result && result.IsSuccessful) {
                        props.fetchData(props.param);
                    }
                    
                    props.setNotification({
                        message: result?.IsSuccessful ? messageSwitchStatus.SwitchStatusSuccessfully : result?.Message || messageSwitchStatus.FailedToSwitchStatus,
                        isConfirmPopup: false
                    });
                }
            });
        }
    }

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter />
            <ControlButtons
                messages={messageDelete}
                gridData={props.gridData}
                fetchData={() => props.fetchData(props.param)}
                checkedItems={props.checkedAssets}
                setCheckedItems={props.setCheckedAssets}
                toggleLoader={props.toggleLoader}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                setSelectedItemId={props.setSelectedMediaId}
                deleteItems={(mediaIds) => props.deleteAssets({ mediaIds, ...props.param })}
                optionLabel="Switch Status <span class='oi oi-loop-circular'></span>"
                clickOptionButton={_handleSwitchStatus}
            />
            <Content />
            <MediaReviewPopup
                mediaDetailUrl={props.assetDetailUrl}
                setMediaDetailUrl={props.setAssetDetailUrl}
            />
            <AssetUploaderForm />
            <NotificationPopup
                message={props.notification.message}
                isConfirmPopup={props.notification.isConfirmPopup}
                onClose={(result) => {
                    if (props.notification.onConfirm && result) {
                        props.notification.onConfirm();
                    }
                    props.setNotification({ message: '', isConfirmPopup: false, onConfirm: null });
                }}
            />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, param, gridData, checkedAssets, assetDetailUrl, notification }) {
    return Object.assign({}, { isShowLoader, param, gridData, checkedAssets, assetDetailUrl, notification });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, fetchData, toggleLoader, setSelectedMediaId, setCheckedAssets, deleteAssets, setShouldResetScrollPosition, setAssetDetailUrl, setNotification, toggleAssetStatus }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));