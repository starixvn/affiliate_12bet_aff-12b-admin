import React from 'react';

import GeneralInfo from './GeneralInfo';
import PanelList from './PanelList';

const MainInfo = (props) => {
    return (
        <table cellSpacing='0' cellPadding='0' width='98%' border='0'>
            <tbody>
                <GeneralInfo />
                <tr>
                    <td colSpan='2' style={{ height: 5 }}>
                    </td>
                </tr>
                <tr>
                    <td colSpan='2' style={{ verticalAlign: 'top' }}>
                        <PanelList />
                    </td>
                </tr>
            </tbody>
        </table>
    )
}

export default MainInfo;