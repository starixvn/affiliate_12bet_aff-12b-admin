import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam, setParamFromHome, getSubCategoryCasinoList, getSubCategorySportsbookList } from '../actions';
import { headerText, globalResource, deviceTypes, pcLaptop, Asian, Casino, Sportsbook } from '../libs/constants';

export const Filter = (props) => {
    const { categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList, paramFromHome } = props;
    const [category, setCategory] = useState('');
    const [subCategory, setSubCategory] = useState('');
    const [language, setLanguage] = useState('');
    const [currency, setCurrency] = useState('');
    const [imageType, setImageType] = useState(paramFromHome && paramFromHome.ImageType ? paramFromHome.ImageType : '');
    const [size, setSize] = useState('');
    const [affType, setAffType] = useState(Asian);
    const [deviceType, setDeviceType] = useState(deviceTypes[pcLaptop]);

    const isCategoryCasino = category === Casino;
    const hasSubCategory = isCategoryCasino || category === Sportsbook;
    const subCategoryList = hasSubCategory ? (isCategoryCasino ? subCategoryCasinoList : subCategorySportsbookList) : null;

    useEffect(() => {
        const { subCategoryCasinoList, subCategorySportsbookList, paramFromHome } = props;

        !subCategoryCasinoList && props.getSubCategoryCasinoList();
        !subCategorySportsbookList && props.getSubCategorySportsbookList();
        if (paramFromHome && paramFromHome.IsFromHome) {
            _handleSearch();
            props.setParamFromHome({ IsFromHome: false });
        }
    }, [props.subCategoryCasinoList, props.subCategorySportsbookList, props.paramFromHome]);

    const _handleSearch = () => {
        const { pagingOption } = props;
        const newParam = Object.assign({}, {
            Category: category,
            SubCategory: subCategory,
            Language: language,
            Currency: currency,
            ImageType: imageType,
            AffType: affType,
            DeviceType: deviceType,
            Size: size,
            PageIndex: 1,
            PageSize: pagingOption.PageSize,
        });

        props.setParam(newParam);
    }

    const _handleChangeCategory = (event) => {
        setCategory(event.target.value);
        setSubCategory('');
    }

    return categoryList && languageList && currencyList && sizeList && affTypeList && (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 pb-2">
                    <Label for="category" className="mr-sm-2">{headerText.Category}</Label>
                    <Input type="select" name="category" id="category" value={category} onChange={_handleChangeCategory}>
                        <option key={'category-all'} value={''}>{globalResource.All}</option>
                        {
                            categoryList.map(item => {
                                return (<option key={`category-${item.CategoryName}`} value={item.CategoryName}>{item.CategoryName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                {
                    subCategoryList && (
                        <FormGroup className="mr-sm-3 pb-2">
                            <Label for="subCategory" className="mr-sm-2">{headerText.SubCategory}</Label>
                            <Input type="select" name="subCategory" id="subCategory" value={subCategory} onChange={(e) => setSubCategory(e.target.value)}>
                                <option key={'subCategory-all'} value={''}>{globalResource.All}</option>
                                {
                                    subCategoryList.map(item => {
                                        return (<option key={`subCategory-${item.CategoryName}`} value={item.CategoryName}>{item.CategoryName}</option>)
                                    })
                                }
                            </Input>
                        </FormGroup>
                    )
                }
                <FormGroup className="mr-sm-3 pb-2">
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
                <FormGroup className="mr-sm-3 pb-2">
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
                <FormGroup className="mr-sm-3 pb-2">
                    <Label for="imageType" className="mr-sm-2">{headerText.Type}</Label>
                    <Input type="select" name="imageType" id="imageType" value={imageType} onChange={(e) => setImageType(e.target.value)}>
                        <option key={'imageType-all'} value={''}>{globalResource.All}</option>
                        {
                            imageTypeList.map(item => {
                                return (<option key={`imageType-${item.Name}`} value={item.Name}>{item.Name}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3 pb-2">
                    <Label for="size" className="mr-sm-2">{headerText.Size}</Label>
                    <Input type="select" name="size" id="size" value={size} onChange={(e) => setSize(e.target.value)}>
                        <option key={'size-all'} value={''}>{globalResource.All}</option>
                        {
                            sizeList.map(item => {
                                return (<option key={`size-${item.Name}`} value={item.Name}>{item.Name}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3 pb-2">
                    <Label for="affType" className="mr-sm-2">{headerText.AffType}</Label>
                    <Input type="select" name="affType" id="affType" value={affType} onChange={(e) => setAffType(e.target.value)}>
                        {
                            affTypeList.map(item => {
                                return (<option key={`affType-${item.AffTypeId}`} value={item.AffTypeId}>{item.AffTypeName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3 pb-2">
                    <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                    <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={(e) => setDeviceType(e.target.value)}>
                        {
                            Object.keys(deviceTypes).map(key => {
                                return (<option key={`deviceType-${deviceTypes[key]}`} value={deviceTypes[key]}>{key}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <Button outline color="primary" type="button" className="mb-2" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList, pagingOption, paramFromHome }) {
    return Object.assign({}, { categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList, pagingOption, paramFromHome });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setParamFromHome, getSubCategoryCasinoList, getSubCategorySportsbookList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));