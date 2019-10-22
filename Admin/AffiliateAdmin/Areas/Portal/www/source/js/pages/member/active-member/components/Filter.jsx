import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setUserNames, setShowText, fetchActivatedData, fetchUnactivatedData } from '../actions';
import { headerText } from '../libs/constants';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

export const Filter = (props) => {

    const [userNames, setUserNames] = useState('');
    const [message, setMessage] = useState('');

    const _handleSearch = () => {
        !!userNames ? _handleFetchData(userNames) : setMessage('Please input Deposit User Name!');
    }

    const _handleFetchData = async (userNames) => {
        props.setUserNames(userNames);
        const activeGridDataLength = await props.fetchActivatedData({ userNames });
        const unactiveGridDataLength = await props.fetchUnactivatedData({ userNames });

        if (!activeGridDataLength && !unactiveGridDataLength) {
            props.setShowText(true)
        }
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
            {
                !!message && <NotificationPopup
                    message={message}
                    onClose={() => setMessage('')}
                />
            }
        </div>
    )
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserNames, setShowText, fetchActivatedData, fetchUnactivatedData }, dispatch);
}

export default connect(null, mapDispatchToProps)(React.memo(Filter));