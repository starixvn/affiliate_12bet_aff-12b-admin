import React from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import Unactivated from './Unactivated';
import Activated from './Activated';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <div className="rule-text mt-2">Each username seperated by Comma ","</div>
            <Filter />
            {
                props.isShowText && <div className="text-red">These members do not exist: <strong>{props.userNames}</strong></div>
            }
            <Unactivated />
            <Activated />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, isShowText, userNames }) {
    return Object.assign({}, { isShowLoader, isShowText, userNames });
}

export default connect(mapStateToProps)(React.memo(AppContainer));