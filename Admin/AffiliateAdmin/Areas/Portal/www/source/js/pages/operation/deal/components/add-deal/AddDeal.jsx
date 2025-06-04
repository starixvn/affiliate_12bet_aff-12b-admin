import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert } from 'reactstrap';

import { setDealInfo } from '../../actions';
import { buildDefaultDealInfo } from '../../libs/service';

import DealInfo from './../shared/DealInfo';
import SubAffRateSetting from './../shared/sub-aff-rate';

export const AddDeal = (props) => {

    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(true);

    const [activeTab, setActiveTab] = useState('CPA');

    useEffect(() => {
        const dealInfo = buildDefaultDealInfo();

        props.setDealInfo(dealInfo);
    }, []);

    return props.dealInfo && (
        <React.Fragment>
            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
            </Alert>
            <DealInfo />
            <SubAffRateSetting />
            <div className="tabs">
                <button onClick={() => setActiveTab('CPA')} className={activeTab === 'CPA' ? 'active' : ''}><h6>CPA</h6></button>
                <button onClick={() => setActiveTab('RevenueShare')} className={activeTab === 'RevenueShare' ? 'active' : ''}><h6>Revenue Share</h6></button>
            </div>
        </React.Fragment>
    )
}

const mapStateToProps = ({ dealInfo }) => {
    return {
        dealInfo
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddDeal));