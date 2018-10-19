import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);

        const { today } = window.initialState;
        this.today = today;
    }

    render() {
        return(
            <AppLayout> 
                <Content today={this.today} />
            </AppLayout>
        );
    }
}