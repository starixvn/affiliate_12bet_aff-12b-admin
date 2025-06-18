import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Input } from 'reactstrap';

import { FIRST_ITEM_INDEX, INFINITY_MONEY } from '../../../../libs/constants';
import { setDealInfo } from '../../../../actions';
import { format } from '../../../../../../../modules/numberFormat';

export const TierSettingDataItem = (props) => {
    const { tierSetting, isFirstItem, tier, index, dealInfo } = props;
    const isFirstTier = index === FIRST_ITEM_INDEX;

    const parseValue = (value) => Number(value.replace(/,/g, ''));

    const getValidatedValue = (rawValue, numericValue, oldValue, allowZero = true, emptyFallback = 0) => {
        if (rawValue === '') {
            return emptyFallback;
        }

        if ((allowZero ? numericValue >= 0 : numericValue > 0) && numericValue <= INFINITY_MONEY) {
            return numericValue;
        }

        return oldValue;
    };

    const updateTierSetting = (conditionFn, updateFn) => {
        const newTierSetting = dealInfo.CPATierSetting.map(t =>
            conditionFn(t) ? updateFn(t) : t
        );
        props.setDealInfo({ CPATierSetting: newTierSetting });
    };

    const _handleChangeValue = (e, tierId) => {
        const { name, value } = e.target;
        const numericValue = parseValue(value);
        const oldValue = tier[name];

        const newValue = getValidatedValue(value, numericValue, oldValue, true, 0);

        if (isFirstItem) {
            const currentTierIndex = tierSetting.findIndex(s => s.TierId === tierId);

            const newTierSetting = dealInfo.CPATierSetting.map(t => {
                if (t.GroupProductId === tier.GroupProductId && t.TierId === tierId) {
                    return { ...t, [name]: newValue };
                }

                if (t.GroupProductId !== tier.GroupProductId) {
                    const updatedTierSetting = dealInfo.CPATierSetting.filter(c => c.GroupProductId === t.GroupProductId);
                    const updateTierItemIndex = updatedTierSetting.findIndex(c => c.TierId === t.TierId);

                    if (updateTierItemIndex !== -1 && updateTierItemIndex === currentTierIndex) {
                        return { ...t, [name]: newValue };
                    }
                }

                return t;
            });

            props.setDealInfo({ CPATierSetting: newTierSetting });
        }
    };

    const _handleChangeMinTurnover = (e, groupProductId, tierId) => {
        const { name, value } = e.target;
        const numericValue = parseValue(value);
        const oldValue = tier[name];

        const newValue = getValidatedValue(value, numericValue, oldValue, true, 0);

        updateTierSetting(
            (t) => t.GroupProductId === groupProductId && t.TierId === tierId,
            (t) => ({ ...t, [name]: newValue })
        );
    };

    const _handleChangeCpaAmount = (e, groupProductId, tierId) => {
        const { name, value } = e.target;
        const numericValue = parseValue(value);
        const oldValue = tier[name];

        const newValue = getValidatedValue(value, numericValue, oldValue, false, 1);

        updateTierSetting(
            (t) => t.GroupProductId === groupProductId && t.TierId === tierId,
            (t) => ({ ...t, [name]: newValue })
        );
    };

    const _handleAddTierItem = (groupProductId) => {
        let newTierItem = null;

        if (isFirstItem) {
            const lastTierItem = tierSetting[tierSetting.length - 1];
            newTierItem = {
                TierId: lastTierItem.TierId + 1,
                GroupProductId: lastTierItem.GroupProductId,
                MinFdm: lastTierItem.MinFdm + 1,
                MinTotalDeposit: lastTierItem.MinTotalDeposit + 1,
                MinTO: lastTierItem.MinTO + 1,
                CpaAmount: lastTierItem.CpaAmount + 1
            }
        } else {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;
            const firstTier = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);

            const currentTier = dealInfo.CPATierSetting.filter(t => t.GroupProductId === groupProductId);
            const lastCurrentTierItem = currentTier[currentTier.length - 1];
            const nextCurrentTierItem = firstTier[currentTier.length];

            if (nextCurrentTierItem) {
                newTierItem = {
                    TierId: lastCurrentTierItem.TierId + 1,
                    GroupProductId: groupProductId,
                    MinFdm: nextCurrentTierItem.MinFdm,
                    MinTotalDeposit: nextCurrentTierItem.MinTotalDeposit,
                    MinTO: lastCurrentTierItem.MinTO + 1,
                    CpaAmount: lastCurrentTierItem.CpaAmount + 1
                }
            } else {
                newTierItem = {
                    TierId: lastCurrentTierItem.TierId + 1,
                    GroupProductId: groupProductId,
                    MinFdm: lastCurrentTierItem.MinFdm + 1,
                    MinTotalDeposit: lastCurrentTierItem.MinTotalDeposit + 1,
                    MinTO: lastCurrentTierItem.MinTO + 1,
                    CpaAmount: lastCurrentTierItem.CpaAmount + 1
                }
            }
        }

        if (newTierItem) {
            props.setDealInfo({
                CPATierSetting: [...props.dealInfo.CPATierSetting, newTierItem]
            });
        }
    }

    const _handleDeleteTierItem = (groupProductId, tierId) => {
        props.setDealInfo({
            CPATierSetting: props.dealInfo.CPATierSetting.filter(t => t.GroupProductId !== groupProductId || t.TierId !== tierId)
        });
    }

    return (
        <tr key={index}>
            <td className="text-center">{index + 1}</td>
            <td className="text-center">
                <Input
                    disabled={!isFirstItem || !dealInfo.CheckedMinFdm}
                    type="text"
                    name="MinFdm"
                    id={"MinFdm" + tier.TierId}
                    value={format(tier.MinFdm)}
                    onChange={(e) => _handleChangeValue(e, tier.TierId)}
                />
            </td>
            <td className="text-center">
                <Input
                    disabled={!isFirstItem || !dealInfo.CheckedMinTotalDeposit}
                    type="text"
                    name="MinTotalDeposit"
                    id={"MinTotalDeposit" + tier.TierId}
                    value={format(tier.MinTotalDeposit)}
                    onChange={(e) => _handleChangeValue(e, tier.TierId)}
                />
            </td>
            <td className="text-center">
                <Input
                    disabled={dealInfo.IsInstant || !dealInfo.CheckedMinTurnover}
                    type="text"
                    name="MinTO"
                    id={"MinTO" + tier.TierId}
                    value={format(tier.MinTO)}
                    onChange={(e) => _handleChangeMinTurnover(e, tier.GroupProductId, tier.TierId)}
                />
            </td>
            <td className="text-center">
                <Input
                    type="text"
                    name="CpaAmount"
                    id={"CpaAmount" + tier.TierId}
                    value={format(tier.CpaAmount)}
                    onChange={(e) => _handleChangeCpaAmount(e, tier.GroupProductId, tier.TierId)}
                />
            </td>
            <td className="text-center">
                {isFirstTier
                    ? <button className="btn btn-tier" onClick={() => _handleAddTierItem(tier.GroupProductId)}>+</button>
                    : <button className="btn btn-tier" onClick={() => _handleDeleteTierItem(tier.GroupProductId, tier.TierId)}>-</button>}
            </td>
        </tr>
    )
}

const mapStateToProps = ({ dealInfo }) => {
    return {
        dealInfo
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ setDealInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(TierSettingDataItem));