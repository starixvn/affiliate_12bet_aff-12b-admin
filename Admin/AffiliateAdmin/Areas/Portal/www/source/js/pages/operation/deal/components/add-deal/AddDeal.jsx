import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert, FormGroup, Button, Col } from 'reactstrap';

import { setDealInfo, toggleLoader, addDeal, setParam, getDealDetail } from '../../actions';
import { buildDefaultDealInfo } from '../../libs/service';
import { validateDealInfo } from '../../libs/service';

import DealInfo from '../shared/DealInfo';
import DealPanel from '../shared/DealPanel';
import SubAffRateSetting from '../shared/sub-aff-rate';
import { messages, DEAL_HYBRID, DEAL_HYBRID_CPA_SECTION, DEAL_HYBRID_REVENUE_SHARE_SECTION } from '../../libs/constants';
import { dealPanelKey } from '../../libs/deal-panels';

export const AddDeal = (props) => {

    const { dealId } = props;
    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(true);
    const [isDisabledSubmitBtn, setIsDisabledSubmitBtn] = useState(false);
    const [activePanelWhenHasInvalid, setActivePanelWhenHasInvalid] = useState({ panel: dealPanelKey.cpa });

    useEffect(() => {
        if (dealId) {
            props.getDealDetail(dealId);
        } else {
            const newDealInfo = buildDefaultDealInfo();
            const countryCodes = _getAllTargetCountryCodes();

            props.setDealInfo({ ...newDealInfo, CountryCodes: countryCodes });
        }
    }, [dealId]);

    const _getAllTargetCountryCodes = () => {
        return props.targetCountryList.map(c => c.TargetCountryCode).join(',');
    }

    const _handleSave = async () => {
        props.onSave();

        const { dealInfo } = props;

        const { isValid, messageList } = validateDealInfo(dealInfo);
        if (!isValid) {
            if (dealInfo.DealType === DEAL_HYBRID) {
                const hasInvalidCPA = messageList.some(m => m.includes(DEAL_HYBRID_CPA_SECTION));
                const hasInvalidRevenueShare = messageList.some(m => m.includes(DEAL_HYBRID_REVENUE_SHARE_SECTION));

                const panel = hasInvalidCPA
                    ? dealPanelKey.cpa
                    : hasInvalidRevenueShare
                        ? dealPanelKey.revenueShare
                        : dealPanelKey.cpa;

                setActivePanelWhenHasInvalid({ panel });
            }
            _handleShowMessage(false, messageList);
        } else {
            if (dealInfo.DealType === DEAL_HYBRID) {
                setActivePanelWhenHasInvalid({ panel: dealPanelKey.cpa });
            }
            props.toggleLoader(true);
            const param = _handleSaveParam(dealInfo);

            const response = await props.addDeal(param);
            if (response && response.status === 200) {
                const result = response.data;
                if (result.IsSuccessful) {
                    setIsDisabledSubmitBtn(true);
                    setTimeout(props.onClose, 3000);
                    const { pagingOption, param } = props;
                    const gettingParam = {
                        ...param,
                        PageIndex: pagingOption.PageIndex,
                        PageSize: pagingOption.PageSize,
                    };
                    props.setParam(gettingParam);
                }

                _handleShowMessage(result.IsSuccessful, [result.IsSuccessful ? messages.addDealSuccessfully : result.Message]);
            }

            props.toggleLoader(false);
        }
    }

    const _handleShowMessage = (isSuccess, message) => {
        setIsShowMessage(true);
        setIsSuccess(isSuccess);
        setMessage(message);
    }

    const _handleSaveParam = (dealInfo) => {
        const cloneDealInfo = { ...dealInfo };
        const { IsActivePlayerHoldingPeriod, IsQuota, ActiveDays } = cloneDealInfo;

        cloneDealInfo.ActiveDays = ActiveDays || 0;
        cloneDealInfo.GroupId = 0;
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
            <DealPanel dealType={props.dealInfo.DealType} activePanelWhenHasInvalid={activePanelWhenHasInvalid} />
            <FormGroup row inline className="align-items-center">
                <Col sm={1}>
                    <Button type="button"
                        className="my-2 btn btn-success btn-save"
                        onClick={_handleSave}
                        disabled={isDisabledSubmitBtn}>
                        SAVE
                    </Button>
                </Col>
                <Col sm={1}>
                    <Button type="button"
                        className="my-2 btn btn-info btn-cancel"
                        onClick={props.onClose}>
                        CANCEL
                    </Button>
                </Col>
            </FormGroup>
        </React.Fragment>
    )
}

const mapStateToProps = ({ dealInfo, pagingOption, targetCountryList, param }) => {
    return {
        dealInfo,
        targetCountryList,
        pagingOption,
        param
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo,
        toggleLoader,
        addDeal,
        setParam,
        getDealDetail
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddDeal));