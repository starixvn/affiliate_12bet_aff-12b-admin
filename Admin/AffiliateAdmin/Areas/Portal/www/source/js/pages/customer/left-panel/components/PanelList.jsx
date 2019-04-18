import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody } from 'reactstrap';

import Panel from './Panel';
import { updateAffType, updateClosedStatus, getAffiliateInfo, getOnLineList, toggleLoader, updateStatus } from '../actions';

import { affType, status, resource, message } from '../modules/constants';
import { formatDecimal } from '../../../../modules/numberFormat';
import { resolveClientUrl } from '../../../../modules/utils';

export class PanelList extends React.Component {
    constructor(props) {
        super(props);
        
        this.affTypeDropdown = React.createRef();
        this.affStatusDropdown = React.createRef();

        this.state = {
            onlineInfoList: null,
            isChangingAffType: false,
            isChangingAffStatus: false,
            isChangingCommissionGroup: false
        }
    }

    componentDidMount() {
        this.handleGetOnLineList(this.props.affName);
    }

    async handleGetOnLineList(affName) {
        this.props.toggleLoader(true);

        const data = await this.props.getOnLineList(affName);

        this.setState({ onlineInfoList: data });

        this.props.toggleLoader(false);
    }

    toggleChangingAffType(isChanging) {
        this.setState({ isChangingAffType: isChanging }, () => isChanging && this.affTypeDropdown.current.focus());
    }

    toggleChangingAffStatus(isChanging) {
        this.setState({ isChangingAffStatus: isChanging }, () => isChanging && this.affStatusDropdown.current.focus());
    }

    handleChangeStatus = async (event) => {
        const isConfirmed = window.confirm("Are you sure you want to change status for this affiliate?");
        if (isConfirmed) {
            const status = event.target.value;
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, Status: status});
            const data = await this.props.updateStatus(param);

            if (data.IsSuccessful) {
                window.alert("The status has been updated successfully!");
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(data.Message);
            }
        }
    }

    handleChangeClosedStatus = async () => {
        const { affiliateInfo } = this.props;
        const nextStatus = !affiliateInfo.IsClosed;
        const isConfirmed = confirm('Are you sure you want to ' + (nextStatus ? 'close' : 'open') + ' this affiliate?');
        if (isConfirmed) {
            const param = { AffId: this.props.affiliateInfo.AffId, IsClosed: nextStatus };
            const data = await this.props.updateClosedStatus(param);

            if (data.IsSuccessful) {
                window.alert("The status has been updated successfully!");
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(data.Message);
            }
        }
    }

    handleChangeAffType = async (event) => {
        const affType = event.target.value;
        const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, AffType: affType });
        const data = await this.props.updateAffType(param);

        if (data.IsSuccessful) {
            this.props.getAffiliateInfo(this.props.affName);
            this.toggleChangingAffType(false);
        }
        else {
            window.alert("Occur an error when updating affiliate type!");
        }
    }

    toggleEditingCommissionPopup = (isChanging) => {
        this.setState({ isChangingCommissionGroup: isChanging })
    }

    _renderCommissionPanel() {
        const { isChangingCommissionGroup } = this.state;
        const { affiliateInfo } = this.props;
        const url = resolveClientUrl('GroupSetting/AssignGroup.aspx') + `?affid=${affiliateInfo.AffId}&affName=${affiliateInfo.UserName}`;
        const title = 'Change Commission Group For \"' + affiliateInfo.UserName + '\"';

        return (
            <Panel heading={resource.Commission}>
                <tr>
                    <td>{resource.CommGroup}</td>
                    <td className='text-right'>
                        {
                            affiliateInfo.Closed ? affiliateInfo.CommGroupName : 
                            (<span className='popup' onClick={() => this.toggleEditingCommissionPopup(true)}>{affiliateInfo.CommGroupName}</span>)
                        }

                        <Modal isOpen={isChangingCommissionGroup} backdrop='static' style={{maxWidth: 570}}>
                            <ModalHeader toggle={() => this.toggleEditingCommissionPopup(false)}>
                                <span className="iconpopup"></span>{title}
                            </ModalHeader>
                            <ModalBody>
                                <div className="modal-body-wrapper" style={{height: 283, padding: 0}}>
                                    <iframe src={url} width="100%" height="100%" frameBorder="0"></iframe>
                                </div>
                            </ModalBody>
                        </Modal>
                    </td>
                </tr>
                <tr>
                    <td>{resource.BalanceForward}</td>
                    <td className="r">${formatDecimal(affiliateInfo.BalanceBf)}</td>
                </tr>
            </Panel>
        )
    }

    _renderSettingPanel(isApproved) {
        const { affiliateInfo } = this.props;
        const { isChangingAffType, isChangingAffStatus } = this.state;

        return (
            <Panel heading={resource.Setting}>
                <tr>
                    <td>{resource.AffiliateType}</td>
                    <td className="text-right">
                        <select style={{ display: isChangingAffType ? 'inline-block' : 'none' }} defaultValue={affiliateInfo.AffType}
                            onBlur={this.toggleChangingAffType.bind(this, false)} ref={this.affTypeDropdown}
                            onChange={this.handleChangeAffType}>
                            {
                                Object.keys(affType).map((key, index) => {
                                    return (<option value={key} key={`affinfo-type-${index}`}>{affType[key]}</option>)
                                })
                            }
                        </select>
                        <span className="popup" style={{ display: isChangingAffType ? 'none' : 'block' }}
                            onClick={this.toggleChangingAffType.bind(this, true)}>{affType[affiliateInfo.AffType]}</span>
                    </td>
                </tr>
                <tr>
                    <td>{resource.Status}</td>
                    <td className="text-right">
                        {
                            isApproved ?
                                affiliateInfo.Status :
                                (
                                    <div>
                                        <select style={{ display: isChangingAffStatus ? 'inline-block' : 'none' }} defaultValue={affiliateInfo.Status}
                                            onBlur={this.toggleChangingAffStatus.bind(this, false)}
                                            onChange={this.handleChangeStatus} ref={this.affStatusDropdown}>
                                            <option value="Approved">Approved</option>
                                            <option value="Disapproved">Disapproved</option>
                                            <option value="Pending">Pending</option>
                                        </select>
                                        <span className="popup" style={{ display: isChangingAffStatus ? 'none' : 'block' }}
                                            onClick={this.toggleChangingAffStatus.bind(this, true)}>{affiliateInfo.Status}</span>
                                    </div>
                                )
                        }
                    </td>
                </tr>
                {
                    isApproved &&
                    <tr>
                        <td>{resource.Closed}</td>
                        <td className="text-right">
                            <a className="text_box" onClick={this.handleChangeClosedStatus}>{affiliateInfo.IsClosed ? resource.Yes : resource.No}</a>
                        </td>
                    </tr>
                }
            </Panel>
        )
    }

    _renderOnlineInfoPanel() {
        const { onlineInfoList } = this.state;

        return onlineInfoList && (
            <Panel heading={resource.CurrentOnlineList} style={{ overflow: 'auto', maxHeight: 125 }}>
                <tr>
                    <td width="10" className="text_bold text-center">{resource.Index}</td>
                    <td width="70" className="text_bold text-center">{resource.Ip}</td>
                    <td width="120" className="text_bold text-center">{resource.LastCheckIn}</td>
                </tr>

                {
                    onlineInfoList && onlineInfoList.map((item, index) => {
                        return (
                            <tr key={`onl-info-${index}`}>
                                <td className="text-center">{index + 1}</td>
                                <td className="text-center">
                                    <a target="_blank" href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${item.Ip}&j=1`}>
                                        {item.Ip}
                                    </a>
                                </td>
                                <td className="text-left">{item.ReloadTimeText}</td>
                            </tr>
                        )
                    })
                }
            </Panel>
        )
    }

    render() {
        const { affiliateInfo } = this.props;
        const isApproved = affiliateInfo && affiliateInfo.Status.toLowerCase() === status.Approved;

        return affiliateInfo && (
            <React.Fragment>
                {isApproved && this._renderCommissionPanel()}
                {this._renderSettingPanel(isApproved)}
                {isApproved && this._renderOnlineInfoPanel()}
            </React.Fragment>
        )
    }
}

function mapStateToProps({ affiliateInfo, affName }) {
    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateAffType, updateClosedStatus, getAffiliateInfo, getOnLineList, toggleLoader, updateStatus }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PanelList);