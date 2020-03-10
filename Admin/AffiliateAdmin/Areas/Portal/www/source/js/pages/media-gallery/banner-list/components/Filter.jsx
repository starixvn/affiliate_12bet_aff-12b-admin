import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam, setParamFromHome, getSubCategoryCasinoList, getSubCategorySportsbookList, getSizeList } from '../actions';
import { headerText, globalResource, deviceTypes, pcLaptop, Asian, Casino, Sportsbook } from '../libs/constants';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { paramFromHome } = this.props;

        this.state = {
            category: '',
            subCategory: '',
            language: '',
            currency: '',
            imageType: paramFromHome && paramFromHome.ImageType ? paramFromHome.ImageType : '',
            affType: Asian,
            deviceType: pcLaptop,
            size: ''
        }
    }

    componentDidMount() {
        const { paramFromHome, subCategoryCasinoList, subCategorySportsbookList } = this.props;

        if (paramFromHome && paramFromHome.IsFromHome) {
            this._handleSearch();
            this.props.setParamFromHome({ IsFromHome: false });
        }

        if (!subCategoryCasinoList) {
            this.props.getSubCategoryCasinoList();
        }

        if (!subCategorySportsbookList) {
            this.props.getSubCategorySportsbookList();
        }
    }

    _handleChangeCategory = (event) => {
        const selectedCategory = event.target.value;

        const param = Object.assign({}, this.state, { category: selectedCategory, subCategory: '' });

        this.props.getSizeList(param);
        this.setState({ category: selectedCategory, subCategory: '' });
    }

    _handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;

        const param = Object.assign({}, this.state, { [name]: value });

        this.props.getSizeList(param);
        this.setState({ [name]: value });
    }

    _handleSearch = () => {
        const { pagingOption } = this.props;

        const newParam = Object.assign({}, this.state, { PageIndex: 1, PageSize: pagingOption.PageSize });

        this.props.setParam(newParam);
    }

    render() {
        const { categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList } = this.props;
        const { category, subCategory, language, currency, imageType, affType, deviceType, size } = this.state;
        const isCategoryCasino = category === Casino;
        const hasSubCategory = isCategoryCasino || category === Sportsbook;
        const subCategoryList = hasSubCategory ? (isCategoryCasino ? subCategoryCasinoList : subCategorySportsbookList) : null;

        return (
            <div className="filter-container my-2 pb-0">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="category" className="mr-sm-2">{headerText.Category}</Label>
                        <Input type="select" name="category" id="category" value={category} onChange={this._handleChangeCategory}>
                            <option key={'category-all'} value={''}>{globalResource.All}</option>
                            {
                                categoryList && categoryList.map(item => {
                                    return (<option key={`category-${item.name}`} value={item.name}>{item.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {
                        subCategoryList && (
                            <FormGroup className="mr-sm-3 pb-2">
                                <Label for="subCategory" className="mr-sm-2">{headerText.SubCategory}</Label>
                                <Input type="select" name="subCategory" id="subCategory" value={subCategory} onChange={this._handleChange}>
                                    <option key={'subCategory-all'} value={''}>{globalResource.All}</option>
                                    {
                                        subCategoryList.map(item => {
                                            return (<option key={`subCategory-${item.SubCategoryName}`} value={item.SubCategoryName}>{item.SubCategoryName}</option>)
                                        })
                                    }
                                </Input>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="language" className="mr-sm-2">{headerText.Language}</Label>
                        <Input type="select" name="language" id="language" value={language} onChange={this._handleChange}>
                            <option key={'language-all'} value={''}>{globalResource.All}</option>
                            {
                                languageList && languageList.map(item => {
                                    return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="currency" className="mr-sm-2">{headerText.Currency}</Label>
                        <Input type="select" name="currency" id="currency" value={currency} onChange={this._handleChange}>
                            <option key={'currency-all'} value={''}>{globalResource.All}</option>
                            {
                                currencyList && currencyList.map(item => {
                                    return (<option key={`currency-${item.name}`} value={item.name}>{item.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="imageType" className="mr-sm-2">{headerText.Type}</Label>
                        <Input type="select" name="imageType" id="imageType" value={imageType} onChange={this._handleChange}>
                            <option key={'imageType-all'} value={''}>{globalResource.All}</option>
                            {
                                imageTypeList && imageTypeList.map(item => {
                                    return (<option key={`imageType-${item.Name}`} value={item.Name}>{item.Name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {
                        affTypeList && (
                            <FormGroup className="mr-sm-3 pb-2">
                                <Label for="affType" className="mr-sm-2">{headerText.AffType}</Label>
                                <Input type="select" name="affType" id="affType" value={affType} onChange={this._handleChange}>
                                    {
                                        affTypeList.map(item => {
                                            return (<option key={`affType-${item.key}`} value={item.key}>{item.name}</option>)
                                        })
                                    }
                                </Input>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this._handleChange}>
                            {
                                Object.keys(deviceTypes).map(key => {
                                    return (<option key={`deviceType-${key}`} value={key}>{key}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="size" className="mr-sm-2">{headerText.Size}</Label>
                        <Input type="select" name="size" id="size" value={size} onChange={(e) => this.setState({ size: e.target.value })}>
                            <option key={'size-all'} value={''}>{globalResource.All}</option>
                            {
                                sizeList && sizeList.map(item => {
                                    return (<option key={`size-${item.name}`} value={item.name}>{item.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline color="primary" type="button" className="mb-2" onClick={this._handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList, pagingOption, paramFromHome }) {
    return Object.assign({}, { categoryList, subCategoryCasinoList, subCategorySportsbookList, languageList, currencyList, imageTypeList, sizeList, affTypeList, pagingOption, paramFromHome });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setParamFromHome, getSubCategoryCasinoList, getSubCategorySportsbookList, getSizeList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);