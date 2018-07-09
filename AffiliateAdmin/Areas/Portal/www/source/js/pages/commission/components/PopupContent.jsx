import React, { Component } from "react";

export default class PopupContent extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="comm-popup-container">
                <table style={{ tableLayout: "fixed" }}>
                    <tbody>
                        <tr className="row-form">
                            <td width="90">Group Name: </td>
                            <td><input type="text" /> <span className="text-danger">*</span></td>
                        </tr>
                        <tr className="row-form">
                            <td>Description: </td>
                            <td><input type="text" className="text-desc" /></td>
                        </tr>
                        <tr className="row-form">
                            <td>Active All: </td>
                            <td><input type="checkbox" /></td>
                        </tr>
                    </tbody>
                </table>

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

                <fieldset>
                    <legend><h4 className="text-bold">QM Turnover</h4></legend>
                    <p>A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to</p>
                    <p>USD <span><input type="text" value={100} /></span></p>
                </fieldset>

                <fieldset>
                    <legend><h4 className="text-bold">Tier Setting</h4></legend>
                    <table className="table table-bordered tier-setting-table" style={{tableLayout: "fixed"}}>
                        <thead>
                            <tr>
                                <th width="40">Tier</th>
                                <th>Min AM</th>
                                <th>Max AM</th>
                                <th>Min QM</th>
                                <th>Max QM</th>
                                <th width="100">Commission</th>
                                <th width="50">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td className="text-center">1</td>
                                <td><input type="text" className="input-tier" value="0"/></td>
                                <td><input type="text" className="input-tier max" value="0"/><input className="chk-infinity" type="checkbox"/>∞</td>
                                <td><input type="text" className="input-tier" value="0"/></td>
                                <td><input type="text" className="input-tier max" value="5"/><input className="chk-infinity" type="checkbox" disabled/>∞</td>
                                <td>
                                    <select className="select-comm">
                                        <option>0.00</option>
                                    </select>
                                </td>
                                <td className="text-center"><a href="javascript:;"><span className="icdelete1"></span></a></td>
                            </tr>
                        </tbody>
                    </table>

                    <div>
                        <button type="button" className="btn-addTier">Add Tier</button>
                    </div>
                </fieldset>

                <button type="button">SAVE</button>
            </div>
        )
    }
}