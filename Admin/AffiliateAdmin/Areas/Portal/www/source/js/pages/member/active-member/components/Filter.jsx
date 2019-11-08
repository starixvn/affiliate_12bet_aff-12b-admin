import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setMessage, setShowText, fetchActivatedData, fetchUnactivatedData, setUserNames } from '../actions';
import { headerText, getUnexistUserNames } from '../libs/constants';

export const Filter = (props) => {

    const [userNames, setUserNames] = useState('');

    const _handleSearch = () => {
        const userNamesTrim = userNames.replace(/ /g, '');

        !!userNamesTrim ? _handleFetchData(userNamesTrim) : props.setMessage('Please input Deposit User Name!');
    }

    const _handleFetchData = async (userNames) => {
        const activeGridData = await props.fetchActivatedData({ userNames });
        const unactiveGridData = await props.fetchUnactivatedData({ userNames });

        const unexistUserNames = getUnexistUserNames(activeGridData, unactiveGridData, userNames);

        unexistUserNames && props.setShowText(true);
        props.setUserNames(unexistUserNames);
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
                    <Input type="text" className="deposit-user-name" value={userNames} onKeyPress={_handlePressEnter} onChange={(e) => setUserNames(e.target.value)} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>{headerText.CheckStatus}</Button>
            </Form>
        </div>
    )
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMessage, setShowText, fetchActivatedData, fetchUnactivatedData, setUserNames }, dispatch);
}

export default connect(null, mapDispatchToProps)(React.memo(Filter));