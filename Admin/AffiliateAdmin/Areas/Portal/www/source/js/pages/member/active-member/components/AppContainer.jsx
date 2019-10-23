import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import Unactivated from './Unactivated';
import Activated from './Activated';
import { NotificationPopup } from '../../../common/components/NotificationPopup';
import { setMessage } from '../actions';

export const AppContainer = (props) => {
    const { isShowLoader, isShowText, userNames, message } = props;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <div className="rule-text mt-2">Each username seperated by Comma ","</div>
            <Filter />
            {
                isShowText && <div className="text-red">These members do not exist: <strong>{userNames}</strong></div>
            }
            <Unactivated />
            <Activated />
            {
                !!message && <NotificationPopup
                    message={message}
                    onClose={() => props.setMessage('')}
                />
            }
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, isShowText, userNames, message }) {
    return Object.assign({}, { isShowLoader, isShowText, userNames, message });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMessage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));