import React, { Component } from "react";
import { post } from '../../../../modules/ajaxRequest';
import { Col, FormGroup, Label, Button, Input } from 'reactstrap';
import { resolveClientUrl } from '../../../../modules/utils';
import { messages } from '../../modules/constant';

export default class AssignGroup extends Component {
    constructor(props) {
        super(props);

        let { commGroupList, selectedGroup, affInfo } = window.initialState;
        if (selectedGroup === 0) {
            selectedGroup = commGroupList.length ? Number(commGroupList[0].Id) : 0
        }

        this.state = Object.assign({}, { commGroupList, selectedGroup, affInfo }, { isPrivateGroup: false });
    }

    handleChange = (event) => {
        const { value } = event.target;
        this.setState({ selectedGroup: Number(value) });
    }

    handleCheck = (event) => {
        const { checked } = event.target;
        this.setState({ isPrivateGroup: checked });
    }

    handleSave = async () => {
        const { isPrivateGroup, affInfo, selectedGroup } = this.state;
        if (isPrivateGroup) {
            const url = resolveClientUrl('Portal/CommissionGroup/AssignPrivateGroup') + '?affId=' + affInfo.AffId;
            parent.ibcWnd.open(url, `Approve affiliate ${affInfo.UserName} with a private commission group`, 150, 50, 800, 529, 'class1', 'class2');
        }
        else {
            if(affInfo.GroupId > 0 && affInfo.GroupId === selectedGroup){
                window.alert(messages.choseAnotherGroup);
                return;
            }

            if (confirm(messages.confirmChangeGroup.replace('{0}', affInfo.UserName))) {
                const url = 'Portal/CommissionGroup/AssignGroup';
                const params = {
                    assignParam: {
                        groupId: selectedGroup
                    },
                    affInfo
                }
                const response = await post(url, params);
                const result = response.data;

                if (result.IsSuccessful) {
                    window.alert(messages.assignGroupSuccessfully);
                    const mainFrame = top.frames["mainFrame"];
                    if (mainFrame) {
                        mainFrame.location.reload();
                    }
                    else {
                        window.location.reload();
                    }
                }
                else {
                    window.alert(result.Message);
                }
            }
        }
    }

    render() {
        const { commGroupList, selectedGroup, affInfo, isPrivateGroup } = this.state;
        return (
            <div className="assign-normal-group" style={{ padding: 10 }}>
                <fieldset>
                    <legend><h6 className="font-weight-bold">Assignee</h6></legend>
                    <p>{affInfo.UserName}</p>
                </fieldset>
                <fieldset>
                    <legend><h6 className="font-weight-bold">Comm Group</h6></legend>
                    <FormGroup row>
                        <Label xs="4" className="font-weight-bold">New Comm Group: </Label>

                        <Col xs="5" className="select-comm">
                            <select name="SelectedGroup" className="form-control" onChange={this.handleChange} value={selectedGroup.toString()}>
                                {
                                    commGroupList && commGroupList.map((item, index) => {
                                        return (<option key={`commgroup-${index}`} value={item.Id}>{item.Name}</option>)
                                    })
                                }
                            </select>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label className="font-weight-bold" for="isPrivateGroup" xs="4">Use private group: </Label>
                        <Col xs="5">
                            <FormGroup check>
                                <Label check>
                                    <Input type="checkbox" id="isPrivateGroup" style={{ marginTop: -5 }}
                                        onChange={this.handleCheck} checked={isPrivateGroup} />
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