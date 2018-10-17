import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        const { monthDate } = window.initialState;

        this.state = {
            monthDate: new Date(monthDate)
        }
    }

    render() {
        return(
            <AppLayout> 
                <Content monthDate={this.state.monthDate} />
            </AppLayout>
        );
    }
}