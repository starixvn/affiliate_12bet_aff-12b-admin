import React from "react";

const CommGroupInfo = (props) => {
    return (
        <table style={{ tableLayout: "fixed" }}>
            <tbody>
                <tr className="row-form">
                    <td width="90">Group Name: </td>
                    <td><input type="text" className="input-groupname" /> <span className="text-danger">*</span></td>
                </tr>
                <tr className="row-form">
                    <td>Description: </td>
                    <td><input type="text" className="input-desc" /></td>
                </tr>
                <tr className="row-form">
                    <td>Active All: </td>
                    <td><input type="checkbox" /></td>
                </tr>
            </tbody>
        </table>
    )
}

export default CommGroupInfo;