import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert, FormGroup, Button, Col } from 'reactstrap';

import { setDealInfo, toggleLoader, addDeal, setParam, setIsSubmitted, getDealDetail } from '../../actions';
import { buildDefaultDealInfo } from '../../libs/service';
import { validateDealInfo } from '../../libs/service';

import DealInfo from '../shared/DealInfo';
import DealPanel from '../shared/DealPanel';
import SubAffRateSetting from '../shared/sub-aff-rate';
import { messages, DEAL_CPA } from '../../libs/constants';

export const AddDeal = (props) => {

    const { dealId } = props;
    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(true);

    useEffect(() => {
        if (dealId) {
            props.getDealDetail(dealId);
        } else {
            const newDealInfo = buildDefaultDealInfo();
            newDealInfo.DealType = props.param.DealType || DEAL_CPA;
            const countryCodes = _getAllTargetCountryCodes();

            props.setDealInfo({ ...newDealInfo, CountryCodes: countryCodes });
        }
    }, [dealId]);

    const _getAllTargetCountryCodes = () => {
        return props.targetCountryList.map(c => c.TargetCountryCode).join(',');
    }

    const _handleSave = async () => {
        props.onSave();

        if (!props.isSubmitted) {
            props.setIsSubmitted(true);
        }

        const { dealInfo } = props;

        const { isValid, messageList } = validateDealInfo(dealInfo);
        if (!isValid) {
            setIsShowMessage(true);
            setMessage(messageList);
            setIsSuccess(false);
        } else {
            props.toggleLoader(true);
            const param = _handleSaveParam(dealInfo);

            const response = await props.addDeal(param);
            if (response && response.status === 200) {
                const result = response.data;
                if (result.IsSuccessful) {
                    setTimeout(props.onClose, 3000);
                    const { pagingOption, param } = props;
                    const gettingParam = {
                        ...param,
                        PageIndex: pagingOption.PageIndex,
                        PageSize: pagingOption.PageSize,
                    };
                    props.setParam(gettingParam);
                }

                setIsShowMessage(true);
                setIsSuccess(result.IsSuccessful);
                setMessage([result.IsSuccessful ? messages.addCPASuccessfully : result.Message]);
            }

            props.setIsSubmitted(false);
            props.toggleLoader(false);
        }
    }

    const _handleSaveParam = (dealInfo) => {
        const cloneDealInfo = { ...dealInfo };
        const { IsActivePlayerHoldingPeriod, IsQuota } = cloneDealInfo;

        if (!IsActivePlayerHoldingPeriod) {
            cloneDealInfo.HoldingTickets = 0;
            cloneDealInfo.HoldingDays = 0;
        }
        if (!IsQuota) {
            cloneDealInfo.Quota = 0;
        }

        return cloneDealInfo;
    }

    return props.dealInfo && (
        <React.Fragment>
            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
            </Alert>
            <DealInfo />
            <SubAffRateSetting />
            <DealPanel dealType={props.dealInfo.DealType} />
            <FormGroup row inline className="align-items-center">
                <Col sm={1}>
                    <Button type="button" className="my-2 btn btn-success btn-save" onClick={_handleSave}>SAVE</Button>
                </Col>
                <Col sm={1}>
                    <Button type="button" className="my-2 btn btn-info" onClick={props.onClose}>CANCEL</Button>
                </Col>
            </FormGroup>
        </React.Fragment>
    )
}

const mapStateToProps = ({ dealInfo, pagingOption, targetCountryList, isSubmitted, param }) => {
    return {
        dealInfo,
        targetCountryList,
        pagingOption,
        isSubmitted,
        param
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo,
        toggleLoader,
        addDeal,
        setParam,
        setIsSubmitted,
        getDealDetail
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddDeal));