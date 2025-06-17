import React, { Suspense, useState, useEffect } from 'react';
import { DEAL_CPA, DEAL_REVENUE_SHARE, dealPanelKey, dealPanelList } from '../../libs/deal-panels';

export const DealPanel = ({ dealType }) => {
    const [activePanel, setActivePanel] = useState(dealType == DEAL_REVENUE_SHARE ? dealPanelKey.revenueShare : dealPanelKey.cpa);
    const [dealPanels, setDealPanels] = useState(dealPanelList.filter(m => m.supportedDeals.includes(dealType)));

    useEffect(() => {
        const activeKey = dealType == DEAL_REVENUE_SHARE ? dealPanelKey.revenueShare : dealPanelKey.cpa;

        if (activeKey != activePanel) {
            setActivePanel(activeKey);
        }

        setDealPanels(dealPanelList.filter(m => m.supportedDeals.includes(dealType)));

    }, [dealType]);

    const selectedPanel = dealPanels.find(p => p.key === activePanel);
    const PanelComponent = !selectedPanel ? null : selectedPanel.component;

    return (
        <React.Fragment>
            <div className="tabs">
                {
                    dealPanels.map(m => (
                        <button key={m.key} onClick={() => setActivePanel(m.key)} className={activePanel === m.key ? 'active' : ''}><h6>{m.name}</h6></button>
                    ))
                }
            </div>
            <div className="tab-content">
                {
                    PanelComponent &&
                    <Suspense fallback={null}>
                        <PanelComponent />
                    </Suspense>
                }
            </div>
            {
                dealType != DEAL_CPA && <p className="text-danger my-2">Note: Change will take effect on current month (GMT-4)</p>
            }
        </React.Fragment>
    );
};

export default React.memo(DealPanel);