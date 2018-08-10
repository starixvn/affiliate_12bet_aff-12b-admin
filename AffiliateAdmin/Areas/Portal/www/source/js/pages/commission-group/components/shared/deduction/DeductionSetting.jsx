import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeSetting from './PlatformFeeSetting';
import FeeSettingItem from './FeeSettingItem';
import { getDeductionSetting, convertRemainingDeduction } from '../../../modules/service';

export class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        const { deductionList, selectedGroup } = this.props;
        const deductionSetting = getDeductionSetting(deductionList);
        
        const remainingFee = convertRemainingDeduction([selectedGroup.OperationalFee, selectedGroup.BankingFee], deductionSetting);

        this.operationalFeeSetting = remainingFee.OperationalFee;
        this.bankingFeeSetting = remainingFee.BankingFee;
    }

    handleSelectOperationalFee = (event) => {
        this.props.setSelectedGroup({ 
            OperationalFee: { ...this.operationalFeeSetting, DeductedPercentage: Number(event.target.value) } 
        });
    }

    handleSelectBankingFee = (event) => {
        this.props.setSelectedGroup({ 
            BankingFee: { ...this.bankingFeeSetting, DeductedPercentage: Number(event.target.value) } 
        });
    }

    render() {
        const { OperationalFee, BankingFee } = this.props.selectedGroup;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <PlatformFeeSetting />

                <FeeSettingItem label="Operational Fee%" basedValueName="| Gross Win Loss |"
                    onChange={this.handleSelectOperationalFee}
                    data={OperationalFee} />

                <FeeSettingItem label="Banking Fee%" basedValueName="(Deposit + Withdrawal)"
                    onChange={this.handleSelectBankingFee}
                    data={BankingFee} />
            </fieldset>
        )
    }
}

function mapStateToProp({ selectedGroup, deductionList }) {
    return { selectedGroup, deductionList };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedGroup }, dispatch);
}

export default connect(mapStateToProp, mapDispatchToProps)(DeductionSetting);