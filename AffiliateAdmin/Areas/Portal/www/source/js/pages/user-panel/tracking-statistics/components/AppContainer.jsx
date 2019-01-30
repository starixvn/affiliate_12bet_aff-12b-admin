﻿import React, { Component } from 'react';

import AppLayout from '../../../../layouts/AppLayout';

export default class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <h2>Traking Statistics</ h2>
            </AppLayout>
        );
    }
}