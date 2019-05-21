import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import LinkMenu from '../../daily-earning/components/LinkMenu';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';

import { setSelectedUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { selectedAllProduct, monthNames } from '../../../../modules/constants';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { param, affId } = props;
        const monthDate = param && param.monthDate;
        const date = new Date(monthDate);
        const dailyEarningParam = {
            affId,
            monthDate,
            productId: param.selectedProductId
        }

        const linkMenuList = [
            { handleClick: () => props.setSelectedUserPanel(userPanelMenuKey.statementHistory), title: 'Statement History' },
            { handleClick: () => props.redirectToDailyEarning(dailyEarningParam), title: `${monthNames[date.getMonth()]} ${date.getFullYear()}` },
            { title: monthDate }
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

function mapStateToProps({ affCommonInfo, dailyEarningDetail }) {
    const { isShowLoader, param } = dailyEarningDetail;
    const { affId } = affCommonInfo;
    return Object.assign({}, { isShowLoader, param, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel, redirectToDailyEarning }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));