import React, { PureComponent } from 'react';
import { Container, Row, Col, Card, CardHeader, CardBody, CardText, Input } from 'reactstrap';

export default class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { data } = this.props;
        if (!data) {
            return null;
        }

        const { RawData, ConvertedData } = data;

        return (
            <div className="my-2">
                <Container fluid={true}>
                    <Row>
                        <Col xs={ConvertedData === null ? 12 : 6}>
                            <Card>
                                <CardHeader>Raw Data</CardHeader>
                                <CardBody>
                                    <CardText>
                                        <Input type="textarea" readOnly value={RawData} rows={55} />
                                    </CardText>
                                </CardBody>
                            </Card>
                        </Col>
                        {ConvertedData !== null && (
                            <Col xs="6">
                                <Card>
                                    <CardHeader>Converted Data</CardHeader>
                                    <CardBody>
                                        <CardText>
                                            <Input type="textarea" readOnly value={data.ConvertedData} rows={55} />
                                        </CardText>
                                    </CardBody>
                                </Card>
                            </Col>)
                        }
                    </Row>
                </Container>
            </div>
        )
    }
}