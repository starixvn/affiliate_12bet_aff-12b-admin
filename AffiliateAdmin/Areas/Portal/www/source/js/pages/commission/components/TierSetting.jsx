import React, { Component } from "react";

export default class TierSetting extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <fieldset>
                <legend><h4 className="text-bold">Tier Setting</h4></legend>
                <table className="table table-bordered tier-setting-table" style={{ tableLayout: "fixed" }}>
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
                            <td><input type="text" className="input-tier" value="0" /></td>
                            <td><input type="text" className="input-tier max" value="0" /><input className="chk-infinity" type="checkbox" />∞</td>
                            <td><input type="text" className="input-tier" value="0" /></td>
                            <td><input type="text" className="input-tier max" value="5" /><input className="chk-infinity" type="checkbox" disabled />∞</td>
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
        )
    }
}