import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../common/components/ControlButtons';
import Content from './Content';
import InsertUpdateForm from './InsertUpdateForm';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedLandingPageId, setCheckedLandingPages, deleteLandingPages, setShouldResetScrollPosition } from '../actions';
import { claimResource } from '../../../../modules/claim';

export const AppContainer = (props) => {

    useEffect(() => {
        const { gridData } = props;

        !gridData && handleFetchData();
    }, [props.gridData]);

    const handleFetchData = async () => {
        await props.fetchData();
    }

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            { props.isModifiable &&
                <ControlButtons
                    messages={messages}
                    gridData={props.gridData}
                    fetchData={props.fetchData}
                    checkedItems={props.checkedLandingPages}
                    setCheckedItems={props.setCheckedLandingPages}
                    toggleLoader={props.toggleLoader}
                    setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                    setSelectedItemId={props.setSelectedLandingPageId}
                    deleteItems={props.deleteLandingPages}
                />
            }
            <Content />
            <InsertUpdateForm />
        </AppLayout>
    );
}

function mapStateToProps({ isShowLoader, gridData, checkedLandingPages, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { isShowLoader, gridData, checkedLandingPages, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedLandingPageId, setCheckedLandingPages, deleteLandingPages, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));