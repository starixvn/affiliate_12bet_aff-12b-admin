import React from "react";
import { Col, Button, FormGroup, Label, Input, FormText } from 'reactstrap';

const CommGroupInfo = (props) => {
    return (
        <div>
            <FormGroup row>
                <Label for="GroupName" sm={2}>Group Name: </Label>
                <Col sm={8}>
                    <Input type="text" name="GroupName" id="GroupName" />
                </Col>
                <Col sm={2}>
                    <p className="text-danger required">*</p>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label for="Description" sm={2}>Description: </Label>
                <Col sm={10}>
                    <Input type="text" name="Description" id="Description" />
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label for="ActiveAll" sm={2}>ActiveAll: </Label>
                <Col sm={{ size: 10 }}>
                    <FormGroup check>
                        <Label check>
                            <Input type="checkbox" id="ActiveAll" />
                            &nbsp;
                        </Label>
                    </FormGroup>
                </Col>
            </FormGroup>
        </div>
    )
}

export default CommGroupInfo;