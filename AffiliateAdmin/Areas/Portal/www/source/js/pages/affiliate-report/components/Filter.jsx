import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../components/multiple-select';
import MonthPicker from '@9softstudio/react-monthpicker';
import { sortByValues, allProduct } from '../modules/constants';
import * as actionCreator from '../actions'

class Filter extends Component {
    constructor(props) {
        super(props);

        this.searchedAffiliate = '';
    }

    handleChangeAffiliate = (event) => {
        this.searchedAffiliate = event.target.value;
    }

    handleChangeLanguage = (event) => {
        this.props.setSelectedLanguage(event.target.value);
    }

    handleSelectFromMonth = (month, year) => {
        this.props.setFromMonth({ month, year });
    }

    handleSelectToMonth = (month, year) => {
        this.props.setToMonth({ month, year });
    }

    handleChangeProduct = (item, selectedProductList) => {
        this.props.setSelectedProductList(selectedProductList);
    }

    handleChangeGetAll = () => {
        this.props.toggleCheckedGetAll();
    }

    handleChangeSortAscending = () => {
        this.props.toggleSortAscending();
    }

    handleChangeSortBy = (event) => {
        this.props.setSortBy(Number(event.target.value));
    }

    handleClickSearch = () => {
        const { productList, selectedProductList, selectedLanguage, fromMonth, toMonth, isGetAll, sortBy, isSortAscending, pagingOption } = this.props;

        this.props.setSearchedAffiliate(this.searchedAffiliate);

        const isCheckedAllProduct = selectedProductList === allProduct || selectedProductList.split(',').length === productList.length;

        const param = {
            UserName: this.searchedAffiliate,
            FromDate: `${fromMonth.month}/1/${fromMonth.year}`,
            ToDate: `${toMonth.month}/1/${toMonth.year}`,
            IsGetAll: isGetAll,
            PreferredLanguage: selectedLanguage,
            ProductIds:  isCheckedAllProduct ? allProduct : selectedProductList,
            SortType: sortBy,
            SortDirectionType: isSortAscending ? 0 : 1,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        }

        console.log(param);
        this.props.getGridData(param);
    }

    _getProductDataSource = () => {
        const { productList, selectedProductList } = this.props;
        if (selectedProductList === allProduct) {
            return productList.slice();
        }

        const selectedProductIdList = selectedProductList.split(',');

        return productList.map(p => {
            return Object.assign({}, p, { Checked: selectedProductIdList.indexOf(p.ProductId.toString()) !== -1 })
        })
    }

    render() {
        const { affiliate, selectedLanguage, fromMonth, toMonth, isGetAll, sortBy, isSortAscending } = this.props;

        const productDataSource = this._getProductDataSource();

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">Affiliate</Label>
                        <Input type="text" name="Affiliate" id="Affiliate" onChange={this.handleChangeAffiliate} defaultValue={affiliate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Input type="select" name="Language" id="Language" value={selectedLanguage} onChange={this.handleChangeLanguage}>
                            <option value="">All Language</option>
                            <option value="en-US">English</option>
                            <option value="en-GB">English_Euro</option>
                            <option value="zh-TW">Chinese Traditional</option>
                            <option value="zh-CN">Chinese Simplified</option>
                            <option value="th-TH">Thai</option>
                            <option value="es-ES">Spanish</option>
                            <option value="vi-VN">Vietnamese</option>
                            <option value="id-ID">Indonesian</option>
                            <option value="ko-KR">Korean</option>
                            <option value="ja-JP">Japanese</option>
                            <option value="hi-IN">Republic of India</option>
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
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">From</Label>
                        <MonthPicker displayedYear={fromMonth.year} selectedMonth={fromMonth.month} onSelect={this.handleSelectFromMonth} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">To</Label>
                        <MonthPicker displayedYear={toMonth.year} selectedMonth={toMonth.month} onSelect={this.handleSelectToMonth} />
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

function mapStateToProps({ productList, affiliate, selectedLanguage, fromMonth, toMonth, isGetAll, sortBy, isSortAscending, pagingOption, selectedProductList }) {
    return Object.assign({}, {
        productList,
        affiliate,
        selectedLanguage,
        fromMonth,
        toMonth,
        isGetAll,
        sortBy,
        isSortAscending,
        pagingOption,
        selectedProductList
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ ...actionCreator }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);