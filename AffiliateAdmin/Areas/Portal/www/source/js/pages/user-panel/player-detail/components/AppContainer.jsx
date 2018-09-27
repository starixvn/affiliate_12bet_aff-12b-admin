import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';
import { post } from '../../../../modules/ajaxRequest';
import { defaultPagingOption } from '../../../../modules/constants';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        const { productList, param } = window.initialState;

        this.productList = productList;
        this.param = {
            ProductId: param.ProductId,
            MonthDate: param.MonthDate,
            PageIndex: defaultPagingOption.PageIndex,
            PageSize: defaultPagingOption.PageSize
        };

        this.state = {
            isShowLoader: false,
            selectedProduct: param.ProductId,
            monthDate: new Date(param.MonthDate),
            data: { gridData: null, totalItem: 0 }
        }
    }

    componentDidMount() {
    }

    async fetchData(param) {
        this.setState({ isShowLoader: true });
        const url = 'Portal/UserPanel/PlayerDetail/GetPlayerDetailList';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;

            this.setState({
                isShowLoader: false,
                selectedProduct: param.ProductId,
                monthDate: param.MonthDate,
                data: { gridData: MainData, totalItem: TotalItem }
            });
        }
        else {
            this.setState({
                isShowLoader: false,
                selectedProduct: param.ProductId,
                monthDate: param.MonthDate
            });
        }
    }

    handleSubmit = (filterParam) => {
        this.param = Object.assign({}, this.param, filterParam, { PageIndex: 1 });
        this.fetchData(this.param);
    }

    handlePaging = (pagingOption) => {
        const param = Object.assign(this.param, pagingOption);
        this.fetchData(param);
    }

    render() {
        const { selectedProduct, monthDate, data, isShowLoader } = this.state;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter onSubmit={this.handleSubmit}
                    productList={this.productList}
                    selectedProduct={selectedProduct}
                    monthDate={monthDate} />
                <Content data={data} onPaging={this.handlePaging} />
            </AppLayout>
        );
    }
}