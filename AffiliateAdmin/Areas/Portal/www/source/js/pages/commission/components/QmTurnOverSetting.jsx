import React from "react";

const QmTurnOverSetting = (props) => {
    return (
        <fieldset>
            <legend><h4 className="text-bold">QM Turnover</h4></legend>
            <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
            <p>USD <span><input type="text" defaultValue="100" /></span></p>
        </fieldset>
    )
}

export default QmTurnOverSetting;