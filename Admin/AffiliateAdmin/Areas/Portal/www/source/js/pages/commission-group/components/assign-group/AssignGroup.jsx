import React, { Component } from "react";
import { post } from '../../../../modules/ajaxRequest';
import { Col, FormGroup, Label, Button, Input, Alert } from 'reactstrap';
import { messages } from '../../modules/constant';
import Loader from '../../../../components/Loader';
import { popupEventNames } from '../../../common/modules/constants/commissionGroup';
import { htmlDecode } from '../../../../modules/htmlHelper';

export default class AssignGroup extends Component {
    constructor(props) {
        super(props);

        let { commGroupList, commGroupTypes, selectedGroupType, selectedGroup, affInfo } = window.initialState;

        const commGroupListByType = commGroupList.filter(g => g.GroupTypeId === selectedGroupType);

        if (selectedGroup === 0) {
            selectedGroup = commGroupList && commGroupList.length ? commGroupList[0].GroupId : 0;
        }

        this.state = Object.assign({}, { commGroupList, commGroupTypes, selectedGroupType, commGroupListByType, selectedGroup, affInfo }, {
            isPrivateGroup: false,
            isShowLoader: false,
            isShowMessage: false,
            isSuccess: true,
            message: ''
        });
    }

    handleChangeGroupType = (event) => {
        const { value } = event.target;
        const { commGroupList, affInfo } = this.state;

        const newCommGroupListByType = commGroupList.filter(g => g.GroupTypeId === Number(value));
        const affGroup = newCommGroupListByType ? newCommGroupListByType.find(g => g.GroupId === affInfo.GroupId) : undefined;

        this.setState({
            selectedGroupType: Number(value),
            selectedGroup: affGroup ? affGroup.GroupId : (newCommGroupListByType && newCommGroupListByType.length > 0 ? newCommGroupListByType[0].GroupId : 0),
            commGroupListByType: newCommGroupListByType
        });
    }

    handleChangeGroup = (event) => {
        const { value } = event.target;
        this.setState({ selectedGroup: Number(value) });
    }

    handleCheck = (event) => {
        const { checked } = event.target;
        this.setState({ isPrivateGroup: checked });
    }

    handleSave = async () => {
        const { isPrivateGroup, affInfo, selectedGroup, selectedGroupType } = this.state;
        if (isPrivateGroup) {
            parent.postMessage(
                {
                    eventName: popupEventNames.changeAssignToPrivateGroup,
                    groupTypeId: selectedGroupType
                }
                , window.location.origin);
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

            this.setState({ isShowLoader: true });
            const url = 'Portal/CommGroupAssignment/AssignGroup';
            const params = {
                assignParam: {
                    groupId: selectedGroup,
                    affId: affInfo.AffId
                },
                affInfo
            }
            const response = await post(url, params);
            if (response && response.status === 200) {
                const result = response.data;

                this.setState({
                    isShowMessage: true,
                    isSuccess: result.IsSuccessful,
                    message: result.IsSuccessful ? messages.assignGroupSuccessfully : result.Message
                }, () => window.setTimeout(() => {
                    this.hideMessage();
                    result.IsSuccessful && parent.postMessage(popupEventNames.assignGroupSuccessfully, window.location.origin);
                }, 3000));
            }

            this.setState({ isShowLoader: false });
        }
    }

    hideMessage = () => {
        this.setState({
            isShowMessage: false
        });
    }

    render() {
        const { commGroupTypes, commGroupListByType, selectedGroupType, selectedGroup, affInfo, isPrivateGroup, isShowLoader, isShowMessage, isSuccess, message } = this.state;
        return affInfo && (
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
                    <legend><h6 className="font-weight-bold">Revenue Group</h6></legend>
                    <FormGroup row>
                        <Label xs="4" className="font-weight-bold">New Revenue Group Type: </Label>
                        <Col xs="5" className="select-comm">
                            <select name="selectedGroupType" className="form-control" onChange={this.handleChangeGroupType} value={selectedGroupType}>
                                {
                                    commGroupTypes && commGroupTypes.map((item, index) => {
                                        return (<option key={`comm-group-type-${index}`} value={item.GroupTypeId}>{htmlDecode(item.GroupTypeName)}</option>)
                                    })
                                }
                            </select>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label xs="4" className="font-weight-bold">New Revenue Group: </Label>
                        <Col xs="5" className="select-comm">
                            <select name="selectedGroup" className="form-control" onChange={this.handleChangeGroup} value={selectedGroup}>
                                {
                                    commGroupListByType && commGroupListByType.map((item, index) => {
                                        return (<option key={`comm-group-${index}`} value={item.GroupId}>{htmlDecode(item.GroupName)}</option>)
                                    })
                                }
                            </select>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label className="font-weight-bold" htmlFor="isPrivateGroup" xs="4">Use private group: </Label>
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
                <p className="text-danger">{messages.note}</p>
                <Button type="button" className="my-2 btn btn-success" onClick={this.handleSave}>SAVE</Button>
            </div>
        );
    }
}