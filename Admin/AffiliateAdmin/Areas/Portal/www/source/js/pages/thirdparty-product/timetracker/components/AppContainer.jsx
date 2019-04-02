import React from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';

import { post } from '../../../../modules/ajaxRequest';
import { productList } from '../../modules/constants';

export default class AppContainer extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            data: null,
            isLoadingData: false,
            productName: null
        }
    }

    handleSubmit = async (param) => {
        const product = productList.find(x => x.id === Number(param.productId));
        this.setState({ data: null, isLoadingData: true, productName: product && product.name });

        const response = await post('ThirdPartyProduct/TimeTracker/GetDataList', param);

        const { data } = response;
        this.setState({ data, isLoadingData: false });
        alert('Done!');
    }

    render() {
        const { isLoadingData, data, productName } = this.state;
        return (
            <AppLayout>
                <Filter onSubmit={this.handleSubmit} isLoadingData={isLoadingData} />
                <Content data={data} productName={productName} />
            </AppLayout>
        )
    }
}