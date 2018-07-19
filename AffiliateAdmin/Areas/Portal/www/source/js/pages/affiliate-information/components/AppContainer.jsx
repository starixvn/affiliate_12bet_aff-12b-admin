import React, { Component } from 'react';
import { connect } from 'react-redux';

import axios from 'axios';

import AppLayout from '../../../layouts/AppLayout';
import Content from './Content';

class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount(){
        axios.get('/Portal/Affiliate/GetInfo?username=12BETAFF2233').then(response => {
            console.log(response);
        })
    }

    render() {
        return (
            <AppLayout>
                <Content />
            </AppLayout>
        );
    }
}

export default connect()(AppContainer);