import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setMessage, fetchData, setUserNames } from '../actions';
import { headerText } from '../libs/constants';

export const Filter = (props) => {

    const [userNames, setUsernames] = useState('');

    const _handleSearch = () => {
        const userNamesTrim = userNames.replace(/ /g, '');

        if (userNamesTrim) {
            if (userNamesTrim.split(',').length < 100) {
                _handleFetchData(userNamesTrim);
            } else {
                props.setMessage('The function can only check up to 100 accounts per request!')
            }
        } else {
            props.setMessage('Please input Deposit User Name!')
        }
    }

    const _handleFetchData = async (names) => {
        props.setUserNames(names);
        await props.fetchData({ userNames: names });
    }

    const _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            _handleSearch();
        }
    }

    return (
        <div className="filter-container mt-3">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label className="mr-2">{headerText.DepositUserName}</Label>
                    <Input type="text" className="deposit-user-name" value={userNames} maxLength="4000" onKeyPress={_handlePressEnter} onChange={(e) => setUsernames(e.target.value)} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>{headerText.CheckStatus}</Button>
            </Form>
        </div>
    )
}

function mapStateToProps() {
    return {};
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMessage, fetchData, setUserNames }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));