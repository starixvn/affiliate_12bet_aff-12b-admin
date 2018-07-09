import React, { Component } from "react";

export default class QmTurnOverSetting extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <fieldset>
                <legend><h4 className="text-bold">QM Turnover</h4></legend>
                <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
                <p>USD <span><input type="text" value={100} /></span></p>
            </fieldset>
        )
    }
}