import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../actions';

import { Col, FormGroup, Label, Input } from 'reactstrap';
import { htmlDecode } from '../../../../modules/htmlHelper';

const MAX_LENGTH = 100;

export class CommGroupInfo extends React.PureComponent {
    constructor(props) {
        super(props);

    }

    handleChangeText = (event) => {
        const { name, value } = event.target;

        if (value.length <= MAX_LENGTH) {
            this.props.setSelectedGroup({ [name]: value });
        }
    }

    render() {
        const { GroupName, Description } = this.props.selectedGroup;

        return (
            <div>
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

function mapStateToProp({ selectedGroup }) {
    return { selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(CommGroupInfo);