import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

import { getGroupSettingList, setSearchedGroupName } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            groupName: ''
        }
    }

    handleChange = (event) => {
        const { name, value } = event.target;

        this.setState({
            [name]: value
        })
    }

    handleSearch = () => {
        const { pagingOption } = this.props;
        const { groupName } = this.state;

        this.props.setSearchedGroupName(groupName);

        const param = Object.assign({}, { PageIndex: 1, PageSize: pagingOption.PageSize }, { GroupName: groupName });
        this.props.getGroupSettingList(param);
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSearch();
        }
    }

    render() {

        const { groupName } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div" style={{ backgroundColor: 'transparent' }}>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">Group Name</Label>{' '}
                        <Input name="groupName"
                            type="text"
                            className="mr-sm-2 search-box"
                            value={groupName}
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChange} />
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}><span className="oi oi-magnifying-glass"></span> Search</Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ pagingOption }) {
    return Object.assign({}, { pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList, setSearchedGroupName }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
