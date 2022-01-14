import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ControlButtons from '../../common/components/ControlButtons';
import Content from './Content';
import InsertUpdateFileHtmlForm from './InsertUpdateFileHtmlForm';

import { messages } from '../libs/constants';
import { fetchData, toggleLoader, setSelectedId, setCheckedIds, deleteOfferPages, setSelectedPreviewId, setShouldResetScrollPosition } from '../actions';
import { claimResource } from '../../../../modules/claim';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const { gridData } = this.props;

        !gridData && this.handleFetchData();
    };

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (param !== prevProps.param) {
            this.handleFetchData();
        }
    };

    handleFetchData = async () => {
        await this.props.fetchData(this.props.param);
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
                        deleteItems={this.props.deleteOfferPages}
                    />
                }
                <Content />
                <InsertUpdateFileHtmlForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ param, isShowLoader, gridData, checkedIds, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { param, isShowLoader, gridData, checkedIds, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedId, setCheckedIds, deleteOfferPages, setSelectedPreviewId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);