import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../../common/components/ControlButtons';
import Content from './Content';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedFormId, setCheckedForms, deleteForms, setShouldResetScrollPosition } from '../actions';

export const AppContainer = (props) => {

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            <ControlButtons
                messages={messages}
                gridData={props.gridData || []}
                fetchData={() => props.fetchData()}
                checkedItems={props.checkedForms}
                setCheckedItems={props.setCheckedForms}
                toggleLoader={props.toggleLoader}
                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                setSelectedItemId={props.setSelectedFormId}
                deleteItems={(formIdList) => props.deleteForms({ formIdList })}
            />
            <Content />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, gridData, checkedForms }) {
    return Object.assign({}, { isShowLoader, gridData, checkedForms });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedFormId, setCheckedForms, deleteForms, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));