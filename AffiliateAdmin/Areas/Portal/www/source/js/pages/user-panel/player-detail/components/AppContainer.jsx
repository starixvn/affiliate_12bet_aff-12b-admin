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

        const { productList, param, isFromStatistics } = window.initialState;

        this.isFromStatistics = isFromStatistics === 1;
        this.productList = productList;

        this.state = {
            isShowLoader: false,
            selectedProduct: param.ProductId,
            monthDate: new Date(param.MonthDate),
            data: { gridData: null },
            pagingOption: Object.assign({}, defaultPagingOption)
        }
    }

    componentDidMount() {
        if(this.isFromStatistics){
            const { selectedProduct, monthDate, pagingOption } = this.state;

            const param = {
                ProductId: selectedProduct,
                MonthDate: monthDate,
                PageIndex: pagingOption.PageIndex,
                PageSize: pagingOption.PageSize
            }

            this.fetchData(param);
        }
    }

    async fetchData(param) {
        this.setState({ isShowLoader: true });

        const url = 'Portal/UserPanel/PlayerDetail/GetPlayerDetailList';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;
            const pagingOption = Object.assign({}, this.state.pagingOption, {
                PageIndex: param.PageIndex,
                PageSize: param.PageSize,
                TotalItem
            });

            this.setState({
                isShowLoader: false,
                selectedProduct: param.ProductId,
                monthDate: param.MonthDate,
                data: { gridData: MainData },
                pagingOption
            });
        }
        else {
            this.setState({
                isShowLoader: false
            });
        }
    }

    handleSubmit = (filterParam) => {
        const { pagingOption } = this.state;
        const param = {
            ...filterParam,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        }
        this.fetchData(param);
    }

    handlePaging = (pagingOption) => {
        const { selectedProduct, monthDate } = this.state;

        const param = {
            ProductId: selectedProduct,
            MonthDate: monthDate,
            PageIndex: pagingOption.PageIndex,
            PageSize: pagingOption.PageSize
        }

        this.fetchData(param);
    }

    render() {
        const { selectedProduct, monthDate, data, isShowLoader, pagingOption } = this.state;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter onSubmit={this.handleSubmit}
                    productList={this.productList}
                    selectedProduct={selectedProduct}
                    monthDate={monthDate} />
                <Content data={data} pagingOption={pagingOption} onPaging={this.handlePaging} />
            </AppLayout>
        );
    }
}