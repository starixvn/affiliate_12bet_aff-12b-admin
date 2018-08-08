import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone, updateObjectInArray } from "../../../modules/dataHandler";

export default class PlatformFeeSetting extends Component {
    constructor(props) {
        super(props);

        this.defaultPlatformSetting = this.props.defaultSettingItem;
        this.state = {
            platformFeeSettings: [this.defaultPlatformSetting]
        }
    }

    handleAdd = () => {
        const platformFeeSettings = this.state.platformFeeSettings;
        const { productList, commission } = this.defaultPlatformSetting;
        const newProductList = productList.map(item => {
            return Object.assign({}, item, { checked: false });
        });

        this.setState({
            platformFeeSettings: [...platformFeeSettings, { productList: newProductList, commission }]
        });
    }

    handleRemove = (index) => {
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);

        const rootProductList = platformFeeSettings[0].productList;
        const checkedProductList = platformFeeSettings[index].productList.filter(p => p.checked);

        for (let i = 0; i < checkedProductList.length; i++) {
            const productId = checkedProductList[i].productId;
            const rootProductItem = rootProductList.find(p => p.productId === productId);

            if (rootProductItem) {
                rootProductItem.checked = true;
            }
        }

        platformFeeSettings.splice(index, 1);

        this.setState({ platformFeeSettings });
    }

    handleSelect = (index, selectedItem) => {
        const isChecked = selectedItem.checked;
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);
        const settingCount = platformFeeSettings.length;

        if (settingCount > 1) {
            const currentproductList = platformFeeSettings[index].productList;
            const currentProductItem = currentproductList.find(p => p.productId === selectedItem.productId);
            currentProductItem.checked = isChecked;

            if (isChecked) {
                for (let i = 0; i < platformFeeSettings.length; i++) {
                    if (i !== index) {
                        const productList = platformFeeSettings[i].productList;
                        const productItem = productList.find(p => p.productId === selectedItem.productId && p.checked === isChecked);

                        if (productItem) {
                            productItem.checked = !isChecked;
                            break;
                        }
                    }
                }
            }
            else {
                const productList = platformFeeSettings[0].productList;
                const productItem = productList.find(p => p.productId === selectedItem.productId);
                productItem.checked = !isChecked;
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
                deductionId: this.props.deductionId,
                productIds: item.productList.filter(p => p.checked).map(p => p.productId).join(','),
                deductedPercentage: item.commission
            }
        });

        this.props.onSelect(settingList);
    }

    render() {
        return (
            <div className="platformfee-group">
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