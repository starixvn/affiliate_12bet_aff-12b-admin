import React, { PureComponent } from "react";
import { Container, Button, Row, Col } from 'reactstrap';

import Section from './Section';
import {Personal} from './sections/Personal';
import {Additional} from './sections/Additional';
import {EmailFormat} from './sections/EmailFormat';
import {WebSite} from './sections/WebSite';
import {Payment} from './sections/Payment';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Container fluid={true} className="user-account-container">
                <Row>
                    <Col xs="6">
                        <Section title="Personal Information">
                            <Personal />
                        </Section>
                    </Col>
                    <Col xs="6">
                        <Section title="Additional Information">
                            <Additional />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Email Format Preference">
                            <EmailFormat />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Website Information">
                            <WebSite />
                        </Section>
                    </Col>
                </Row>

                <Row>
                    <Col xs="12">
                        <Section title="Payment Information">
                            <Payment />
                        </Section>
                    </Col>
                </Row>

                <Button color="success">Submit</Button>
            </Container>
        );
    }
}