import React, { Component } from "react";

import DeductionSetting from './DeductionSetting';
import QmTurnOverSetting from './QmTurnOverSetting';
import TierSetting from './TierSetting';
import CommGroupInfo from './CommGroupInfo';

export default class PopupContent extends Component {
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

    handleChangeInputTier = (index, field, value) => {

    }

    handleSave = () => {
        const param = Object.assign({},
            this.commGroupInfo,
            this.deductionSettings,
            { qmTurnover: this.qmTurnover },
            this.tierSetting);

        console.log(param);
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