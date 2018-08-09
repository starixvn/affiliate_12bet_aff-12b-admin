import React, { Component } from 'react';

import TierSettingItem from './TierSettingItem';
import { updateObjectInArray, removeItem } from '../../../../../modules/dataHandler';
import { isValidNumber } from '../../../../../modules/utils';

const defaultTierSetting = { MinAmount: 0, MaxAmount: 0, MinQm: 0, MaxQm: 0, Commission: 0, InfinityAm: false, InfinityQm: false};

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tierSettingList: this.props.data
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
            defaultMinAmount = lastItem.MaxAmount + 1;
            defaultMinQm = lastItem.MaxQm + 1;
        }

        const newTierSetting = Object.assign({}, defaultTierSetting, {
            TierId: itemCount + 1,
            MinAmount: defaultMinAmount,
            MaxAmount: defaultMinAmount,
            MinQm: defaultMinQm,
            MaxQm: defaultMinQm
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
        const { MinAmount, MaxAmount, MinQm, MaxQm } = settingValue;
        if ((!MinAmount || isValidNumber(MinAmount)) &&
            (!MaxAmount || isValidNumber(MaxAmount)) &&
            (!MinQm || isValidNumber(MinQm)) &&
            (!MaxQm || isValidNumber(MaxQm))) {
            const tierSettingList = updateObjectInArray(this.state.tierSettingList, index, settingValue);

            this.setState({ tierSettingList });
            this._callbackToParent(tierSettingList);
        }
    }

    render() {
        const { tierSettingList } = this.state;
        const canAddTier = tierSettingList.filter(item => item.InfinityAm).length > 0;

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
                        <span className="oi oi-plus"></span> Add Tier
                    </button>
                </div>
            </fieldset>
        )
    }
}