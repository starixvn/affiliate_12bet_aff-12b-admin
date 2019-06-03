import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Panel from '../Panel';
import { Row, Col, Button } from 'reactstrap';

import { affType, resource } from '../../modules/constants';
import { updateAffType, updateClosedStatus, updateStatus, getAffiliateInfo } from '../../actions';
import CustomSelectInput from '../../../../common/components/CustomSelectInput';

export class SettingPanel extends PureComponent {
    constructor(props) {
        super(props);

        this.affTypeDropdown = React.createRef();
        this.affStatusDropdown = React.createRef();

        this.state = {
            isChangingAffType: false,
            isChangingAffStatus: false
        }
    }

    toggleChangingAffType(isChanging) {
        this.setState({ isChangingAffType: isChanging }, () => isChanging && this.affTypeDropdown.current.focusSelectInput());
    }

    toggleChangingAffStatus(isChanging) {
        this.setState({ isChangingAffStatus: isChanging }, () => isChanging && this.affStatusDropdown.current.focusSelectInput());
    }

    handleChangeStatus = async (event) => {
        const isConfirmed = window.confirm("Are you sure you want to change status for this affiliate?");
        if (isConfirmed) {
            const status = event.target.value;
            const param = Object.assign({}, { AffId: this.props.affiliateInfo.AffId, Status: status });
            const result = await this.props.updateStatus(param);

            if (result.IsSuccessful) {
                window.alert("The status has been updated successfully!");
                this.props.getAffiliateInfo(this.props.affName);
            }
            else {
                window.alert(result.Message);
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
                window.alert("The status has been updated successfully!");
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
            window.alert("Occur an error when updating affiliate type!");
        }
    }

    render() {
        const { affiliateInfo, isApproved } = this.props;
        const { isChangingAffType, isChangingAffStatus } = this.state;

        return (
            <Panel heading={resource.Setting}>
                <Row>
                    <Col>{resource.AffiliateType}</Col>
                    <Col className="text-right text-primary">
                        <CustomSelectInput style={{ display: isChangingAffType ? 'inline-block' : 'none' }} value={affiliateInfo.AffType}
                            onChange={this.handleChangeAffType} onBlur={this.toggleChangingAffType.bind(this, false)} ref={this.affTypeDropdown}>
                            {
                                Object.keys(affType).map((key, index) => {
                                    return (<option value={key} key={`affinfo-type-${index}`}>{affType[key]}</option>)
                                })
                            }
                        </CustomSelectInput>
                        <Button color="link p-0 left-panel-btn-link" style={{ display: isChangingAffType ? 'none' : 'inline-block' }} onClick={this.toggleChangingAffType.bind(this, true)}>
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
                                        <CustomSelectInput style={{ display: isChangingAffStatus ? 'inline-block' : 'none' }} value={affiliateInfo.Status}
                                            onChange={this.handleChangeStatus} onBlur={this.toggleChangingAffStatus.bind(this, false)} ref={this.affStatusDropdown}>
                                            <option value="Approved">Approved</option>
                                            <option value="Disapproved">Disapproved</option>
                                            <option value="Pending">Pending</option>
                                        </CustomSelectInput>
                                        <Button color="link p-0" style={{ display: isChangingAffStatus ? 'none' : 'inline-block' }} onClick={this.toggleChangingAffStatus.bind(this, true)}>
                                            {affiliateInfo.Status}
                                        </Button>
                                    </div>
                                )
                        }
                    </Col>
                </Row>
                {
                    isApproved && (
                        <Row>
                            <Col>{resource.Closed}</Col>
                            <Col className="text-right text-primary">
                                <Button color="link" className="border border-secondary left-panel-btn-link btn-closed" onClick={this.handleChangeClosedStatus}>
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