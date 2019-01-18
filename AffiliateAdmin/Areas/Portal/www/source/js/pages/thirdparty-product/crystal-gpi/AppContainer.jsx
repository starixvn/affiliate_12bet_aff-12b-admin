import React, { PureComponent } from 'react';

import AppLayout from '../../../layouts/AppLayout';
import { post } from '../../../modules/ajaxRequest';
import Filter from './Filter';
import Content from './Content';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            isLoadingData: false
        }
    }

    handleGetRawData = async (param) => {
        this.setState({ data: null, isLoadingData: true });
        const response = await post('ThirdPartyProduct/CrystalGpi/Index', param);

        const { LogMessage, RawData, ConvertedData } = response.data;
        if (LogMessage) {
            alert('Error!');
            console.log(LogMessage);
        }
        else {
            alert('Done!');
            this.setState({ data: { RawData, ConvertedData }, isLoadingData: false });
        }
    }

    render() {
        const { data, isLoadingData } = this.state;

        return (
            <AppLayout>
                <Filter onGetRawData={this.handleGetRawData} isLoadingData={isLoadingData} />
                <Content data={data} />
            </AppLayout>
        )
    }
}