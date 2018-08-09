import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import PlatformFeeSetting from './PlatformFeeSetting';
import { deductionNameList } from '../../../modules/constant';
import FeeSettingItem from './FeeSettingItem';

export class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        const deductionList = this._convertDeductionList();

        this.platformFeeDeductionSetting = deductionList[deductionNameList.platformFee];
        this.operationalFeeDeductionSetting = deductionList[deductionNameList.operationalFee];
        this.bankingFeeFeeDeductionSetting = deductionList[deductionNameList.bankingFee];

        this.operationalFee = {
            deductionId: this.operationalFeeDeductionSetting.DeductionId,
            productIds: 0,
            deductedPercentage: this.operationalFeeDeductionSetting.DeductedPercentage
        };
        this.bankingFee = {
            deductionId: this.bankingFeeFeeDeductionSetting.DeductionId,
            productIds: 0,
            deductedPercentage: this.bankingFeeFeeDeductionSetting.DeductedPercentage
        };

        const productList = this._convertToPlatformSettingList();
        this.defaultPlatformSetting = {
            productList: productList,
            commission: this.platformFeeDeductionSetting.DeductedPercentage
        };
        this.platformFeeList = [{
            deductionId: this.platformFeeDeductionSetting.DeductionId,
            productIds: productList.map(p => p.productId).join(','),
            deductedPercentage: this.platformFeeDeductionSetting.DeductedPercentage
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

    _convertToPlatformSettingList() {
        const { productList } = this.props;

        return productList.map(item => {
            return {
                productId: item.ProductId,
                productName: item.ProductName,
                checked: true
            }
        })
    }

    _callbackToParent() {
        const remainingFeeList = [];
        this.operationalFee && remainingFeeList.push(this.operationalFee);
        this.bankingFee && remainingFeeList.push(this.bankingFee);

        const setting = Object.assign({},
            { platformFeeList: this.platformFeeList },
            { remainingFeeList });

        this.props.onSelect(setting);
    }

    handleSelectPlatformfee = (platformFeeSettingList) => {
        this.platformFeeList = platformFeeSettingList;
        this._callbackToParent();
    }

    handleSelectOperationalFee = (event) => {
        this.operationalFee = Object.assign({}, this.operationalFee, { deductedPercentage: Number(event.target.value) });
        this._callbackToParent();
    }

    handleSelectBankingFee = (event) => {
        this.bankingFee = Object.assign({}, this.bankingFee, { deductedPercentage: Number(event.target.value) });
        this._callbackToParent();
    }

    render() {
        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <PlatformFeeSetting onSelect={this.handleSelectPlatformfee}
                    deductionId={this.platformFeeDeductionSetting.DeductionId}
                    defaultSettingItem={this.defaultPlatformSetting} />

                <FeeSettingItem label="Operational Fee%" selectedComm={this.operationalFeeDeductionSetting.DeductedPercentage}
                   basedValueName="| Gross Win Loss |" onChange={this.handleSelectOperationalFee} />

                <FeeSettingItem label="Banking Fee%" selectedComm={this.bankingFeeFeeDeductionSetting.DeductedPercentage}
                   basedValueName="(Deposit + Withdrawal)" onChange={this.handleSelectBankingFee} />
            </fieldset>
        )
    }
}

function mapStateToProps({ productList }) {
    return Object.assign({}, { productList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({}, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DeductionSetting);