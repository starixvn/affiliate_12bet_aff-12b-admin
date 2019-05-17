import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import LinkMenu from './LinkMenu';
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
        this.props.resetUserPanel(userPanelMenuKey.dailyEarningDetail);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
				<LinkMenu param={this.props.param} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ dailyEarningDetail }) {
    const { isShowLoader, param } = dailyEarningDetail;
    return Object.assign({}, { isShowLoader, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ resetUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);