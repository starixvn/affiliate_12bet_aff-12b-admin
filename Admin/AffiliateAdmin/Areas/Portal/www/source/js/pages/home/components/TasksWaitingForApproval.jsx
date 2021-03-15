import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row } from 'reactstrap';

import { resolveClientUrl } from '../../../modules/utils';
import { getGeneralInfo } from '../actions';
import { format } from '../../../modules/numberFormat';
import { claimResource } from '../../../modules/claim';

export class TasksWaitingForApproval extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.getGeneralInfo();
    }

    render() {
        const { generalInfo, isShowAffLink, isShowPaymentLink } = this.props;

        return (
            <div className="waiting-approval">
                <Row>
                    <span className="tbl-icon02"></span>
                    <span className="text-blue">Tasks waiting for approval</span>
                    <Col sm={12} className="background-gray">
                        <Col sm={3} className="text-border">
                            <span><strong>Affiliate</strong></span>
                        </Col>
                        <Col sm={2} className="text-border">
                            {
                                isShowAffLink ? (
                                    <a href={resolveClientUrl("Portal/Affiliate?Status=Pending&IsFromHome=true")}><strong>{format(generalInfo.PendingAffiliateNumber)}</strong></a>
                                ) : (
                                        <strong>{format(generalInfo.PendingAffiliateNumber)}</strong>
                                    )
                            }
                        </Col>
                        <Col sm={5} className="text-border">
                            <span><strong>Payment Request</strong></span>
                        </Col>
                        <Col sm={2} className="text-border">
                            {
                                isShowPaymentLink ? (
                                    <a href={resolveClientUrl("Portal/PaymentRequest?SearchBy=2&Status=Pending&FromDate=12/01/2009&IsFromHome=true")}><strong>{format(generalInfo.PaymentRequestNumber)}</strong></a>
                                ) : (
                                        <strong>{format(generalInfo.PaymentRequestNumber)}</strong>
                                    )
                            }
                        </Col>
                    </Col>
                </Row>
            </div>
        )
    }
}

function mapStateToProps({ generalInfo, claimsPrincipal }) {
    const isShowAffLink = claimsPrincipal.hasReadClaim(claimResource.AffiliateInfo);
    const isShowPaymentLink = claimsPrincipal.hasReadClaim(claimResource.PaymentRequest);

    return Object.assign({}, { generalInfo, isShowAffLink, isShowPaymentLink });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGeneralInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(TasksWaitingForApproval);