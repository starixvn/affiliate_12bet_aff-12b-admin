import React, { useEffect, useState } from 'react';
import { post } from '../../../../../modules/ajaxRequest';

const Dashboard = ({ onCardClick }) => {
    const [dashboardData, setDashboardData] = useState({});
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const controller = new AbortController();
        let isMounted = true;
        const fetchDashboardData = async () => {
            try {
                setIsLoading(true);
                const url = "Portal/CampaignManagement/GetDashboard";
                const response = await post(url, {}, { signal: controller.signal });
                if (isMounted) {
                    setDashboardData(response.data);
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

        fetchDashboardData();

        return () => {
            isMounted = false;
            controller.abort();
        };
    }, []);

    const {
        TotalCampaigns = 0,
        ActiveCampaigns = 0,
        DraftCampaigns = 0,
        PausedCampaigns = 0,
        InactiveCampaigns = 0
    } = dashboardData || {};

    const statsCards = [
        {
            status: 'Total',
            value: TotalCampaigns,
            icon: '📊',
            type: 'total',
            filterValue: null
        },
        {
            status: 'Active',
            value: ActiveCampaigns,
            icon: '✅',
            type: 'active',
            filterValue: 1
        },
        {
            status: 'Draft',
            value: DraftCampaigns,
            icon: '✏️',
            type: 'draft',
            filterValue: 2
        },
        {
            status: 'Paused',
            value: PausedCampaigns,
            icon: '⏸️',
            type: 'paused',
            filterValue: 3
        },
        {
            status: 'Inactive',
            value: InactiveCampaigns,
            icon: '🗑️',
            type: 'inactive',
            filterValue: 4
        }
    ];

    const handleCardClick = (card) => {
        if (!isLoading) {
            onCardClick && onCardClick(card.filterValue);
        }
    };

    const LoadingSpinner = () => (
        <div className="loading-spinner">
            <div className="spinner-circle"></div>
        </div>
    );

    return (
        <div className="campaign-dashboard">
            <div className="dashboard-header">
                <h2 className="title">Campaign Dashboard</h2>

                <div className="cards-container">
                    {statsCards.map((card, index) => (
                        <div
                            key={index}
                            className={`dashboard-card dashboard-card--${card.type} ${isLoading ? 'dashboard-card--loading' : ''}`}
                            onClick={() => handleCardClick(card)}
                            style={{ cursor: isLoading ? 'not-allowed' : 'pointer' }}
                        >
                            <div className="card-icon">
                                {card.icon}
                            </div>
                            <div className="card-info">
                                <div className="card-title">
                                    <strong>{card.status}</strong>
                                    <br />
                                    Campaigns
                                </div>
                                <div className="card-value">
                                    {isLoading ? <LoadingSpinner /> : card.value}
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
