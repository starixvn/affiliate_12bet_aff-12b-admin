import React, { PureComponent } from 'react';

import AppLayout from '../../layouts/AppLayout';
import { post } from '../../modules/ajaxRequest';

export const createReportContainer = (FilterComponent, ReportContent, { apiUrl }) => class ReportContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            data: null,
            isLoadingData: false
        }
    }

    handleGetRawData = async (param) => {
        this.setState({ data: null, isLoadingData: true });
        const response = await post(apiUrl, param);
        const { data } = response;

        if (data && data.MainData) {
            alert('Done!');
            this.setState({ data: response.data, isLoadingData: false });
        }
        else {
            alert('Error!');
            console.log(response.data);
        }
    }

    render() {
        const { data, isLoadingData } = this.state;

        return (
            <AppLayout>
                <FilterComponent onGetRawData={this.handleGetRawData} isLoadingData={isLoadingData} />
                <ReportContent data={data} />
            </AppLayout>
        )
    }
}