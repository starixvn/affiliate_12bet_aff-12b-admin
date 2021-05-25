import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Form, FormGroup, Input, Label, Button } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { globalResource, selectedAllProduct, minDate } from '../libs/constants';
import { setParam, getProductList, exportExcel } from '../actions';
import ReportExcel from '../libs/reportExcel';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { claimResource } from '../../../../modules/claim';
import { getDateRange } from '../../../../modules/utils';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today, param } = this.props;

        const now = new Date(today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        const sixMonthAgo = new Date(today);
        sixMonthAgo.setMonth(sixMonthAgo.getMonth() - 6);

        let fromDate = `${sixMonthAgo.getMonth() + 1}/${sixMonthAgo.getDate()}/${sixMonthAgo.getFullYear()}`;
        let toDate = this.nowString;

        if (param.isClickLink && !!param.monthDate) {
            const date = new Date(param.monthDate);

            fromDate = `${date.getMonth() + 1}/1/${date.getFullYear()}`;
            toDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
        }

        this.state = {
            selectedProduct: selectedAllProduct,
            isGetAll: false,
            fromDate,
            toDate,
            productList: null
        }
    }

    componentDidMount() {
        if (!this.state.productList) {
            this.handleGetProductList();
        }
        if (this.props.param.isClickLink) {
            this.handleSearch();
        }
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        if (param.isClickLink && param !== prevProps.param) {
            const newMonthDate = new Date(param.monthDate);

            const fromDate = `${newMonthDate.getMonth() + 1}/1/${newMonthDate.getFullYear()}`;
            const toDate = `${newMonthDate.getMonth() + 1}/${newMonthDate.getDate()}/${newMonthDate.getFullYear()}`;

            this.setState({ fromDate, toDate, isGetAll: false, selectedProduct: selectedAllProduct }, this.handleSearch);
        }
    }

    handleGetProductList = async () => {
        const productList = await this.props.getProductList();
        this.setState({ productList });
    }

    handleChangeProduct = (event) => {
        const selectedProduct = Number(event.target.value);

        this.setState({
            selectedProduct
        })
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(prevState => getDateRange(momentDate, prevState.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handleChecked = (event) => {
        this.setState({ [event.target.name]: event.target.checked });
    }

    handleSearch = () => {
        const { selectedProduct, isGetAll, fromDate, toDate } = this.state;
        const { sortedParam } = this.props;

        const param = {
            productId: selectedProduct,
            fromDate: isGetAll ? `${minDate.getMonth() + 1}/${minDate.getDate()}/${minDate.getFullYear()}` : fromDate,
            toDate: isGetAll ? this.nowString : toDate,
            ...sortedParam
        }
        this.props.setParam(param);
    }

    handleExportExcel = async () => {
        const { selectedProduct, isGetAll, fromDate, toDate } = this.state;
        const { affId, hasTurnOverPermission, sortedParam } = this.props;

        const param = {
            IsExportExcel: true,
            productId: selectedProduct,
            fromDate: isGetAll ? `${minDate.getMonth() + 1}/${minDate.getDate()}/${minDate.getFullYear()}` : fromDate,
            toDate: isGetAll ? this.nowString : toDate,
            affId,
            ...sortedParam
        }

        const data = await this.props.exportExcel(param);

        if (data) {
            const title = this._getReportTitle();
            const reportExcel = new ReportExcel(data, title, hasTurnOverPermission);
            reportExcel.export();
        }
    }

    _getReportTitle() {
        const { selectedProduct, isGetAll, fromDate, toDate } = this.state;
        const productTitle = selectedProduct === 0 ? 'All Products' : this._getSelectedProductName(selectedProduct);
        const selectedMonthTitle = isGetAll ? "All Dates" : `From ${fromDate} To ${toDate}`;

        return `Player Detail - ${productTitle} - ${selectedMonthTitle}`;
    }

    _getSelectedProductName(productId) {
        const selectedProduct = this.state.productList.find(p => p.ProductId === productId);

        return selectedProduct ? selectedProduct.ProductName : '';
    }

    render() {
        const { productList, selectedProduct, fromDate, toDate, isGetAll } = this.state;

        return productList && (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-2">
                        <Label className="mr-2">{globalResource.Product}</Label>
                        <Input type="select" name="Product" id="Product" value={selectedProduct} onChange={this.handleChangeProduct}>
                            <option value="0">{globalResource.All}</option>
                            {
                                productList.map(p => {
                                    return (<option value={p.ProductId} key={p.ProductId}>{htmlDecode(p.ProductName)}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>

                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate} isEnable={!isGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>

                    <FormGroup className="mr-sm-2">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate} isEnable={!isGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>

                    <FormGroup className="mr-sm-2">
                        <FormGroup check>
                            <Label check>
                                {globalResource.GetAll}
                                <Input name="isGetAll" className="ml-1 mr-2" type="checkbox" checked={isGetAll} onChange={this.handleChecked} />
                            </Label>
                        </FormGroup>
                    </FormGroup>

                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>

                    {
                        this.props.isDisplayExportExcelBtn && (
                            <Button color="link" type="button" className="ml-auto btn-excel" onClick={this.handleExportExcel} title="Export Excel">
                                <span className="oi icon-excel"></span>
                            </Button>
                        )
                    }
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ today, affCommonInfo, playerDetail, claimsPrincipal }) {
    const { param, sortedParam } = playerDetail;
    const { affId } = affCommonInfo;
    const hasTurnOverPermission = claimsPrincipal.hasReadClaim(claimResource.TurnOver);
    const isDisplayExportExcelBtn = claimsPrincipal.hasClaim(claimResource.ExportPlayerDetailExcel);

    return Object.assign({}, {
        param,
        affId,
        today,
        sortedParam,
        hasTurnOverPermission,
        isDisplayExportExcelBtn
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, exportExcel, getProductList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);