import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { post } from '../../../../modules/ajaxRequest';

import { Form, FormGroup, Input, Label, Button } from 'reactstrap';
import MonthPicker from '@9softstudio/react-monthpicker';

import { shortedMonthNames, minMonthDate } from '../../../../modules/constants';
import { resource } from '../modules/constants';
import { setParam } from '../actions/index';
import { selectedAllProduct } from '../../../../modules/constants';
import { getProductList } from '../../../customer/user-panel/modules/service';

import ReportExcel from '../modules/reportExcel';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const monthDate = new Date(today);

        this.state = {
            selectedProduct: selectedAllProduct,
            monthDate,
            productList: []
        }
    }

    componentDidMount() {
        this.handleSearch();

        if (this.state.productList.length === 0) {
            this.handleGetProductList();
        }
    }

    componentDidUpdate(prevProps) {
        const { today } = this.props;
        if (prevProps.today !== today) {
            const monthDate = new Date(today);

            this.setState({ monthDate });
        }
    }

    handleGetProductList = async () => {
        const productList = await getProductList();
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

    handleSearch = () => {
        const { selectedProduct, monthDate } = this.state;
        const { affId } = this.props;

        const param = {
            productId: selectedProduct,
            monthDate: `${monthDate.getMonth() + 1}/1/${monthDate.getFullYear()}`,
            affId
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

        const response = await post('Portal/UserPanel/PlayerDetail/GetPlayerDetailList', param);
        if (response && response.status === 200) {
            const title = this._getReportTitle();
            const reportExcel = new ReportExcel(response.data, title);
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

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">{resource.Product}</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{resource.All}</option>
                            {
                                productList.map(p => {
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

                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
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

function mapStateToProps({ today, affCommonInfo, userPanel }) {
    const { param } = userPanel;
    const { affId } = affCommonInfo;
    const monthDate = param && param.monthDate;
    const newMonthDate = monthDate ? today : monthDate;

    return Object.assign({}, {
        today: newMonthDate,
        affId
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);