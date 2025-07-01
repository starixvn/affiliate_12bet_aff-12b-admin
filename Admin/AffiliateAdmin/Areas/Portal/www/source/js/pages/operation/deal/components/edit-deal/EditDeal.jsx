import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert, FormGroup, Button, Col } from 'reactstrap';

import { getDealDetail, toggleLoader, updateDeal, setParam } from '../../actions';
import { validateDealInfo } from '../../libs/service';

import DealInfo from '../shared/DealInfo';
import SubAffRateSetting from '../shared/sub-aff-rate';
import DealPanel from '../shared/DealPanel';
import { messages, DEAL_HYBRID, DEAL_HYBRID_CPA_SECTION, DEAL_HYBRID_REVENUE_SHARE_SECTION } from '../../libs/constants';
import { dealPanelKey } from '../../libs/deal-panels';

export const EditDeal = (props) => {

    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(false);
    const [activePanelWhenHasInvalid, setActivePanelWhenHasInvalid] = useState({ panel: dealPanelKey.cpa });

    useEffect(() => {
        if (props.dealId) {
            props.getDealDetail(props.dealId);
        }
    }, [props.dealId]);

    const _handleSave = async () => {
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

            const response = await props.updateDeal(param);
            if (response && response.status === 200) {
                const result = response.data;
                if (result.IsSuccessful) {
                    setTimeout(props.onClose, 3000);
                    const { pagingOption, param } = props;
                    const gettingParam = Object.assign({}, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize }, param);
                    props.setParam(gettingParam);
                }

                _handleShowMessage(result.IsSuccessful, [result.IsSuccessful ? messages.updateDealSuccessfully : result.Message]);
            }

            props.toggleLoader(false);
        }
        setTimeout(props.onSave, 100);
    }

    const _handleShowMessage = (isSuccess, message) => {
        setIsShowMessage(true);
        setIsSuccess(isSuccess);
        setMessage(message);
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
            <DealPanel dealType={props.dealInfo.DealType} activePanelWhenHasInvalid={activePanelWhenHasInvalid} />
            <FormGroup row inline className="align-items-center">
                {
                    props.isModifiable ? (
                        <React.Fragment>
                            <Col sm={1}>
                                <Button type="button"
                                    className="my-2 btn btn-success btn-save"
                                    onClick={_handleSave}
                                    disabled={isSuccess}>
                                    SAVE
                                </Button>
                            </Col>
                            <Col sm={1}>
                                <Button type="button"
                                    className="my-2 btn btn-info"
                                    onClick={props.onClose}>
                                    CANCEL
                                </Button>
                            </Col>
                        </React.Fragment>
                    ) : (
                        <Col sm={1}>
                            <Button type="button" className="my-2 btn-close" onClick={props.onClose}>CLOSE</Button>
                        </Col>
                    )
                }
            </FormGroup>
        </React.Fragment>
    )
}

const mapStateToProps = ({ dealInfo, pagingOption, targetCountryList, param, isModifiable }) => {
    return {
        dealInfo,
        targetCountryList,
        pagingOption,
        param,
        isModifiable
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        getDealDetail,
        toggleLoader,
        updateDeal,
        setParam
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(EditDeal));