import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import Loader from '../../../../components/Loader';
import { post } from '../../../../modules/ajaxRequest';

const allProduct = 0;

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        const { productList, param } = window.initialState;

        this.productList = productList;
        this.param = param;

        this.state = {
            isShowLoader: false,
            selectedProduct: allProduct,
            data: { gridData: null, total: null }
        }
    }

    componentDidMount() {
        this.fetchData(this.param);
    }

    async fetchData(param) {
        this.setState({ isShowLoader: true });
        const url = 'Portal/UserPanel/StatementHistory/GetDailyEarningDetail';
        const response = await post(url, param);
        if (response && response.status === 200) {
            const { MainData, Total } = response.data;

            this.setState({
                isShowLoader: false,
                selectedProduct: param.ProductId,
                data: { gridData: MainData, total: Total }
            });
        }
        else {
            this.setState({
                isShowLoader: false,
                selectedProduct: param.ProductId
            });
        }
    }

    handleChangeProduct = (selectedProduct) => {
        const param = Object.assign(this.param, { ProductId: selectedProduct });
        this.fetchData(param);
    }

    render() {
        const { selectedProduct, data, isShowLoader } = this.state;
        const productColumnClassName = selectedProduct === allProduct ? 'text-right' : 'text-right highlight';

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter onChangeProduct={this.handleChangeProduct} productList={this.productList} selectedProduct={selectedProduct} />
                <Content data={data} productColumnClassName={productColumnClassName} />
            </AppLayout>
        );
    }
}