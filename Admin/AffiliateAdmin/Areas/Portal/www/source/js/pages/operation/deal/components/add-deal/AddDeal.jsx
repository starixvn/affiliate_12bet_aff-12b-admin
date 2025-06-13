import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert, FormGroup, Button, Col } from 'reactstrap';

import { setDealInfo, toggleLoader, addCPADeal, setParam } from '../../actions';
import { buildDefaultDealInfo } from '../../libs/service';
import { validateDealInfo } from '../../libs/service';

import DealInfo from './../shared/DealInfo';
import SubAffRateSetting from './../shared/sub-aff-rate';
import { DealTypeInfo } from './../shared/DealTypeInfo';
import { messages } from '../../libs/constants';

export const AddDeal = (props) => {

    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(true);

    useEffect(() => {
        const dealInfo = buildDefaultDealInfo();
        const countryCodes = _getAllTargetCountryCodes();

        props.setDealInfo({ ...dealInfo, CountryCodes: countryCodes });
    }, []);

    const _getAllTargetCountryCodes = () => {
        return props.targetCountryList.map(c => c.TargetCountryCode).join(',');
    }

    const _handleSave = async () => {
        props.onSave();

        const { dealInfo } = props;

        const { isValid, messageList } = validateDealInfo(dealInfo);

        if (!isValid) {
            setIsShowMessage(true);
            setMessage(messageList);
            setIsSuccess(false);
        } else {
            props.toggleLoader(true);
            const param = _handleSaveParam(dealInfo);

            const response = await props.addCPADeal(param);
            if (response && response.status === 200) {
                const result = response.data;
                if (result.IsSuccessful) {
                    setTimeout(props.onClose, 3000);
                    const { pagingOption, param } = props;
                    const gettingParam = Object.assign({}, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize }, param);
                    props.setParam(gettingParam);
                }

                setIsShowMessage(true);
                setIsSuccess(result.IsSuccessful);
                setMessage([result.IsSuccessful ? messages.addCPASuccessfully : result.Message]);
            }

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
            <DealTypeInfo />
            <p className="text-danger my-2">Note: Change will take effect on current month (GMT-4)</p>
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

const mapStateToProps = ({ dealInfo, pagingOption, targetCountryList }) => {
    return {
        dealInfo,
        targetCountryList,
        pagingOption
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo,
        toggleLoader,
        addCPADeal,
        setParam
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddDeal));