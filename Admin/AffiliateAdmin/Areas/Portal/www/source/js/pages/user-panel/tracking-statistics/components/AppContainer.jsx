import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
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
                {this.props.selectedMonth && <Detail />}
            </AppLayout>
        );
    }
}

function mapStateToProps({ trackingStatistics }) {
    const { isShowLoader, selectedMonth } = trackingStatistics;
    return Object.assign({}, { isShowLoader, selectedMonth });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);