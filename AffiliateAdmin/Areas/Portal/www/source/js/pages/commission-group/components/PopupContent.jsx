import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import DeductionSetting from './DeductionSetting';
import QmTurnOverSetting from './QmTurnOverSetting';
import TierSetting from './TierSetting';
import CommGroupInfo from './CommGroupInfo';
import { insertCommissionGroup, setCommGroupAfterInserting } from '../actions';
import { INFINITY_AM, INFINITY_QM } from '../modules/constant';

export class PopupContent extends Component {
    constructor(props) {
        super(props);

        this.commGroupInfo = {};
        this.deductionSettings = {};
        this.qmTurnover = 0;
        this.tierSetting = [];
    }

    handleChangeInput = (name, value) => {
        this.commGroupInfo[name] = value;
    }

    handleSelectDeduction = (deductionSettingList) => {
        this.deductionSettings = deductionSettingList;
    }

    handleChangeInputQmTurnover = (value) => {
        this.qmTurnover = value;
    }

    handleChangeInputTier = (tierSettingList) => {
        this.tierSetting = tierSettingList;
        console.log(this.tierSetting);
    }

    handleSave = async () => {
        const tierSettingList = this.tierSetting.map(item => {
            return {
                minAmount: item.minAmount,
                maxAmount: item.infinityAm ? INFINITY_AM : item.maxAmount,
                minQm: item.minQm,
                maxQm: item.infinityQm ? INFINITY_QM : item.maxQm,
                commission: item.commission
            }
        })

        const param = Object.assign({},
            this.commGroupInfo,
            this.deductionSettings,
            { qmTurnover: this.qmTurnover },
            { tierSettingList });

        console.log(param);
        const response = await this.props.insertCommissionGroup(param);
        const result = response.data;
        console.log(result);
    }

    render() {
        return (
            <div className="comm-popup-container">
                <CommGroupInfo onChange={this.handleChangeInput} />
                <DeductionSetting onSelect={this.handleSelectDeduction} />
                <QmTurnOverSetting onChange={this.handleChangeInputQmTurnover} />
                <TierSetting onChange={this.handleChangeInputTier} />

                <button type="button" className="my-2" onClick={this.handleSave}>SAVE</button>
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertCommissionGroup, setCommGroupAfterInserting }, dispatch);
}

export default connect(null, mapDispatchToProps)(PopupContent);