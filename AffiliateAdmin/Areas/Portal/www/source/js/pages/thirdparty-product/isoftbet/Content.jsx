import React, { PureComponent } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody, CardText } from 'reactstrap';

export default class Filter extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="my-2">
                <Container fluid={true}>
                    <Row>
                        <Col xs="6">
                            <Card>
                                <CardHeader>Raw Data</CardHeader>
                                <CardBody>
                                    <CardText>With supporting text below as a natural lead-in to additional content.</CardText>
                                </CardBody>
                            </Card>
                        </Col>
                        <Col xs="6">
                            <Card>
                                <CardHeader>Converted Data</CardHeader>
                                <CardBody>
                                    <CardText>With supporting text below as a natural lead-in to additional content.</CardText>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            </div>
        )
    }
}