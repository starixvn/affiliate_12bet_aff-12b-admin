import React, { Component } from 'react';

import TierSettingItem from './TierSettingItem';
import { updateObjectInArray, removeItem } from '../../../modules/dataHandler';
import { isValidNumber } from '../../../modules/utils';

const defaultTierSetting = { minAmount: 0, maxAmount: 0, minQm: 0, maxQm: 0, commission: 0, infinityAm: false, infinityQm: false };

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tierSettingList: [
            ]
        }
    }

    _callbackToParent(tierSettingList) {
        this.props.onChange(tierSettingList);
    }

    handleAdd = () => {
        const { tierSettingList } = this.state;
        let defaultMinAmount = 0;
        let defaultMinQm = 0;

        const itemCount = tierSettingList.length;
        if (itemCount > 0) {
            const lastItem = tierSettingList[itemCount - 1];
            defaultMinAmount = lastItem.maxAmount + 1;
            defaultMinQm = lastItem.maxQm + 1;
        }

        const newTierSetting = Object.assign({}, defaultTierSetting, {
            tierId: itemCount + 1,
            minAmount: defaultMinAmount,
            maxAmount: defaultMinAmount,
            minQm: defaultMinQm,
            maxQm: defaultMinQm
        });
        const newTierSettingList = [...tierSettingList, newTierSetting];

        this.setState({ tierSettingList: newTierSettingList });
        this._callbackToParent(newTierSettingList);
    }

    handleRemove = (index) => {
        const tierSettingList = removeItem(index, this.state.tierSettingList);
        this.setState({ tierSettingList });
        this._callbackToParent(tierSettingList);
    }

    handleChangeInput = (index, settingValue) => {
        const { minAmount, maxAmount, minQm, maxQm } = settingValue;
        if ((!minAmount || isValidNumber(minAmount)) && 
            (!maxAmount || isValidNumber(maxAmount)) && 
            (!minQm || isValidNumber(minQm)) && 
            (!maxQm || isValidNumber(maxQm))) {
            const tierSettingList = updateObjectInArray(this.state.tierSettingList, index, settingValue);

            this.setState({ tierSettingList });
            this._callbackToParent(tierSettingList);
        }
    }

    render() {
        const { tierSettingList } = this.state;
        const canAddTier = tierSettingList.filter(item => item.infinityAm).length > 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Tier Setting</h6></legend>
                <table className="table table-bordered tier-setting-table" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th style={{ width: 40 }}>Tier</th>
                            <th>Min AM</th>
                            <th>Max AM</th>
                            <th>Min QM</th>
                            <th>Max QM</th>
                            <th style={{ width: 85 }}>Commission</th>
                            <th style={{ width: 50 }}>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            tierSettingList.length ?
                                tierSettingList.map((item, i) => {
                                    return (<TierSettingItem index={i} data={item} key={`tier-${i}`}
                                        isDisabledInfinity={i < tierSettingList.length - 1}
                                        onRemove={() => this.handleRemove(i)}
                                        onChange={this.handleChangeInput} />);
                                }) :
                                (<tr><td colSpan="7" className="text-center">There is no information available</td></tr>)
                        }
                    </tbody>
                </table>

                <div>
                    <button type="button" className="btn btn-addTier btn-secondary" onClick={this.handleAdd} disabled={canAddTier}>
                        <span class="oi oi-plus"></span> Add Tier
                    </button>
                </div>
            </fieldset>
        )
    }
}