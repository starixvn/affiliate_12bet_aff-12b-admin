import React, { Component } from "react";
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

export class WebSite extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="WebSiteUrl" sm={3}>Website URL</Label>
                    <Col sm={9}>
                        <Input type="text" id="WebSiteUrl" name="WebSiteUrl" value="WebSiteUrl" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}