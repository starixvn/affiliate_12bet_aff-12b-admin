import React, { Component } from "react";

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone, updateObjectInArray } from "../../../../../modules/dataHandler";

export default class PlatformFeeSetting extends Component {
    constructor(props) {
        super(props);

        this.defaultPlatformSetting = this.props.defaultSettingItem;

        this.state = {
            platformFeeSettings: this.props.data
        }
    }

    handleAdd = () => {
        const platformFeeSettings = this.state.platformFeeSettings;
        const { productList, commission } = this.defaultPlatformSetting;
        const newProductList = productList.map(item => {
            return Object.assign({}, item, { Checked: false });
        });

        this.setState({
            platformFeeSettings: [...platformFeeSettings, { productList: newProductList, commission }]
        });
    }

    handleRemove = (index) => {
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);

        const rootProductList = platformFeeSettings[0].productList;
        const checkedProductList = platformFeeSettings[index].productList.filter(p => p.Checked);

        for (let i = 0; i < checkedProductList.length; i++) {
            const productId = checkedProductList[i].ProductId;
            const rootProductItem = rootProductList.find(p => p.ProductId === productId);

            if (rootProductItem) {
                rootProductItem.Checked = true;
            }
        }

        platformFeeSettings.splice(index, 1);

        this.setState({ platformFeeSettings });
    }

    handleSelect = (index, selectedItem) => {
        const isChecked = selectedItem.Checked;
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);
        const settingCount = platformFeeSettings.length;

        if (settingCount > 1) {
            const currentproductList = platformFeeSettings[index].productList;
            const currentProductItem = currentproductList.find(p => p.ProductId === selectedItem.ProductId);
            currentProductItem.Checked = isChecked;

            if (isChecked) {
                for (let i = 0; i < platformFeeSettings.length; i++) {
                    if (i !== index) {
                        const productList = platformFeeSettings[i].productList;
                        const productItem = productList.find(p => p.ProductId === selectedItem.ProductId && p.Checked === isChecked);

                        if (productItem) {
                            productItem.Checked = !isChecked;
                            break;
                        }
                    }
                }
            }
            else {
                const productList = platformFeeSettings[0].productList;
                const productItem = productList.find(p => p.ProductId === selectedItem.ProductId);
                productItem.Checked = !isChecked;
            }
        }

        this.setState({ platformFeeSettings });
        this._callbackToParent(platformFeeSettings);
    }

    handleChangeCommission = (index, value) => {
        const { platformFeeSettings } = this.state;
        const currentSetting = Object.assign({}, platformFeeSettings[index]);
        currentSetting.commission = value;

        const newState = updateObjectInArray(platformFeeSettings, index, currentSetting);
        this.setState({ platformFeeSettings: newState });
        this._callbackToParent(newState);
    }

    _callbackToParent(dataList) {
        const settingList = dataList.map(item => {
            return {
                DeductionId: this.props.deductionId,
                ProductIds: item.productList.filter(p => p.Checked).map(p => p.ProductId).join(','),
                DeductedPercentage: item.commission
            }
        });

        this.props.onSelect(settingList);
    }

    render() {
        return (
            <div className="platformfee-group">
                <p className="font-weight-bold">Platform Fee %</p>
                {
                    this.state.platformFeeSettings.map((item, i) => {
                        const isFirst = i === 0;
                        const handleClick = isFirst ? this.handleAdd : () => this.handleRemove(i);

                        return (
                            <PlatformFeeItem data={item} key={`platformfee-${i}`}
                                onSelectProduct={(selectedItem) => this.handleSelect(i, selectedItem)}
                                onChangeCommission={(value) => this.handleChangeCommission(i, value)}
                                handleClick={handleClick}
                                btnText={isFirst ? '+' : '-'}>
                            </PlatformFeeItem>
                        )
                    })
                }
            </div>
        )
    }
}