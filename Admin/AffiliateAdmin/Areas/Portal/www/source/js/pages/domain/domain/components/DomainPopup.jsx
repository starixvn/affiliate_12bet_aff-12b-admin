import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { defaultState, targetCountryStatus } from '../libs/constants';
import { Col, Button, Form, FormGroup, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
import { insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainById, getTargetCountryListByDomain } from '../actions/index';

export class DomainPopup extends PureComponent {
    constructor(props) {
        super(props)

        this.state = defaultState;
    }

    componentDidUpdate(prevProps) {
        const { selectedDomainId, isAffDomain } = this.props;
        if (selectedDomainId !== null && prevProps.selectedDomainId !== selectedDomainId) {
            this._handleUpdateState(selectedDomainId, isAffDomain);
        }
    }

    _handleUpdateState = async (selectedDomainId, isAffDomain) => {
        if (selectedDomainId === 0) {
            const targetCountryListByDomains = await this.props.getTargetCountryListByDomain({ affDomainId: 0, redirectDomainId: 0 });

            this.setState({ isInsert: true, selectedDomain: { domainId: selectedDomainId, domainName: '', blockTargetCountries: [], isSSL: false }, targetCountryListByDomains });
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainById(selectedDomainId, isAffDomain);

            if (domain) {
                const { DomainName, IsSSL } = domain;
                var domainIdParam = isAffDomain ? "affDomainId" : "redirectDomainId";
                const targetCountryListByDomains = await this.props.getTargetCountryListByDomain({ [domainIdParam]: selectedDomainId });
                if (targetCountryListByDomains) {
                    const usingTargetCountries = this._handleGetUsingTargetCountries(targetCountryListByDomains);
                    const blockTargetCountries = this._handleGetBlockTargetCountries(targetCountryListByDomains);
                    const isBlockAllTargetCountries = blockTargetCountries.length !== 0 && (usingTargetCountries.length + blockTargetCountries.length) === targetCountryListByDomains.length;

                    this.setState({ isInsert: false, usingTargetCountries, selectedDomain: { domainId: selectedDomainId, domainName: DomainName, blockTargetCountries, isSSL: IsSSL }, targetCountryListByDomains, isBlockAllTargetCountries });
                } else {
                    this.setState({ isInsert: false, selectedDomain: { domainId: selectedDomainId, domainName: DomainName, blockTargetCountries: '', isSSL: IsSSL } });
                }
            }
            this.props.toggleLoader(false);
        }
    }

    _handleGetBlockTargetCountries = (targetCountryListByDomains) => {
        return targetCountryListByDomains.filter(m => m.Status === targetCountryStatus.Blocked).map(m => m.TargetCountryId);
    }

    _handleGetUsingTargetCountries = (targetCountryListByDomains) => {
        return targetCountryListByDomains.filter(m => m.Status === targetCountryStatus.Using).map(m => m.TargetCountryId);
    }

    _handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;

        const { selectedDomain } = this.state;
        const newDomain = Object.assign({}, selectedDomain, { [target.name]: value });

        this.setState({ selectedDomain: newDomain });
    }

    _handleChangeTargetCountry = (event, targetCountryId) => {
        const isChecked = event.target.checked;

        const { selectedDomain, usingTargetCountries, targetCountryListByDomains } = this.state;

        if (usingTargetCountries.includes(targetCountryId)) {
            return;
        }

        const newTargetCountryListByDomains = targetCountryListByDomains.map(item => {
            if (item.TargetCountryId !== targetCountryId) {
                return item;
            }
            return {
                ...item,
                Status: isChecked ? targetCountryStatus.Blocked : targetCountryStatus.Unblocked
            };
        });

        const blockTargetCountries = this._handleGetBlockTargetCountries(newTargetCountryListByDomains);
        const newDomain = Object.assign({}, selectedDomain, { blockTargetCountries });
        const isBlockAllTargetCountries = (usingTargetCountries.length + blockTargetCountries.length) === newTargetCountryListByDomains.length;

        this.setState({ selectedDomain: newDomain, targetCountryListByDomains: newTargetCountryListByDomains, isBlockAllTargetCountries });
    }

    _handleChangeAllTargetCountries = (event) => {
        const isChecked = event.target.checked;
        const { selectedDomain, targetCountryListByDomains } = this.state;

        if (targetCountryListByDomains) {
            const status = isChecked ? targetCountryStatus.Blocked : targetCountryStatus.Unblocked;
            const newTargetCountryListByDomains = targetCountryListByDomains.map(item => {
                if (item.Status === targetCountryStatus.Using) {
                    return item;
                }
                return {
                    ...item,
                    Status: status
                };
            });

            const blockTargetCountries = this._handleGetBlockTargetCountries(newTargetCountryListByDomains);
            const newDomain = Object.assign({}, selectedDomain, { blockTargetCountries });

            this.setState({ selectedDomain: newDomain, targetCountryListByDomains: newTargetCountryListByDomains, isBlockAllTargetCountries: isChecked });
        }
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isAffDomain } = this.props;
        const { selectedDomain } = this.state;

        if(selectedDomain.domainName) {
            const { isInsert } = this.state;
            const domainInfo = Object.assign({}, selectedDomain, { isAffDomain, blockTargetCountries: selectedDomain.blockTargetCountries.join(',') })
            const message = isInsert ? "Add Successfully!" : "Update Successfully!";

            const result = isInsert ? await this.props.insertDomain(domainInfo) : await this.props.updateDomain(domainInfo);

            if (result) {
                this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
            } else {
                this._handleShowAlert(false, isInsert ? "Add Domain is failed!" : "Update Domain is failed!");
            }

        } else {
            this._handleShowAlert(false, "Please input domain name!")
        }

        this.props.toggleLoader(false);
    }

    _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSubmit();
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
            this.props.fetchData(this.props.isAffDomain);
            this._handleClosePopup();
        }
    }

    _handleClosePopup = () => {
        this.props.setSelectedDomainId(null);
        this.setState(defaultState);
    }

    render() {
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, usingTargetCountries, targetCountryListByDomains, isBlockAllTargetCountries } = this.state;
        const isShowPopup = selectedDomain !== null;
        const title = isInsert ? "Add Domain" : "Edit Domain";
        const buttonLabel = isInsert ? "Add" : "Submit";

        return selectedDomain && (
            <Modal isOpen={isShowPopup} toggle={this._handleClosePopup} backdrop="static" style={{ maxWidth: 650 }}>
                <ModalHeader toggle={this._handleClosePopup}><span className="iconpopup"></span>{title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="domain-form-container">
                            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                <div dangerouslySetInnerHTML={{ __html: message }}></div>
                            </Alert>
                            <Form tag="form" className="mb-3" autoComplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="domainName" sm={3} className="required">Domain</Label>
                                    <Col sm={9}>
                                        <Input type="text" className="input-required mt-2" name="domainName"
                                            value={selectedDomain.domainName} onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                                    </Col>
                                </FormGroup>
                                <FormGroup row className="mb-2">
                                    <Label for="isSSL" sm={3}>SSL</Label>
                                    <Col sm={9}>
                                        <input type="checkbox" name="isSSL" id="isSSL" checked={selectedDomain.isSSL} onChange={this._handleChange} />
                                    </Col>
                                </FormGroup>
                                {
                                    targetCountryListByDomains && (
                                        <FormGroup row className="mb-2">
                                            <Label sm={3}>Block Target Country</Label>
                                            <Col sm={9}>
                                                <div className="block-target-country">
                                                    {
                                                        targetCountryListByDomains.map(item => {
                                                            const isUsing = usingTargetCountries.includes(item.TargetCountryId);

                                                            return (
                                                                <div key={item.TargetCountryId} className="block-target-country-item">
                                                                    <input type="checkbox" className="mr-2" disabled={isUsing} title={isUsing && "This Affiliate domain has been used for this target country!"} id={item.TargetCountryId} checked={item.Status === targetCountryStatus.Blocked} onChange={(e) => this._handleChangeTargetCountry(e, item.TargetCountryId)} />
                                                                    <Label for={item.TargetCountryId}>{item.TargetCountryName}</Label>
                                                                </div>
                                                            )
                                                        })
                                                    }
                                                    <div className="block-target-country-item block-target-country-All">
                                                        <input type="checkbox" className="mr-2" disabled={targetCountryListByDomains.every(i => i.Status === targetCountryStatus.Using)} id="All-Target-Countries" checked={isBlockAllTargetCountries} onChange={this._handleChangeAllTargetCountries} />
                                                            <Label for="All-Target-Countries">All</Label>
                                                        </div>
                                                </div>
                                            </Col>
                                        </FormGroup>
                                        )
                                }
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

function mapStateToProps({ selectedDomainId, isAffDomain }) {
    return Object.assign({}, { selectedDomainId, isAffDomain });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ insertDomain, updateDomain, toggleLoader, fetchData, setSelectedDomainId, getDomainById, getTargetCountryListByDomain }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DomainPopup);