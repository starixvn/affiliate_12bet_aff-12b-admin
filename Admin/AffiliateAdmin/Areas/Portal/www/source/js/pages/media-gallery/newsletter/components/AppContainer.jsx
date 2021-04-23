import React, { PureComponent } from 'react';
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

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        const { gridData } = this.props;

        !gridData && this.handleFetchData();
    };

    handleFetchData = async () => {
        await this.props.fetchData(this.props.param);
    }

    render() {
        const { isShowLoader, isModifiable, gridData, param, checkedNewsLetterPages } = this.props;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                { isModifiable &&
                    <ControlButtons
                        messages={messages}
                        gridData={gridData}
                        param={param}
                        fetchData={this.props.fetchData}
                        checkedItems={checkedNewsLetterPages}
                        setCheckedItems={this.props.setCheckedNewsLetterPages}
                        toggleLoader={this.props.toggleLoader}
                        setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                        setSelectedItemId={this.props.setSelectedNewsLetterPageId}
                        deleteItems={this.props.deleteNewsLetterPages}
                    />
                }
                <Content />
                <InsertUpdateForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ param, isShowLoader, gridData, checkedNewsLetterPages, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { param, isShowLoader, gridData, checkedNewsLetterPages, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, toggleLoader, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, deleteNewsLetterPages, setSelectedPreviewPageId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);