import React, { Component } from "react";

import DeductionSetting from './DeductionSetting';
import QmTurnOverSetting from './QmTurnOverSetting';
import TierSetting from './TierSetting';
import CommGroupInfo from './CommGroupInfo';

export default class PopupContent extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="comm-popup-container">
                <CommGroupInfo />
                <DeductionSetting />
                <QmTurnOverSetting />
                <TierSetting />

                <button type="button" className="my-2">SAVE</button>
            </div>
        )
    }
}