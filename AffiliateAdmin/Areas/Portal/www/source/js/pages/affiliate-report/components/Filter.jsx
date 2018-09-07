import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../components/multiple-select';
import MonthPicker from '@9softstudio/react-monthpicker';
import { sortByValues, allProduct, minMonthRange } from '../modules/constants';
import { setReportTitle, setParam } from '../actions';
import { formatUnit } from '../../../modules/utils';

class Filter extends Component {
    constructor(props) {
        super(props);

        this.searchedAffiliate = '';

        const now = new Date();
        const defaultMonth = { month: now.getMonth() + 1, year: now.getFullYear() };

        this.state = {
            selectedLanguage: '',
            fromMonth: defaultMonth,
            toMonth: defaultMonth,
            isGetAll: false,
            sortBy: sortByValues.Affiliate,
            isSortAscending: true,
            selectedProductList: allProduct
        }
    }

    componentDidMount() {
        this.handleClickSearch();
    }

    handlePressEnter = (event) => {
        if(event.charCode === 13){
            this.handleClickSearch();
        }
    }

    handleChangeAffiliate = (event) => {
        this.searchedAffiliate = event.target.value;
    }

    handleChangeLanguage = (event) => {
        this.setState({ selectedLanguage: event.target.value });
    }

    handleSelectFromMonth = (month, year) => {
        this.setState({ fromMonth: { month, year } });
    }

    handleSelectToMonth = (month, year) => {
        this.setState({ toMonth: { month, year } });
    }

    handleChangeProduct = (item, selectedProductList) => {
        this.setState({ selectedProductList: selectedProductList });
    }

    handleChangeGetAll = () => {
        this.setState((prevState) => ({
            isGetAll: !prevState.isGetAll
        }))
    }

    handleChangeSortAscending = () => {
        this.setState((prevState) => ({
            isSortAscending: !prevState.isSortAscending
        }))
    }

    handleChangeSortBy = (event) => {
        this.setState({ sortBy: Number(event.target.value) });
    }

    handleClickSearch = () => {
        const { selectedProductList, selectedLanguage, fromMonth, toMonth, isGetAll, sortBy, isSortAscending } = this.state;
        const { productList, pagingOption } = this.props;

        const isCheckedAllProduct = selectedProductList === allProduct || selectedProductList.split(',').length === productList.length;
        const reportTitle = this._buildReportTitle(isCheckedAllProduct);
        this.props.setReportTitle(reportTitle);

        const param = {
            UserName: this.searchedAffiliate,
            FromDate: `${fromMonth.month}/1/${fromMonth.year}`,
            ToDate: `${toMonth.month}/1/${toMonth.year}`,
            IsGetAll: isGetAll,
            PreferredLanguage: selectedLanguage,
            ProductIds: isCheckedAllProduct ? allProduct : selectedProductList,
            SortType: sortBy,
            SortDirectionType: isSortAscending ? 0 : 1,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        }

        this.props.setParam(param);
    }

    _buildReportTitle(isCheckedAllProduct) {
        const { languageList } = this.props;
        const { selectedLanguage, fromMonth, toMonth, isGetAll } = this.state;

        const userNameTitle = `${this.searchedAffiliate && '- USERNAME \'' + this.searchedAffiliate + '\''}`;
        const language = languageList[selectedLanguage] || 'All languages';

        const productList = this._getProductDataSource();
        const productName = isCheckedAllProduct ? 'All Products' : productList.filter(p => p.Checked).map(p => p.ProductName).join(',');

        const fromToTitle = isGetAll ?
            'Get All Dates' :
            `FROM ${formatUnit(fromMonth.month)}/${fromMonth.year} TO ${formatUnit(toMonth.month)}/${toMonth.year}`;

        return `AFFILIATE REPORT ${userNameTitle} - ${language} ${productName && '- ' + productName} - ${fromToTitle}`;
    }

    _getProductDataSource = () => {
        const { productList } = this.props;
        const { selectedProductList } = this.state;
        if (selectedProductList === allProduct) {
            return productList.slice();
        }

        const selectedProductIdList = selectedProductList.split(',');

        return productList.map(p => {
            return Object.assign({}, p, { Checked: selectedProductIdList.indexOf(p.ProductId.toString()) !== -1 })
        })
    }

    render() {
        const { selectedLanguage, fromMonth, toMonth, isGetAll, sortBy, isSortAscending } = this.state;
        const { languageList } = this.props;

        const productDataSource = this._getProductDataSource();

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">Affiliate</Label>
                        <Input type="text" name="Affiliate" id="Affiliate"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChangeAffiliate}
                            defaultValue={this.searchedAffiliate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="select" name="Language" id="Language" value={selectedLanguage} onChange={this.handleChangeLanguage}>
                            <option value="">All Language</option>
                            {
                                Object.keys(languageList).map(lang => {
                                    return (<option value={lang}>{languageList[lang]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0" style={{ width: 200 }}>
                        <MultipleSelect
                            dataSource={productDataSource}
                            keyField="ProductId"
                            valueField="ProductName"
                            statusField="Checked"
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={this.handleChangeProduct} />
                        <div className="mx-1 rounded-circle note-info" title="Filtering product affects only to Turnover and Winloss columns. Others always show value for all products">
                            <span className="oi oi-info"></span>
                        </div>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">From</Label>
                        <MonthPicker minMonth={minMonthRange.month} minYear={minMonthRange.year} enable={!isGetAll}
                            selectedDropdownYear={fromMonth.year}
                            selectedMonth={fromMonth.month}
                            onSelect={this.handleSelectFromMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">To</Label>
                        <MonthPicker minMonth={minMonthRange.month} minYear={minMonthRange.year} enable={!isGetAll}
                            selectedDropdownYear={toMonth.year}
                            selectedMonth={toMonth.month}
                            onSelect={this.handleSelectToMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label check>
                            Get All
                            <Input className="ml-1" type="checkbox" checked={isGetAll} onChange={this.handleChangeGetAll} />
                        </Label>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SortBy" className="mr-sm-2">Sort By</Label>
                        <Input type="select" name="SortBy" id="SortBy" value={sortBy} onChange={this.handleChangeSortBy}>
                            {
                                Object.keys(sortByValues).map(k => {
                                    return (<option value={sortByValues[k]}>{k}</option>);
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label check>
                            A-Z
                            <Input className="ml-1" type="checkbox" checked={isSortAscending} onChange={this.handleChangeSortAscending} />
                        </Label>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleClickSearch}>
                        <span className="oi oi-magnifying-glass"></span> Search
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ productList, pagingOption, languageList }) {
    return Object.assign({}, { productList, pagingOption, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);