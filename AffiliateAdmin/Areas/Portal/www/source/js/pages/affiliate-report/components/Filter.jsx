import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../components/multiple-select';
import MonthPicker from '@9softstudio/react-monthpicker';
import { defaultSortBy, sortByMainView, sortByMonthOnMonthView, allProduct, minMonthRange, viewLevel } from '../modules/constants';
import { setReportTitle, setParam } from '../actions';
import { formatUnit } from '../../../modules/utils';

class Filter extends Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.currentDate);
        const defaultMonth = { month: now.getMonth() + 1, year: now.getFullYear() };

        this.state = {
            userName: '',
            selectedLanguage: '',
            fromMonth: defaultMonth,
            toMonth: defaultMonth,
            sortBy: defaultSortBy,
            isSortAscending: true,
            selectedProductList: allProduct
        }
    }

    componentDidUpdate(prevProps) {
        const { param, currentViewLevel } = this.props;

        if (currentViewLevel !== prevProps.currentViewLevel || param !== prevProps.param) {
            this.setState({ userName: param.UserName }, () => {
                const { selectedProductList } = this.state;
                const { productList } = this.props;
                const isCheckedAllProduct = selectedProductList === allProduct || selectedProductList.split(',').length === productList.length;
                const reportTitle = this._buildReportTitle(isCheckedAllProduct);
                this.props.setReportTitle(reportTitle);
            });
        }
    }

    _isMainView() {
        return this.props.currentViewLevel === viewLevel.main;
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            this.handleClickSearch();
        }
    }

    handleChangeAffiliate = (event) => {
        this.setState({ userName: event.target.value });
    }

    handleChangeLanguage = (event) => {
        this.setState({ selectedLanguage: event.target.value });
    }

    handleSelectFromMonth = (month, year) => {
        const fromMonth = { month, year };
        const toMonthRange = this._getToMonthRange(fromMonth);

        const { min, max } = toMonthRange;
        const { toMonth } = this.state;

        const isInRange = min.month <= toMonth.month && min.year <= toMonth.year &&
            max.month >= toMonth.month && max.year >= toMonth.year;

        const newState = isInRange ? {
            fromMonth: { month, year }
        } :
            {
                fromMonth: { month, year },
                toMonth: { month, year }
            }

        this.setState(newState);
    }

    handleSelectToMonth = (month, year) => {
        this.setState({ toMonth: { month, year } });
    }

    handleChangeProduct = (item, selectedProductList) => {
        this.setState({ selectedProductList: selectedProductList });
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
        const { selectedProductList, selectedLanguage, fromMonth, toMonth, sortBy, isSortAscending, userName } = this.state;

        if (selectedProductList === '') {
            return;
        }

        const { productList, pagingOption } = this.props;

        const isCheckedAllProduct = selectedProductList === allProduct || selectedProductList.split(',').length === productList.length;
        const reportTitle = this._buildReportTitle(isCheckedAllProduct);
        this.props.setReportTitle(reportTitle);

        const param = {
            UserName: userName,
            FromDate: `${fromMonth.month}/1/${fromMonth.year}`,
            ToDate: `${toMonth.month}/1/${toMonth.year}`,
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
        const { selectedLanguage, fromMonth, toMonth, userName } = this.state;
        const isMainView = this._isMainView();

        const userNameTitle = isMainView ? `${userName && '- USERNAME \'' + userName + '\''}` : (userName && '- ' + userName);
        const language = languageList[selectedLanguage] || 'All languages';

        const productList = this._getProductDataSource();
        const productName = isCheckedAllProduct ? 'All Products' : productList.filter(p => p.Checked).map(p => p.ProductName).join(',');

        const fromToTitle = `FROM ${formatUnit(fromMonth.month)}/${fromMonth.year} TO ${formatUnit(toMonth.month)}/${toMonth.year}`;
        const reportName = isMainView ? 'AFFILIATE REPORT' : 'AFFILIATE REPORT MONTH ON MONTH';

        return `${reportName} ${userNameTitle} ${isMainView ? '- ' + language : ''} ${productName && '- ' + productName} - ${fromToTitle}`;
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

    _getToMonthRange(from) {
        let min = Object.assign({}, from);
        let maxToMonth = from.month + 5;
        let maxToYear = maxToMonth > 12 ? from.year + 1 : from.year;

        const now = new Date(this.props.currentDate);
        let actualMaxMonth = maxToMonth % 12;

        const nowMonth = now.getMonth() + 1;
        const nowYear = now.getFullYear();

        if (maxToYear > nowYear || actualMaxMonth > nowMonth && maxToYear === nowYear) {
            actualMaxMonth = nowMonth;
            maxToYear = nowYear;

            min = { month: from.month, year: maxToYear }
        }

        return {
            min,
            max: { month: actualMaxMonth, year: maxToYear }
        };
    }

    render() {
        const { selectedLanguage, fromMonth, toMonth, sortBy, isSortAscending, selectedProductList, userName } = this.state;
        const { languageList } = this.props;

        const productDataSource = this._getProductDataSource();
        const isValidSelectedProduct = selectedProductList.length > 0;

        const toMonthRange = this._getToMonthRange(fromMonth);
        const sortByData = this._isMainView() ? sortByMainView : sortByMonthOnMonthView;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">Affiliate</Label>
                        <Input type="text" name="Affiliate" id="Affiliate"
                            onKeyPress={this.handlePressEnter}
                            onChange={this.handleChangeAffiliate}
                            value={userName} />
                    </FormGroup>
                    {
                        this._isMainView() &&
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
                    }
                    <FormGroup className={"mb-2 mr-sm-2 mb-sm-0" + `${isValidSelectedProduct ? '' : ' multiple-select-wrapper is-invalid'}`}
                        style={{ width: 200 }}>
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
                        <MonthPicker minMonth={minMonthRange.month} minYear={minMonthRange.year}
                            selectedDropdownYear={fromMonth.year}
                            selectedMonth={fromMonth.month}
                            onSelect={this.handleSelectFromMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">To</Label>
                        <MonthPicker minMonth={toMonthRange.min.month} minYear={toMonthRange.min.year}
                            maxMonth={toMonthRange.max.month} maxYear={toMonthRange.max.year}
                            selectedDropdownYear={toMonth.year}
                            selectedYear={toMonth.year}
                            selectedMonth={toMonth.month}
                            onSelect={this.handleSelectToMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SortBy" className="mr-sm-2">Sort By</Label>
                        <Input type="select" name="SortBy" id="SortBy" value={sortBy} onChange={this.handleChangeSortBy}>
                            {
                                Object.keys(sortByData).map(k => {
                                    return (<option value={sortByData[k]}>{k}</option>);
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
                    <Button outline color="primary" type="button" onClick={this.handleClickSearch} disabled={!isValidSelectedProduct}>
                        <span className="oi oi-magnifying-glass"></span> Search
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ productList, pagingOption, languageList, currentDate, currentViewLevel, param }) {
    return Object.assign({}, { productList, pagingOption, languageList, currentDate, currentViewLevel, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);