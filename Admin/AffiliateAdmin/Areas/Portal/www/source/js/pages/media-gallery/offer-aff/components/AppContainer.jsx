import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../common/components/ControlButtons';
import Filter from './Filter';
import Content from './Content';
import InsertForm from './InsertForm';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedId, setCheckedIds, deleteOfferAffs, setSelectedPreviewId, setShouldResetScrollPosition } from '../actions';
import { claimResource } from '../../../../modules/claim';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props)
    }

    render() {
        const { isShowLoader, isModifiable, gridData, param, checkedIds } = this.props;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                { isModifiable &&
                    <ControlButtons
                        messages={messages}
                        gridData={gridData}
                        param={param}
                        fetchData={this.props.fetchData}
                        checkedItems={checkedIds}
                        setCheckedItems={this.props.setCheckedIds}
                        toggleLoader={this.props.toggleLoader}
                        setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                        setSelectedItemId={this.props.setSelectedId}
                        deleteItems={this.props.deleteOfferAffs}
                    />
                }
                <Filter />
                <Content />
                <InsertForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ param, isShowLoader, gridData, checkedIds, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { param, isShowLoader, gridData, checkedIds, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedId, setCheckedIds, deleteOfferAffs, setSelectedPreviewId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);