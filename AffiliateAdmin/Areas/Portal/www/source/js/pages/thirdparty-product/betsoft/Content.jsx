import React, { PureComponent } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody, CardText } from 'reactstrap';

export default class Filter extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { data } = this.props;
        if (!data) {
            return null;
        }

        return (
            <div className="my-2">
                <Container fluid={true}>
                    <Row>
                        <Col xs="6">
                            <Card>
                                <CardHeader>Raw Data</CardHeader>
                                <CardBody>
                                    <CardText>{data.RawData}</CardText>
                                </CardBody>
                            </Card>
                        </Col>
                        <Col xs="6">
                            <Card>
                                <CardHeader>Converted Data</CardHeader>
                                <CardBody>
                                    <CardText>{data.ConvertedData}</CardText>
                                </CardBody>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            </div>
        )
    }
}