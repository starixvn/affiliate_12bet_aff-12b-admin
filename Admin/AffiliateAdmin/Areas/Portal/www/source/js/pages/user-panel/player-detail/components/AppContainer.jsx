import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';

import { resetUserPanel } from '../../../customer/common/actions';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentWillUnmount() {
        this.props.resetUserPanel(userPanelMenuKey.playerDetail);
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
    return bindActionCreators({ resetUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);