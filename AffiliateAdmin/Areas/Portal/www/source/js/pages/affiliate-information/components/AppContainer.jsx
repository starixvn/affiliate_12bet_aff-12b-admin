import React, { Component } from 'react';
import { connect } from 'react-redux';

import AppLayout from '../../../layouts/AppLayout';
import Content from './Content';

const AppContainer = () => {
    return (
        <AppLayout>
            <Content />
        </AppLayout>
    );
}

export default AppContainer;