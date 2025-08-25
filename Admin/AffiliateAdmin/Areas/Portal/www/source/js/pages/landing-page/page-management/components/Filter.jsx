import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam } from '../actions';
import { globalResource } from '../libs/constants';

export const Filter = ({ 
    targetCountryList, 
    languageList, 
    dealList, 
    statusList, 
    pagingOption, 
    setParam 
}) => {
    const [filters, setFilters] = useState({
        landingPageName: '',
        dealName: '',
        targetCountry: '',
        language: '',
        status: ''
    });

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFilters(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSearch = () => {
        const newParam = {
            ...filters,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        };
        setParam(newParam);
    };

    return (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="landingPageName" className="mr-sm-2">Landing Page Name</Label>
                    <Input 
                        type="text" 
                        name="landingPageName" 
                        id="landingPageName" 
                        value={filters.landingPageName} 
                        onChange={handleChange}
                        placeholder=""
                    />
                </FormGroup>
                
                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="dealName" className="mr-sm-2">Deal Name</Label>
                    <Input type="select" name="dealName" id="dealName" value={filters.dealName} onChange={handleChange}>
                        <option value="">All Deals</option>
                        {
                            dealList && dealList.map(item => (
                                <option key={`deal-${item.GroupId}`} value={item.GroupId}>
                                    {item.GroupName}
                                </option>
                            ))
                        }
                    </Input>
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="targetCountry" className="mr-sm-2">Target Country</Label>
                    <Input type="select" name="targetCountry" id="targetCountry" value={filters.targetCountry} onChange={handleChange}>
                        <option value="">All Target Countries</option>
                        {
                            targetCountryList && targetCountryList.map(item => (
                                <option key={`country-${item.TargetCountryCode}`} value={item.TargetCountryCode}>
                                    {item.TargetCountryName}
                                </option>
                            ))
                        }
                    </Input>
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="language" className="mr-sm-2">Language</Label>
                    <Input type="select" name="language" id="language" value={filters.language} onChange={handleChange}>
                        <option value="">All Languages</option>
                        {
                            languageList && languageList.map(item => (
                                <option key={`language-${item.LanguageCode}`} value={item.LanguageCode}>
                                    {item.LanguageName}
                                </option>
                            ))
                        }
                    </Input>
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="status" className="mr-sm-2">Status</Label>
                    <Input type="select" name="status" id="status" value={filters.status} onChange={handleChange}>
                        <option value="">All Statuses</option>
                        {
                            statusList && statusList.map(item => (
                                <option key={`status-${item.StatusId}`} value={item.StatusId}>
                                    {item.Status}
                                </option>
                            ))
                        }
                    </Input>
                </FormGroup>

                <Button outline color="primary" type="button" className="mb-2" onClick={handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    );
};

function mapStateToProps({ targetCountryList, languageList, dealList, statusList, pagingOption }) {
    return {
        targetCountryList,
        languageList,
        dealList,
        statusList,
        pagingOption
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));