import React from 'react';
import Panel from './Panel';

const PanelList = (props) => {
    return (
        <React.Fragment>
            <Panel heading={"Commission"}>
                <tr>
                    <td>Comm Group</td>
                    <td className='r'>
                        <span className='popup'>Default</span>
                    </td>
                </tr>
                <tr>
                    <td>Balance Brought Forward</td>
                    <td className="r">$9,999,798.45</td>
                </tr>
                <tr>
                    <td><a href="../Report/MemberReport.aspx?isaff=1" target="fMain">Report Details</a></td>
                </tr>
            </Panel>

            <Panel heading={"Setting"}>
                <tr>
                    <td>Affiliate Type</td>
                    <td className="r">
                        <select style={{ display: 'none' }}>
                            <option value="2" selected>Asian</option>
                            <option value="3">Media</option>
                            <option value="4">European</option>
                        </select>
                        <span className="popup">Asian</span>
                    </td>
                </tr>
                <tr>
                    <td>Status</td>
                    <td className="r">
                        <select style={{ display: 'none' }}>
                            <option value="Approved">Approved</option>
                            <option value="Disapproved">Disapproved</option>
                            <option value="Pending" selected>Pending</option>
                        </select>
                        <span className="popup">Approved</span>
                    </td>
                </tr>
                <tr>
                    <td>Closed</td>
                    <td className="r">
                        <a className="text_box">No</a>
                    </td>
                </tr>
            </Panel>

            <Panel heading={"Current Online List"}>
                <tr>
                    <td width="10" className="text_bold c">#</td>
                    <td width="70" className="text_bold c">Ip</td>
                    <td width="120" className="text_bold c">Last Check In</td>
                </tr>
                <tr>
                    <td className="c">1</td>
                    <td className="c">
                        <a target="_blank" href="http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&amp;token=&amp;toolhandler_redirect=0&amp;ip=10.21.10.33&amp;j=1">10.21.10.33</a>
                    </td>
                    <td className="c">02/13/2017 06:36:00</td>
                </tr>
            </Panel>
        </React.Fragment>
    )
}

export default PanelList;