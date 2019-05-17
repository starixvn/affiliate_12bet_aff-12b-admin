import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
import Loader from '../../../../components/Loader';

import { resetUserPanel } from '../../../customer/common/actions';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentWillUnmount() {
        this.props.resetUserPanel(userPanelMenuKey.trackingStatistics);
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
    return bindActionCreators({ resetUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);