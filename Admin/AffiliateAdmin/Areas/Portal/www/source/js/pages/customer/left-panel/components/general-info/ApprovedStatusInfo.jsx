import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button } from 'reactstrap';

import ChangePasswordForm from '../../../../common/components/ChangePasswordForm';
import { resource } from '../../modules/constants';
import { resolveClientUrl } from '../../../../../modules/utils';


const ApprovedStatusInfo = (props) => {
    const [isChangingPassword, setChangingPassword] = useState(false);
    const { affiliateInfo, affName } = props;

    function toggleChangingPassword(isChanging) {
        setChangingPassword(isChanging);
    }

    return (
        <React.Fragment>
            <Row>
                <Col sm={5}>{resource.Password}</Col>
                <Col sm={7} className='text-right text-primary'>
                    <Button color="link p-0 left-panel-btn-link" onClick={() => toggleChangingPassword(true)}>
                        [{resource.Change}]
                    </Button>
                    {affiliateInfo && <ChangePasswordForm
                        showPopupPassword={isChangingPassword}
                        affId={affiliateInfo.AffId}
                        setShowPopupPassword={toggleChangingPassword}
                    />}
                </Col>
            </Row>
            <a href={resolveClientUrl(`Portal/Member?txtAffUsername=${affName}`)} target='fMain'>{resource.MemberList}</a>
        </React.Fragment>
    )
}

function mapStateToProps({ affCommonInfo, leftPanel }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, affName });
}

export default connect(mapStateToProps)(React.memo(ApprovedStatusInfo));