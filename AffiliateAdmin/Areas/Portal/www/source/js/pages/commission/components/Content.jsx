import React, { Component } from "react";
import { connect } from "react-redux";

class Content extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="content-container table-responsive">
                <table className="aff-table table table-bordered" cellSpacing="1" cellPadding="3" width="100%" style={{ tableLayout: "fixed" }}>
                    <thead>
                        <tr>
                            <th rowSpan="2" width="3%"></th>
                            <th rowSpan="2">Name</th>
                            <th rowSpan="2">No.Aff</th>
                            <th rowSpan="2">Description</th>
                            <th rowSpan="2">Creation</th>
                            <th colSpan="11">Policy</th>
                            <th rowSpan="2">Action</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Tier</th>
                            <th title="Minimum all members' winlost amount">Min AM($)</th>
                            <th title="Maximum all members' winlost amount">Max AM($)</th>
                            <th title="Minimum number of qualified members">Min QM</th>
                            <th title="Maximum number of qualified members">Max QM</th>
                            <th>Comm Rate</th>
                            <th>Operational Fee</th>
                            <th>Banking Fee</th>
                            <th>Adjustment</th>
                            <th>Qm Turnover</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td rowSpan="6">1</td>
                            <td rowSpan="6">Default</td>
                            <td rowSpan="6">1,507</td>
                            <td rowSpan="6">Default System Commission Group</td>
                            <td rowSpan="6">pgmDung 12/25/2009 01:10:51</td>
                            <td rowSpan="4">Tier Setting</td>
                            <td>1</td>
                            <td>0</td>
                            <td>0</td>
                            <td>0</td>
                            <td>5</td>
                            <td>0.00</td>
                            <td rowSpan="6">0.07</td>
                            <td rowSpan="6">0.02</td>
                            <td rowSpan="6">Yes</td>
                            <td rowSpan="6">100.00</td>
                            <td rowSpan="6"><a>Add</a> | <a>Delete</a></td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>1</td>
                            <td>2,999</td>
                            <td>6</td>
                            <td>10</td>
                            <td>0.20</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>3,000</td>
                            <td>45,000</td>
                            <td>11</td>
                            <td>50</td>
                            <td>0.30</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>45,001</td>
                            <td>∞</td>
                            <td>51</td>
                            <td>∞</td>
                            <td>0.40</td>
                        </tr>
                        <tr>
                            <td rowSpan="2">Platform Fee</td>
                            <td colSpan="5">Sportsbook, Number Game, Virtual Sports, Keno, ColossusBets, Fishing World, Cashout, Lottery, Exchange, Lb Keno</td>
                            <td>0.17</td>
                        </tr>
                        <tr>
                            <td colSpan="5">Sportsbook, Number Game, Virtual Sports, Keno, ColossusBets, Fishing World, Cashout, Lottery, Exchange, Lb Keno</td>
                            <td>0.12</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
}

export default connect()(Content);