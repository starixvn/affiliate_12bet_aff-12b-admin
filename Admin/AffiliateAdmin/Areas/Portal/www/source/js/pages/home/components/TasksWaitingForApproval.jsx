import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Row } from 'reactstrap';

import { resolveClientUrl } from '../../../modules/utils';
import { getTasksWaitingForApproval } from '../actions';

export class TasksWaitingForApproval extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            tasksWaitingForApproval: null
        }
    }

    //componentDidMount() {
    //    this.props.getTasksWaitingForApproval();
    //}

    render() {
        const { tasksWaitingForApproval } = this.state;

        return (
            <div className="waiting-approval">
                <Row>
                    <span className="tbl-icon02"></span>
                    <h5>Tasks waiting for approval</h5>
                    <Col sm={12}>
                        <span sm={3}>Affiliate</span>
                        <a sm={2} href={resolveClientUrl("Portal/Affiliate?Status=Pending&IsFromHome=true")}>1,024</a>
                        <span sm={4}>Payment Request</span>
                        <a sm={2} href={resolveClientUrl("Portal/PaymentRequest?SearchBy=2&Status=Pending&FromDate=12/01/2009&IsFromHome=true")}>1</a>
                    </Col>
                </Row>
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getTasksWaitingForApproval }, dispatch);
}

export default connect(null, mapDispatchToProps)(TasksWaitingForApproval);