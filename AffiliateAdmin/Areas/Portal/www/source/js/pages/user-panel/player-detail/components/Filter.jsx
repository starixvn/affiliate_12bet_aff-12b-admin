import React, { Component } from 'react';

import { Form, FormGroup, Input, Label, Button } from 'reactstrap';
import MonthPicker from '@9softstudio/react-monthpicker';

import { shortedMonthNames, minMonthDate } from '../../../../modules/constants';
import { resource } from '../modules/constants';

export default class Filter extends Component {
    constructor(props) {
        super(props);

        const { productList, selectedProduct, monthDate } = this.props;
        this.productList = productList;

        this.state = {
            selectedProduct,
            monthDate
        }
    }

    handleSelectMonth = (month, year) => {
        this.setState({
            monthDate: new Date(year, month - 1)
        })
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);

        this.setState({
            selectedProduct
        })
    }

    handleFormatMonthPicker = (month, year) => {
        return `${shortedMonthNames[month - 1]}-${year}`
    }

    handleSubmit = (event) => {
        event.preventDefault();

        const { selectedProduct, monthDate } = this.state;

        const param = {
            ProductId: selectedProduct,
            MonthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`
        }

        this.props.onSubmit(param);
    }

    handleExportExcel = () => {
        const { selectedProduct, monthDate } = this.state;

        const param = {
            ProductId: selectedProduct,
            MonthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`
        }

        const title = this._getReportTitle();
        this.props.onExportExcel(param, title);
    }

    _getReportTitle() {
        const { selectedProduct, monthDate } = this.state;
        const productTitle = selectedProduct === 0 ? 'All Products' : this._getSelectedProductName(selectedProduct);
        const selectedMonthTitle = this.handleFormatMonthPicker(monthDate.getMonth() + 1, monthDate.getFullYear());

        return `Player Detail - ${productTitle} - ${selectedMonthTitle}`;
    }

    _getSelectedProductName(productId) {
        const selectedProduct = this.productList.find(p => p.ProductId === productId);

        return selectedProduct ? selectedProduct.ProductName : '';
    }

    render() {
        const { selectedProduct, monthDate } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">{resource.Product}</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{resource.All}</option>
                            {
                                this.productList.map(p => {
                                    return (<option value={p.ProductId}>{p.ProductName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{resource.Month}</Label>
                        <MonthPicker minMonth={minMonthDate.month} minYear={minMonthDate.year}
                            selectedDropdownYear={monthDate.getFullYear()}
                            selectedMonth={monthDate.getMonth() + 1}
                            onSelect={this.handleSelectMonth}
                            onFormat={this.handleFormatMonthPicker}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={this.handleSubmit}>
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>

                    <Button color="link" type="button" className="ml-auto btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                        <span className="oi icon-excel"></span>
                    </Button>
                </Form>
            </div>
        );
    }
}