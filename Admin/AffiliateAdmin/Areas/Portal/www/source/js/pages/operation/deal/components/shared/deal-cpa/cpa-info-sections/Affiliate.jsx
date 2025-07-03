import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { FormGroup, Label, Col, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import NotificationPopup from '../../../../../../common/components/NotificationPopup';

import { setDealInfo } from '../../../../actions';
import { INFINITY_NUMBER, MONTHLY_SCHEDULE_ID } from '../../../../libs/constants';
import { format } from '../../../../../../../modules/numberFormat';


export const Affiliate = ({ dealInfo, paymentFrequencyList, setDealInfo, isEdit, isSubmitted, today }) => {

    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(false);
    const [scheduleId, setScheduleId] = useState(0);

    const now = new Date(today);
    const nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

    useEffect(() => {
        if (!dealInfo.StartDate && !isEdit) {
            setDealInfo({ StartDate: nowString });
        }
    }, []);

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult) {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;

            const newGroupProduct = dealInfo.ProductGroup.find(p => p.GroupProductId === firstGroupProductId);
            const newTierSetting = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);

            setDealInfo({
                ProductGroup: [newGroupProduct],
                CPATierSetting: newTierSetting,
                IsInstant: true,
                ScheduleId: scheduleId
            });
        }
    }

    const _handleCheckRedeposit = (e) => {
        const { name } = e.target;
        const toggledValue = !dealInfo[name];

        const updatedInfo = { [name]: toggledValue };

        if (toggledValue) {
            updatedInfo.RedepositRate = '';
            updatedInfo.RedepositDays = '';
        } else {
            updatedInfo.RedepositRate = 0;
            updatedInfo.RedepositDays = 0;
        }

        setDealInfo(updatedInfo);
    };

    const _handleChangePositiveInteger = (e) => {
        const { name, value } = e.target;
        const numericValue = Number(value.replace(/,/g, ''));
        const oldValue = dealInfo[name];

        let newValue;

        if (value === '') {
            newValue = '';
        } else if (numericValue >= 0 && numericValue <= INFINITY_NUMBER) {
            newValue = numericValue;
        } else {
            newValue = oldValue;
        }

        setDealInfo({ [name]: newValue });
    };

    const _handleChangeScheduleId = (e) => {
        if (isEdit) {
            return;
        }

        const value = Number(e.target.value);

        const isMonthly = value === MONTHLY_SCHEDULE_ID;
        const isOther = value !== 0 && !isMonthly;

        if (isMonthly) {
            setDealInfo({ ScheduleId: MONTHLY_SCHEDULE_ID, IsInstant: false });
            return;
        }

        if (isOther && !dealInfo.CheckedMinTurnover) {
            setScheduleId(value);
            setMessage('Checking for Instant Payment will disable Min Turnover.</br>Do you want to proceed?');
            setIsConfirmPopup(true);
            return;
        }

        // value === 0
        setDealInfo({ ScheduleId: 0, IsInstant: false });
    };

    const _handleCheckQuota = (e) => {
        const { name } = e.target;
        const toggledValue = !dealInfo[name];

        const updatedInfo = { [name]: toggledValue };

        if (toggledValue) {
            updatedInfo.Quota = '';
        } else {
            updatedInfo.Quota = 0;
        }
        setDealInfo(updatedInfo);
    }

    const _handleChangeRedepositRate = (event) => {
        let val = event.target.value;
        let lastValidRedeposit = dealInfo.RedepositRate;

        const leadingZeroRegex = /^0\d+/; // Remove leading zero(s) like "012" -> "12"
        const nonLeadingDigitRegex = /^[^1-9]/; // Reset if value is empty or starts with a non-1-9 digit (e.g., ".", "a", "00")
        const validRedepositRateRegex = /^(100(\.0{0,2})?|([1-9][0-9]?)(\.\d{0,2})?)$/; // 0-100 with up to 2 decimal places

        if (leadingZeroRegex.test(val)) {
            // Remove leading zero(s) like "012" -> "12"
            val = val.replace(/^0+/, '');
        }

        const dotCount = (val.match(/\./g) || []).length;
        const isValid = dotCount <= 1 && validRedepositRateRegex.test(val);

        if (isValid) {
            setDealInfo({ RedepositRate: val });
        } else {
            const mustReset = nonLeadingDigitRegex.test(val);

            if (val === '') {
                lastValidRedeposit = '';
            } else if (mustReset) {
                lastValidRedeposit = '0';
            }

            setDealInfo({ RedepositRate: lastValidRedeposit });
        }
    };

    const _handleChangeDate = (momentDate) => {
        if (!isEdit) {
            setDealInfo({ StartDate: momentDate.format('MM/DD/YYYY') });
        }
    }

    const _getErrorClass = (value, checkedField = true) => {
        if (isSubmitted && checkedField) {
            return Number.isNaN(value) || value < 1 || value > INFINITY_NUMBER ? 'has-error' : '';
        }

        return '';
    }

    const isDisabledScheduleId = isEdit || dealInfo.CheckedMinTurnover && dealInfo.ScheduleId !== 0 && dealInfo.ScheduleId !== MONTHLY_SCHEDULE_ID;
    const isRedepositRateError = isSubmitted && dealInfo.IsRedeposit && (Number.isNaN(dealInfo.RedepositRate) || dealInfo.RedepositRate < 1 || dealInfo.RedepositRate > 100);

    return (
        <fieldset className="affiliate-group">
            <legend><h6 className="font-weight-bold">Affiliate</h6></legend>

            <FormGroup row className='align-items-center'>
                <Label sm={4} className="d-flex" htmlFor='IsRedeposit'>
                    <input
                        type="checkbox"
                        name="IsRedeposit"
                        id="IsRedeposit"
                        checked={dealInfo.IsRedeposit}
                        onChange={_handleCheckRedeposit}
                    />
                    <span className="ml-1">Redeposit</span>
                    <span title="The affiliate receive CPA Amount only when a minimum percentage of their referred players redeposit within a specified number of days." className="ml-1 oi icon-info"></span>
                </Label>

                <Col sm={2} className="d-flex align-items-center">
                    <Input type="text"
                        disabled={!dealInfo.IsRedeposit}
                        name="RedepositRate"
                        className={isRedepositRateError ? 'has-error' : ''}
                        id="RedepositRate"
                        value={dealInfo.RedepositRate}
                        onChange={_handleChangeRedepositRate} />
                </Col>
                <Label className="text-content mb-0 ms-2">% of eligible players | Re-checked within</Label>
                <Col sm={2} className="d-flex align-items-center">
                    <Input type="text"
                        disabled={!dealInfo.IsRedeposit}
                        name="RedepositDays"
                        className={_getErrorClass(dealInfo.RedepositDays, dealInfo.IsRedeposit)}
                        id="RedepositDays"
                        value={format(dealInfo.RedepositDays)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(days)</Label>
            </FormGroup>
            <FormGroup row className='align-items-center'>
                <Label sm={4} className="d-flex" htmlFor='IsQuota'>
                    <input
                        type="checkbox"
                        name="IsQuota"
                        id="IsQuota"
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
                        className={_getErrorClass(dealInfo.Quota, dealInfo.IsQuota)}
                        id="Quota"
                        value={format(dealInfo.Quota)}
                        onChange={_handleChangePositiveInteger} />
                </Col>
                <Label className="text-content mb-0 ms-2">(players)</Label>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label htmlFor="ScheduleId" sm={4} className="d-flex">
                    Payment Frequency
                    <span title="The player must complete all required actions within this time period to qualify. For example: within 30 days from registration." className="ml-1 oi icon-info"></span>

                </Label>
                <Col sm={3}>
                    <Input type="select"
                        name="ScheduleId"
                        id="ScheduleId"
                        disabled={isEdit}
                        value={dealInfo.ScheduleId}
                        onChange={_handleChangeScheduleId}
                        className={`mr-sm-2 search-box ${isSubmitted && dealInfo.ScheduleId === 0 ? 'has-error' : ''}`}>
                        <option value={0}>Please Select</option>
                        {
                            paymentFrequencyList.map(pf =>
                                <option key={pf.ScheduleId}
                                    value={pf.ScheduleId}
                                    disabled={isDisabledScheduleId}>
                                    {pf.ScheduleName}
                                </option>)
                        }
                    </Input>
                </Col>
                <Label className="text-content mb-0 ms-2">Start from</Label>
                <Col sm={4}>
                    <DatePicker
                        isEnable={!isEdit}
                        minDate={nowString}
                        date={dealInfo.StartDate || nowString}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={_handleChangeDate}
                    />
                </Col>
            </FormGroup>
            {
                !!message && <NotificationPopup
                    message={message}
                    isConfirmPopup={isConfirmPopup}
                    onClose={_handleCloseNotificationPopup}
                />
            }
        </fieldset>
    );
}

const mapStateToProps = ({ dealInfo, paymentFrequencyList, isEdit, isSubmitted, today }) => {
    return {
        dealInfo,
        paymentFrequencyList,
        isEdit,
        isSubmitted,
        today
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Affiliate));