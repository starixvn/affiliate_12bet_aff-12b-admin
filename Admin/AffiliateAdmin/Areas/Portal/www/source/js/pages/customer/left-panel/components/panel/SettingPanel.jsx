import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Row, Col, Button } from 'reactstrap';

import { affType, resource, status, messages } from '../../libs/constants';
import { updateAffType, updateClosedStatus, updateStatus, getAffiliateInfo } from '../../actions';
import CommissionGroupPopup from '../../../../common/components/CommissionGroupPopup';
import { NotificationPopup } from '../../../../common/components/NotificationPopup';
import { claimResource } from '../../../../../modules/claim';

export class SettingPanel extends PureComponent {
    constructor(props) {
        super(props);

        this.affTypeDropdown = React.createRef();
        this.affStatusDropdown = React.createRef();

        this.state = {
            isChangingAffType: false,
            isChangingAffStatus: false,
            isAssignCommissionGroup: false,
            selectedStatus: '',
            isClosed: false,
            messageNotification: '',
            isConfirmPopup: true,
            isConfirmStatus: true
        }
    }

    toggleChangingAffType = (isChanging) => {
        this.setState({ isChangingAffType: isChanging }, () => isChanging && this.affTypeDropdown.current.focus());
    }

    toggleChangingAffStatus = (isChanging) => {
        this.setState({ isChangingAffStatus: isChanging }, () => isChanging && this.affStatusDropdown.current.focus());
    }

    toggleAssignCommissionPopup = (isChanging) => {
        this.setState({ isAssignCommissionGroup: isChanging })
    }

    handleCloseNotificationPopup = (confirmResult) => {
        this.setState({ messageNotification: '' });

        const { isConfirmStatus, selectedStatus, isClosed } = this.state;
        if (isConfirmStatus && confirmResult && selectedStatus) {
            this._changeStatus(selectedStatus);
        }

        if (!isConfirmStatus && confirmResult) {
            this._openCloseAff(isClosed);
        }
    }

    _handleChangeStatus = (event) => {
        this.setState({ messageNotification: messages.areYouChangeStatus, isConfirmPopup: true, isConfirmStatus: true, selectedStatus: event.target.value });
    }

    _changeStatus = async (selectedStatus) => {
        if (selectedStatus && selectedStatus.toLowerCase() === status.Approved) {
            this.setState({ isAssignCommissionGroup: true });
        } else {
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, Status: selectedStatus });
            const result = await this.props.updateStatus(param);

            if (result && result.IsSuccessful) {
                this.setState({ messageNotification: messages.statusUpdatedSuccess, isConfirmPopup: false });
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                this.setState({ messageNotification: result.Message, isConfirmPopup: false });
            }
        }
    }

    _handleOpenCloseAff = () => {
        const { affiliateInfo, today } = this.props;
        const currentDate = new Date(today).getDate();

        const isClosed = !affiliateInfo.IsClosed;

        //closes an affiliate between the 1st and 5th of the month
        const isSpecialDate = currentDate > 0 && currentDate < 6;
        const specialMessage = isClosed && isSpecialDate ? '<p class="text-danger">Closing an affiliate from 1 to 5 of each month will prevent the affiliate from being commissioned for the previous month.</p>' : '';

        this.setState({ messageNotification: `${specialMessage}Are you sure you want to ${isClosed ? 'close' : 'open'} this affiliate?`, isConfirmPopup: true, isConfirmStatus: false, isClosed });
    }

    _openCloseAff = async (isClosed) => {
        const param = { AffId: this.props.affiliateInfo.AffId, IsClosed: isClosed };
        const result = await this.props.updateClosedStatus(param);

        if (result && result.IsSuccessful) {
            this.setState({ messageNotification: messages.statusUpdatedSuccess, isConfirmPopup: false });
            this.props.getAffiliateInfo(this.props.affName);
        }
        else {
            this.setState({ messageNotification: result.Message, isConfirmPopup: false });
        }
    }

    handleChangeAffType = async (event) => {
        const affType = event.target.value;
        const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, AffType: affType });
        const result = await this.props.updateAffType(param);

        if (result && result.IsSuccessful) {
            this.props.getAffiliateInfo(this.props.affName);
            this.toggleChangingAffType(false);
        }
        else {
            this.setState({ messageNotification: messages.affTypeUpdatedFail, isConfirmPopup: false });
        }
    }

    render() {
        const { affiliateInfo, isApproved, isStatusShowable, isStatusModifiable } = this.props;
        const { isChangingAffType, isChangingAffStatus, isAssignCommissionGroup, messageNotification, isConfirmPopup } = this.state;
        const isReadOnly = isStatusShowable && !isStatusModifiable;
        return (
            <Panel heading={resource.Setting}>
                <Row>
                    <Col>{resource.AffiliateType}</Col>
                    <Col className="text-right text-primary">
                        <select style={{ display: isChangingAffType ? 'inline-block' : 'none' }} value={affiliateInfo.AffType}
                            onChange={this.handleChangeAffType} onBlur={() => this.toggleChangingAffType(false)} ref={this.affTypeDropdown}>
                            {
                                Object.keys(affType).map((key, index) => {
                                    return (<option value={key} key={`affinfo-type-${index}`}>{affType[key]}</option>)
                                })
                            }
                        </select>
                        <Button color="link" className="left-panel-btn-link" style={{ display: isChangingAffType ? 'none' : 'inline-block' }} onClick={() => this.toggleChangingAffType(true)}>
                            {affType[affiliateInfo.AffType]}
                        </Button>
                    </Col>
                </Row>
                { isStatusShowable &&
                    <Row>
                        <Col>{resource.Status}</Col>
                        <Col className="text-right">
                            {
                                isApproved || isReadOnly ?
                                    affiliateInfo.Status :
                                    (
                                        < div className="text-primary">
                                            <select style={{ display: isChangingAffStatus ? 'inline-block' : 'none' }} value={affiliateInfo.Status}
                                                onChange={this._handleChangeStatus}
                                                onBlur={() => this.toggleChangingAffStatus(false)}
                                                ref={this.affStatusDropdown}>
                                                <option value="Approved">Approved</option>
                                                <option value="Disapproved">Disapproved</option>
                                                <option value="Pending">Pending</option>
                                            </select>
                                            <Button color="link" className="left-panel-btn-link" style={{ display: isChangingAffStatus ? 'none' : 'inline-block' }}
                                                onClick={() => this.toggleChangingAffStatus(true)}>
                                                {affiliateInfo.Status}
                                            </Button>
                                        </div>
                                    )
                            }
                            <CommissionGroupPopup
                                showPopupCommGroup={isAssignCommissionGroup}
                                isPrivateGroup={false}
                                isNewAffiliate={true}
                                affName={affiliateInfo.AffName}
                                affId={affiliateInfo.AffId}
                                setShowPopupCommGroup={this.toggleAssignCommissionPopup}
                                getAffiliateInfo={this.props.getAffiliateInfo}
                            />
                        </Col>
                    </Row>
                }
                {
                    isApproved && isStatusShowable && (
                        <Row>
                            <Col>{resource.Closed}</Col>
                            { isStatusModifiable
                                ? (<Col className="text-right text-primary">
                                    <Button color="link" className="left-panel-btn-link btn-closed" onClick={this._handleOpenCloseAff}>
                                        {affiliateInfo.IsClosed ? resource.Yes : resource.No}
                                    </Button>
                                </Col>)
                                : (<Col className="text-right">{affiliateInfo.IsClosed ? resource.Yes : resource.No}</Col>)
                            }
                        </Row>)
                }
                {
                    !!messageNotification && <NotificationPopup
                        message={messageNotification}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this.handleCloseNotificationPopup}
                    />
                }
            </Panel>
        );
    }
}

function mapStateToProps({ leftPanel, claimsPrincipal, today }) {
    const { affiliateInfo, affName } = leftPanel;
    const isStatusModifiable = claimsPrincipal.hasWriteClaim(claimResource.AffAccountStatus);
    const isStatusShowable = claimsPrincipal.hasReadClaim(claimResource.AffAccountStatus);
    return Object.assign({}, { affiliateInfo, affName, isStatusShowable, isStatusModifiable, today });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateAffType, updateClosedStatus, updateStatus }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SettingPanel);