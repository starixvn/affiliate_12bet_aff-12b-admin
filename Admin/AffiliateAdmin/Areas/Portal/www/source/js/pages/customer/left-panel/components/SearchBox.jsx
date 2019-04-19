import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getAffiliateInfo, setAffName } from '../actions';

import { Col, Button, Form, Input, FormGroup } from 'reactstrap';

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
        const { affName } = this.state;

        this.props.setAffName(affName);
        this.props.getAffiliateInfo(affName);
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
                    <Col sm={8}>
                        <Input type="text" name="affName" onChange={this.handleChange} onKeyPress={this.handlePressEnter} value={affName} />
                    </Col>
                    <Button id="search-affName" onClick={this.handleClick}>Search</Button>
                </FormGroup>
            </Form >
        )
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo, affName } = leftPanel;
    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, setAffName }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBox);