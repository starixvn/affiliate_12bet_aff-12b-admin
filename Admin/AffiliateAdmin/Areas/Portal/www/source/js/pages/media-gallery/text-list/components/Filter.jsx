import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam } from '../actions';
import { headerText, globalResource, deviceTypes, defaultDevice } from '../libs/constants';

export const Filter = (props) => {
    const { categoryList, languageList, currencyList } = props;
    const [category, setCategory] = useState('');
    const [language, setLanguage] = useState('');
    const [currency, setCurrency] = useState('');
    const [deviceType, setDeviceType] = useState(deviceTypes[defaultDevice]);

    const _handleSearch = () => {
        const { pagingOption } = props;

        const param = Object.assign({}, {
            category,
            language,
            currency,
            deviceType,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        })

        props.setParam(param);
    }

    return categoryList && languageList && currencyList && (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label for="category" className="mr-sm-2">{headerText.Category}</Label>
                    <Input type="select" name="category" id="category" value={category} onChange={(e) => setCategory(e.target.value)}>
                        <option key={'category-all'} value={''}>{globalResource.All}</option>
                        {
                            categoryList.map(item => {
                                return (<option key={`category-${item.CategoryName}`} value={item.CategoryName}>{item.CategoryName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="language" className="mr-sm-2">{headerText.Language}</Label>
                    <Input type="select" name="language" id="language" value={language} onChange={(e) => setLanguage(e.target.value)}>
                        <option key={'language-all'} value={''}>{globalResource.All}</option>
                        {
                            languageList.map(item => {
                                return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="currency" className="mr-sm-2">{headerText.Currency}</Label>
                    <Input type="select" name="currency" id="currency" value={currency} onChange={(e) => setCurrency(e.target.value)}>
                        <option key={'currency-all'} value={''}>{globalResource.All}</option>
                        {
                            currencyList.map(item => {
                                return (<option key={`currency-${item.Name}`} value={item.Name}>{item.Name}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                    <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={(e) => setDeviceType(e.target.value)}>
                        {
                            Object.keys(deviceTypes).map(key => {
                                return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ categoryList, languageList, currencyList, pagingOption }) {
    return Object.assign({}, { categoryList, languageList, currencyList, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));