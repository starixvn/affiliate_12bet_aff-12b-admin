import React from 'react';
import Dashboard from './components/Dashboard';
import FilterForm from './components/FilterForm';
import TableGrid from './components/TableGrid';

const AllCampaigns = () => {
    const dashboardData = {
        totalCampaigns: 4,
        activeCampaigns: 2,
        draftCampaigns: 1,
        pausedCampaigns: 1,
        inactiveCampaigns: 1
    };

    return (
        <div className="all-campaigns">
            <Dashboard dashboardData={dashboardData} />
            <FilterForm />
            <TableGrid />
        </div>
    );
};

export default AllCampaigns;
