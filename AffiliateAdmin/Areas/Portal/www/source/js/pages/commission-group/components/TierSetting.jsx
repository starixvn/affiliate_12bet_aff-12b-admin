import React, { Component } from "react";

import TierSettingItem from "./TierSettingItem";

import { setItem, removeItem } from "../../../modules/utils";
import { commissionValueList } from '../modules/constant';

const defaultTierSetting = { minAm: 0, maxAm: 0, minQm: 0, maxQm: 0, commission: 0.01, infinityAm: false, infinityQm: false };

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tierSettingList: [
                Object.assign({}, defaultTierSetting)
            ],
            commissionList: commissionValueList
        }
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
                <legend><h6 className="font-weight-bold">Tier Setting</h6></legend>
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