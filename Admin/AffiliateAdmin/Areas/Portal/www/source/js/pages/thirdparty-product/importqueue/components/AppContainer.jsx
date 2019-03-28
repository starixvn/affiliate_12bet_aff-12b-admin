import React from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';

import { post } from '../../../../modules/ajaxRequest';

export default class AppContainer extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            data: null,
            isLoadingData: false
        }
    }

    handleSubmit = async (param) => {
        this.setState({ data: null, isLoadingData: true });
        const response = await post('ThirdPartyProduct/ImportQueue/GetDataList', param);

        const { data } = response;
        this.setState({ data, isLoadingData: false });
        alert('Done!');
    }

    render() {
        const { isLoadingData, data } = this.state;
        return (
            <AppLayout>
                <Filter onSubmit={this.handleSubmit} isLoadingData={isLoadingData} />
                <Content data={data} />
            </AppLayout>
        )
    }
}