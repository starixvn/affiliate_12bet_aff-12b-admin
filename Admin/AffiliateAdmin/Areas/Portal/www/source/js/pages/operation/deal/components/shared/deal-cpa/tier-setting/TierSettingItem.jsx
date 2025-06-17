import React, { useState } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { FormGroup, Col, Form, Label, Input } from 'reactstrap';

import MultipleSelect from '../../../../../../../components/multiple-select';
import TierSettingDataItem from './TierSettingDataItem';
import NotificationPopup from '../../../../../../common/components/NotificationPopup';

import { setDealInfo } from '../../../../actions';
import { deepClone } from '../../../../../../../modules/dataHandler';

export const TierSettingItem = (props) => {
    const { tierSetting, isFirstItem, index, dealInfo } = props;
    const { ProductGroup, TierSetting } = tierSetting;

    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(true);

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult) {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;

            const newGroupProduct = dealInfo.ProductGroup.find(p => p.GroupProductId === firstGroupProductId);
            const newTierSetting = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);

            props.setDealInfo({
                ProductGroup: [newGroupProduct],
                CPATierSetting: newTierSetting,
                CheckedMinTurnover: false
            });
        }
    }

    const _handleAddTier = () => {
        const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;
        const lastGroupProductId = dealInfo.ProductGroup[dealInfo.ProductGroup.length - 1].GroupProductId;
        const firstTierItem = dealInfo.CPATierSetting.find(t => t.GroupProductId === firstGroupProductId && t.TierId === 1);

        const newProductGroup = {
            ProductIds: '',
            GroupProductId: lastGroupProductId + 1,
        }
        const newTierItem = {
            TierId: 1,
            GroupProductId: lastGroupProductId + 1,
            MinFdm: firstTierItem.MinFdm,
            MinTotalDeposit: firstTierItem.MinTotalDeposit,
            MinTO: 0,
            CpaAmount: 1
        }

        props.setDealInfo({
            CPATierSetting: [...props.dealInfo.CPATierSetting, newTierItem],
            ProductGroup: [...props.dealInfo.ProductGroup, newProductGroup]
        });
    }

    const _handleDeleteTier = (groupProductId) => {
        props.setDealInfo({
            CPATierSetting: dealInfo.CPATierSetting.filter(t => t.GroupProductId !== groupProductId),
            ProductGroup: dealInfo.ProductGroup.filter(p => p.GroupProductId !== groupProductId)
        });
    }

    const _handleCheck = (e) => {
        const { name } = e.target;

        props.setDealInfo({
            [name]: !dealInfo[name]
        });
    }

    const _handleSelect = (index, selectedItem) => {
        const isChecked = selectedItem.Checked;

        const { ProductGroup } = dealInfo;
        const productGroup = deepClone(ProductGroup);

        const currentProductIds = productGroup[index].ProductIds.split(',').filter(p => !!p);
        if (isChecked) {
            const newProductIds = [...currentProductIds, selectedItem.ProductId];
            productGroup[index].ProductIds = newProductIds.join(',');
        } else {
            const newProductIds = currentProductIds.filter(p => Number(p) !== Number(selectedItem.ProductId));
            productGroup[index].ProductIds = newProductIds.join(',');
        }

        props.setDealInfo({
            ProductGroup: productGroup
        });
    }

    const _handleCheckMinTurnover = () => {
        const isChecked = !dealInfo.CheckedMinTurnover;

        if (isChecked) {
            props.setDealInfo({ CheckedMinTurnover: true });
        } else {
            setMessage('Unchecking for Min Turnover will enable Instant Payment and remove all data from additional Product Groups, if present. Only the first Product Group will be retained.</br>Do you want to proceed?');
            setIsConfirmPopup(true);
        }

    }

    const _buildHeader = () => {
        return (
            <tr>
                <th style={{ width: '10%' }}>Tier</th>
                <th style={{ width: '28%' }}>
                    {isFirstItem
                        ? <Form inline>
                            <FormGroup check className="mx-sm-1">
                                <Label check>
                                    <Input type="checkbox" name="CheckedMinFdm" checked={dealInfo.CheckedMinFdm} onChange={_handleCheck} />
                                </Label>
                            </FormGroup>
                            Min FDM
                        </Form>
                        : 'Min FDM'
                    }
                </th>
                <th style={{ width: '30%' }}>
                    {isFirstItem ?
                        <Form inline>
                            {isFirstItem &&
                                <FormGroup check className="mx-sm-1">
                                    <Label check>
                                        <Input type="checkbox" name="CheckedMinTotalDeposit" checked={dealInfo.CheckedMinTotalDeposit} onChange={_handleCheck} />
                                    </Label>
                                </FormGroup>
                            }
                            Min Total Deposit
                        </Form>
                        : 'Min Total Deposit'
                    }
                </th>
                <th style={{ width: '30%' }}>
                    {isFirstItem ?
                        <Form inline>
                            {isFirstItem &&
                                <FormGroup check className="mx-sm-1">
                                    <Label check>
                                        <Input type="checkbox" disabled={dealInfo.IsInstant} name="CheckedMinTurnover" checked={dealInfo.CheckedMinTurnover} onChange={_handleCheckMinTurnover} />
                                    </Label>
                                </FormGroup>
                            }
                            Min Turnover
                        </Form>
                        : 'Min Turnover'
                    }
                </th>
                <th style={{ width: '20%' }}>CPA Amount</th>
                <th style={{ width: '12%' }}>Action</th>
            </tr>
        )
    }

    const isValid = dealInfo.CheckedMinTurnover ? ProductGroup.filter(p => !p.Checked).length < ProductGroup.length : true;

    return (
        <div>
            <FormGroup row className="align-items-center my-2">
                <Col sm={3}>
                    <span className="group-index">{index + 1}.</span>
                    <div disabled={!dealInfo.CheckedMinTurnover} className={`select-product ${isValid ? '' : 'is-invalid'}`}>
                        <MultipleSelect
                            dataSource={ProductGroup}
                            keyField="ProductId"
                            valueField="ProductName"
                            statusField="Checked"
                            isDisableItemField="IsDisabledItem"
                            hasAllOption={false}
                            hasInputOption={false}
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={(selectedItem) => _handleSelect(index, selectedItem)}
                            isDisable={!dealInfo.CheckedMinTurnover} />
                    </div>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Col sm={11}>
                    <table className='table table-bordered table-striped'>
                        <thead>
                            {_buildHeader()}
                        </thead>
                        <tbody>
                            {TierSetting && TierSetting.map((tier, index) => (
                                <TierSettingDataItem
                                    tierSetting={TierSetting}
                                    isFirstItem={isFirstItem}
                                    tier={tier}
                                    index={index} />
                            ))}
                        </tbody>
                    </table>
                </Col>
                <Col sm={1} className='pl-0'>
                    {isFirstItem
                        ? <button disabled={!dealInfo.CheckedMinTurnover} className="btn btn-action" onClick={_handleAddTier}>Add +</button>
                        : <button className="btn btn-action" onClick={() => _handleDeleteTier(TierSetting[0].GroupProductId)}>
                            <span className="oi oi-trash text-secondary"></span>
                        </button>}
                </Col>
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

const mapStateToProps = ({ dealInfo }) => {
    return {
        dealInfo
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch)
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(TierSettingItem));