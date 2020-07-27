import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { globalResource } from '../libs/constants';
import { setParam, fetchData } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            userName: ''
        }
    }

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSearch();
        }
    }

    handleSearch = () => {
        const { pagingOption } = this.props;
        const param = Object.assign({}, this.state, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize })

        this.props.setParam(param);
    }

    render() {
        const { userName } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-2">User Name</Label>
                        <Input type="text" name="userName" placeholder="Keyword" value={userName} onKeyPress={this.handlePressEnter} onChange={this.handleChange} />
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ pagingOption }) {
    return Object.assign({}, { pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);