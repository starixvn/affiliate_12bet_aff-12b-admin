import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row } from 'reactstrap';

import Panel from './Panel';
import { resolveClientUrl } from '../../../modules/utils';
import { getAffiliateStatisticsUpToNow } from '../actions';
import { format } from '../../../modules/numberFormat';

export class AffiliateStatistics extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { isUpToNow, generalInfo } = this.props;

        return generalInfo && (
            <Panel heading={`Affiliate statistics (${isUpToNow ? 'up to now' : 'this month'})`} className={"icon-home01"}>
                <Row>
                    <Col sm={12} className="mx-2">
                        <span>We have <strong>{format(isUpToNow ? generalInfo.AllAffiliateNumber : generalInfo.AllAffiliateMonthNumber)}</strong> affiliates in the program</span>
                    </Col>
                    <Col sm={12} className="mx-2">
                        {
                            isUpToNow ? (
                                <span>
                                    <a href={resolveClientUrl("Portal/Affiliate?Status=Pending&IsFromHome=true")}><strong>{format(generalInfo.PendingAffiliateNumber)}</strong></a>
                                    Pending / <a href={resolveClientUrl("Portal/Affiliate?Status=Approved&IsFromHome=true")}><strong>{format(generalInfo.ApprovedAffiliateNumber)}</strong></a>
                                    Approved / <a href={resolveClientUrl("Portal/Affiliate?Status=Disapproved&IsFromHome=true")}><strong>{format(generalInfo.DisapprovedAffiliateNumber)}</strong></a>
                                    Disapproved / <a href={resolveClientUrl("Portal/Affiliate?Status=Closed&IsFromHome=true")}><strong>{format(generalInfo.ClosedAffiliateNumber)}</strong></a>
                                    Closed
                                </span>
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
}

function mapStateToProps({ generalInfo }) {
    return Object.assign({}, { generalInfo });
}

export default connect(mapStateToProps)(AffiliateStatistics);