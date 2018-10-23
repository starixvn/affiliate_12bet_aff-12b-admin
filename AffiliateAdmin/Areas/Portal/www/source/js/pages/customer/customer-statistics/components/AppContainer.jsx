import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return(
            <AppLayout> 
                <Content />
            </AppLayout>
        );
    }
}