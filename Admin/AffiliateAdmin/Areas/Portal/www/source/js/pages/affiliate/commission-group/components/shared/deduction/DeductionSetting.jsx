import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeSetting from './PlatformFeeSetting';
import FeeSettingItem from './FeeSettingItem';
import { FormGroup, Label, Input, Row, Col } from 'reactstrap';

export class DeductionSetting extends Component {
    constructor(props) {
        super(props);
    }

    handleSelectOperationalFee = (value) => {
        const { OperationalFee } = this.props.selectedGroup;
        this.props.setSelectedGroup({
            OperationalFee: Object.assign({}, OperationalFee, value)
        });
    }

    handleSelectBankingFee = (value) => {
        const { BankingFee } = this.props.selectedGroup;
        this.props.setSelectedGroup({
            BankingFee: Object.assign({}, BankingFee, value)
        });
    }

    handleApplyBonus = (field, value) => {
        this.props.setSelectedGroup({
            [field]: value
        });
    }

    render() {
        const { OperationalFee, BankingFee, GroupId, IsApplyBonus, NextIsApplyBonus } = this.props.selectedGroup;
        const isCreating = GroupId === 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <PlatformFeeSetting />

                <FeeSettingItem label="Operational Fee%" basedValueName="| Gross Win Loss |"
                    onChangeCommission={this.handleSelectOperationalFee}
                    data={OperationalFee}
                    isCreating={isCreating} />

                <FeeSettingItem label="Banking Fee%" basedValueName="(Deposit + Withdrawal)"
                    onChangeCommission={this.handleSelectBankingFee}
                    data={BankingFee}
                    isCreating={isCreating} />
                {
                    isCreating ? (
                        <FormGroup row className="fee-group">
                            <Col sm={4}>
                                <Row className="align-items-baseline">
                                    <Label sm={6} className="font-weight-bold">Bonus</Label>
                                    <FormGroup check className="ml-3">
                                        <Label check>
                                            <Input name="IsApplyBonus" type="radio" className="mr-2" checked={IsApplyBonus} onChange={() => this.handleApplyBonus("IsApplyBonus", true)} />
                                            Include
                                        </Label>
                                    </FormGroup>
                                </Row>
                            </Col>
                            <Col sm={8}>
                                <FormGroup check>
                                    <Label check>
                                        <Input name="IsApplyBonus" type="radio" className="mr-2" checked={!IsApplyBonus} onChange={() => this.handleApplyBonus("IsApplyBonus", false)} />
                                        Exclude
                                    </Label>
                                </FormGroup>
                            </Col>
                        </FormGroup>
                    ) : (
                         <FormGroup row className="fee-group">
                            <Label sm={4} className="font-weight-bold">Bonus</Label>
                            <Col sm={8}>
                                <Row className="align-items-baseline">
                                    <FormGroup className="mb-2 mb-sm-0">
                                        <Label style={{marginLeft: -3}}>Current: {IsApplyBonus ? "Include" : "Exclude"} | Next Month</Label>
                                    </FormGroup>
                                    <FormGroup check className="mx-3">
                                        <Label check>
                                                <Input name="NextIsApplyBonus" type="radio" className="mr-2" checked={!!NextIsApplyBonus} onChange={() => this.handleApplyBonus("NextIsApplyBonus", true)} />
                                            Include
                                        </Label>
                                    </FormGroup>
                                    <FormGroup check>
                                        <Label check>
                                            <Input name="NextIsApplyBonus" type="radio" className="mr-2" checked={!NextIsApplyBonus} onChange={() => this.handleApplyBonus("NextIsApplyBonus", false)} />
                                            Exclude
                                        </Label>
                                    </FormGroup>
                                </Row>
                            </Col>
                        </FormGroup>
                    )
                }
            </fieldset>
        )
    }
}

function mapStateToProp({ selectedGroup }) {
    return { selectedGroup };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(DeductionSetting);