import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';
import InsertUpdateNoteForm from './InsertUpdateNoteForm';
import Loader from '../../../../components/Loader';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Content />
                <InsertUpdateNoteForm />
            </AppLayout>
        );
    }
}

function mapStateToProps({ note }) {
    const { isShowLoader } = note;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);