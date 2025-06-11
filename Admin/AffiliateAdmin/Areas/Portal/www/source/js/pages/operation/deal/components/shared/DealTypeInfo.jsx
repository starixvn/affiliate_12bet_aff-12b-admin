import React, { useState } from 'react';
import CPAInfo from './deal-cpa/CPAInfo';
import TierSetting from './deal-cpa/tier-setting/TierSetting';

export const DealTypeInfo = () => {
    const [activeTab, setActiveTab] = useState('CPA');

    return (
        <React.Fragment>
            <div className="tabs">
                <button onClick={() => setActiveTab('CPA')} className={activeTab === 'CPA' ? 'active' : ''}><h6>CPA</h6></button>
                {/*<button onClick={() => setActiveTab('RevenueShare')} className={activeTab === 'RevenueShare' ? 'active' : ''}><h6>Revenue Share</h6></button>*/}
            </div>
            <div className="tab-content">
                {activeTab === 'CPA' && (
                    <React.Fragment>
                        <CPAInfo />
                        <TierSetting />
                    </React.Fragment>
                )}
                {activeTab === 'RevenueShare' && (
                    <div>
                        {/* Nội dung của tab Revenue Share */}
                        Revenue Share Content
                    </div>
                )}
            </div>
        </React.Fragment>
    );
};
