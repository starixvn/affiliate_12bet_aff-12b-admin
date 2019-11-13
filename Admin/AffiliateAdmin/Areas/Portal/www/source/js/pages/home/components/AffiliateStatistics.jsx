import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row } from 'reactstrap';

import Panel from './Panel';
import { resolveClientUrl } from '../../../modules/utils';
import { getAffiliateStatisticsUpToNow } from '../actions';

export class AffiliateStatistics extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {

        }
    }

    componentDidUpdate() {
    }

    render() {
        const { } = this.state;
        const { isUpToNow } = this.props;

        return (
            <Panel heading={"Affiliate statistics (up to now)"} className={"icon-home01"}>
                <Row>
                    <Col sm={12}>
                        <span>We have 3,113 affiliates in the program</span>
                    </Col>
                    <Col sm={12}>
                        <span><a href={resolveClientUrl("Portal/Affiliate?Status=Pending&IsFromHome=true")}>1,024</a> Pending / <a href={resolveClientUrl("Portal/Affiliate?Status=Approved&IsFromHome=true")}>236</a> Approved / <a href={resolveClientUrl("Portal/Affiliate?Status=Disapproved&IsFromHome=true")}>1,016</a> Disapproved / <a href={resolveClientUrl("Portal/Affiliate?Status=Closed&IsFromHome=true")}>837</a> Closed</span>
                    </Col>
                </Row>
            </Panel>
        );
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateStatisticsUpToNow }, dispatch);
}

export default connect(null, mapDispatchToProps)(AffiliateStatistics);