import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam } from '../actions';
import { headerText } from '../libs/constants';

export const Filter = (props) => {
    const { pagingOption } = props;

    const [affName, setAffName] = useState('');
    const [userName, setUserName] = useState('');

    const _handleSearch = () => {
        const newParam = Object.assign({}, {
            AffName: affName,
            UserName: userName,
            PageIndex: 1,
            PageSize: pagingOption.PageSize,
        });

        props.setParam(newParam);
    }

    const _handleChangeAffName = (event) => {
        setAffName(event.target.value)
    }

    const _handleChangeUserName = (event) => {
        setUserName(event.target.value)
    }

    const _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            _handleSearch();
        }
    }

    return (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label className="mr-2">{headerText.Affiliate}</Label>
                    <Input type="text" name="affName" value={affName} onKeyPress={_handleKeyPress} onChange={_handleChangeAffName} />
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label className="mr-2">{headerText.UserName}</Label>
                    <Input type="text" name="userName" value={userName} onKeyPress={_handleKeyPress} onChange={_handleChangeUserName} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {headerText.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ pagingOption }) {
    return Object.assign({}, { pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));