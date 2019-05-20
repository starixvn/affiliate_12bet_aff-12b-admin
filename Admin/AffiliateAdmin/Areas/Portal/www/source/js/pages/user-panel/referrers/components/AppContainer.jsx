import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';

import Filter from './Filter';
import Content from './Content';
import Detail from './Detail';
import NavMenu from './NavMenu';
import { setSelectedUserPanel } from '../../../customer/user-panel/actions';

class AppContainer extends PureComponent {
    const navMenuList = [
        { isActive: true, handleClick: () => props.setSelectedUserPanel('clickStatistics'), title: 'Referrers' },
        { isActive: false, handleClick: () => props.setSelectedUserPanel('landingPageStatistics'), title: 'Landing Pages' }
    ];
    render() {
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

function mapStateToProps({ clickStatistics }) {
    const { isShowLoader } = clickStatistics;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));