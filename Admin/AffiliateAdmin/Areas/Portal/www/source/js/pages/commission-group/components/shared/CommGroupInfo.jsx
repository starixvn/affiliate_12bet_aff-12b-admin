import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../actions';

import { Col, FormGroup, Label, Input } from 'reactstrap';
import { htmlDecode } from '../../../../modules/htmlHelper';

const MAX_LENGTH = 100;

export class CommGroupInfo extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isDisableGroupType: false
        };
    }

    componentDidMount() {
        const { GroupId } = this.props.selectedGroup;
        if (GroupId !== 0) {
            this.setState({
                isDisableGroupType: true
            })
        } else {
            this.props.setSelectedGroup({ GroupTypeId: this.props.searchedGroupType });
        }
    }

    handleChangeText = (event) => {
        const { name, value } = event.target;

        if (value.length <= MAX_LENGTH) {
            this.props.setSelectedGroup({ [name]: value });
        }
    }

    handleChangeGroupType = (event) => {
        if (!this.state.isDisableGroupType) {
            const { value } = event.target;
            this.props.setSelectedGroup({ GroupTypeId: Number(value) });
        }
    }

    render() {
        const { GroupName, GroupTypeId, Description } = this.props.selectedGroup;
        const { commGroupTypes } = this.props;
        const { isDisableGroupType } = this.state;

        return (
            <div>
                <FormGroup row>
                    <Label htmlFor="GroupTypeId" sm={2}>Group Type <span className="text-danger required" style={{ margin: 0 }}>*</span></Label>
                    <Col sm={8}>
                        <Input disabled={isDisableGroupType} name="GroupTypeId" type="select" value={GroupTypeId} onChange={this.handleChangeGroupType}>
                            {
                                commGroupTypes.map(type => {
                                    return (<option key={'type-' + type.GroupTypeId} value={type.GroupTypeId}>{type.GroupTypeName}</option>)
                                })
                            }
                        </Input>
                    </Col>
                    <Col sm={2}>
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label htmlFor="GroupName" sm={2}>Group Name <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
                    <Col sm={8}>
                        <Input type="text" name="GroupName" id="GroupName" value={htmlDecode(GroupName)} onChange={this.handleChangeText} />
                    </Col>
                    <Col sm={2}>
                    </Col>
                </FormGroup>
                <FormGroup row>
                    <Label htmlFor="Description" sm={2}>Description </Label>
                    <Col sm={10}>
                        <Input type="text" name="Description" id="Description" value={htmlDecode(Description)} onChange={this.handleChangeText} />
                    </Col>
                </FormGroup>
            </div>
        )
    }
}

function mapStateToProp({ selectedGroup, searchedGroupType, commGroupTypes }) {
    return { selectedGroup, searchedGroupType, commGroupTypes };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(CommGroupInfo);