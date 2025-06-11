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
            const newTierSetting = dealInfo.CPATierSetting.map(t =>
                (t.GroupProductId === tier.GroupProductId && t.TierId === tierId)
                    || (t.GroupProductId !== tier.GroupProductId && t.MinFdm === tier.MinFdm && t.MinTotalDeposit === tier.MinTotalDeposit)
                    ? { ...t, [name]: newValue }
                    : t
            );

            props.setDealInfo({ CPATierSetting: newTierSetting });
        }
    };

    const _handleChangeMinWagering = (e, groupProductId, tierId) => {
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
                MinWagering: lastTierItem.MinWagering + 1,
                CpaAmount: lastTierItem.CpaAmount + 1
            }
        } else {
            const firstGroupProductId = dealInfo.ProductGroup[0].GroupProductId;
            const firstTier = dealInfo.CPATierSetting.filter(t => t.GroupProductId === firstGroupProductId);
            const lastFirstTierItem = firstTier[firstTier.length - 1];

            const currentTier = dealInfo.CPATierSetting.filter(t => t.GroupProductId === groupProductId);
            const lastCurrentTierItem = currentTier[currentTier.length - 1];

            const existNextTierItem = lastFirstTierItem.MinFdm > lastCurrentTierItem.MinFdm && lastFirstTierItem.MinTotalDeposit > lastCurrentTierItem.MinTotalDeposit;
            const nextCurrentTierItem = existNextTierItem ? firstTier.find(t => t.MinFdm > lastCurrentTierItem.MinFdm && t.MinTotalDeposit > lastCurrentTierItem.MinTotalDeposit) : null;

            if (nextCurrentTierItem) {
                newTierItem = {
                    TierId: lastCurrentTierItem.TierId + 1,
                    GroupProductId: groupProductId,
                    MinFdm: nextCurrentTierItem.MinFdm,
                    MinTotalDeposit: nextCurrentTierItem.MinTotalDeposit,
                    MinWagering: lastCurrentTierItem.MinWagering + 1,
                    CpaAmount: lastCurrentTierItem.CpaAmount + 1
                }
            } else {
                newTierItem = {
                    TierId: lastCurrentTierItem.TierId + 1,
                    GroupProductId: groupProductId,
                    MinFdm: lastCurrentTierItem.MinFdm + 1,
                    MinTotalDeposit: lastCurrentTierItem.MinTotalDeposit + 1,
                    MinWagering: lastCurrentTierItem.MinWagering + 1,
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
                    disabled={dealInfo.IsInstantPayment || !dealInfo.CheckedMinWagering}
                    type="text"
                    name="MinWagering"
                    id={"MinWagering" + tier.TierId}
                    value={format(tier.MinWagering)}
                    onChange={(e) => _handleChangeMinWagering(e, tier.GroupProductId, tier.TierId)}
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