import React, { useState } from 'react';
import { connect } from 'react-redux';
import { Form, FormGroup, Input, Button } from 'reactstrap';
import { bindActionCreators } from 'redux';

import { setParam, setIsShowAddAffMktAccountPopup, setAffId } from '../actions';
import { allStatuses, globalResource, statusList } from '../libs/constants';

export const Filter = (props) => {
    const { languageCodeList, targetCountryCodeList, canAddAffMktAccount } = props;

    const [languageCode, setLanguageCode] = useState('');
    const [targetCountryCode, setTargetCountryCode] = useState('');
    const [status, setStatus] = useState(allStatuses);
    const [userName, setUserName] = useState('');

    const _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();

            _handleSearch();
        }
    }

    const _handleSearch = () => {
        // TODO: Need Review

        // const params = {
        //     languageCode,
        //     targetCountryCode,
        //     status,
        //     userName
        // };

        const params = {
            "ParentAffName": "",
            "Status": "",
            "IsFromHome": false,
            "GroupIds": "all",
            "TargetCountryCode": "",
            "Language": "",
            "UserName": "",
            "Email": "",
            "SortBy": 1,
            "SortDirectionType": false,
            "AffCode": "",
            "AffPlayer": "",
            "PageIndex": 1,
            "PageSize": 20
        }

        props.setParam(params);
    };

    const _handleAddNewAccount = () => {
        props.setIsShowAddAffMktAccountPopup(true);
        props.setAffId(927841);
    }
    
    return (
        <div className="filter-container">
            <Form inline tag="div">
                <FormGroup className="mr-sm-2 my-2">
                    <Input type="select" name="targetCountry" id="targetCountry"
                        value={targetCountryCode}
                        onChange={(e) => setTargetCountryCode(e.target.value)}>
                        <option value="">All Target Countries</option>
                        {
                            Object.keys(targetCountryCodeList).map(key => {
                                return (<option key={`targetCountry-${key}`} value={key}>{targetCountryCodeList[key]}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-2 my-2">
                    <Input type="select" name="language" id="language"
                        value={languageCode}
                        onChange={(e) => setLanguageCode(e.target.value)}>
                        <option value="">All Languages</option>
                        {
                            Object.keys(languageCodeList).map(key => {
                                return (<option key={`language-${key}`} value={key}>{languageCodeList[key]}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="my-sm-2 mr-sm-2">
                    <Input type="select" name="status" value={status} onChange={(e) => setStatus(e.target.value)}>
                        {
                            Object.keys(statusList).map(key => {
                                return (<option key={`status-${key === allStatuses ? 'all' : key}`} value={key}>{statusList[key]}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Input type="text" name="affiliate" id="affiliate" maxLength="30"
                        onKeyPress={_handlePressEnter}
                        onChange={(e) => setUserName(e.target.value)}
                        value={userName} />
                </FormGroup>
                <FormGroup className="mr-sm-2 my-2">
                    <Button outline className="mr-sm-2" color="primary" type="button" onClick={_handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </FormGroup>
            </Form>
            {canAddAffMktAccount && <div className="my-2">
                <Button outline type="button" onClick={() => _handleAddNewAccount()}>
                    Add New Account
                    <span className="oi oi-plus ml-2"></span>
                </Button>
            </div>}
        </div>
    );
};

const mapStateToProps = ({ languageCodeList, targetCountryCodeList, canAddAffMktAccount }) => {
    return {
        languageCodeList,
        targetCountryCodeList,
        canAddAffMktAccount
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setParam,
        setIsShowAddAffMktAccountPopup,
        setAffId
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));