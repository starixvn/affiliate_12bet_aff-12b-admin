import React, { Component } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, FormGroup, Label, Input } from 'reactstrap';

import PlatformFeeSetting from './PlatformFeeSetting';
import { commissionValueList } from '../modules/constant';

export class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        this.operationalFee = null;
        this.bankingFee = null;
        this.platformFeeList = [];
        this.remainingFeeList = [];
    }

    handleSelectPlatformfee = (platformFeeSettingList) => {
        console.log(platformFeeSettingList);
        this.platformFeeList = platformFeeSettingList;
    }

    handleSelectOperationalFee = (event) => {
        this.operationalFee = {
            deductionId: 2,
            productIds: 0,
            deductedPercentage: Number(event.target.value)
        }

        console.log(this.operationalFee);
    }

    handleSelectBankingFee = (event) => {
        this.bankingFee = {
            deductionId: 3,
            productIds: 0,
            deductedPercentage: Number(event.target.value)
        }
        console.log(this.bankingFee);
    }

    render() {
        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <p className="font-weight-bold">Platform Fee %</p>
                <PlatformFeeSetting onSelect={this.handleSelectPlatformfee} />

                <FormGroup row>
                    <Label sm={4} className="font-weight-bold">Operational Fee%</Label>

                    <Col sm={{ size: 2, offset: 1 }}>
                        <select className="form-control" onChange={this.handleSelectOperationalFee}>
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
                        <select className="form-control" onChange={this.handleSelectBankingFee}>
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