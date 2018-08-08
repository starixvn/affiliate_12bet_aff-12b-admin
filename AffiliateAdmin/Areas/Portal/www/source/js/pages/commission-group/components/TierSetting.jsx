import React, { Component } from 'react';

import TierSettingItem from './TierSettingItem';
import { updateObjectInArray, removeItem } from '../../../modules/dataHandler';

const defaultTierSetting = { minAmount: 0, maxAmount: 0, minQm: 0, maxQm: 0, commission: 0, infinityAm: false, infinityQm: false };

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tierSettingList: [
                Object.assign({}, defaultTierSetting, { tierId: 1 })
            ]
        }
    }

    _callbackToParent(tierSettingList) {
        this.props.onChange(tierSettingList);
    }

    handleAdd = () => {
        const { tierSettingList } = this.state;
        const newTierSetting = Object.assign({}, defaultTierSetting, { tierId: tierSettingList.length + 1 });
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
        const tierSettingList = updateObjectInArray(this.state.tierSettingList, index, settingValue);

        this.setState({ tierSettingList });
        this._callbackToParent(tierSettingList);
    }

    render() {
        const { tierSettingList } = this.state;

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
                            tierSettingList.map((item, i) => {
                                return (<TierSettingItem index={i} data={item} key={`tier-${i}`}
                                    onRemove={() => this.handleRemove(i)}
                                    onChange={this.handleChangeInput} />);
                            })
                        }
                    </tbody>
                </table>

                <div>
                    <button type="button" className="btn-addTier" onClick={this.handleAdd}>Add Tier</button>
                </div>
            </fieldset>
        )
    }
}