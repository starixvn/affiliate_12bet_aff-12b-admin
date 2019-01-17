import React, { PureComponent } from 'react';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';

export default class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <Filter />
                <Content />
            </AppLayout>
        )
    }
}