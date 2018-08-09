import React from "react";
import { Col, FormGroup, Label, Input } from 'reactstrap';

export default class CommGroupInfo extends React.Component {
    constructor(props) {
        super(props);

        const { data } = this.props;
        this.state = {
            GroupName: data.GroupName,
            Description: data.Description
        }
    }

    handleChange = (event) => {
        const { name, value } = event.target;

        const newState = { [name]: value };
        this.setState(newState);
        this.props.onChange && this.props.onChange(name, value);
    }

    render() {
        const { GroupName, Description } = this.state;
        return (
            <div>
                <FormGroup row>
                    <Label for="GroupName" sm={2}>Group Name: </Label>
                    <Col sm={8}>
                        <Input type="text" name="GroupName" id="GroupName" value={GroupName} onChange={this.handleChange} />
                    </Col>
                    <Col sm={2}>
                        <p className="text-danger required">*</p>
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label for="Description" sm={2}>Description: </Label>
                    <Col sm={10}>
                        <Input type="text" name="Description" id="Description" value={Description} onChange={this.handleChange} />
                    </Col>
                </FormGroup>
            </div>
        )
    }
}