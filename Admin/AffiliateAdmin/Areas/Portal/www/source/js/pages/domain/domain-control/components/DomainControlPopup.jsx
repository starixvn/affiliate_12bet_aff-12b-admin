import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { headerText, defaultState } from '../libs/constants';
import Select from '../../../../components/select';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getTargetCountryListByDomains } from '../actions/index';

export class DomainControlPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId);
        }
    }

    _handleUpdateState = async (selectedDomainId) => {
        if (selectedDomainId === 0) {
            const { affDomainList, redirectDomainList } = this.props;

            if ((affDomainList && affDomainList.length) && (redirectDomainList && redirectDomainList.length)) {
                const affDomainId = affDomainList[0].DomainId;
                const redirectDomainId = redirectDomainList[0].DomainId;

                const targetCountryListByDomains = await this.props.getTargetCountryListByDomains({ affDomainId, redirectDomainId });
                const targetCountryCode = targetCountryListByDomains ? targetCountryListByDomains[0].TargetCountryCode : "";

                this.setState({ isInsert: true, selectedDomain: { domainControlId: selectedDomainId, affDomainId, redirectDomainId, targetCountryCode, status: false }, targetCountryListByDomains });
            }
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainControlById(selectedDomainId);

            if (domain) {
                const { AffDomainId, RedirectDomainId, TargetCountryCode, Status } = domain;
                const domainIds = { affDomainId: AffDomainId, redirectDomainId: RedirectDomainId };
                const targetCountryListByDomains = await this.props.getTargetCountryListByDomains(domainIds);

                this.setState({ isInsert: false, selectedDomain: { domainControlId: selectedDomainId, ...domainIds, targetCountryCode: TargetCountryCode, status: Status }, targetCountryListByDomains });
            } else {
                this.props.setSelectedDomainId(null);
            }
            this.props.toggleLoader(false);
        }
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        this.setState({ selectedDomain: newDomain });
    }

    _handleChangeAffDomain = (selectedDomainId) => {
        this._handleChangeDomain("affDomainId", selectedDomainId);
    }

    _handleChangeRedirectDomain = (selectedDomainId) => {
        this._handleChangeDomain("redirectDomainId", selectedDomainId);
    }

    _handleChangeDomain = async (domainType, domainId) => {
        this.props.toggleLoader(true);

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [domainType]: domainId });

        const { affDomainId, redirectDomainId } = newDomain;

        const targetCountryListByDomains = await this.props.getTargetCountryListByDomains({ affDomainId, redirectDomainId });
        const selectedTargetCountryCodeByDomains = targetCountryListByDomains ? targetCountryListByDomains[0].TargetCountryCode : "";

        this.setState({ selectedDomain: { ...newDomain, targetCountryCode: selectedTargetCountryCodeByDomains }, targetCountryListByDomains });
        this.props.toggleLoader(false);
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isInsert, selectedDomain } = this.state;
        const message = isInsert ? "Add Successfully!" : "Update Successfully!";

        const result = isInsert ? await this.props.insertDomain(selectedDomain) : await this.props.updateDomain(selectedDomain);

        if (result) {
            this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
        } else {
            this._handleShowAlert(false, isInsert ? "Add Domain is failed!" : "Update Domain is failed!");
        }

        this.props.toggleLoader(false);
    }

    _handleShowAlert = (isSuccess, message) => {
        this.setState({
            message,
            isSuccess,
            isShowMessage: true,
        }, () => window.setTimeout(() => this._hideMessage(isSuccess), 3000));
    }

    _hideMessage = (isSuccess) => {
        this.setState({
            isShowMessage: false
        });

        if (isSuccess) {
            this.props.fetchData(this.props.selectedTargetCountryCode);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { selectedDomainId, affDomainList, redirectDomainList } = this.props;
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, targetCountryListByDomains } = this.state;
        const isShowPopup = selectedDomainId !== null;
        const isDisabled = !targetCountryListByDomains;
        const title = isInsert ? "Add New Domain Control" : "Edit Domain Control";
        const buttonLabel = isInsert ? "Add" : "Submit";

        return selectedDomain && affDomainList && redirectDomainList && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop="static">
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="domain-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{headerText.AffDomain}</Label>
                                    <Col sm={8}>
                                        <Select
                                            id="affDomain"
                                            dataSource={affDomainList}
                                            keyField="DomainId"
                                            valueField="DomainName"
                                            hasInputOption={true}
                                            selectedKey={selectedDomain.affDomainId}
                                            onChange={this._handleChangeAffDomain} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{headerText.RedirectDomain}</Label>
                                    <Col sm={8}>
                                        <Select
                                            id="redirectDomain"
                                            dataSource={redirectDomainList}
                                            keyField="DomainId"
                                            valueField="DomainName"
                                            hasInputOption={true}
                                            selectedKey={selectedDomain.redirectDomainId}
                                            onChange={this._handleChangeRedirectDomain} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{headerText.TargetCountry}</Label>
                                    <Col sm={8}>
                                        {
                                            isDisabled ? (
                                                <Input type="select" name="targetCountryCode" disabled="true" title="All target countries are blocked for these domains!" className="mt-1" value="">
                                                    <option key="target-country-none" value="">Please select!</option>
                                                </Input>
                                            ) : (
                                                    <Input type="select" name="targetCountryCode" className="mt-1" value={selectedDomain.targetCountryCode} onChange={this._handleChange}>
                                                        {
                                                            targetCountryListByDomains.map(item => {
                                                                return (<option key={`target-country-${item.TargetCountryCode}`} value={item.TargetCountryCode}>{item.TargetCountryName}</option>)
                                                            })
                                                        }
                                                    </Input>
                                                )
                                        }
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label sm={4}>{headerText.Status}</Label>
                                    <Col sm={8}>
                                        <input type="checkbox" name="status" checked={selectedDomain.status} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" disabled={isDisabled || !selectedDomain.targetCountryCode} onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>Cancel</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedDomainId, selectedTargetCountryCode, targetCountryList, affDomainList, redirectDomainList }) {
    return Object.assign({}, { selectedDomainId, selectedTargetCountryCode, targetCountryList, affDomainList, redirectDomainList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainControlById, getTargetCountryListByDomains }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainControlPopup);