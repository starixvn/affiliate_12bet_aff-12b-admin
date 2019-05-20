import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ viewLog }) {
    const { isShowLoader } = viewLog;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);