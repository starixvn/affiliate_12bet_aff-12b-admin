import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';

const API_URL = 'Portal/UserPanel/Customer/CustomerStatistics';
export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        const { MonthDate } = window.initialState;

        this.state = {
            monthDate: new Date(MonthDate)
        }
    }

    render(){
        return(
            <AppLayout> 
                <Content monthDate={this.state.monthDate} />
            </AppLayout>
        );
    }
}