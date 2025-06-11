import React, { useState } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { FormGroup, Label, Col, Input } from 'reactstrap';

import NotificationPopup from '../../../../../common/components/NotificationPopup';

import { setDealInfo } from '../../../actions';
import { INFINITY_NUMBER } from '../../../libs/constants';
import { format } from '../../../../../../modules/numberFormat';

export const CPAInfo = ({ targetCountryList, dealInfo, setDealInfo }) => {

    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(true);

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult) {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;

            const newGroupProduct = dealInfo.ProductGroup.find(p => p.GroupProductId === firstGroupProductId);
            const newTierSetting = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);

            setDealInfo({
                ProductGroup: [newGroupProduct],
                CPATierSetting: newTierSetting,
                IsInstantPayment: true
            });
        }
    }

    const _handleChange = (e) => {
        const { name, value } = e.target;
        setDealInfo({ [name]: value });
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
        const isInstantPayment = !dealInfo.IsInstantPayment;

        if (isInstantPayment) {
            setMessage('Checking for Instant Payment will disable Min Wagering and remove all data from additional Product Groups, if present. Only the first Product Group will be retained.</br></br>Do you want to proceed?');
            setIsConfirmPopup(true);
        } else {
            setDealInfo({ IsInstantPayment: false });
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

    return (
        <div>
            <FormGroup row inline className='align-items-center'>
                <Label htmlFor="CountryCodes" sm={4}>Player Target Country
                    <span className="text-danger required" style={{ margin: 0 }}>*</span>
                </Label>
                <Col sm={3}>
                    <Input type="select"
                        name='CountryCodes'
                        id='CountryCodes'
                        className="mr-sm-2 search-box"
                        value={dealInfo.CountryCodes}
                        onChange={_handleChange}
                    >
                        <option key={0} value={''}>All Target Countries</option>
                        {Object.keys(targetCountryList).map((key, index) => (
                            <option key={index} value={key}>{targetCountryList[key]}</option>
                        ))}
                    </Input>
                </Col>
            </FormGroup>

            <FormGroup row className='align-items-center'>
                <Label htmlFor="ActiveDays" sm={4}>Activity Period <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
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
                <Label sm={4}>
                    <input
                        type="checkbox"
                        name="IsActivePlayerHoldingPeriod"
                        checked={dealInfo.IsActivePlayerHoldingPeriod}
                        onChange={_handleCheckMinPlayerHoldingPeriod}
                    />
                    <span>{' '}Minimum Player Holding Period</span>
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
                <Label sm={4}>
                    <input
                        type="checkbox"
                        name="IsQuota"
                        checked={dealInfo.IsQuota}
                        onChange={_handleCheckQuota}
                    />
                    <span>{' '}Quota</span>
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
                <Label sm={4}>
                    <input
                        type="checkbox"
                        disabled={dealInfo.CheckedMinWagering}
                        name="IsInstantPayment"
                        checked={!dealInfo.CheckedMinWagering && dealInfo.IsInstantPayment}
                        onChange={_handleChangeInstantPayment}
                    />
                    <span>{' '}Instant Payment</span>
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