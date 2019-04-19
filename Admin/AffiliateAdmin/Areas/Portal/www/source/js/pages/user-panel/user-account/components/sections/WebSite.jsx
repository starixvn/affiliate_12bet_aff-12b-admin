import React, { PureComponent } from "react";
import { Col, Form, FormGroup, Label, Input, Button } from 'reactstrap';

export class WebSite extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="websiteUrl" sm={3} xl={2}>Website URL</Label>
                    <Col sm={5} xl={6}>
                        <Input type="text" id="websiteUrl" name="websiteUrl" value="www.betwiki.net" />
                    </Col>
                    <Button color="primary"><span aria-hidden>&ndash;</span></Button>
                </FormGroup>
                <FormGroup row>
                    <Label for="websiteDescribe" sm={3} xl={2}>I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL .</Label>
                    <Col sm={6} xl={7}>
                        <Input type="textarea" style={{ height: 70 }} id="websiteDescribe" name="websiteDescribe" value="websiteDescribe value" />
                    </Col>
                </FormGroup>
            </Form>
        )
    }
}