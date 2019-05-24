import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Modal, ModalHeader, ModalBody, Row, Col } from 'reactstrap';

import Panel from './Panel';
import { updateAffType, updateClosedStatus, getOnLineList, toggleLoader, updateStatus } from '../actions';
import { getAffiliateInfo } from '../../mainpage/actions/loadingDataAction';

import { affType, status, resource } from '../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';
import { resolveClientUrl } from '../../../../modules/utils';

export class PanelList extends PureComponent {
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
        window.addEventListener('message', this.handleClosePopupEvent);
        this.handleGetOnLineList(this.props.affName);
    }

    componentWillUnmount() {
        window.removeEventListener('message', this.handleClosePopupEvent);
    }

    handleClosePopupEvent = (event) => {
        if (event.data === 'closePopup') {
            this.toggleEditingCommissionPopup(false);
            this.props.getAffiliateInfo(this.props.affName);
        }
    }

    componentDidUpdate(prevProps) {
        const { affName } = this.props;

        if (affName !== prevProps.affName) {
            this.handleGetOnLineList(affName);
        }
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
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, Status: status });
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
        const url = resolveClientUrl(`Portal/CommissionGroup/AssignGroup?affid=${affiliateInfo.AffId}`);
        const title = `Change Commission Group For "${affiliateInfo.AffName}"`;

        return (
            <Panel heading={resource.Commission}>
                <Row>
                    <Col sm={7}>{resource.CommGroup}</Col>
                    <Col sm={5} className='text-right text-truncate'>
                        {
                            affiliateInfo.IsClosed ? (<span>{affiliateInfo.CommGroupName}</span>) :
                                (<span className='popup text-primary' onClick={() => this.toggleEditingCommissionPopup(true)}>{affiliateInfo.CommGroupName}</span>)
                        }

                        <Modal isOpen={isChangingCommissionGroup} backdrop='static' style={{ maxWidth: 570 }}>
                            <ModalHeader toggle={() => this.toggleEditingCommissionPopup(false)}>
                                <span className="iconpopup"></span>{title}
                            </ModalHeader>
                            <ModalBody>
                                <div className="modal-body-wrapper" style={{ height: 283, padding: 0 }}>
                                    <iframe src={url} width="100%" height="100%" frameBorder="0"></iframe>
                                </div>
                            </ModalBody>
                        </Modal>
                    </Col>
                </Row>
                <Row>
                    <Col sm={7} className="pr-0">{resource.BalanceForward}</Col>
                    <Col sm={5} className="text-right text-break pl-0">{formatDecimalWithCurrencySymbol(affiliateInfo.BalanceBf)}</Col>
                </Row>
            </Panel>
        )
    }

    _renderSettingPanel(isApproved) {
        const { affiliateInfo } = this.props;
        const { isChangingAffType, isChangingAffStatus } = this.state;

        return (
            <Panel heading={resource.Setting}>
                <Row>
                    <Col>{resource.AffiliateType}</Col>
                    <Col className="text-right text-primary">
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
                    </Col>
                </Row>
                <Row>
                    <Col>{resource.Status}</Col>
                    <Col className="text-right">
                        {
                            isApproved ?
                                affiliateInfo.Status :
                                (
                                    <div className="text-primary">
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
                    </Col>
                </Row>
                {
                    isApproved &&
                    <Row>
                        <Col>{resource.Closed}</Col>
                        <Col className="text-right text-primary">
                            <a className="text_box px-1 border border-secondary" onClick={this.handleChangeClosedStatus}>{affiliateInfo.IsClosed ? resource.Yes : resource.No}</a>
                        </Col>
                    </Row>
                }
            </Panel>
        )
    }

    _renderOnlineInfoPanel() {
        const { onlineInfoList } = this.state;

        return onlineInfoList && (
            <Panel heading={resource.CurrentOnlineList} style={{ overflow: 'auto', maxHeight: 125 }}>
                <Row className="mr-0">
                    <Col sm={2} className="text_bold text-center pr-0">{resource.Index}</Col>
                    <Col sm={5} className="text_bold text-center px-0">{resource.Ip}</Col>
                    <Col sm={5} className="text_bold text-center px-0">{resource.LastCheckIn}</Col>
                </Row>

                {
                    onlineInfoList && onlineInfoList.map((item, index) => {
                        return (
                            <Row className="mr-0" key={`onl-info-${index}`}>
                                <Col sm={2} className="text-center pr-0">{index + 1}</Col>
                                <Col sm={5} className="text-center px-0">
                                    <a target="_blank" href={`http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${item.Ip}&j=1`}>
                                        {item.Ip}
                                    </a>
                                </Col>
                                <Col sm={5} className="text-center px-0">{item.ReloadTimeText}</Col>
                            </Row>
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

function mapStateToProps({ affCommonInfo, leftPanel }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;

    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateAffType, updateClosedStatus, getAffiliateInfo, getOnLineList, toggleLoader, updateStatus }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PanelList);