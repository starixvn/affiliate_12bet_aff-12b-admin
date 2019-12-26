import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Form, FormGroup, Input, Label, Button } from 'reactstrap';
import MonthPicker from '@9softstudio/react-monthpicker';

import { resource, selectedAllProduct, shortedMonthNames, minMonthDate } from '../modules/constants';
import { setParam, getProductList, exportExcel } from '../actions';
import ReportExcel from '../modules/reportExcel';
import { htmlDecode } from '../../../../modules/htmlHelper';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today, param } = this.props;
        const monthDate = param.isClickLink ? (param.monthDate || today) : today;
        const newMonthDate = new Date(monthDate);

        this.state = {
            selectedProduct: selectedAllProduct,
            monthDate: newMonthDate,
            productList: null
        }
    }

    componentDidMount() {
        if (!this.state.productList) {
            this.handleGetProductList();
        }
        if (this.props.param.isClickLink) {
            const { monthDate } = this.state;
            this.handleSearch(monthDate);
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        if (param.isClickLink && param !== prevProps.param) {
            const newMonthDate = new Date(param.monthDate);

            this.setState({ monthDate: newMonthDate, selectedProduct: selectedAllProduct });
            this.handleSearch(newMonthDate);
        }
    }

    handleGetProductList = async () => {
        const productList = await this.props.getProductList();
        this.setState({ productList });
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

    handleSearch = (monthDate) => {
        const { selectedProduct } = this.state;

        const param = {
            productId: selectedProduct,
            monthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`,
        }
        this.props.setParam(param);
    }

    handleExportExcel = async () => {
        const { selectedProduct, monthDate } = this.state;
        const { affId } = this.props;

        const param = {
            IsExportExcel: true,
            productId: selectedProduct,
            monthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`,
            affId
        }

        const data = await this.props.exportExcel(param);

        if (data) {
            const title = this._getReportTitle();
            const reportExcel = new ReportExcel(data, title);
            reportExcel.export();
        }
    }

    _getReportTitle() {
        const { selectedProduct, monthDate } = this.state;
        const productTitle = selectedProduct === 0 ? 'All Products' : this._getSelectedProductName(selectedProduct);
        const selectedMonthTitle = this.handleFormatMonthPicker(monthDate.getMonth() + 1, monthDate.getFullYear());

        return `Player Detail - ${productTitle} - ${selectedMonthTitle}`;
    }

    _getSelectedProductName(productId) {
        const selectedProduct = this.state.productList.find(p => p.ProductId === productId);

        return selectedProduct ? selectedProduct.ProductName : '';
    }

    render() {
        const { productList, selectedProduct, monthDate } = this.state;

        return productList && (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-2">{resource.Product}</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{resource.All}</option>
                            {
                                productList.map(p => {
                                    return (<option value={p.ProductId} key={p.ProductId}>{htmlDecode(p.ProductName)}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{resource.Month}</Label>
                        <MonthPicker minMonth={minMonthDate.month} minYear={minMonthDate.year}
                            selectedDropdownYear={monthDate.getFullYear()}
                            selectedMonth={monthDate.getMonth() + 1}
                            onSelect={this.handleSelectMonth}
                            onFormat={this.handleFormatMonthPicker}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={() => this.handleSearch(monthDate)}>
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

function mapStateToProps({ today, affCommonInfo, playerDetail }) {
    const { param } = playerDetail;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        param,
        affId,
        today
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, exportExcel, getProductList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);