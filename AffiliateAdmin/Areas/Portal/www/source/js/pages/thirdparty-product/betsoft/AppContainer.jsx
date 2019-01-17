import React, { PureComponent } from 'react';

import AppLayout from '../../../layouts/AppLayout';
import { post } from '../../../modules/ajaxRequest';
import Filter from './Filter';
import Content from './Content';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null
        }
    }

    handleGetRawData = async (param) => {
        this.setState({ data: null });
        const response = await post('ThirdPartyProduct/Betsoft/Index', param);

        const { LogMessage, RawData, ConvertedData } = response.data;
        if (LogMessage) {
            alert('Error!');
            console.log(LogMessage);
        }
        else {
            this.setState({ data: { RawData, ConvertedData } });
        }
    }

    render() {
        const { data } = this.state;

        return (
            <AppLayout>
                <Filter onGetRawData={this.handleGetRawData} />
                <Content data={data} />
            </AppLayout>
        )
    }
}