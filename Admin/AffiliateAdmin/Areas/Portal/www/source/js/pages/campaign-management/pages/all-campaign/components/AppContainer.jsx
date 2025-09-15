import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';

import Dashboard from './Dashboard';
import FilterForm from './FilterForm';
import TableGrid from './TableGrid';
import { convertListToMutilpleSelected } from '../../../../../modules/utils';
import { post } from '../../../../../modules/ajaxRequest';
import { ActionCreators } from '../actions';

import AppLayout from '../../../../../layouts/AppLayout';

const AppContainer = () => {
    const { setStatusList } = ActionCreators;
    const dispatch = useDispatch();
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const controller = new AbortController();
        let isMounted = true;
        const fetchCampaignStatus = async () => {
            try {
                setIsLoading(true);
                const url = "Portal/CampaignManagement/GetAllCampaignStatus";
                const response = await post(url, {}, { signal: controller.signal });
                if (isMounted) {
                    dispatch(setStatusList(convertListToMutilpleSelected(response.data, 'StatusId', 'Status')));
                }
            } catch (err) {
                if (err.name !== "AbortError") {
                    console.error("Fetch failed:", err);
                }
            } finally {
                if (isMounted) {
                    setIsLoading(false);
                }
            }
        }

        fetchCampaignStatus();

        return () => {
            isMounted = false;
            controller.abort();
        };
    }, []);

    return (
        <AppLayout>
            <Dashboard />
            <FilterForm isLoading={isLoading} />
            <TableGrid />
        </AppLayout>
    );
}

export default AppContainer;