import React, { PureComponent } from 'react';
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

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const { gridData } = this.props;

        !gridData && this.handleFetchData();
    }

    handleFetchData = async () => {
        await this.props.fetchData();
    }

    render() {
        const { isShowLoader, isModifiable, gridData, checkedLandingPages } = this.props;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                { isModifiable &&
                    <ControlButtons
                        messages={messages}
                        gridData={gridData}
                        fetchData={this.props.fetchData}
                        checkedItems={checkedLandingPages}
                        setCheckedItems={this.props.setCheckedLandingPages}
                        toggleLoader={this.props.toggleLoader}
                        setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                        setSelectedItemId={this.props.setSelectedLandingPageId}
                        deleteItems={this.props.deleteLandingPages}
                    />
                }
                <Content />
                <InsertUpdateForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, gridData, checkedLandingPages, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { isShowLoader, gridData, checkedLandingPages, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedLandingPageId, setCheckedLandingPages, deleteLandingPages, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);