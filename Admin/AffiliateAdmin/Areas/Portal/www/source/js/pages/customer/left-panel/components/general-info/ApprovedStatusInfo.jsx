import React, { useState } from 'react';
import { connect } from 'react-redux';
import { Row, Col, Button } from 'reactstrap';

import ChangePasswordForm from '../../../../common/components/ChangePasswordForm';
import { resource } from '../../libs/constants';
import { resolveClientUrl } from '../../../../../modules/utils';
import { claimResource } from '../../../../../modules/claim';


export const ApprovedStatusInfo = (props) => {
    const [isChangingPassword, setChangingPassword] = useState(false);
    const { affiliateInfo, affName, isShowMemberLstLink } = props;

    function toggleChangingPassword(isChanging) {
        setChangingPassword(isChanging);
    }

    return (
        <React.Fragment>
            { props.isPwdChangeable && <Row>
                <Col sm={5}>{resource.Password}</Col>
                <Col sm={7} className='text-right text-primary'>
                    <Button color="link left-panel-btn-link" onClick={() => toggleChangingPassword(true)}>
                        [{resource.Change}]
                    </Button>
                    {affiliateInfo && <ChangePasswordForm
                        showPopupPassword={isChangingPassword}
                        selectedId={affiliateInfo.AffId}
                        setShowPopupPassword={toggleChangingPassword}
                    />}
                </Col>
            </Row>
            }
            { isShowMemberLstLink && <a href={resolveClientUrl(`Portal/Member?AffName=${affName}`)} target='fMain'>{resource.MemberList}</a>}
        </React.Fragment>
    )
}

function mapStateToProps({ affCommonInfo, leftPanel, claimsPrincipal }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;
    const isPwdChangeable = claimsPrincipal.hasWriteClaim(claimResource.AffPassword);
    const isShowMemberLstLink = claimsPrincipal.hasReadClaim(claimResource.AffMember)
    return Object.assign({}, { affiliateInfo, affName, isPwdChangeable, isShowMemberLstLink });
}

export default connect(mapStateToProps)(React.memo(ApprovedStatusInfo));