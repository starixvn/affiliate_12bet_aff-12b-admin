import React, { Component } from "react";

import { formatDecimal } from "../../../modules/numberFormat";
import TierSettingItem from "./TierSettingItem";

import { setItem, removeItem } from "../../../modules/utils";

const defaultTierSetting = { minAm: 0, maxAm: 0, minQm: 0, maxQm: 0, commission: 0.01, infinityAm: false, infinityQm: false };

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tierSettingList: [
                Object.assign({}, defaultTierSetting)
            ],
            commissionList: this._getCommissionList()
        }
    }

    _getCommissionList() {
        let min = 0, max = 0.99;
        const commissionList = [];

        while (min < max) {
            min = min + 0.01;
            commissionList.push(formatDecimal(min));
        }

        return commissionList;
    }

    handleAdd = () => {
        const { tierSettingList } = this.state;

        this.setState({ tierSettingList: [...tierSettingList, Object.assign({}, defaultTierSetting)] });
    }

    handleRemove = (index) => {
        const tierSettingList = removeItem(index, this.state.tierSettingList);
        this.setState({ tierSettingList });
    }

    handleChangeInput = (index, settingValue) => {
        const settingList = this.state.tierSettingList;
        const tierSettingList = setItem(index, settingList, settingValue);

        this.setState({ tierSettingList });
    }

    render() {
        const { tierSettingList, commissionList } = this.state;

        return (
            <fieldset>
                <legend><h4 className="text-bold">Tier Setting</h4></legend>
                <table className="table table-bordered tier-setting-table" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th width="40">Tier</th>
                            <th>Min AM</th>
                            <th>Max AM</th>
                            <th>Min QM</th>
                            <th>Max QM</th>
                            <th width="100">Commission</th>
                            <th width="50">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            tierSettingList.map((item, i) => {
                                return (<TierSettingItem index={i} data={item} key={`tier-${i}`}
                                    commissionList={commissionList}
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