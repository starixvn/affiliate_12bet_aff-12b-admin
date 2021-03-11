import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../common/components/ControlButtons';
import Content from './Content';
import InsertUpdateForm from './InsertUpdateForm';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, deleteNewsLetterPages, setSelectedPreviewPageId, setShouldResetScrollPosition } from '../actions';
import { claimResource } from '../../../../modules/claim';

export const AppContainer = (props) => {

    useEffect(() => {
        const { gridData } = props;

        !gridData && handleFetchData();
    }, [props.gridData]);

    const handleFetchData = async () => {
        await props.fetchData(props.param);
    }

    return (
        <AppLayout>
            <Loader show={props.isShowLoader} />
            { props.isModifiable &&
                <ControlButtons
                    messages={messages}
                    gridData={props.gridData}
                    fetchData={() => props.fetchData(props.param)}
                    checkedItems={props.checkedNewsLetterPages}
                    setCheckedItems={props.setCheckedNewsLetterPages}
                    toggleLoader={props.toggleLoader}
                    setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                    setSelectedItemId={props.setSelectedNewsLetterPageId}
                    deleteItems={props.deleteNewsLetterPages}
                    param={props.param}
                />
            }
            <Content />
            <InsertUpdateForm />
        </AppLayout>
    );
}

function mapStateToProps({ param, isShowLoader, gridData, checkedNewsLetterPages, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGallery);
    return Object.assign({}, { param, isShowLoader, gridData, checkedNewsLetterPages, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, deleteNewsLetterPages, setSelectedPreviewPageId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));