import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getAffiliateInfo } from '../actions';

import { Col, Button, Form, Label, Input } from 'reactstrap';

export class SearchBox extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '12BETAFF2233'
        };

        this.currentUserName = '';
    }

    componentDidMount() {
        const userName = this.state.userName;
        this.props.getAffiliateInfo(userName);
    }

    handleChange = (event) => {
        this.setState({ userName: event.target.value.trim() });
    }

    handleClick = (event) => {
        event.preventDefault();
        const userName = this.state.userName;
        this.currentUserName = userName;
        this.props.getAffiliateInfo(userName);
    }

    render() {
        const { userName } = this.state;
        const { hasData } = this.props;
        return (
            <Form tag="div">
                <FormGroup row>
                    <Label for="exampleEmail" sm={2}>User Name</Label>
                    <Col sm={7}>
                        <Input type="text" name="userName" onChange={this.handleChange} value={userName} />
                    </Col>
                    <Button onClick={this.handleClick}>Search</Button>
                </FormGroup>
            </Form >
        )
    }
}

function mapStateToProps({ affiliateInfo }) {
    return { affiliateInfo };
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBox);