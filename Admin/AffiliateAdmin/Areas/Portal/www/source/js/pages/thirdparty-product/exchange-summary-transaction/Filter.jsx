import React, { PureComponent } from 'react';

import { Form, FormGroup, Input, Label, InputGroup, InputGroupAddon, Button } from 'reactstrap';
import { getNowString } from '../modules/utils';

const environmentList = ['PRD','UAT','POC','SIT']

export default class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const nowString = getNowString();
        this.state = {
            startTime: nowString,
            endTime: nowString,
            pageNumber: 1,
            isGetByApiTimeZone: true,
            custId: 0,
            environment: environmentList[0]
        }
    }

    handleInputChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit = () => {
        const param = Object.assign({}, this.state);
        this.props.onGetRawData(param);
    }

    render() {
        const { startTime, endTime, pageNumber, isGetByApiTimeZone, custId, environment } = this.state;
        const { isLoadingData } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">Start Time</Label>
                        <InputGroup size="sm">
                            <Input type="text" name="startTime" value={startTime} onChange={this.handleInputChange} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">End Time</Label>
                        <InputGroup size="sm">
                            <Input type="text" name="endTime" value={endTime} onChange={this.handleInputChange} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">Page Number</Label>
                        <Input type="text" name="pageNumber" value={pageNumber} onChange={this.handleInputChange} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">custId</Label>
                        <Input type="text" name="custId" value={custId} onChange={this.handleInputChange} />
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">Environment</Label>
                        <Input type="select" name="environment" id="environment" value={environment} onChange={this.handleInputChange}>
                            {
                                environmentList.map(env => {
                                    return (<option key={'env-' + env} value={env}>{env}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup check className="mb-2 mr-sm-4 mb-sm-0">
                        <Label check>
                            <Input type="checkbox" name="isGetByApiTimeZone" checked={isGetByApiTimeZone} onChange={this.handleInputChange} />{' '}
                            Use Api Time Zone (GMT +8)
                        </Label>
                    </FormGroup>

                    <Button color="primary" type="button" disabled={isLoadingData} onClick={this.handleSubmit}>
                        Get Raw Data
                    </Button>
                </Form>
            </div>
        );
    }
}