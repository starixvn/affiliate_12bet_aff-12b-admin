import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedUserPanel } from '../../../customer/user-panel/actions';
import { monthNames } from '../../../../modules/constants';

const LinkMenu = (props) => {
    const { param } = props;
    const monthDate = param && param.monthDate;
    const date = new Date(monthDate);

    return monthDate && (
        <div className="breadcrumb-container">
            <ol className="breadcrumb">
                <li className="breadcrumb-item"><a href="javascript:;" onClick={() => props.setSelectedUserPanel("StatementHistory")}>Statement History</a></li>
                <li className="breadcrumb-item active">{`${monthNames[date.getMonth()]} ${date.getFullYear()}`}</li>
            </ol>
        </div>
    );
}

function mapStateToProps({ userPanel }) {
    const { param } = userPanel;
    return Object.assign({}, { param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LinkMenu));