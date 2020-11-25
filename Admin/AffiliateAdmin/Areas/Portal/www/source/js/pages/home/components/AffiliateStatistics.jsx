import React from 'react';
import { connect } from 'react-redux';
import { Col, Row } from 'reactstrap';

import Panel from './Panel';
import { resolveClientUrl } from '../../../modules/utils';
import { format } from '../../../modules/numberFormat';
import { claimResource } from '../../../modules/claim';

export const AffiliateStatistics = (props) => {

    const { isUpToNow, generalInfo, isShowAffLink } = props;

    return (
        <Panel heading={`Affiliate statistics (${isUpToNow ? 'up to now' : 'this month'})`} className={"icon-home01"}>
            <Row>
                <Col sm={12} className="pl-4">
                    <span>We have <strong>{format(isUpToNow ? generalInfo.AllAffiliateNumber : generalInfo.AllAffiliateMonthNumber)}</strong> affiliates in the program</span>
                </Col>
                <Col sm={12} className="pl-4">
                    {
                        isUpToNow ? (
                            isShowAffLink ? (
                                <span>
                                    <a href={resolveClientUrl("Portal/Affiliate?Status=Pending&IsFromHome=true")}><strong>{format(generalInfo.PendingAffiliateNumber)}</strong></a> Pending / <a href={resolveClientUrl("Portal/Affiliate?Status=Approved&IsFromHome=true")}><strong>{format(generalInfo.ApprovedAffiliateNumber)}</strong></a> Approved / <a href={resolveClientUrl("Portal/Affiliate?Status=Disapproved&IsFromHome=true")}><strong>{format(generalInfo.DisapprovedAffiliateNumber)}</strong></a> Disapproved / <a href={resolveClientUrl("Portal/Affiliate?Status=Closed&IsFromHome=true")}><strong>{format(generalInfo.ClosedAffiliateNumber)}</strong></a> Closed
                                </span>
                            ) : (
                                    <span>
                                        <strong>{format(generalInfo.PendingAffiliateNumber)}</strong> Pending / <strong>{format(generalInfo.ApprovedAffiliateNumber)}</strong> Approved / <strong>{format(generalInfo.DisapprovedAffiliateNumber)}</strong> Disapproved / <strong>{format(generalInfo.ClosedAffiliateNumber)}</strong> Closed
                                    </span>
                                )
                        ) : (
                                <span>
                                    <strong>{format(generalInfo.PendingAffiliateMonthNumber)}</strong> Pending / <strong>{format(generalInfo.ApprovedAffiliateMonthNumber)}</strong> Approved / <strong>{format(generalInfo.DisapprovedAffiliateMonthNumber)}</strong> Disapproved / <strong>{format(generalInfo.ClosedAffiliateMonthNumber)}</strong> Closed
                                </span>
                            )
                    }
                </Col>
            </Row>
        </Panel>
    );
}

function mapStateToProps({ generalInfo, claimsPrincipal }) {
    const isShowAffLink = claimsPrincipal.hasReadClaim(claimResource.AffInfo);

    return Object.assign({}, { generalInfo, isShowAffLink });
}

export default connect(mapStateToProps)(React.memo(AffiliateStatistics));