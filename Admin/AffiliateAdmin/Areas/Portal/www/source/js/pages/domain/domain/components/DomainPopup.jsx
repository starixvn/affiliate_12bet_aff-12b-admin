import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import ExcludedTargetCountryFormGroup from '../../../common/components/ExcludedTargetCountryFormGroup';

import { defaultState, headerText } from '../libs/constants';
import { htmlDecode } from '../../../../modules/htmlHelper';
import { getExcludedTargetCountries, getUsingTargetCountries, checkExcludedAllTargetCountries } from '../../../common/libs/utils/targetcountry';
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

    _handleUpdateState = async (domainId, isAffDomain) => {
        if (domainId === 0) {
            const targetCountryList = await this.props.getTargetCountryListByDomain({ affDomainId: 0, redirectDomainId: 0 });

            this.setState({ isInsert: true, selectedDomain: { domainId, domainName: '', excludedTargetCountries: [], isSSL: false }, targetCountryList });
        } else {
            this.props.toggleLoader(true);
            const domain = await this.props.getDomainById(domainId, isAffDomain);

            if (domain) {
                const isSSL = domain.IsSSL;
                const domainName = htmlDecode(domain.DomainName);
                const domainIdParam = isAffDomain ? "affDomainId" : "redirectDomainId";
                const targetCountryList = await this.props.getTargetCountryListByDomain({ [domainIdParam]: domainId });

                if (targetCountryList) {
                    const usingTargetCountries = getUsingTargetCountries(targetCountryList);
                    const excludedTargetCountries = getExcludedTargetCountries(targetCountryList);
                    const isExcludedAllTargetCountries = checkExcludedAllTargetCountries(targetCountryList, usingTargetCountries, excludedTargetCountries);

                    this.setState({ isInsert: false, usingTargetCountries, selectedDomain: { domainId, domainName, isSSL, excludedTargetCountries }, targetCountryList, isExcludedAllTargetCountries });
                } else {
                    this.setState({ isInsert: false, selectedDomain: { domainId, domainName, isSSL, excludedTargetCountries: '' } });
                }
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

    handleChangeTargetCountry = (isExcludedAllTargetCountries, excludedTargetCountries, newTargetCountryList) => {
        const newDomain = Object.assign({}, selectedDomain, { excludedTargetCountries });

        this.setState({ selectedDomain: newDomain, targetCountryList: newTargetCountryList, isExcludedAllTargetCountries });
    }

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const { isAffDomain } = this.props;
        const { selectedDomain } = this.state;

        if(selectedDomain.domainName) {
            const { isInsert } = this.state;
            const domainInfo = Object.assign({}, selectedDomain, { isAffDomain, excludedTargetCountries: selectedDomain.excludedTargetCountries.join(',') })
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
        const { isInsert, isSuccess, isShowMessage, message, selectedDomain, usingTargetCountries, targetCountryList, isExcludedAllTargetCountries } = this.state;
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
                                <ExcludedTargetCountryFormGroup
                                    lblText={headerText.ExcludedTargetCountry}
                                    targetCountryList={targetCountryList}
                                    usingTargetCountries={usingTargetCountries}
                                    isExcludedAllTargetCountries={isExcludedAllTargetCountries}
                                    changeTargetCountry={this.handleChangeTargetCountry}
                                    />
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