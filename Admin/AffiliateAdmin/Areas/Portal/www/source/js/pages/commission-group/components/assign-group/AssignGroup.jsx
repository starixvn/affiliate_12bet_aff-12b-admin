import React, { Component } from "react";
import { post } from '../../../../modules/ajaxRequest';
import { Col, FormGroup, Label, Button, Input, Alert } from 'reactstrap';
import { messages } from '../../modules/constant';
import Loader from '../../../../components/Loader';

export default class AssignGroup extends Component {
    constructor(props) {
        super(props);

        let { commGroupList, selectedGroup, affInfo } = window.initialState;
        if (selectedGroup === 0) {
            selectedGroup = commGroupList.length ? Number(commGroupList[0].Id) : 0
        }

        this.state = Object.assign({}, { commGroupList, selectedGroup, affInfo }, {
            isPrivateGroup: false,
            isShowLoader: false,
            isShowMessage: false,
            isSuccess: true,
            message: ''
        });
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
            parent.postMessage('usingPrivateGroup', window.location.origin);
        }
        else {
            if (affInfo.GroupId > 0 && affInfo.GroupId === selectedGroup) {
                this.setState({
                    isShowMessage: true,
                    isSuccess: false,
                    message: messages.choseAnotherGroup
                }, () => window.setTimeout(this.hideMessage, 3000));
                return;
            }

            if (confirm(messages.confirmChangeGroup.replace('{0}', affInfo.UserName))) {
                this.setState({ isShowLoader: true });
                const url = 'Portal/CommissionGroup/AssignGroup';
                const params = {
                    assignParam: {
                        groupId: selectedGroup
                    },
                    affInfo
                }
                const response = await post(url, params);
                if (response && response.status === 200) {
                    const result = response.data;

                    if (result.IsSuccessful) {
                        this.setState({
                            isShowMessage: true,
                            isSuccess: result.IsSuccessful,
                            message: messages.assignGroupSuccessfully
                        }, () => window.setTimeout(this.hideMessage, 3000));
                        window.setTimeout(() => {
                            parent.postMessage('closeCommissionGroupPopup', window.location.origin);
                        }, 3100);
                    }
                    else {
                        this.setState({
                            isShowMessage: true,
                            isSuccess: false,
                            message: result.Message
                        }, () => window.setTimeout(this.hideMessage, 3000));
                    }
                }

                this.setState({ isShowLoader: false });
            }
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { commGroupList, selectedGroup, affInfo, isPrivateGroup, isShowLoader, isShowMessage, isSuccess, message } = this.state;
        return (
            <div className="assign-normal-group" style={{ padding: 10 }}>
                <Loader show={isShowLoader} />
                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                </Alert>
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