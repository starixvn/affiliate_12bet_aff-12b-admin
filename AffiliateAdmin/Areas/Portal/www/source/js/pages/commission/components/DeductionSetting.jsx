import React, { Component } from "react";

export default class DeductionSetting extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <fieldset>
                <legend><h4 className="text-bold">Deductions</h4></legend>
                <p className="text-bold">Platform Fee %</p>
                <ul className="platformfee-group">
                    <li className="inline">
                        <select className="select-product">
                            <option>1</option>
                        </select>
                        <select className="select-percent">
                            <option>0.17</option>
                        </select>
                        <span className="sign">x</span>
                        <span className="unit-name">|Gross Win Loss|</span>
                        <span><button type="button" className="btn-action">+</button></span>
                    </li>
                    <li className="inline">
                        <select className="select-product">
                            <option>1</option>
                        </select>
                        <select className="select-percent">
                            <option>0.17</option>
                        </select>
                        <span className="sign">x</span>
                        <span className="unit-name">|Gross Win Loss|</span>
                        <span><button type="button" className="btn-action">-</button></span>
                    </li>
                </ul>

                <div className="inline">
                    <span className="label">Operational Fee%</span>
                    <select className="select-percent">
                        <option>0.07</option>
                    </select>
                    <span className="sign">x</span>
                    <span className="unit-name">|Gross Win Loss|</span>
                </div>

                <div className="inline">
                    <span className="label">Banking Fee%</span>
                    <select className="select-percent">
                        <option>0.02</option>
                    </select>
                    <span className="sign">x</span>
                    <span className="unit-name">(Deposit + Withdrawal)</span>
                </div>

                <div className="inline">
                    <span className="label">Adjustment</span>
                    <input type="checkbox" className="checkbox" />
                </div>
            </fieldset>
        )
    }
}