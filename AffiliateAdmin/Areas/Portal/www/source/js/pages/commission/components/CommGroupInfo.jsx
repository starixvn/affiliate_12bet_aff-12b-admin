import React, { Component } from "react";

export default class CommGroupInfo extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
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
        )
    }
}