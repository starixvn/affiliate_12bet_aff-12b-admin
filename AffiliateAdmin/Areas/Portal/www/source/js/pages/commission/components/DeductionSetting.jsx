import React, { Component } from "react";

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone } from "../../../modules/utils";

const productList = [
    { code: "SB", name: "Sportsbook", checked: true },
    { code: "CS", name: "Playtech Casino", checked: true },
    { code: "FW", name: "Fishing World", checked: true },
    { code: "MGC", name: "Microgaming - Live Casino", checked: true },
    { code: "AB", name: "Allbet", checked: true }
];

const platformFeeSettings = [
    {
        productList: [...productList],
        commission: 0.17
    }
];

export default class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        this.state = {
            platformFeeSettings: platformFeeSettings
        }
    }

    handleAdd = () => {
        const platformFeeSettings = this.state.platformFeeSettings;
        const newProductList = productList.map(item => {
            return Object.assign({}, item, { checked: !item.checked });
        });

        this.setState({
            platformFeeSettings: [...platformFeeSettings, { productList: newProductList, commission: 0.17 }]
        });
    }

    handleRemove = (index) => {
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);

        const rootProductList = platformFeeSettings[0].productList;
        const checkedProductList = platformFeeSettings[index].productList.filter(p => p.checked);

        for (let i = 0; i < checkedProductList.length; i++) {
            const code = checkedProductList[i].code;
            const rootProductItem = rootProductList.find(p => p.code === code);

            if (rootProductItem) {
                rootProductItem.checked = true;
            }
        }

        platformFeeSettings.splice(index, 1);

        this.setState({ platformFeeSettings });
    }

    handleSelect = (index, selectedItem, newDataSource) => {
        const isChecked = selectedItem.checked;
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);
        const settingCount = platformFeeSettings.length;

        if (settingCount > 1) {
            const currentproductList = platformFeeSettings[index].productList;
            const currentProductItem = currentproductList.find(p => p.code === selectedItem.code);
            currentProductItem.checked = isChecked;

            if (isChecked) {
                for (let i = 0; i < platformFeeSettings.length; i++) {
                    if (i !== index) {
                        const productList = platformFeeSettings[i].productList;
                        const productItem = productList.find(p => p.code === selectedItem.code && p.checked === isChecked);

                        if (productItem) {
                            productItem.checked = !isChecked;
                            break;
                        }
                    }
                }
            }
            else {
                const productList = platformFeeSettings[0].productList;
                const productItem = productList.find(p => p.code === selectedItem.code);
                productItem.checked = !isChecked;
            }
        }

        this.setState({ platformFeeSettings });
    }

    render() {
        return (
            <fieldset>
                <legend><h4 className="text-bold">Deductions</h4></legend>
                <p className="text-bold">Platform Fee %</p>
                <ul className="platformfee-group">
                    {
                        this.state.platformFeeSettings.map((item, i) => {
                            const isFirst = i === 0;

                            return (
                                <PlatformFeeItem data={item} key={`fee-${i}`}
                                    onSelect={(selectedItem, newDataSource) => this.handleSelect(i, selectedItem, newDataSource)}>
                                    {isFirst ?
                                        (<span><button type="button" className="btn-action" onClick={this.handleAdd}>+</button></span>) :
                                        (<span><button type="button" className="btn-action" onClick={() => this.handleRemove(i)}>-</button></span>)
                                    }
                                </PlatformFeeItem>
                            )
                        })
                    }
                </ul>

                <div className="inline">
                    <span className="label">Operational Fee%</span>
                    <select className="select-percent">
                        <option>0.07</option>
                    </select>
                    <span className="sign">x</span>
                    <span className="unit-name">| Gross Win Loss |</span>
                </div>

                <div className="inline">
                    <span className="label">Banking Fee%</span>
                    <select className="select-percent">
                        <option>0.02</option>
                    </select>
                    <span className="sign">x</span>
                    <span className="unit-name">(Deposit + Withdrawal)</span>
                </div>

                <div className="inline">
                    <span className="label">Adjustment</span>
                    <input type="checkbox" className="checkbox" />
                </div>
            </fieldset>
        )
    }
}