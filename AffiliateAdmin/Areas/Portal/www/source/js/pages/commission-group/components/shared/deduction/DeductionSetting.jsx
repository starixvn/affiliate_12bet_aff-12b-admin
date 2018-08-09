import React, { Component } from "react";

import PlatformFeeSetting from './PlatformFeeSetting';
import { deductionNameList } from '../../../modules/constant';
import FeeSettingItem from './FeeSettingItem';

export default class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        const { PlatformFeeList, RemainingFeeList } = this.props.data;

        const deductionList = this._convertDeductionList();

        this.platformFeeSetting = deductionList[deductionNameList.platformFee];
        this.operationalFeeSetting = deductionList[deductionNameList.operationalFee];
        this.bankingFeeSetting = deductionList[deductionNameList.bankingFee];

        const operationalFee = RemainingFeeList.filter(item => item.DeductionId === this.operationalFeeSetting.DeductionId);
        this.operationalFee = operationalFee.length ? operationalFee[0] : {
            DeductionId: this.operationalFeeSetting.DeductionId,
            ProductIds: 0,
            DeductedPercentage: this.operationalFeeSetting.DeductedPercentage
        };

        const bankingFee = RemainingFeeList.filter(item => item.DeductionId === this.bankingFeeSetting.DeductionId);
        this.bankingFee = bankingFee.length ? bankingFee[0] : {
            DeductionId: this.bankingFeeSetting.DeductionId,
            ProductIds: 0,
            DeductedPercentage: this.bankingFeeSetting.DeductedPercentage
        };

        const productList = this._convertProductList();
        this.defaultPlatformSetting = {
            productList: productList,
            commission: this.platformFeeSetting.DeductedPercentage
        };

        this.platformFeeList = PlatformFeeList.length ? PlatformFeeList : [{
            DeductionId: this.platformFeeSetting.DeductionId,
            ProductIds: productList.map(p => p.ProductId).join(','),
            DeductedPercentage: this.platformFeeSetting.DeductedPercentage
        }];

        this._callbackToParent();
    }

    _convertDeductionList() {
        const deductions = {};
        for (let i = 0; i < this.props.deductionList.length; i++) {
            const item = this.props.deductionList[i];
            deductions[item.DeductionName] = item;
        }

        return deductions;
    }

    _convertProductList(productIds) {
        const { productList } = this.props;
        const idList = productIds ? productIds.split(',') : '';
        const isCheckAll = !productIds || idList.length === productList.length;

        return productList.map(item => {
            return {
                ProductId: item.ProductId,
                ProductName: item.ProductName,
                Checked: isCheckAll || idList.filter(i => i === item.ProductId).length > 0
            }
        })
    }

    _callbackToParent() {
        const remainingFeeList = [this.operationalFee, this.bankingFee];

        const setting = Object.assign({},
            { PlatformFeeList: this.platformFeeList },
            { RemainingFeeList: remainingFeeList });

        this.props.onSelect(setting);
    }

    handleSelectPlatformfee = (platformFeeSettingList) => {
        this.platformFeeList = platformFeeSettingList;
        this._callbackToParent();
    }

    handleSelectOperationalFee = (event) => {
        this.operationalFee = Object.assign({}, this.operationalFee, { DeductedPercentage: Number(event.target.value) });
        this._callbackToParent();
    }

    handleSelectBankingFee = (event) => {
        this.bankingFee = Object.assign({}, this.bankingFee, { DeductedPercentage: Number(event.target.value) });
        this._callbackToParent();
    }

    render() {
        const platformFeeData = this.platformFeeList.map(item => {
            return {
                productList: this._convertProductList(item.ProductIds),
                commission: item.DeductedPercentage
            }
        })

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <PlatformFeeSetting onSelect={this.handleSelectPlatformfee}
                    deductionId={this.platformFeeSetting.DeductionId}
                    defaultSettingItem={this.defaultPlatformSetting}
                    data={platformFeeData} />

                <FeeSettingItem label="Operational Fee%" selectedComm={this.operationalFeeSetting.DeductedPercentage}
                    basedValueName="| Gross Win Loss |" onChange={this.handleSelectOperationalFee}
                    data={this.operationalFee} />

                <FeeSettingItem label="Banking Fee%" selectedComm={this.bankingFeeSetting.DeductedPercentage}
                    basedValueName="(Deposit + Withdrawal)" onChange={this.handleSelectBankingFee}
                    data={this.bankingFee} />
            </fieldset>
        )
    }
}