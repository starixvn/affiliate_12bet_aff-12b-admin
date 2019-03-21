import React from 'react';

//import GeneralInfo from './GeneralInfo';
//import PanelList from './PanelList';
import { Col, Button, Form, Label, Input } from 'reactstrap';

const MainInfo = (props) => {
    return (
        <Form tag="div">
            <FormGroup row>
                <Label sm={2}>Affiliate</Label>
                <Col sm={10}>
                    <Input plaintext value="ABC" />
                </Col>
            </FormGroup>
        </Form >
    )
}

export default MainInfo;