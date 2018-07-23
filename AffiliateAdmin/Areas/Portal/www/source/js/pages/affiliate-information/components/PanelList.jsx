import React from 'react';
import Panel from './Panel';

import { connect } from 'react-redux';

import { affType, status } from '../modules/helper';
import { formatDecimal } from '../../../modules/numberFormat';
import { formatDateTime, parseDateTime } from '../../../modules/utils';

export class PanelList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isChangingAffType: false
        }
    }

    _renderCommissionPanel() {
        const { resource } = window;
        const { Closed, GroupName, BalBroughtForward } = this.props;

        return (
            <Panel heading={resource.comm}>
                <tr>
                    <td>{resource.commGroup}</td>
                    <td className='text-right'>
                        {
                            Closed ? GroupName : (<span className='popup'>{GroupName}</span>)
                        }
                    </td>
                </tr>
                <tr>
                    <td>{resource.balanceForward}</td>
                    <td className="r">${formatDecimal(BalBroughtForward)}</td>
                </tr>
                <tr>
                    <td><a href="../Report/MemberReport.aspx?isaff=1" target="fMain">{resource.reportDetails}</a></td>
                </tr>
            </Panel>
        )
    }

    _renderSettingPanel(isApproved) {
        const { resource } = window;
        const { AffType, Status, Closed } = this.props;
        const { isChangingAffType } = this.state;

        return (
            <Panel heading={resource.setting}>
                <tr>
                    <td>{resource.affiliateType}</td>
                    <td className="text-right">
                        <select style={{display: `${isChangingAffType ? 'inline-block' : 'none'}`}} defaultValue={AffType}>
                            {
                                Object.keys(affType).map((key, index) => {
                                    return (<option value={key} key={`affinfo-type-${index}`}>{affType[key]}</option>)
                                })
                            }
                        </select>
                        <span className="popup">{affType[AffType]}</span>
                    </td>
                </tr>
                <tr>
                    <td>{resource.status}</td>
                    <td className="text-right">
                        {
                            isApproved ?
                                Status :
                                (
                                    <div>
                                        <select style={{display: 'none'}}>
                                            <option value="Approved">Approved</option>
                                            <option value="Disapproved">Disapproved</option>
                                            <option value="Pending">Pending</option>
                                        </select>
                                        <span className="popup">{Status}</span>
                                    </div>
                                )

                        }
                    </td>
                </tr>
                {
                    isApproved &&
                    <tr>
                        <td>{resource.closed}</td>
                        <td className="text-right">
                            <a className="text_box">{Closed ? resource.yes : resource.no}</a>
                        </td>
                    </tr>
                }
            </Panel>
        )
    }

    _renderOnlineInfoPanel() {
        const { OnlineInfoList } = this.props;

        return (
            <Panel heading={resource.currOnlineList} style={{ overflow: 'auto', maxHeight: 110 }}>
                <tr>
                    <td width="10" className="text_bold text-center">#</td>
                    <td width="70" className="text_bold text-center">{resource.ip}</td>
                    <td width="120" className="text_bold text-center">{resource.lastCheckIn}</td>
                </tr>

                {
                    OnlineInfoList && OnlineInfoList.map((item, index) => {
                        return (
                            <tr key={`onl-info-${index}`}>
                                <td className="text-center">{index + 1}</td>
                                <td className="text-center">
                                    <a target="_blank" href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${item.Ip}&j=1`}>
                                        {item.Ip}
                                    </a>
                                </td>
                                <td className="text-left">{formatDateTime(parseDateTime(item.ReloadTime), 'MM/dd/yyyy HH:mm:ss')}</td>
                            </tr>
                        )
                    })
                }
            </Panel>
        )
    }

    render() {
        const { Status } = this.props;
        const isApproved = Status.toLowerCase() === status.Approved;

        return (
            <React.Fragment>
                {isApproved && this._renderCommissionPanel()}
                {this._renderSettingPanel(isApproved)}
                {isApproved && this._renderOnlineInfoPanel()}
            </React.Fragment>
        )
    }
}

function mapStateToProps(state) {
    const { GeneralInfo, OnlineInfoList } = state.leftMenuData;

    const { GroupName, BalBroughtForward, AffType, Status, Closed } = GeneralInfo;

    return { GroupName, BalBroughtForward, AffType, Status, Closed, OnlineInfoList };
};

export default connect(mapStateToProps)(PanelList);