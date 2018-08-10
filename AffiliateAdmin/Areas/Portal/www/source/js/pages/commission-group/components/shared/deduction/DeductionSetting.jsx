import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeSetting from './PlatformFeeSetting';
import FeeSettingItem from './FeeSettingItem';
import { getDeductionSetting, createDefaultOperationalSetting, createDefaultBankingSetting } from '../../../modules/service';

export class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        const { deductionList } = this.props;
        const deductionSetting = getDeductionSetting(deductionList);

        this.operationalFeeSetting = createDefaultOperationalSetting(deductionSetting.operationalFeeSetting);
        this.bankingFeeSetting = createDefaultBankingSetting(deductionSetting.bankingFeeSetting);
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
        const { OperationalFee, BankingFee, GroupId } = this.props.selectedGroup;
        const isInserting = GroupId === 0;

        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <PlatformFeeSetting />

                <FeeSettingItem label="Operational Fee%" basedValueName="| Gross Win Loss |"
                    onChange={this.handleSelectOperationalFee}
                    data={OperationalFee}
                    isInserting={isInserting} />

                <FeeSettingItem label="Banking Fee%" basedValueName="(Deposit + Withdrawal)"
                    onChange={this.handleSelectBankingFee}
                    data={BankingFee}
                    isInserting={isInserting} />
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