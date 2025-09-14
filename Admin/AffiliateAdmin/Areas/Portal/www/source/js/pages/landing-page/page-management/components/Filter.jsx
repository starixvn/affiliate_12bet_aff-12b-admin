import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../../components/multiple-select';
import { handleChangeMultipleSelected } from '../../../../modules/utils';

import { setParam, setCampaignList, setDealList, setTargetCountryList, setLanguageList, setStatusList } from '../actions';
import { globalResource } from '../libs/constants';

export const Filter = ({ 
    targetCountryList, 
    languageList, 
    dealList, 
    statusList, 
    pagingOption, 
    campaignList,
    setParam,
    setCampaignList,
    setDealList,
    setTargetCountryList,
    setLanguageList,
    setStatusList
}) => {
    const [filters, setFilters] = useState({
        landingPageName: '',
        campaignIds: campaignList ? campaignList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        dealIds: dealList ? dealList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        targetCountryCodes: targetCountryList ? targetCountryList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        languageCodes: languageList ? languageList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        statusIds: statusList ? statusList.filter(item => item.checked).map(item => item.key).join(',') : 'all'
    });

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFilters(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleChangeCampaign = (item, selectedCampaignList) => {
        setFilters({ ...filters, campaignIds: selectedCampaignList });
        setCampaignList(handleChangeMultipleSelected(campaignList, selectedCampaignList));
    };

    const handleChangeDeal = (item, selectedDealList) => {
        setFilters({ ...filters, dealIds: selectedDealList });
        setDealList(handleChangeMultipleSelected(dealList, selectedDealList));
    };

    const handleChangeTargetCountry = (item, selectedTargetCountryList) => {
        setFilters({ ...filters, targetCountryCodes: selectedTargetCountryList });
        setTargetCountryList(handleChangeMultipleSelected(targetCountryList, selectedTargetCountryList));
    };

    const handleChangeLanguage = (item, selectedLanguageList) => {
        setFilters({ ...filters, languageCodes: selectedLanguageList });
        setLanguageList(handleChangeMultipleSelected(languageList, selectedLanguageList));
    };

    const handleChangeStatus = (item, selectedStatusList) => {
        setFilters({ ...filters, statusIds: selectedStatusList });
        setStatusList(handleChangeMultipleSelected(statusList, selectedStatusList));
    };

    const handleSearch = () => {
        const { landingPageName, campaignIds, dealIds, targetCountryCodes, languageCodes, statusIds } = filters;
        
        const isSelectAllCampaigns = campaignList ? campaignList.every(c => c.checked) : true;
        const isSelectAllDeals = dealList ? dealList.every(c => c.checked) : true;
        const isSelectAllTargetCountries = targetCountryList ? targetCountryList.every(c => c.checked) : true;
        const isSelectAllLanguages = languageList ? languageList.every(c => c.checked) : true;
        const isSelectAllStatuses = statusList ? statusList.every(c => c.checked) : true;

        const newParam = {
            landingPageName,
            campaignIds: campaignIds,//isSelectAllCampaigns ? 'all' : campaignIds,
            dealIds: dealIds,//isSelectAllDeals ? 'all' : dealIds,
            targetCountryCodes: targetCountryCodes,//isSelectAllTargetCountries ? 'all' : targetCountryCodes,
            languageCodes: languageCodes,//isSelectAllLanguages ? 'all' : languageCodes,
            statusIds: statusIds,//isSelectAllStatuses ? 'all' : statusIds,
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
                
                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="campaign" className="mr-sm-2">Campaign</Label>
                    <MultipleSelect
                        id="campaign"
                        dataSource={campaignList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeCampaign} />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="deal" className="mr-sm-2">Deal</Label>
                    <MultipleSelect
                        id="deal"
                        dataSource={dealList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeDeal} />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="targetCountry" className="mr-sm-2">Target Country</Label>
                    <MultipleSelect
                        id="targetCountry"
                        dataSource={targetCountryList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeTargetCountry} />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="language" className="mr-sm-2">Language</Label>
                    <MultipleSelect
                        id="language"
                        dataSource={languageList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeLanguage} />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="status" className="mr-sm-2">Status</Label>
                    <MultipleSelect
                        id="status"
                        dataSource={statusList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeStatus} />
                </FormGroup>

                <Button outline color="primary" type="button" className="mb-2" onClick={handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    );
};

function mapStateToProps({ targetCountryList, languageList, dealList, statusList, pagingOption, campaignList }) {
    return {
        targetCountryList,
        languageList,
        dealList,
        statusList,
        pagingOption,
        campaignList
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ 
        setParam, 
        setCampaignList, 
        setDealList, 
        setTargetCountryList, 
        setLanguageList, 
        setStatusList 
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));