import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { setSelectedGroup } from '../../../actions';

import PlatformFeeSetting from './PlatformFeeSetting';
import FeeSettingItem from './FeeSettingItem';

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

    render() {
        const { OperationalFee, BankingFee, GroupId } = this.props.selectedGroup;
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