import React from 'react';
import { connect } from 'react-redux';

import GeneralInfo from './GeneralInfo';
import PanelList from './PanelList';
import { Col, Button, Form, Label, Input, FormGroup } from 'reactstrap';

export class MainInfo extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label sm={2}>Affiliate</Label>
                    <Col sm={10}>
                        <Input plaintext value="ABC" />
                        <p>{this.props.affiliateInfo.AffName}</p>
                    </Col>
                </FormGroup>
                <GeneralInfo />
                <PanelList />
            </Form >
        );
    }
}

function mapStateToProps({ affiliateInfo }) {
    return Object.assign({}, { affiliateInfo });
}

export default connect(mapStateToProps)(MainInfo);