import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';

import { post } from '../../../../modules/ajaxRequest';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    handleClick = async () => {
        const url = 'Portal/LandingPage/GetLandingPageList';
        const response = await post(url);
        if (response) {
            console.log(response.data);
        }
        else {
            console.log(response);
        }
    }

    render() {
        return (
            <AppLayout>
                <button type="button" onClick={this.handleClick}>Get Data</button>
            </AppLayout>
        )
    }
}