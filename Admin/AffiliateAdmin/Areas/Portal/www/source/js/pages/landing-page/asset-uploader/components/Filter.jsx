import React, { useState, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam } from '../actions';
import { headerText } from '../libs/constants';
import { globalResource } from '../../../../modules/globalResource';

export const Filter = ({
    moduleList,
    targetCountryList,
    languageList,
    pagingOption, 
    param,
    setParam,
}) => {
    const [filters, setFilters] = useState({
        module: '',
        targetCountry: '',
        language: '',
    });

    const handleSearch = useCallback(() => {
        const newParam = { ...filters, PageIndex: 1, PageSize: pagingOption.PageSize };
        setParam(newParam);
    }, [filters, pagingOption.PageSize, setParam]);

    const handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        let newFilters = { ...filters, [name]: value };

        setFilters(newFilters);
    };

    return (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="selectedModuleId" className="mr-sm-2">{headerText.Module}</Label>
                    <Input type="select" name="module" id="module" value={filters.module} onChange={handleChange}>
                        <option key={'module-all'} value="0">{globalResource.All}</option>
                        {
                            moduleList && moduleList.map(item => {
                                return (<option key={`module-${item.ModuleId}`} value={item.ModuleId}>{item.ModuleName}</option>)
                            })
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

                <Button outline color="primary" type="button" className="mb-2" onClick={handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    );
};

function mapStateToProps({ moduleList, targetCountryList, languageList, pagingOption, param }) {
    return Object.assign({}, {
        moduleList, targetCountryList, languageList, pagingOption, param
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));