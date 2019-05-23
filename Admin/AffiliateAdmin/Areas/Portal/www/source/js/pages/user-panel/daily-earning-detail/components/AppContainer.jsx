import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import UserPanelBreadcrumb from '../../../common/components/UserPanelBreadcrumb';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';

import { setSelectedUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { monthNames } from '../../../../modules/constants';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        const { param, affId } = this.props;
        this.affId = affId;
        this.monthDate = param && param.monthDate;
        this.date = new Date(this.monthDate);
    }

    render() {
        const { param } = this.props;
        const dailyEarningParam = {
            affId: this.affId,
            monthDate: this.monthDate,
            productId: param.productId
        }

        const breadcrumbList = [
            { handleClick: () => this.props.setSelectedUserPanel(userPanelMenuKey.statementHistory), title: 'Statement History' },
            { handleClick: () => this.props.redirectToDailyEarning(dailyEarningParam), title: `${monthNames[this.date.getMonth()]} ${this.date.getFullYear()}` },
            { title: this.monthDate }
        ];

        return this.monthDate && (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <UserPanelBreadcrumb breadcrumbList={breadcrumbList} />
                <Filter />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ affCommonInfo, dailyEarningDetail }) {
    const { isShowLoader, param } = dailyEarningDetail;
    const { affId } = affCommonInfo;
    return Object.assign({}, { isShowLoader, param, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel, redirectToDailyEarning }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);