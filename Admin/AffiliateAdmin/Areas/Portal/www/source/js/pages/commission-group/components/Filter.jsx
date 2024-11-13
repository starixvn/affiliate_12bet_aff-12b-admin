import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

import { getGroupSettingList, setSearchedGroupName, setSearchedGroupType } from '../actions';

export class Filter extends Component {
    constructor(props) {
        super(props);

        this.state = {
            groupName: '',
            groupTypeId: 1
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
        const { groupName, groupTypeId } = this.state;

        const groupTypeIdValue = Number(groupTypeId);

        this.props.setSearchedGroupName(groupName);
        this.props.setSearchedGroupType(groupTypeIdValue);

        const param = Object.assign({}, { PageIndex: 1, PageSize: pagingOption.PageSize }, { GroupName: groupName, GroupTypeId: groupTypeIdValue });
        this.props.getGroupSettingList(param);
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSearch();
        }
    }

    render() {

        const { groupName, groupTypeId } = this.state;
        const { commGroupTypes } = this.props;

        return (
            <div className="filter-container">
                <Form inline tag="div" style={{ backgroundColor: 'transparent' }}>
                    <FormGroup className="mr-sm-3">
                        <Label className="mr-sm-2">Group Type</Label>
                        <Input name="groupTypeId" type="select" value={groupTypeId} onChange={this.handleChange}>
                            {
                                commGroupTypes.map(type => {
                                    return (<option key={'type-' + type.GroupTypeId} value={type.GroupTypeId}>{type.GroupTypeName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
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

function mapStateToProps({ pagingOption, commGroupTypes }) {
    return Object.assign({}, { pagingOption, commGroupTypes });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getGroupSettingList, setSearchedGroupName, setSearchedGroupType }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);
