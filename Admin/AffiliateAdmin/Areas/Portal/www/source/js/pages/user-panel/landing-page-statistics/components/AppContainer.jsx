import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
import NavMenu from '../../../../components/pages/common/NavMenu';
import { setSelectedUserPanel } from '../../../customer/user-panel/actions';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const navMenuList = [
            { isActive: false, handleClick: () => this.props.setSelectedUserPanel('clickStatistics'), title: 'Referrers' },
            { isActive: true, handleClick: () => this.props.setSelectedUserPanel('landingPageStatistics'), title: 'Landing Pages' }
        ];

        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <NavMenu navMenuList={navMenuList} />
                <Filter />
                <Content />
                <Detail />
            </AppLayout>
        );
    }
}

function mapStateToProps({ landingPageStatistics }) {
    const { isShowLoader } = landingPageStatistics;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);