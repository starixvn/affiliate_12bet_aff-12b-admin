import React, { useState, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../../components/multiple-select';
import { handleChangeMultipleSelected } from '../../../../modules/utils';

import { setParam, setModuleList, setTargetCountryList, setLanguageList } from '../actions';
import { headerText } from '../libs/constants';
import { assetStatus } from '../../libs/constants';
import { globalResource } from '../../../../modules/globalResource';

export const Filter = ({
    moduleList,
    targetCountryList,
    languageList,
    statusList,
    pagingOption, 
    setParam,
    setModuleList,
    setTargetCountryList,
    setLanguageList,
}) => {
    const [filters, setFilters] = useState({
        mediaName: '',
        targetCountryCodes: targetCountryList.filter(item => item.checked).map(item => item.key).join(',') || 'all',
        languageCodes: languageList.filter(item => item.checked).map(item => item.key).join(',') || 'all',
        status: assetStatus.all,
    });

    const handleSearch = useCallback(() => {
        const { mediaName, targetCountryCodes, languageCodes, status } = filters;
        
        const isSelectAllTargetCountry = targetCountryList.every(c => c.checked);
        const isSelectAllLanguage = languageList.every(c => c.checked);

        const newParam = {
            MediaName: mediaName,
            TargetCountryCodes: isSelectAllTargetCountry ? 'all' : targetCountryCodes,
            LanguageCodes: isSelectAllLanguage ? 'all' : languageCodes,
            Status: status,
            PageIndex: 1,
            PageSize: pagingOption.PageSize,
        };

        setParam(newParam);
    }, [filters, pagingOption.PageSize, setParam]);

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        let newFilters = { ...filters, [name]: value };

        setFilters(newFilters);
    };

    const handleChangeTargetCountry = (item, selectedTargetCountryList) => {

        setFilters({ ...filters, targetCountryCodes: selectedTargetCountryList });

        setTargetCountryList(handleChangeMultipleSelected(targetCountryList, selectedTargetCountryList));
    }

    const handleChangeLanguage = (item, selectedLanguageList) => {
        setFilters({ ...filters, languageCodes: selectedLanguageList });

        setLanguageList(handleChangeMultipleSelected(languageList, selectedLanguageList));
    }

    return (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="mediaName" className="mr-sm-2">Form Name</Label>
                    <Input 
                        type="text" 
                        name="mediaName" 
                        id="mediaName" 
                        value={filters.mediaName} 
                        onChange={handleChange}
                        placeholder="Search form name"
                    />
                </FormGroup>
                <FormGroup className="mr-sm-3 pb-2 multiple-currency-filter">
                    <Label htmlFor="targetCountry" className="mr-sm-2">{headerText.TargetCountry}</Label>
                    <MultipleSelect
                        id="targetCountry"
                        dataSource={targetCountryList}
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
                    <Label htmlFor="language" className="mr-sm-2">{headerText.Language}</Label>
                    <MultipleSelect
                        id="language"
                        dataSource={languageList}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="None Selected"
                        selectedAllLabel="All"
                        onChange={handleChangeLanguage} />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="status" className="mr-sm-2">Status</Label>
                    <Input type="select" name="status" id="status" value={filters.status} onChange={handleChange}>
                        <option value={assetStatus.all}>All</option>
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

function mapStateToProps({ moduleList, targetCountryList, languageList, pagingOption, statusList, setModuleList, setTargetCountryList, setLanguageList }) {
    return Object.assign({}, {
        moduleList, targetCountryList, languageList, pagingOption, statusList, setModuleList, setTargetCountryList, setLanguageList
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setModuleList, setTargetCountryList, setLanguageList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));