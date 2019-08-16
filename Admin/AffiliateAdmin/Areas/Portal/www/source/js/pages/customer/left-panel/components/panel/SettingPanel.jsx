import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Row, Col, Button } from 'reactstrap';

import { affType, resource, status, messages } from '../../modules/constants';
import { updateAffType, updateClosedStatus, updateStatus, getAffiliateInfo } from '../../actions';
import CommissionGroupPopup from '../../../../common/components/CommissionGroupPopup';

export class SettingPanel extends PureComponent {
    constructor(props) {
        super(props);

        this.affTypeDropdown = React.createRef();
        this.affStatusDropdown = React.createRef();

        this.state = {
            isChangingAffType: false,
            isChangingAffStatus: false,
            isAssignCommissionGroup: false
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

    handleChangeStatus = async (event) => {
        const isConfirmed = window.confirm(messages.areYouChangeStatus);
        if (isConfirmed) {
            const selectedStatus = event.target.value;
            if (selectedStatus && selectedStatus.toLowerCase() === status.Approved) {
                this.setState({ isAssignCommissionGroup: true });
            } else {
                const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, Status: selectedStatus });
                const result = await this.props.updateStatus(param);

                if (result.IsSuccessful) {
                    window.alert(messages.statusUpdatedSuccess);
                    this.props.getAffiliateInfo(this.props.affName);
                }
                else {
                    window.alert(result.Message);
                }
            }
        }
    }

    handleChangeClosedStatus = async () => {
        const { affiliateInfo } = this.props;
        const nextStatus = !affiliateInfo.IsClosed;
        const isConfirmed = confirm('Are you sure you want to ' + (nextStatus ? 'close' : 'open') + ' this affiliate?');
        if (isConfirmed) {
            const param = { AffId: this.props.affiliateInfo.AffId, IsClosed: nextStatus };
            const result = await this.props.updateClosedStatus(param);

            if (result.IsSuccessful) {
                window.alert(messages.statusUpdatedSuccess);
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(result.Message);
            }
        }
    }

    handleChangeAffType = async (event) => {
        const affType = event.target.value;
        const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, AffType: affType });
        const result = await this.props.updateAffType(param);

        if (result.IsSuccessful) {
            this.props.getAffiliateInfo(this.props.affName);
            this.toggleChangingAffType(false);
        }
        else {
            window.alert(messages.affTypeUpdatedFail);
        }
    }

    render() {
        const { affiliateInfo, isApproved } = this.props;
        const { isChangingAffType, isChangingAffStatus, isAssignCommissionGroup } = this.state;

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
                <Row>
                    <Col>{resource.Status}</Col>
                    <Col className="text-right">
                        {
                            isApproved ?
                                affiliateInfo.Status :
                                (
                                    <div className="text-primary">
                                        <select style={{ display: isChangingAffStatus ? 'inline-block' : 'none' }} value={affiliateInfo.Status}
                                            onChange={this.handleChangeStatus}
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
                {
                    isApproved && (
                        <Row>
                            <Col>{resource.Closed}</Col>
                            <Col className="text-right text-primary">
                                <Button color="link" className="left-panel-btn-link btn-closed" onClick={this.handleChangeClosedStatus}>
                                    {affiliateInfo.IsClosed ? resource.Yes : resource.No}
                                </Button>
                            </Col>
                        </Row>)
                }
            </Panel>
        );
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo, affName } = leftPanel;

    return Object.assign({}, { affiliateInfo, affName });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateAffType, updateClosedStatus, updateStatus }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SettingPanel);