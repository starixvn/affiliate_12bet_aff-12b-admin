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