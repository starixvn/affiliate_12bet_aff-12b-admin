import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedUserPanel, redirectToDailyEarning } from '../../../customer/user-panel/actions';
import { monthNames } from '../../../../modules/constants';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

const LinkMenu = (props) => {
    const { param, affId } = props;
    const monthDate = param && param.monthDate;
    const date = new Date(monthDate);
    const dailyEarningParam = {
        affId,
        monthDate,
        productId: param.selectedProductId
    }

    return monthDate && (
        <div className="breadcrumb-container">
            <ol className="breadcrumb">
                <li className="breadcrumb-item">
                    <a href="javascript:;" onClick={() => props.setSelectedUserPanel(userPanelMenuKey.statementHistory)}>
                        Statement History
                    </a>
                </li>
                <li className="breadcrumb-item">
                    <a href="javascript:;" onClick={() => props.redirectToDailyEarning(dailyEarningParam)}>
                        {`${monthNames[date.getMonth()]} ${date.getFullYear()}`}
                    </a>
                </li>
                <li className="breadcrumb-item active">{monthDate}</li>
            </ol>
        </div>
    );
}

function mapStateToProps({ affCommonInfo }) {
    const { affId } = affCommonInfo;
    return Object.assign({}, { affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel, redirectToDailyEarning }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LinkMenu));