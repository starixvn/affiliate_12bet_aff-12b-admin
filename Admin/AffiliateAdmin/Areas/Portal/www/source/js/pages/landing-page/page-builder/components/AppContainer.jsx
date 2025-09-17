import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import LandingPageForm from './LandingPageForm';
import NotificationPopup from '../../../common/components/NotificationPopup';
import { setNotification } from '../actions';

const AppContainer = (props) => {
    const { isShowLoader, notification } = props;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <LandingPageForm />
            {notification && (
                <NotificationPopup
                    message={notification.message}
                    isConfirmPopup={notification.isConfirmPopup}
                    onClose={(result) => {
                        if (notification.onConfirm && result) {
                            notification.onConfirm(result);
                        }
                        props.setNotification({ message: '', isConfirmPopup: false, onConfirm: null });
                    }}
                />
            )}
        </AppLayout>
    );
};

function mapStateToProps({ isShowLoader, notification }) {
    return Object.assign({}, { isShowLoader, notification });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setNotification }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));