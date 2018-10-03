import React, { Component } from 'react';
import { Col, Button, Form, FormGroup, Label, Input } from 'reactstrap';

export default class InsertUpdateForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: this.props.data
        }
    }

    handleChange = (event) => {
        const { name, value } = event.target;
        const data = Object.assign({}, this.state.data, { [name]: value });

        this.setState({ data });
    }

    handleSave = () => {
        this.props.onSave(this.state.data);
    }

    render() {
        const { onCancel } = this.props;
        const { data } = this.state;

        return (
            <div className="landing-page-form-container">
                <Form tag="div" className="mb-5">
                    <FormGroup row>
                        <Label for="PageName" sm={7}>Landing Page Name (Shown in dropdown)</Label>
                        <Col sm={5}>
                            <Input type="text" name="PageName" id="PageName" value={data.PageName} onChange={this.handleChange} />
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="PageCode" sm={7}>Landing Page Code (Contained in links)</Label>
                        <Col sm={5}>
                            <Input type="text" name="PageCode" id="PageCode" value={data.PageCode} onChange={this.handleChange} />
                        </Col>
                    </FormGroup>
                </Form>

                <Button type="button" color="success" className="mr-3" onClick={this.handleSave}>Submit</Button>
                <Button type="button" color="info" onClick={onCancel}>Cancel</Button>
            </div>
        )
    }
}