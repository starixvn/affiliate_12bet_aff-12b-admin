import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { insertSpecificDomain, updateSpecificDomain, toggleLoader, fetchData, setSelectedDomainId, getSpecificDomainById } from '../actions/index';
import { defaultState, headerText, affsCondition } from '../libs/constants';

export class SpecificDomainControlPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState;
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId);
        }
    }

    _handleUpdateState = async (selectedDomainId) => {
        if (selectedDomainId === 0) {
            this.setState({ isInsert: true, selectedDomain: { domainControlId: selectedDomainId, affDomain: '', redirectDomain: '', affDomainSSL: false, redirectDomainSSL: false, isUsed: false } });
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getSpecificDomainById(selectedDomainId);

            if (domain) {
                const { AffName, AffDomain, RedirectDomain, AffDomainSSL, RedirectDomainSSL, IsUsed } = domain;

                this.setState({
                    isInsert: false, selectedDomain: {
                        domainControlId: selectedDomainId,
                        affName: AffName,
                        affDomain: AffDomain,
                        affDomainSSL: AffDomainSSL,
                        redirectDomain: RedirectDomain,
                        redirectDomainSSL: RedirectDomainSSL,
                        isUsed: IsUsed
                    }
                });
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

    _handleChangeAffNames = (event) => {
        event.preventDefault();
        const value = event.target.value;

        if (affsCondition.charactersPattern.test(value)) {
            return;
        }

        this.setState({ affNames: value });
    }

    _handleBlurAffNames = () => {
        this.setState(({ affNames }) => {
            const newAffNames = affNames.replace(/\n+|\s+/g, ',').replace(/,+/g, ',').replace(/(^,)|(,$)/g, '');

            return { affNames: newAffNames };
        });
    }

    _validateDomains = (selectedDomain) => {
        if (!selectedDomain.affDomain || !selectedDomain.redirectDomain) {
            this._handleShowAlert(false, "Please input Aff or Redirect Domain name!");
            return false;
        }

        return true;
    }

    _validateAffNames = (affNames) => {
        if (!affNames) {
            this._handleShowAlert(false, "Please enter Affiliate(s)!");
            return false;
        }

        if (!affsCondition.affsPattern.test(affNames)) {
            this._handleShowAlert(false, "Affiliate(s) is an invalid format or affiliate!");
            return false;
        }

        if (affNames.split(',').length > affsCondition.maximumNumber) {
            this._handleShowAlert(false, 'The function can only create assignment for up to 500 affiliates per request!');
            return false
        }

        return true;
    }

    _handleSubmit = async () => {
        const { isInsert } = this.state;

        this.props.toggleLoader(true);

        isInsert ? await this._handleInsertSpecificDomain() : await this._handleUpdateSpecificDomain();

        this.props.toggleLoader(false);
    }

    _handleInsertSpecificDomain = async () => {
        const { selectedDomain, affNames } = this.state;

        if (this._validateAffNames(affNames) && this._validateDomains(selectedDomain)) {
            const result = await this.props.insertDomain({ ...selectedDomain, affName: affNames });

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? "Add Specific Domain Successfully!" : result.Message);
            } else {
                this._handleShowAlert(false, "Add Specific Domain is failed!");
            }
        }
    }

    _handleUpdateSpecificDomain = async () => {
        const { selectedDomain } = this.state;

        if (this._validateDomains(selectedDomain)) {
            const result = await this.props.updateDomain(selectedDomain);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? "Update Specific Domain Successfully!" : result.Message);
            } else {
                this._handleShowAlert(false, "Update Specific Domain is failed!");
            }
        }
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
            this.props.fetchData(this.props.param);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { selectedDomainId } = this.props;
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, affNames } = this.state;
        const isShowPopup = selectedDomainId !== null;
        const title = isInsert ? "Add New Specific Domain Control" : "Edit Specific Domain Control";
        const buttonLabel = isInsert ? "Add" : "Submit";

        return selectedDomain && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop='static' style={{ maxWidth: isInsert ? 550 : 500 }}>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="domain-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-5" autoComplete="off">
                                <FormGroup row className="mb-2 align-items-center">
                                    <Label htmlFor="affNames" className="required" sm={4}>{headerText.Affiliate}</Label>
                                    {
                                        isInsert ? (
                                            <Col sm={8}>
                                                <Input type="textarea" rows="6" id="affNames" name="affNames" className="input-required"
                                                    value={affNames} onChange={this._handleChangeAffNames} onBlur={this._handleBlurAffNames} />
                                                <div><Label>Max 500 affiliates, separated by [Comma], [Tab] or [Enter]</Label></div>
                                            </Col>
                                        ) : (
                                                <Col sm={8}>
                                                    <Input type="text" disabled className="input-required" id="affNames" name="affNames" value={selectedDomain.affName} />
                                                </Col>
                                                )
                                    }
                                </FormGroup>
                                <FormGroup row className="mb-2 align-items-center">
                                    <Label htmlFor="affDomain" className="required" sm={4}>{headerText.AffDomain}</Label>
                                    <Col sm={6}>
                                        <Input type="text" className="input-required" id="affDomain" name="affDomain"
                                            value={selectedDomain.affDomain} onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                                    </Col>
                                    <Col sm={2}>
                                        <FormGroup check>
                                            <Label check>
                                                <Input type="checkbox" checked={selectedDomain.affDomainSSL} name="affDomainSSL"
                                                    onChange={this._handleChange} /> {headerText.SSL}
                                            </Label>
                                        </FormGroup>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2 align-items-center">
                                    <Label htmlFor="redirectDomain" className="required" sm={4}>{headerText.RedirectDomain}</Label>
                                    <Col sm={6}>
                                        <Input type="text" className="input-required" id="redirectDomain" name="redirectDomain"
                                            value={selectedDomain.redirectDomain} onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                                    </Col>
                                    <Col sm={2}>
                                        <FormGroup check>
                                            <Label check>
                                                <Input type="checkbox" checked={selectedDomain.redirectDomainSSL} name="redirectDomainSSL"
                                                    onChange={this._handleChange} /> {headerText.SSL}
                                            </Label>
                                        </FormGroup>
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2 align-items-center">
                                    <Label sm={4}>{headerText.Status}</Label>
                                    <Col sm={8}>
                                        <input type="checkbox" name="isUsed" checked={selectedDomain.isUsed} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3" onClick={this._handleSubmit}>{buttonLabel}</Button>
                            <Button type="button" color="info" onClick={this._handleClosePopup}>Cancel</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ selectedDomainId, param }) {
    return Object.assign({}, { selectedDomainId, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertSpecificDomain, updateSpecificDomain, toggleLoader, fetchData, setSelectedDomainId, getSpecificDomainById }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SpecificDomainControlPopup);