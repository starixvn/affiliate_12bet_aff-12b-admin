import React, { Component } from "react";
import { Col, FormGroup, Label, Button, Input } from 'reactstrap';
import { resolveClientUrl } from '../../../../modules/utils';

export default class AssignNormalGroup extends Component {
    constructor(props) {
        super(props);

        this.state = Object.assign({}, window.initialState, { isPrivateGroup: false });
    }

    handleChange = (event) => {
        const { value } = event.target;
        this.setState({ selectedGroup: value });
    }

    handleCheck = (event) => {
        const { checked } = event.target;
        this.setState({ isPrivateGroup: checked });
    }

    handleSave = () => {
        console.log(this.state);
        const { isPrivateGroup } = this.state;
        if (isPrivateGroup) {
            window.location.href = resolveClientUrl('Portal/CommissionGroup/AssignPrivateGroup');
        }
        else {

        }
    }

    render() {
        const { commGroupList, selectedGroup, affName, isPrivateGroup } = this.state;
        return (
            <div className="assign-normal-group">
                <fieldset>
                    <legend><h6 className="font-weight-bold">Assignee</h6></legend>
                    <p>{affName}</p>
                </fieldset>
                <fieldset>
                    <legend><h6 className="font-weight-bold">Comm Group</h6></legend>
                    <FormGroup row>
                        <Label sm={3} className="font-weight-bold">New Comm Group: </Label>

                        <Col sm={5} className="select-comm">
                            <select name="SelectedGroup" className="form-control" onChange={this.handleChange} value={selectedGroup}>
                                {
                                    commGroupList && commGroupList.map((item, index) => {
                                        return (<option key={`commgroup-${index}`} value={item.Id}>{item.Name}</option>)
                                    })
                                }
                            </select>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label className="font-weight-bold" for="isPrivateGroup" sm={3}>Use private group: </Label>
                        <Col sm={5}>
                            <FormGroup check>
                                <Label check>
                                    <Input type="checkbox" id="isPrivateGroup" onChange={this.handleCheck} checked={isPrivateGroup} />
                                </Label>
                            </FormGroup>
                        </Col>
                    </FormGroup>
                </fieldset>
                <Button type="button" className="my-2 btn btn-success" onClick={this.handleSave}>SAVE</Button>
            </div>
        );
    }
}