import React from 'react';
import { useSelector } from 'react-redux';

import Dashboard from './Dashboard';
import FilterForm from './FilterForm';
import TableGrid from './TableGrid';

import AppLayout from '@/layouts/AppLayout';
import Loader from '@/components/Loader';

const AppContainer = () => {
    const [isShowLoader] = useSelector(state => [state.isShowLoader]);

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <Dashboard />
            <FilterForm />
            <TableGrid />
        </AppLayout>
    );
}

export default AppContainer;