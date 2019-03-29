import React, { PureComponent } from 'react';

import { Form, FormGroup, Input, Label, InputGroup, InputGroupAddon, Button } from 'reactstrap';
import { getNowString } from '../../modules/utils';
import { productList } from '../../modules/constants';

export default class Filter extends PureComponent {
    constructor(props) {
        super(props);

        var nowString = getNowString();
        this.state = {
            productId: productList[0].id,
            fromDate: nowString,
            toDate: nowString,
            jobType: 0
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
        this.props.onSubmit(param);
    }

    render() {
        const { productId, fromDate, toDate, jobType } = this.state;
        const { isLoadingData } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">ProductId</Label>
                        <Input type="text" name="productId" value={productId} onChange={this.handleInputChange} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">From Date</Label>
                        <InputGroup size="sm">
                            <Input type="text" name="fromDate" value={fromDate} onChange={this.handleInputChange} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">To Date</Label>
                        <InputGroup size="sm">
                            <Input type="text" name="toDate" value={toDate} onChange={this.handleInputChange} />
                            <InputGroupAddon addonType="append">yyyy/MM/dd hh:mm:ss</InputGroupAddon>
                        </InputGroup>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-4 mb-sm-0">
                        <Label className="mr-2">Job Type</Label>
                        <Input type="text" name="jobType" value={jobType} onChange={this.handleInputChange} />
                    </FormGroup>

                    <Button color="primary" type="button" disabled={isLoadingData} onClick={this.handleSubmit}>
                        Submit
                    </Button>
                </Form>
            </div>
        );
    }
}