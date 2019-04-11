import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getAffiliateInfo } from '../actions';

import { Col, Button, Form, Label, Input, FormGroup } from 'reactstrap';

export class SearchBox extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            affName: this.props.affName
        };
    }

    componentDidMount() {
        this.props.getAffiliateInfo(this.state.affName);
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value.trim() });
    }

    handleClick = (event) => {
        event.preventDefault();
        this.props.getAffiliateInfo(this.state.affName);
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            this.handleClick(event);
        }
    }

    render() {
        const { affName } = this.state;
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="affName" sm={2}>User Name</Label>
                    <Col sm={7}>
                        <Input type="text" name="affName" onChange={this.handleChange} onKeyPress={this.handlePressEnter} value={affName} />
                    </Col>
                    <Button onClick={this.handleClick}>Search</Button>
                </FormGroup>
            </Form >
        )
    }
}

function mapStateToProps({ affiliateInfo, affName }) {
    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBox);