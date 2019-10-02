import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../components/multiple-select';
import MonthPicker from '@9softstudio/react-monthpicker';
import AutoComplete from '../../../components/auto-complete';
import { defaultSortBy, sortByMainView, sortByMonthOnMonthView, allProduct, viewLevel, sortDirectionType } from '../modules/constants';
import { setReportTitle, setParam } from '../actions';
import { formatUnit, resolveClientUrl } from '../../../modules/utils';
import { minMonthDate } from '../../../modules/constants';

export class Filter extends Component {
    constructor(props) {
        super(props);

        this.now = new Date(this.props.today);
        const defaultMonth = { month: this.now.getMonth() + 1, year: this.now.getFullYear() };

        this.state = {
            userName: '',
            selectedLanguage: '',
            fromMonth: defaultMonth,
            toMonth: defaultMonth,
            sortBy: defaultSortBy,
            isSortAscending: true,
            selectedProductList: allProduct,
            isGetAll: false
        }

        this.cachedMainReportParam = null;
    }

    componentDidUpdate(prevProps) {
        const { param, currentViewLevel, pagingOption } = this.props;

        if (currentViewLevel !== prevProps.currentViewLevel) {
            const callback = () => {
                const { selectedProductList } = this.state;
                const { productList } = this.props;
                const isCheckedAllProduct = selectedProductList === allProduct || selectedProductList.split(',').length === productList.length;
                const reportTitle = this._buildReportTitle(isCheckedAllProduct);
                this.props.setReportTitle(reportTitle);
            }

            if (this._isMainView()) {
                const { UserName, PreferredLanguage, FromDate, ToDate, SortType, SortDirectionType, ProductIds } = this.cachedMainReportParam;
                const from = new Date(FromDate);
                const to = new Date(ToDate);

                this.setState({
                    userName: UserName,
                    selectedLanguage: PreferredLanguage,
                    fromMonth: { month: from.getMonth() + 1, year: from.getFullYear() },
                    toMonth: { month: to.getMonth() + 1, year: to.getFullYear() },
                    sortBy: SortType,
                    isSortAscending: SortDirectionType === sortDirectionType.ascending,
                    selectedProductList: ProductIds,
                    isGetAll: false
                }, callback);

                this.props.setParam(this.cachedMainReportParam);
            }
            else {
                this.cachedMainReportParam = Object.assign({}, this.cachedMainReportParam, {
                    PageIndex: pagingOption.PageIndex,
                    PageSize: pagingOption.PageSize
                });

                this.setState({
                    userName: param.UserName,
                    sortBy: sortByMonthOnMonthView.Month,
                    isSortAscending: false
                }, callback);
            }
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
        if (!this._isMainView()) {
            this.setState({ fromMonth });
            return;
        }

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

    handleChangeGetAll = () => {
        this.setState((prevState) => ({
            isGetAll: !prevState.isGetAll
        }))
    }

    handleClickSearch = () => {
        const { selectedProductList } = this.state;

        if (selectedProductList === '') {
            return;
        }

        const { selectedLanguage, fromMonth, toMonth, sortBy, isSortAscending, userName, isGetAll } = this.state;
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
            SortDirectionType: isSortAscending ? sortDirectionType.ascending : sortDirectionType.descending,
            PageIndex: 1,
            PageSize: pagingOption.PageSize,
            IsGetAll: isGetAll
        }

        if (this._isMainView()) {
            this.cachedMainReportParam = Object.assign({}, param);
        }

        this.props.setParam(param);
    }

    _buildReportTitle(isCheckedAllProduct) {
        return this._isMainView() ? this._buildMainReportTitle(isCheckedAllProduct) : this._buildMonthOnMonthReportTile(isCheckedAllProduct);
    }

    _buildMainReportTitle(isCheckedAllProduct) {
        const { languageList } = this.props;
        const { selectedLanguage, fromMonth, toMonth, userName } = this.state;

        const userNameTitle = `${userName && '- Username contains \'' + userName + '\''}`;
        const language = languageList[selectedLanguage] || 'All languages';

        const productList = this._getProductDataSource();
        const productName = isCheckedAllProduct ? 'All Products' : productList.filter(p => p.Checked).map(p => p.ProductName).join(',');

        const fromToTitle = `FROM ${formatUnit(fromMonth.month)}/${fromMonth.year} TO ${formatUnit(toMonth.month)}/${toMonth.year}`;
        const reportName = 'AFFILIATE REPORT ';

        return `${reportName}${userNameTitle} ${'- ' + language} ${productName && '- ' + productName} - ${fromToTitle}`;
    }

    _buildMonthOnMonthReportTile(isCheckedAllProduct) {
        const { fromMonth, toMonth, userName, isGetAll } = this.state;

        const userNameTitle = userName && '- ' + userName;
        const productList = this._getProductDataSource();
        const productName = isCheckedAllProduct ? 'All Products' : productList.filter(p => p.Checked).map(p => p.ProductName).join(',');

        const fromToTitle = isGetAll ? 'Get All Dates' :
            `FROM ${formatUnit(fromMonth.month)}/${fromMonth.year} TO ${formatUnit(toMonth.month)}/${toMonth.year}`;
        const reportName = 'MONTH ON MONTH ';

        return `${reportName}${userNameTitle} ${productName && '- ' + productName} - ${fromToTitle}`;
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
        if (!this._isMainView()) {
            return {
                min: Object.assign({}, from),
                max: { month: this.now.getMonth() + 1, year: this.now.getFullYear() }
            };
        }

        let min = Object.assign({}, from);
        let maxToMonth = from.month + 5;
        let maxToYear = maxToMonth > 12 ? from.year + 1 : from.year;

        let actualMaxMonth = maxToMonth % 12;

        const nowMonth = this.now.getMonth() + 1;
        const nowYear = this.now.getFullYear();

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
        const { selectedLanguage, fromMonth, toMonth, sortBy, isSortAscending, selectedProductList, userName, isGetAll } = this.state;
        const { languageList } = this.props;

        const productDataSource = this._getProductDataSource();
        const isValidSelectedProduct = selectedProductList.length > 0;

        const toMonthRange = this._getToMonthRange(fromMonth);
        const isMainView = this._isMainView();
        const sortByData = isMainView ? sortByMainView : sortByMonthOnMonthView;

        return (
            <div className="filter-container mt-2">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">Affiliate</Label>
                        {
                            isMainView ?
                                (<Input type="text" name="Affiliate" id="Affiliate"
                                    onKeyPress={this.handlePressEnter}
                                    onChange={this.handleChangeAffiliate}
                                    value={userName} />) :
                                (
                                    <AutoComplete url={resolveClientUrl('Portal/AffiliateReport/GetAffiliateList')}
                                        onSelect={(value) => this.setState({ userName: value })}
                                        onInput={(value) => this.setState({ userName: value })}
                                        value={userName}
                                        characterCountToSearch={2} />
                                )
                        }
                    </FormGroup>
                    {
                        isMainView &&
                        <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                            <Input type="select" name="Language" id="Language" value={selectedLanguage} onChange={this.handleChangeLanguage}>
                                <option key="lang-all" value="">All Language</option>
                                {
                                    Object.keys(languageList).map(lang => {
                                        return (<option key={'lang-' + lang} value={lang}>{languageList[lang]}</option>)
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
                        <MonthPicker minMonth={minMonthDate.month} minYear={minMonthDate.year}
                            selectedDropdownYear={fromMonth.year}
                            selectedMonth={fromMonth.month}
                            onSelect={this.handleSelectFromMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            enable={!isGetAll} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">To</Label>
                        <MonthPicker minMonth={toMonthRange.min.month} minYear={toMonthRange.min.year}
                            maxMonth={toMonthRange.max.month} maxYear={toMonthRange.max.year}
                            selectedDropdownYear={toMonth.year}
                            selectedYear={toMonth.year}
                            selectedMonth={toMonth.month}
                            onSelect={this.handleSelectToMonth}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            enable={!isGetAll} />
                    </FormGroup>
                    {
                        isMainView ? null : (
                            <FormGroup check>
                                <Label check>
                                    Get All
                                    <Input className="ml-1 mr-2" type="checkbox" checked={isGetAll} onChange={this.handleChangeGetAll} />
                                </Label>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="SortBy" className="mr-sm-2">Sort By</Label>
                        <Input type="select" name="SortBy" id="SortBy" value={sortBy} onChange={this.handleChangeSortBy}>
                            {
                                Object.keys(sortByData).map(k => {
                                    return (<option key={'sortBy-' + k} value={sortByData[k]}>{k}</option>);
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

function mapStateToProps({ productList, pagingOption, languageList, today, currentViewLevel, param }) {
    return Object.assign({}, { productList, pagingOption, languageList, today, currentViewLevel, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);