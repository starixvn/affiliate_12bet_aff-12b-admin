import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, FormGroup, Label, Input } from 'reactstrap';

import PlatformFeeSetting from './PlatformFeeSetting';
import { commissionValueList, deductionNameList } from '../modules/constant';
import { formatDecimal } from '../../../modules/numberFormat';

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
                <p className="font-weight-bold">Platform Fee %</p>
                <PlatformFeeSetting onSelect={this.handleSelectPlatformfee}
                    deductionId={this.platformFeeDeductionSetting.DeductionId}
                    defaultSettingItem={this.defaultPlatformSetting} />

                <FormGroup row>
                    <Label sm={4} className="font-weight-bold">Operational Fee%</Label>

                    <Col sm={{ size: 2, offset: 1 }}>
                        <select className="form-control" onChange={this.handleSelectOperationalFee}
                            defaultValue={formatDecimal(this.operationalFeeDeductionSetting.DeductedPercentage)}>
                            {
                                commissionValueList.map((item, i) => {
                                    return (<option key={`operationalfee-comm-${i}`} value={item}>{item}</option>);
                                })
                            }
                        </select>
                    </Col>
                    <Col sm={1}>
                        <Input plaintext>x</Input>
                    </Col>
                    <Col sm={3}>
                        <Input plaintext>| Gross Win Loss |</Input>
                    </Col>
                </FormGroup>

                <FormGroup row>
                    <Label sm={4} className="font-weight-bold">Banking Fee%</Label>

                    <Col sm={{ size: 2, offset: 1 }}>
                        <select className="form-control" onChange={this.handleSelectBankingFee}
                            defaultValue={formatDecimal(this.bankingFeeFeeDeductionSetting.DeductedPercentage)}>
                            {
                                commissionValueList.map((item, i) => {
                                    return (<option key={`bankingfee-comm-${i}`} value={item}>{item}</option>);
                                })
                            }
                        </select>
                    </Col>
                    <Col sm={1}>
                        <Input plaintext>x</Input>
                    </Col>
                    <Col sm={3}>
                        <Input plaintext>(Deposit + Withdrawal)</Input>
                    </Col>
                </FormGroup>
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