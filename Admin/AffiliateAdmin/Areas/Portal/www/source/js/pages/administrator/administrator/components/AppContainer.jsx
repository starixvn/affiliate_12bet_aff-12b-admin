import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from '../../common/components/Filter';
import Content from '../../common/components/Content';
import { setParam, setShouldResetScrollPosition, fetchData, changeStatus, toggleLoader } from '../actions';

export const AppContainer = (props) => {
    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <Filter
                setParam={props.setParam}
                fetchData={props.fetchData}
            />
            <Content
                param={props.param}
                gridData={props.gridData}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                fetchData={props.fetchData}
                changeStatus={props.changeStatus}
                toggleLoader={props.toggleLoader}
            />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, param, gridData }) {
    return Object.assign({}, { isShowLoader, param, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setShouldResetScrollPosition, fetchData, changeStatus, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));