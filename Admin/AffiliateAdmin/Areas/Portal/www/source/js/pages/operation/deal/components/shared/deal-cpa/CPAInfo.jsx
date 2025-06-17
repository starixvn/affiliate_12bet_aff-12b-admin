import React, { useState } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { FormGroup, Label, Col, Input } from 'reactstrap';

import NotificationPopup from '../../../../../common/components/NotificationPopup';
import MultipleSelect from '../../../../../../components/multiple-select';
import TierSetting from './tier-setting/TierSetting';

import { setDealInfo } from '../../../actions';
import { INFINITY_NUMBER } from '../../../libs/constants';
import { format } from '../../../../../../modules/numberFormat';
import { convertTargetCountryList } from '../../../libs/service';

export const CPAInfo = ({ targetCountryList, dealInfo, setDealInfo }) => {

    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(true);

    const _handleChangeTargetCountry = (item, selectedTargetCountry) => {
        setDealInfo({ CountryCodes: selectedTargetCountry });
    }

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult) {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;

            const newGroupProduct = dealInfo.ProductGroup.find(p => p.GroupProductId === firstGroupProductId);
            const newTierSetting = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);

            setDealInfo({
                ProductGroup: [newGroupProduct],
                CPATierSetting: newTierSetting,
                IsInstant: true
            });
        }
    }

    const _handleChangePositiveInteger = (e) => {
        const { name, value } = e.target;
        const numericValue = Number(value.replace(/,/g, ''));
        const oldValue = dealInfo[name];

        let newValue;

        if (value === '') {
            newValue = '';
        } else if (numericValue > 0 && numericValue <= INFINITY_NUMBER) {
            newValue = numericValue;
        } else {
            newValue = oldValue;
        }

        setDealInfo({ [name]: newValue });
    };


    const _handleChangeInstantPayment = () => {
        const isInstant = !dealInfo.IsInstant;

        if (isInstant) {
            setMessage('Checking for Instant Payment will disable Min Turnover.</br>Do you want to proceed?');
            setIsConfirmPopup(true);
        } else {
            setDealInfo({ IsInstant: false });
        }
    }

    const _handleCheckMinPlayerHoldingPeriod = (e) => {
        const { name } = e.target;
        const toggledValue = !dealInfo[name];

        const updatedInfo = { [name]: toggledValue };

        const isZero = dealInfo.HoldingDays === 0 && dealInfo.HoldingTickets === 0;
        const isEmpty = dealInfo.HoldingDays === '' && dealInfo.HoldingTickets === '';

        if (toggledValue && isZero) {
            updatedInfo.HoldingDays = '';
            updatedInfo.HoldingTickets = '';
        } else if (!toggledValue && isEmpty) {
            updatedInfo.HoldingDays = 0;
            updatedInfo.HoldingTickets = 0;
        }

        setDealInfo(updatedInfo);
    };

    const _handleCheckQuota = (e) => {
        const { name } = e.target;
        const toggledValue = !dealInfo[name];

        const updatedInfo = { [name]: toggledValue };

        const isZero = dealInfo.Quota === 0;
        const isEmpty = dealInfo.Quota === '';

        if (toggledValue && isZero) {
            updatedInfo.Quota = '';
        } else if (!toggledValue && isEmpty) {
            updatedInfo.Quota = 0;
        }
        setDealInfo(updatedInfo);
    }

    const _targetCountryList = convertTargetCountryList(targetCountryList, dealInfo.CountryCodes);
    return (
        <React.Fragment>
            <div>
            <FormGroup row inline className='align-items-center'>
                <Label htmlFor="CountryCodes" sm={4}>Player Target Country
                    <span className="text-danger required" style={{ margin: 0 }}>*</span>
                </Label>
                <Col sm={3} className={`${dealInfo.CountryCodes ? '' : 'is-invalid'}`}>
                    <MultipleSelect
                        dataSource={_targetCountryList}
                        keyField="TargetCountryCode"
                        valueField="TargetCountryName"
                        statusField="Checked"
                        maxDisplayItemCount={1}
                        hasInputOption={false}
                        noneSelectedLabel="Please Select"
                        selectedAllLabel="All Target Countries"
                        onChange={_handleChangeTargetCountry} />
                </Col>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label htmlFor="ActiveDays" sm={4} className="d-flex">
                    Activity Period
                    <span className="text-danger required" style={{ margin: 0 }}>*</span>
                    <span title="The player must complete all required actions within this time period to qualify. For example: within 30 days from registration." className="ml-1 oi icon-info"></span>

                </Label>
                <Col sm={3} className="d-flex align-items-center">
                    <Input type="text"
                        name="ActiveDays"
                        id="ActiveDays"
                        value={format(dealInfo.ActiveDays)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(days)</Label>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label sm={4} className="d-flex" htmlFor='IsActivePlayerHoldingPeriod'>
                    <input
                        type="checkbox"
                        name="IsActivePlayerHoldingPeriod"
                        checked={dealInfo.IsActivePlayerHoldingPeriod}
                        onChange={_handleCheckMinPlayerHoldingPeriod}
                    />
                    <span className="ml-1">Minimum Player Holding Period</span>
                    <span title="After completing the activity period, the player must reach the required ticket amount within the holding period (e.g., 30 days)." className="ml-1 oi icon-info"></span>
                </Label>

                <Col sm={3} className="d-flex align-items-center">
                    <Input type="text"
                        disabled={!dealInfo.IsActivePlayerHoldingPeriod}
                        name="HoldingTickets"
                        id="HoldingTickets"
                        value={format(dealInfo.HoldingTickets)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(tickets) in</Label>
                <Col sm={3} className="d-flex align-items-center">
                    <Input type="text"
                        disabled={!dealInfo.IsActivePlayerHoldingPeriod}
                        name="HoldingDays"
                        id="HoldingDays"
                        value={format(dealInfo.HoldingDays)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(days)</Label>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label sm={4} className="d-flex" htmlFor='IsQuota'>
                    <input
                        type="checkbox"
                        name="IsQuota"
                        checked={dealInfo.IsQuota}
                        onChange={_handleCheckQuota}
                    />
                    <span className="ml-1">Quota</span>
                    <span title="The maximum number of players eligible for CPA payment in this deal." className="ml-1 oi icon-info"></span>
                </Label>
                <Col sm={3} className="d-flex align-items-center">
                    <Input type="text"
                        disabled={!dealInfo.IsQuota}
                        name="Quota"
                        id="Quota"
                        value={format(dealInfo.Quota)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(players)</Label>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label sm={4} className="d-flex" htmlFor='IsInstant'>
                    <input
                        type="checkbox"
                        disabled={dealInfo.CheckedMinTurnover}
                        name="IsInstant"
                        checked={!dealInfo.CheckedMinTurnover && dealInfo.IsInstant}
                        onChange={_handleChangeInstantPayment}
                    />
                    <span className="ml-1">Instant Payment</span>
                    <span title="If enabled, CPA will be paid on the same day a player qualifies by meeting all deal conditions. Only available for deals that use FDM or Total Deposit as condition type." className="ml-1 oi icon-info"></span>
                </Label>
            </FormGroup>
            {
                !!message && <NotificationPopup
                    message={message}
                    isConfirmPopup={isConfirmPopup}
                    onClose={_handleCloseNotificationPopup}
                />
            }
            </div>
            <TierSetting />
        </React.Fragment>
    );
};

const mapStateToProps = ({ dealInfo, targetCountryList }) => {
    return {
        targetCountryList,
        dealInfo
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CPAInfo));