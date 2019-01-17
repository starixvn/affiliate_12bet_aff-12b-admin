import React, { PureComponent } from 'react';

import { Form, FormGroup, Input, Label, InputGroup, InputGroupAddon, Button } from 'reactstrap';
import moment from 'moment';

export default class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const nowString = moment().format('YYYY/MM/DD HH:mm:ss');
        this.state = {
            startTime: nowString,
            endTime: nowString
        }
    }

    render() {
        const { startTime, endTime } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">Start Time</Label>
                        <InputGroup size="sm">
                            <Input type="text" value={startTime} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">End Time</Label>
                        <InputGroup size="sm">
                            <Input type="text" value={endTime} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>

                    <Button color="primary" type="button">
                        Get Raw Data
                    </Button>
                </Form>
            </div>
        );
    }
}