import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import LinkMenu from './LinkMenu';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';
import { monthNames } from '../../../../modules/constants';
import { setSelectedUserPanel } from '../../../customer/user-panel/actions';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { param } = props;
        const monthDate = param && param.monthDate;
        const date = new Date(monthDate);

        const linkMenuList = [
            { handleClick: () => props.setSelectedUserPanel(userPanelMenuKey.statementHistory), title: 'Statement History'},
            { title: `${monthNames[date.getMonth()]} ${date.getFullYear()}`}
        ];

        return monthDate && (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
            <LinkMenu linkMenuList={linkMenuList} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ dailyEarning }) {
    const { isShowLoader, param } = dailyEarning;
    return Object.assign({}, { isShowLoader, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));