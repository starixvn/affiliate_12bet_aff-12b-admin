import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';

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

function mapStateToProps({ playerDetail }) {
    const { isShowLoader } = playerDetail;

    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);