import React from 'react';
import { connect } from 'react-redux';

import CommissionPanel from './panel/CommissionPanel';
import SettingPanel from './panel/SettingPanel';
import OnlineInfoPanel from './panel/OnlineInfoPanel';
import { status } from '../libs/constants';
import { claimResource } from '../../../../modules/claim';

export const PanelList = (props) => {
    const { affiliateInfo, claimsPrincipal } = props;
    const isApproved = affiliateInfo && affiliateInfo.Status && affiliateInfo.Status.toLowerCase() === status.Approved;

    return affiliateInfo && (
        <React.Fragment>
            {isApproved && claimsPrincipal.hasReadClaim(claimResource.AffCommGroup) && <CommissionPanel/>}
            {<SettingPanel isApproved={isApproved} />}
            {isApproved && <OnlineInfoPanel />}
        </React.Fragment>
    )
}

function mapStateToProps({ leftPanel, claimsPrincipal }) {
    const { affiliateInfo } = leftPanel;

    return Object.assign({}, { affiliateInfo, claimsPrincipal });
};

export default connect(mapStateToProps)(React.memo(PanelList));