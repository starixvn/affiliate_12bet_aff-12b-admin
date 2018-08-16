import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../actions';

import { Col, FormGroup, Label, Input } from 'reactstrap';

export class CommGroupInfo extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const { name, value } = event.target;        
        this.props.setSelectedGroup({ [name]: value })
    }

    render() {
        const { GroupName, Description } = this.props.selectedGroup;

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

function mapStateToProp({ selectedGroup }) {
    return { selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(CommGroupInfo);