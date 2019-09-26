import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam } from '../actions';
import { headerText, globalResource, deviceTypes, imageTypes, affTypes, defaultDevice, defaultAffType } from '../libs/constants';

export const Filter = (props) => {
    const { categoryList, languageList, currencyList, sizeList } = props;
    const [category, setCategory] = useState('');
    const [language, setLanguage] = useState('');
    const [currency, setCurrency] = useState('');
    const [imageType, setImageType] = useState('');
    const [size, setSize] = useState('');
    const [affType, setAffType] = useState(defaultAffType);
    const [deviceType, setDeviceType] = useState(deviceTypes[defaultDevice]);

    const _handleSearch = () => {
        const { pagingOption } = props;

        const param = Object.assign({}, {
            category,
            language,
            currency,
            imageType,
            size,
            affType,
            deviceType,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        })

        props.setParam(param);
    }

    return categoryList && languageList && currencyList && sizeList && (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label for="category" className="mr-sm-2">{headerText.Category}</Label>
                    <Input type="select" name="category" id="category" value={category} onChange={(e) => setCategory(e.target.value)}>
                        <option key={'category-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(categoryList).map(key => {
                                return (<option key={`category-${key}`} value={key}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="language" className="mr-sm-2">{headerText.Language}</Label>
                    <Input type="select" name="language" id="language" value={language} onChange={(e) => setLanguage(e.target.value)}>
                        <option key={'language-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(languageList).map(key => {
                                return (<option key={`language-${key}`} value={languageList[key]}>{languageList[key]}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="currency" className="mr-sm-2">{headerText.Currency}</Label>
                    <Input type="select" name="currency" id="currency" value={currency} onChange={(e) => setCurrency(e.target.value)}>
                        <option key={'currency-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(currencyList).map(key => {
                                return (<option key={`currency-${key}`} value={key}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="imageType" className="mr-sm-2">{headerText.Type}</Label>
                    <Input type="select" name="imageType" id="imageType" value={imageType} onChange={(e) => setImageType(e.target.value)}>
                        <option key={'imageType-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(imageTypes).map(key => {
                                return (<option key={`imageType-${key}`} value={key}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="size" className="mr-sm-2">{headerText.Size}</Label>
                    <Input type="select" name="size" id="size" value={size} onChange={(e) => setSize(e.target.value)}>
                        <option key={'size-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(sizeList).map(key => {
                                return (<option key={`size-${key}`} value={key}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label for="affType" className="mr-sm-2">{headerText.AffType}</Label>
                    <Input type="select" name="affType" id="affType" value={affType} onChange={(e) => setAffType(e.target.value)}>
                        <option key={'affType-all'} value={''}>{globalResource.All}</option>
                        {
                            Object.keys(affTypes).map(key => {
                                return (<option key={`affType-${key}`} value={affTypes[key]}>{key}</option>)
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

function mapStateToProps({ categoryList, languageList, currencyList, sizeList, pagingOption }) {
    return Object.assign({}, { categoryList, languageList, currencyList, sizeList, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));