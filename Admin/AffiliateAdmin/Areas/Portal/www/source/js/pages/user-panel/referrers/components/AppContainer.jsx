import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
import UserPanelSubMenu from '../../../common/components/UserPanelSubMenu';
import { setSelectedUserPanel } from '../../../customer/user-panel/actions';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.subMenuList = [
            { isActive: true, handleClick: () => this.props.setSelectedUserPanel('clickStatistics'), title: 'Referrers' },
            { isActive: false, handleClick: () => this.props.setSelectedUserPanel('landingPageStatistics'), title: 'Landing Pages' }
        ];
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <UserPanelSubMenu subMenuList={this.subMenuList} />
                <Filter />
                <Content />
                <Detail />
            </AppLayout>
        );
    }
}

function mapStateToProps({ clickStatistics }) {
    const { isShowLoader } = clickStatistics;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);